﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XMLarhiver.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<group>
H P
HM*
LM*
PM*
<group>
H S
HM*1.xml
LM*1.xml
SM*1.xml
<group>
T S
TM*2.xml
LTM*2.xml
SM*2.xml
<group>
DP P
DP*
LPM*
FP*
<group>
DV P
DV*
LVM*
FV*
<group>
DS P
DS*
LSM*
FS*
<group>
DU P
DU*
LUM*
FU*
<group>
DO P
DO*
LOM*
FO*
<group>
DF P
DF*
LFM*
FF*
<group>
DD P
DD*
LDM*
FD*
<group>
DR P
DR*
LRM*
FR*")]
        public string masks {
            get {
                return ((string)(this["masks"]));
            }
            set {
                this["masks"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string XMLpatch {
            get {
                return ((string)(this["XMLpatch"]));
            }
            set {
                this["XMLpatch"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PackPatch {
            get {
                return ((string)(this["PackPatch"]));
            }
            set {
                this["PackPatch"] = value;
            }
        }
    }
}
