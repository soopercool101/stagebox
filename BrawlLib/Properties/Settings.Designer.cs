﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BrawlLib.Modeling;
using BrawlLib.SSBB.ResourceNodes;
using System;
using System.Collections.Generic;
namespace BrawlLib.Properties
{
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        public static Settings Default { get { return defaultInstance; } }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public Collada.ImportOptions ColladaImportOptions
        {
            get
            {
                return ((Collada.ImportOptions)(this["ColladaImportOptions"]));
            }
            set
            {
                this["ColladaImportOptions"] = value;
            }
        }

		[global::System.Configuration.UserScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("false")]
		public bool HideMDL0Errors {
			get {
				return ((bool)(this["HideMDL0Errors"]));
			}
			set {
				this["HideMDL0Errors"] = value;
			}
		}

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public List<CodeStorage> Codes
        {
            get
            {
                return ((List<CodeStorage>)(this["Codes"]));
            }
            set
            {
                this["Codes"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public bool SaveGCTWithInfo
        {
            get
            {
                return (bool)(this["SaveGCTWithInfo"]);
            }
            set
            {
                this["SaveGCTWithInfo"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double? AudioVolumePercent
        {
            get
            {
                return (double?)(this["AudioVolumePercent"]);
            }
            set
            {
                this["AudioVolumePercent"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("false")]
        public bool CompatibilityMode
        {
            get
            {
                return (bool)(this["CompatibilityMode"]);
            }
            set
            {
                this["CompatibilityMode"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public bool ImportPNGsWithPalettes
        {
            get
            {
                return (bool)(this["ImportPNGsWithPalettes"]);
            }
            set
            {
                this["ImportPNGsWithPalettes"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public bool AutoCompressStages
        {
            get
            {
                return (bool)(this["AutoCompressStages"]);
            }
            set
            {
                this["AutoCompressStages"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public bool AutoDecompressFighterPAC
        {
            get
            {
                return (bool)(this["AutoDecompressFighterPAC"]);
            }
            set
            {
                this["AutoDecompressFighterPAC"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public bool AutoCompressFighterPCS
        {
            get
            {
                return (bool)(this["AutoCompressFighterPCS"]);
            }
            set
            {
                this["AutoCompressFighterPCS"] = value;
            }
        }
    }
}
