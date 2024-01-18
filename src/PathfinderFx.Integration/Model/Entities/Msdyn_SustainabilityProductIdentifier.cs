using PathfinderFx.Integration.Model.OptionSets;

#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PathfinderFx.Integration.Model.Entities
{
	
	
	/// <summary>
	/// Status of the (Preview) Sustainability product identifier
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum Msdyn_SustainabilityProductIdentifier_StateCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 1,
	}
	
	/// <summary>
	/// Reason for the status of the (Preview) Sustainability product identifier
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum Msdyn_SustainabilityProductIdentifier_StatusCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 2,
	}
	
	/// <summary>
	/// Uniquely identifies a product.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("msdyn_sustainabilityproductidentifier")]
	public partial class Msdyn_SustainabilityProductIdentifier : Microsoft.Xrm.Sdk.Entity
	{
		
		/// <summary>
		/// Available fields, a the time of codegen, for the msdyn_sustainabilityproductidentifier entity
		/// </summary>
		public partial class Fields
		{
			public const string CreatedBy = "createdby";
			public const string CreatedByName = "createdbyname";
			public const string CreatedByYomiName = "createdbyyominame";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedByName = "modifiedbyname";
			public const string ModifiedByYomiName = "modifiedbyyominame";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string Msdyn_ApprovalStatus = "msdyn_approvalstatus";
			public const string Msdyn_ApprovalStatusName = "msdyn_approvalstatusname";
			public const string Msdyn_DataConnection = "msdyn_dataconnection";
			public const string Msdyn_DataConnectionName = "msdyn_dataconnectionname";
			public const string Msdyn_DataConnectionRefresh = "msdyn_dataconnectionrefresh";
			public const string Msdyn_DataConnectionRefreshName = "msdyn_dataconnectionrefreshname";
			public const string Msdyn_DataDefinition = "msdyn_datadefinition";
			public const string Msdyn_DataDefinitionName = "msdyn_datadefinitionname";
			public const string Msdyn_Msdyn_SustainabilityProduct_Msdyn_SustainabilityProductIdentifier_SustainabilityProduct = "msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductidentifier_sustainabilityproduct";
			public const string Msdyn_Name = "msdyn_name";
			public const string Msdyn_OriginCorrelationId = "msdyn_origincorrelationid";
			public const string Msdyn_SustainabilityProduct = "msdyn_sustainabilityproduct";
			public const string Msdyn_SustainabilityProductIdentifier = "msdyn_sustainabilityproductidentifier";
			public const string Msdyn_SustainabilityProductIdentifierId = "msdyn_sustainabilityproductidentifierid";
			public const string Id = "msdyn_sustainabilityproductidentifierid";
			public const string Msdyn_SustainabilityProductName = "msdyn_sustainabilityproductname";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string OwnerId = "ownerid";
			public const string OwnerIdName = "owneridname";
			public const string OwnerIdYomiName = "owneridyominame";
			public const string OwningBusinessUnit = "owningbusinessunit";
			public const string OwningBusinessUnitName = "owningbusinessunitname";
			public const string OwningTeam = "owningteam";
			public const string OwningUser = "owninguser";
			public const string StateCode = "statecode";
			public const string StateCodename = "statecodename";
			public const string StatusCode = "statuscode";
			public const string StatusCodename = "statuscodename";
			public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
			public const string UtcConversionTimeZoneCode = "utcconversiontimezonecode";
			public const string VersionNumber = "versionnumber";
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductIdentifier(Guid id) : 
				base(EntityLogicalName, id)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductIdentifier(string keyName, object keyValue) : 
				base(EntityLogicalName, keyName, keyValue)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductIdentifier(Microsoft.Xrm.Sdk.KeyAttributeCollection keyAttributes) : 
				base(EntityLogicalName, keyAttributes)
		{
		}
		
		public const string AlternateKeys = "msdyn_name|msdyn_origincorrelationid";
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductIdentifier() : 
				base(EntityLogicalName)
		{
		}
		
		public const string PrimaryIdAttribute = "msdyn_sustainabilityproductidentifierid";
		
		public const string PrimaryNameAttribute = "msdyn_name";
		
		public const string EntitySchemaName = "msdyn_sustainabilityproductidentifier";
		
		public const string EntityLogicalName = "msdyn_sustainabilityproductidentifier";
		
		public const string EntityLogicalCollectionName = "msdyn_sustainabilityproductidentifiers";
		
		public const string EntitySetName = "msdyn_sustainabilityproductidentifiers";
		
		/// <summary>
		/// Unique identifier of the user who created the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyname")]
		public string CreatedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("createdby"))
				{
					return FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdbyyominame")]
		public string CreatedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("createdby"))
				{
					return FormattedValues["createdby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public Nullable<DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("createdonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyname")]
		public string CreatedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("createdonbehalfby"))
				{
					return FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfbyyominame")]
		public string CreatedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("createdonbehalfby"))
				{
					return FormattedValues["createdonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public Nullable<int> ImportSequenceNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<int>>("importsequencenumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("importsequencenumber", value);
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyname")]
		public string ModifiedByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("modifiedby"))
				{
					return FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedbyyominame")]
		public string ModifiedByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("modifiedby"))
				{
					return FormattedValues["modifiedby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time when the record was modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public Nullable<DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who modified the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("modifiedonbehalfby", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyname")]
		public string ModifiedOnBehalfByName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("modifiedonbehalfby"))
				{
					return FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfbyyominame")]
		public string ModifiedOnBehalfByYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("modifiedonbehalfby"))
				{
					return FormattedValues["modifiedonbehalfby"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Indicator of approval of MSM entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_approvalstatus")]
		public virtual Msdyn_ApprovalStatus? Msdyn_ApprovalStatus
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_ApprovalStatus?)(EntityOptionSetEnum.GetEnum(this, "msdyn_approvalstatus")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_approvalstatus", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_approvalstatusname")]
		public string Msdyn_ApprovalStatusName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_approvalstatus"))
				{
					return FormattedValues["msdyn_approvalstatus"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The connection that ingested this accommodation type
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_dataconnection")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_DataConnection
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_dataconnection");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_dataconnection", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_dataconnectionname")]
		public string Msdyn_DataConnectionName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_dataconnection"))
				{
					return FormattedValues["msdyn_dataconnection"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The connection refresh which ingested this accommodation type
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_dataconnectionrefresh")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_DataConnectionRefresh
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_dataconnectionrefresh");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_dataconnectionrefresh", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_dataconnectionrefreshname")]
		public string Msdyn_DataConnectionRefreshName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_dataconnectionrefresh"))
				{
					return FormattedValues["msdyn_dataconnectionrefresh"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Lookup to corresponding data definition of sustainability product identifier.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_datadefinition")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_DataDefinition
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_datadefinition");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_datadefinition", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_datadefinitionname")]
		public string Msdyn_DataDefinitionName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_datadefinition"))
				{
					return FormattedValues["msdyn_datadefinition"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The name of the custom entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_name")]
		public string Msdyn_Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_name");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_name", value);
			}
		}
		
		/// <summary>
		/// An optional identifier to correlate record with data origin.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_origincorrelationid")]
		public string Msdyn_OriginCorrelationId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_origincorrelationid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_origincorrelationid", value);
			}
		}
		
		/// <summary>
		/// The product this ID is for.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproduct")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_SustainabilityProduct
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_sustainabilityproduct");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_sustainabilityproduct", value);
			}
		}
		
		/// <summary>
		/// Uniquely identifies a product. Each sustainability product ID must be a conforming URN with a namespace value included in the Official IANA Registry of URN Namespaces.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductidentifier")]
		public string SustainabilityProductIdentifier
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_sustainabilityproductidentifier");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_sustainabilityproductidentifier", value);
			}
		}
		
		/// <summary>
		/// Unique identifier for entity instances.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductidentifierid")]
		public Nullable<Guid> Msdyn_SustainabilityProductIdentifierId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<Guid>>("msdyn_sustainabilityproductidentifierid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_sustainabilityproductidentifierid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = Guid.Empty;
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductidentifierid")]
		public override Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				Msdyn_SustainabilityProductIdentifierId = value;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductname")]
		public string Msdyn_SustainabilityProductName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_sustainabilityproduct"))
				{
					return FormattedValues["msdyn_sustainabilityproduct"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public Nullable<DateTime> OverriddenCreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<DateTime>>("overriddencreatedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("overriddencreatedon", value);
			}
		}
		
		/// <summary>
		/// Owner Id
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
		public Microsoft.Xrm.Sdk.EntityReference OwnerId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("ownerid", value);
			}
		}
		
		/// <summary>
		/// Name of the owner
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owneridname")]
		public string OwnerIdName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("ownerid"))
				{
					return FormattedValues["ownerid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Yomi name of the owner
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owneridyominame")]
		public string OwnerIdYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("ownerid"))
				{
					return FormattedValues["ownerid"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier for the business unit that owns the record
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunit")]
		public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningbusinessunit");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunitname")]
		public string OwningBusinessUnitName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("owningbusinessunit"))
				{
					return FormattedValues["owningbusinessunit"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier for the team that owns the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
		public Microsoft.Xrm.Sdk.EntityReference OwningTeam
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningteam");
			}
		}
		
		/// <summary>
		/// Unique identifier for the user that owns the record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
		public Microsoft.Xrm.Sdk.EntityReference OwningUser
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owninguser");
			}
		}
		
		/// <summary>
		/// Status of the (Preview) Sustainability product identifier
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public virtual Msdyn_SustainabilityProductIdentifier_StateCode? StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_SustainabilityProductIdentifier_StateCode?)(EntityOptionSetEnum.GetEnum(this, "statecode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("statecode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecodename")]
		public string StateCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("statecode"))
				{
					return FormattedValues["statecode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Reason for the status of the (Preview) Sustainability product identifier
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual Msdyn_SustainabilityProductIdentifier_StatusCode? StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_SustainabilityProductIdentifier_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("statuscode", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscodename")]
		public string StatusCodename
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("statuscode"))
				{
					return FormattedValues["statuscode"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
		public Nullable<int> TimeZoneRuleVersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<int>>("timezoneruleversionnumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("timezoneruleversionnumber", value);
			}
		}
		
		/// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
		public Nullable<int> UtcConversionTimeZoneCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<int>>("utcconversiontimezonecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("utcconversiontimezonecode", value);
			}
		}
		
		/// <summary>
		/// Version Number
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public Nullable<long> VersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// N:1 msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductidentifier_sustainabilityproduct
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproduct")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductidentifier_sustainab" +
			"ilityproduct")]
		public Msdyn_SustainabilityProduct Msdyn_Msdyn_SustainabilityProduct_Msdyn_SustainabilityProductIdentifier_SustainabilityProduct
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Msdyn_SustainabilityProduct>("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductidentifier_sustainab" +
						"ilityproduct", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SetRelatedEntity<Msdyn_SustainabilityProduct>("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductidentifier_sustainab" +
						"ilityproduct", null, value);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductIdentifier(object anonymousType) : 
				this()
		{
            foreach (var p in anonymousType.GetType().GetProperties())
            {
                var value = p.GetValue(anonymousType, null);
                var name = p.Name.ToLower();
            
                if (name.EndsWith("enum") && value.GetType().BaseType == typeof(Enum))
                {
                    value = new Microsoft.Xrm.Sdk.OptionSetValue((int) value);
                    name = name.Remove(name.Length - "enum".Length);
                }
            
                switch (name)
                {
                    case "id":
                        base.Id = (Guid)value;
                        Attributes["msdyn_sustainabilityproductidentifierid"] = base.Id;
                        break;
                    case "msdyn_sustainabilityproductidentifierid":
                        var id = (Nullable<Guid>) value;
                        if(id == null){ continue; }
                        base.Id = id.Value;
                        Attributes[name] = base.Id;
                        break;
                    case "formattedvalues":
                        // Add Support for FormattedValues
                        FormattedValues.AddRange((Microsoft.Xrm.Sdk.FormattedValueCollection)value);
                        break;
                    default:
                        Attributes[name] = value;
                        break;
                }
            }
		}
	}
}
#pragma warning restore CS1591
