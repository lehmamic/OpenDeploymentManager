﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenDeploymentManager.Server.Contracts.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OpenDeploymentManager.Server.Contracts.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; and &apos;{1}&apos; do not match..
        /// </summary>
        public static string CompareAttribute_MustMatch {
            get {
                return ResourceManager.GetString("CompareAttribute_MustMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} field is not a valid e-mail address..
        /// </summary>
        public static string EmailAddressAttribute_Invalid {
            get {
                return ResourceManager.GetString("EmailAddressAttribute_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm password.
        /// </summary>
        public static string Property_ConfirmPassword {
            get {
                return ResourceManager.GetString("Property_ConfirmPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Created on.
        /// </summary>
        public static string Property_Created {
            get {
                return ResourceManager.GetString("Property_Created", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Display name.
        /// </summary>
        public static string Property_DisplayName {
            get {
                return ResourceManager.GetString("Property_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email.
        /// </summary>
        public static string Property_Email {
            get {
                return ResourceManager.GetString("Property_Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id.
        /// </summary>
        public static string Property_Id {
            get {
                return ResourceManager.GetString("Property_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last modified on.
        /// </summary>
        public static string Property_LastModified {
            get {
                return ResourceManager.GetString("Property_LastModified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New password.
        /// </summary>
        public static string Property_NewPassword {
            get {
                return ResourceManager.GetString("Property_NewPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Old password.
        /// </summary>
        public static string Property_OldPassword {
            get {
                return ResourceManager.GetString("Property_OldPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password.
        /// </summary>
        public static string Property_Password {
            get {
                return ResourceManager.GetString("Property_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        public static string Property_RoleName {
            get {
                return ResourceManager.GetString("Property_RoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User name.
        /// </summary>
        public static string Property_UserName {
            get {
                return ResourceManager.GetString("Property_UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} field is required..
        /// </summary>
        public static string RequiredAttribute_ValidationError {
            get {
                return ResourceManager.GetString("RequiredAttribute_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field {0} must be a string with a minimum length of {2} and a maximum length of {1}..
        /// </summary>
        public static string StringLengthAttribute_ValidationErrorIncludingMinimum {
            get {
                return ResourceManager.GetString("StringLengthAttribute_ValidationErrorIncludingMinimum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password and confirmation password do not match..
        /// </summary>
        public static string ValidationError_ConfirmPasswordNotMatch {
            get {
                return ResourceManager.GetString("ValidationError_ConfirmPasswordNotMatch", resourceCulture);
            }
        }
    }
}
