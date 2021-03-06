﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4016
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Camurphy.CameraDownload.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Camurphy.CameraDownload.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Error connecting to camera. Please disconnect camera and try again.
        /// </summary>
        internal static string CameraConnectError {
            get {
                return ResourceManager.GetString("CameraConnectError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a destination directory.
        /// </summary>
        internal static string DestinationNullError {
            get {
                return ResourceManager.GetString("DestinationNullError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are more files to copy than filenames available using the number of digits specified. Please specify a higher number of digits..
        /// </summary>
        internal static string DigitShortageError {
            get {
                return ResourceManager.GetString("DigitShortageError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot proceed. Rename files not selected and multiple files with the same name and modified date were found on the source drive.
        /// </summary>
        internal static string DuplicateFilesError {
            get {
                return ResourceManager.GetString("DuplicateFilesError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot perform copy. A file already exists. To avoid this issue select resume numbering..
        /// </summary>
        internal static string FileExistsError {
            get {
                return ResourceManager.GetString("FileExistsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The destination directory specified does not exist.
        /// </summary>
        internal static string InvalidDestinationError {
            get {
                return ResourceManager.GetString("InvalidDestinationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid prefix - cannot contain the following characters: \ / : * ? &quot; &lt; &gt; |.
        /// </summary>
        internal static string InvalidPrefixError {
            get {
                return ResourceManager.GetString("InvalidPrefixError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No files found on source device.
        /// </summary>
        internal static string NoFilesError {
            get {
                return ResourceManager.GetString("NoFilesError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot resume numbering, no files with matching prefix and number of digits found in destination directory. Do you want to continue and start from 1?.
        /// </summary>
        internal static string ResumeNumberingWarning {
            get {
                return ResourceManager.GetString("ResumeNumberingWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select a source device.
        /// </summary>
        internal static string SourceNullError {
            get {
                return ResourceManager.GetString("SourceNullError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {B96B3CAE-0728-11D3-9D7B-0000F81EF32E}.
        /// </summary>
        internal static string WiaJPEGFormatID {
            get {
                return ResourceManager.GetString("WiaJPEGFormatID", resourceCulture);
            }
        }
    }
}
