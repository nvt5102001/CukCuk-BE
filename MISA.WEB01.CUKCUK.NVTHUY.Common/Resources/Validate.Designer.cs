﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Validate {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Validate() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.WEB01.CUKCUK.NVTHUY.Common.Resources.Validate", typeof(Validate).Assembly);
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
        ///   Looks up a localized string similar to Sở thích phục vụ &lt;{0} - {1}&gt; đã tồn tại vui lòng kiểm tra lại.
        /// </summary>
        public static string DuplicateFoodAddition {
            get {
                return ResourceManager.GetString("DuplicateFoodAddition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sở thích phục vụ không được để trống khi có thu thêm. Vui lòng kiểm tra lại..
        /// </summary>
        public static string NoDescriptionFoodAddition {
            get {
                return ResourceManager.GetString("NoDescriptionFoodAddition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} đã tồn tại vui lòng kiểm tra lại.
        /// </summary>
        public static string NotAllowedDuplicate {
            get {
                return ResourceManager.GetString("NotAllowedDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} không được phép để trống.
        /// </summary>
        public static string NotAllowedNull {
            get {
                return ResourceManager.GetString("NotAllowedNull", resourceCulture);
            }
        }
    }
}
