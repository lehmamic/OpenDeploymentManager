using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment
{
    public class DeploymentManager : IDeploymentManager
    {
        private readonly IServiceLocator serviceLocator;

        public DeploymentManager(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator.ArgumentNotNull("serviceLocator");
        }

        public void RunDeployment(string template, IDictionary<string, object> arguments, IDictionary<string, string> variables)
        {
            template.ArgumentNotNull("template");
            arguments.ArgumentNotNull("arguments");
            variables.ArgumentNotNull("variables");

            Activity workflow = LoadWorkflow(template);

            var workflowInvoker = new WorkflowInvoker(workflow);
            workflowInvoker.Extensions.Add(this.serviceLocator);
            workflowInvoker.Extensions.Add(new ActivityTracking(new DeploymentInformationNode(Guid.Empty, DateTimeOffset.Now, null, "Executing deployment worflow.")));
            workflowInvoker.Extensions.Add(new DeploymentVariablesExtension(new Dictionary<string, object>()));

            var workflowInputs = arguments;
            workflowInvoker.Invoke(workflowInputs);
        }

        private static Activity LoadWorkflow(string template)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(template)))
            {
                var settings = new ActivityXamlServicesSettings
                    {
                        CompileExpressions = true
                    };

                return ActivityXamlServices.Load(stream, settings);
            }
        }

        private static void CompileExpressions(Activity activity)
        {
            // activityName is the Namespace.Type of the activity that contains the
            // C# expressions.
            string activityName = activity.GetType().ToString();

            // Split activityName into Namespace and Type.Append _CompiledExpressionRoot to the type name
            // to represent the new type that represents the compiled expressions.
            // Take everything after the last . for the type name.
            string activityType = activityName.Split('.').Last() + "_CompiledExpressionRoot";

            // Take everything before the last . for the namespace.
            string activityNamespace = string.Join(".", activityName.Split('.').Reverse().Skip(1).Reverse());

            // Create a TextExpressionCompilerSettings.
            var settings = new TextExpressionCompilerSettings
            {
                Activity = activity,
                Language = "C#",
                ActivityName = activityType,
                ActivityNamespace = activityNamespace,
                RootNamespace = null,
                GenerateAsPartialClass = false,
                AlwaysGenerateSource = true,
                ForImplementation = false
            };

            // Compile the C# expression.
            TextExpressionCompilerResults results = new TextExpressionCompiler(settings).Compile();

            // Any compilation errors are contained in the CompilerMessages.
            if (results.HasErrors)
            {
                throw new ExpressionCompilationFailedException("Compilation failed.");
            }

            // Create an instance of the new compiled expression type.
            var compiledExpressionRoot = Activator.CreateInstance(results.ResultType, new object[] { activity }) as ICompiledExpressionRoot;

            // Attach it to the activity.
            CompiledExpressionInvoker.SetCompiledExpressionRoot(activity, compiledExpressionRoot);
        }
    }
}