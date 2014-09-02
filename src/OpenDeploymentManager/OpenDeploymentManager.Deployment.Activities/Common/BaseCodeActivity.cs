using System;
using System.Activities;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public abstract class BaseCodeActivity : CodeActivity
    {
        private InArgument<bool> logExceptionStack = true;

        [Category("Diagnostics")]
        [Description("Set to true to fail the build if errors are logged")]
        public InArgument<bool> FailBuildOnError { get; set; }

        [Category("Diagnostics")]
        [Description("Set to true to make all warnings errors")]
        public InArgument<bool> TreatWarningsAsErrors { get; set; }

        [Category("Diagnostics")]
        [Description("Set to true to ignore unhandled exceptions")]
        public InArgument<bool> IgnoreExceptions { get; set; }

        [Category("Diagnostics")]
        [Description("Set to true to log the entire stack in the event of an exception")]
        public InArgument<bool> LogExceptionStack
        {
            get { return this.logExceptionStack; }
            set { this.logExceptionStack = value; }
        }

        protected CodeActivityContext ActivityContext { get; private set; }

        #region Overrides of CodeActivity
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            metadata.RequireExtension<IDeploymentLoggingExtension>();
        }

        protected override void Execute(CodeActivityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.ActivityContext = context;

            try
            {
                this.InternalExecute();
            }
            catch (FailingDeploymentException)
            {
                if (this.IgnoreExceptions.Get(context) == false)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                if (this.LogExceptionStack.Get(context))
                {
                    string innerException = string.Empty;
                    if (ex.InnerException != null)
                    {
                        innerException = string.Format("Inner Exception: {0}", ex.InnerException.Message);
                    }

                    this.LogBuildError(string.Format("Error: {0}. Stack Trace: {1}. {2}", ex.Message, ex.StackTrace, innerException));
                }

                if (this.IgnoreExceptions.Get(context) == false)
                {
                    throw;
                }
            }
        }
        #endregion

        protected abstract void InternalExecute();

        protected void LogBuildError(string message, params object[] args)
        {
            string errorMessage = string.Format(CultureInfo.InvariantCulture, message, args);
            Debug.WriteLine(string.Format("DeploymentError: {0}", errorMessage));
            if (this.FailBuildOnError.Get(this.ActivityContext))
            {
                throw new FailingDeploymentException(errorMessage);
            }

            this.ActivityContext.TrackDeploymentError(errorMessage);
        }

        protected void LogBuildWarning(string message, params object[] args)
        {
            string warningMessage = string.Format(CultureInfo.InvariantCulture, message, args);
            if (this.TreatWarningsAsErrors.Get(this.ActivityContext))
            {
                this.LogBuildError(warningMessage);
            }
            else
            {
                this.ActivityContext.TrackDeploymentWarning(warningMessage);
                Debug.WriteLine(string.Format("DeploymentWarning: {0}", warningMessage));
            }
        }

        protected void LogBuildMessage(string message, params object[] args)
        {
            string infoMessage = string.Format(CultureInfo.InvariantCulture, message, args);
            this.ActivityContext.TrackDeploymentMessage(infoMessage);
        }
    }
}