﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenDeploymentManager.Server.Host.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OpenDeploymentManager.Server.Host.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuring the dependency injection container..
        /// </summary>
        internal static string InitializeContainerTask_ConfiguringContainer {
            get {
                return ResourceManager.GetString("InitializeContainerTask_ConfiguringContainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initializing the data transfer object projection..
        /// </summary>
        internal static string InitializeContainerTask_InitializeProjection {
            get {
                return ResourceManager.GetString("InitializeContainerTask_InitializeProjection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initializing the service locator..
        /// </summary>
        internal static string InitializeContainerTask_InitializeServiceLocator {
            get {
                return ResourceManager.GetString("InitializeContainerTask_InitializeServiceLocator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open Deployment Manager server is started..
        /// </summary>
        internal static string Program_AgentStarted {
            get {
                return ResourceManager.GetString("Program_AgentStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Open Deployment Manager server is stopped..
        /// </summary>
        internal static string Program_AgentStopped {
            get {
                return ResourceManager.GetString("Program_AgentStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Press any key to exit.
        /// </summary>
        internal static string Program_PressAnyKeyToExit {
            get {
                return ResourceManager.GetString("Program_PressAnyKeyToExit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starting Open Deployment Manager server..
        /// </summary>
        internal static string Program_StartingAgent {
            get {
                return ResourceManager.GetString("Program_StartingAgent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stopping Open Deployment Manager server..
        /// </summary>
        internal static string Program_StoppingAgent {
            get {
                return ResourceManager.GetString("Program_StoppingAgent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unhandled exception occurred..
        /// </summary>
        internal static string Program_UnhanldedException {
            get {
                return ResourceManager.GetString("Program_UnhanldedException", resourceCulture);
            }
        }
    }
}