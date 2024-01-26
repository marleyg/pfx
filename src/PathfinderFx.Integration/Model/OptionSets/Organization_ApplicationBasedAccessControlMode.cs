#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PathfinderFx.Integration.Model.OptionSets
{
	
    /// <summary>
    /// Application Based Access Control Mode. 0 is Disabled, 1 is Enabled , 2 is audit mode
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    public enum Organization_ApplicationBasedAccessControlMode
    {
		
        [System.Runtime.Serialization.EnumMemberAttribute()]
        [OptionSetMetadataAttribute("AuditMode", 2)]
        Auditmode = 2,
		
        [System.Runtime.Serialization.EnumMemberAttribute()]
        [OptionSetMetadataAttribute("Disabled", 0)]
        Disabled = 0,
		
        [System.Runtime.Serialization.EnumMemberAttribute()]
        [OptionSetMetadataAttribute("Enabled", 1)]
        Enabled = 1,
    }
}
#pragma warning restore CS1591