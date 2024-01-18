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
	/// Option set, with values Active and Inactive
	///
	///Default to Active
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0, "#0000ff")]
		Active = 700610000,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1, "#0000ff")]
		Inactive = 700610001,
	}
	
	/// <summary>
	/// Status of the (Preview) Product footprint
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum Msdyn_SustainabilityProductFootprint_StateCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 1,
	}
	
	/// <summary>
	/// Reason for the status of the (Preview) Product footprint
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum Msdyn_SustainabilityProductFootprint_StatusCode
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Active", 0)]
		Active = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Inactive", 1)]
		Inactive = 2,
	}
	
	/// <summary>
	/// The carbon footprint of a product with values in accordance with the Pathfinder Framework.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("msdyn_sustainabilityproductfootprint")]
	public partial class Msdyn_SustainabilityProductFootprint : Microsoft.Xrm.Sdk.Entity
	{
		
		/// <summary>
		/// Available fields, a the time of codegen, for the msdyn_sustainabilityproductfootprint entity
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
			public const string Msdyn_Comment = "msdyn_comment";
			public const string Msdyn_Company = "msdyn_company";
			public const string Msdyn_CompanyName = "msdyn_companyname";
			public const string Msdyn_CompanyYomiName = "msdyn_companyyominame";
			public const string Msdyn_DataConnection = "msdyn_dataconnection";
			public const string Msdyn_DataConnectionName = "msdyn_dataconnectionname";
			public const string Msdyn_DataConnectionRefresh = "msdyn_dataconnectionrefresh";
			public const string Msdyn_DataConnectionRefreshName = "msdyn_dataconnectionrefreshname";
			public const string Msdyn_DataDefinition = "msdyn_datadefinition";
			public const string Msdyn_DataDefinitionName = "msdyn_datadefinitionname";
			public const string Msdyn_FootprintStatus = "msdyn_footprintstatus";
			public const string Msdyn_FootprintStatusName = "msdyn_footprintstatusname";
			public const string Msdyn_Msdyn_SustainabilityProduct_Msdyn_SustainabilityProductFootprint_SustainabilityProduct = "msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductfootprint_Sustainabilityproduct";
			public const string Msdyn_Msdyn_SustainabilityProductFootprint_SuSt = "msdyn_msdyn_sustainabilityproductfootprint_sust";
			public const string Msdyn_Name = "msdyn_name";
			public const string Msdyn_OriginCorrelationId = "msdyn_origincorrelationid";
			public const string Msdyn_SpecVersion = "msdyn_specversion";
			public const string Msdyn_StatusComment = "msdyn_statuscomment";
			public const string Msdyn_SupplierSurveyDetail = "msdyn_suppliersurveydetail";
			public const string Msdyn_SupplierSurveyDetailName = "msdyn_suppliersurveydetailname";
			public const string Msdyn_SustainabilityProduct = "msdyn_sustainabilityproduct";
			public const string Msdyn_SustainabilityProductCarbonFootprint = "msdyn_sustainabilityproductcarbonfootprint";
			public const string Msdyn_SustainabilityProductCarbonFootprintName = "msdyn_sustainabilityproductcarbonfootprintname";
			public const string Msdyn_SustainabilityProductFootprintId = "msdyn_sustainabilityproductfootprintid";
			public const string Id = "msdyn_sustainabilityproductfootprintid";
			public const string Msdyn_SustainabilityProductName = "msdyn_sustainabilityproductname";
			public const string Msdyn_ValidityPeriodEnd = "msdyn_validityperiodend";
			public const string Msdyn_ValidityPeriodStart = "msdyn_validityperiodstart";
			public const string Msdyn_Version = "msdyn_version";
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
		public Msdyn_SustainabilityProductFootprint(Guid id) : 
				base(EntityLogicalName, id)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductFootprint(string keyName, object keyValue) : 
				base(EntityLogicalName, keyName, keyValue)
		{
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductFootprint(Microsoft.Xrm.Sdk.KeyAttributeCollection keyAttributes) : 
				base(EntityLogicalName, keyAttributes)
		{
		}
		
		public const string AlternateKeys = "msdyn_name|msdyn_origincorrelationid";
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductFootprint() : 
				base(EntityLogicalName)
		{
		}
		
		public const string PrimaryIdAttribute = "msdyn_sustainabilityproductfootprintid";
		
		public const string PrimaryNameAttribute = "msdyn_name";
		
		public const string EntitySchemaName = "msdyn_sustainabilityproductfootprint";
		
		public const string EntityLogicalName = "msdyn_sustainabilityproductfootprint";
		
		public const string EntityLogicalCollectionName = "msdyn_sustainabilityproductfootprints";
		
		public const string EntitySetName = "msdyn_sustainabilityproductfootprints";
		
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
		/// The additional information related to the PCF. While product table description contains product-level information, comment should be used for PCF-related information.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_comment")]
		public string Msdyn_Comment
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_comment");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_comment", value);
			}
		}
		
		/// <summary>
		/// The company that is the data owner.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_company")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_Company
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_company");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_company", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_companyname")]
		public string Msdyn_CompanyName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_company"))
				{
					return FormattedValues["msdyn_company"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_companyyominame")]
		public string Msdyn_CompanyYomiName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_company"))
				{
					return FormattedValues["msdyn_company"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The connection that ingested this sustainability product footprint.
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
		/// The connection refresh which ingested this sustainability product footprint.
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
		/// Lookup to corresponding data definition of sustainability product footprint.
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
		/// The default status of a product footprint is Active. A product footprint with status Inactive shouldn't be used, for example, product footprint calculations by data recipients.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_footprintstatus")]
		public virtual Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus? Msdyn_FootprintStatus
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus?)(EntityOptionSetEnum.GetEnum(this, "msdyn_footprintstatus")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_footprintstatus", value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_footprintstatusname")]
		public string Msdyn_FootprintStatusName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_footprintstatus"))
				{
					return FormattedValues["msdyn_footprintstatus"];
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
		/// The version of the Pathfinder Framework data specification being used.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_specversion")]
		public string Msdyn_SpecVersion
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_specversion");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_specversion", value);
			}
		}
		
		/// <summary>
		/// If defined, the value should be a message explaining the reason for the current status.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_statuscomment")]
		public string Msdyn_StatusComment
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<string>("msdyn_statuscomment");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_statuscomment", value);
			}
		}
		
		/// <summary>
		/// Lookup to the partner survey record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_suppliersurveydetail")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_SupplierSurveyDetail
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_suppliersurveydetail");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_suppliersurveydetail", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_suppliersurveydetailname")]
		public string Msdyn_SupplierSurveyDetailName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_suppliersurveydetail"))
				{
					return FormattedValues["msdyn_suppliersurveydetail"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// The product this footprint is for.
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
		/// The carbon footprint of the given product.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductcarbonfootprint")]
		public Microsoft.Xrm.Sdk.EntityReference Msdyn_SustainabilityProductCarbonFootprint
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("msdyn_sustainabilityproductcarbonfootprint");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_sustainabilityproductcarbonfootprint", value);
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductcarbonfootprintname")]
		public string Msdyn_SustainabilityProductCarbonFootprintName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				if (FormattedValues.Contains("msdyn_sustainabilityproductcarbonfootprint"))
				{
					return FormattedValues["msdyn_sustainabilityproductcarbonfootprint"];
				}
				else
				{
					return default(string);
				}
			}
		}
		
		/// <summary>
		/// Unique identifier for entity instances.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductfootprintid")]
		public Nullable<Guid> Msdyn_SustainabilityProductFootprintId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<Guid>>("msdyn_sustainabilityproductfootprintid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_sustainabilityproductfootprintid", value);
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
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductfootprintid")]
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
				Msdyn_SustainabilityProductFootprintId = value;
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
		/// The end (exclusive) of the valid period of the PCF. Reference the description of validity period start and the Pathfinder Framework for further details.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_validityperiodend")]
		public Nullable<DateTime> Msdyn_ValidityPeriodEnd
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<DateTime>>("msdyn_validityperiodend");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_validityperiodend", value);
			}
		}
		
		/// <summary>
		/// The start of the validity period, which is the interval during which the PCF is declared as valid for use by a data recipient. Reference the Pathfinder Framework for further details. 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_validityperiodstart")]
		public Nullable<DateTime> Msdyn_ValidityPeriodStart
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<DateTime>>("msdyn_validityperiodstart");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_validityperiodstart", value);
			}
		}
		
		/// <summary>
		/// The version of the product footprint with value an integer in the inclusive range of 0 to (2^31)-1.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_version")]
		public Nullable<int> Msdyn_Version
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetAttributeValue<Nullable<int>>("msdyn_version");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetAttributeValue("msdyn_version", value);
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
		/// Status of the (Preview) Sustainability product footprint
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public virtual Msdyn_SustainabilityProductFootprint_StateCode? StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_SustainabilityProductFootprint_StateCode?)(EntityOptionSetEnum.GetEnum(this, "statecode")));
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
		/// Reason for the status of the (Preview) Sustainability product footprint
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual Msdyn_SustainabilityProductFootprint_StatusCode? StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((Msdyn_SustainabilityProductFootprint_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
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
		/// N:1 msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductfootprint_Sustainabilityproduct
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproduct")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductfootprint_Sustainabi" +
			"lityproduct")]
		public Msdyn_SustainabilityProduct Msdyn_Msdyn_SustainabilityProduct_Msdyn_SustainabilityProductFootprint_SustainabilityProduct
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetRelatedEntity<Msdyn_SustainabilityProduct>("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductfootprint_Sustainabi" +
				                                                          "lityproduct", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetRelatedEntity<Msdyn_SustainabilityProduct>("msdyn_msdyn_sustainabilityproduct_msdyn_sustainabilityproductfootprint_Sustainabi" +
				                                                   "lityproduct", null, value);
			}
		}
		
		/// <summary>
		/// N:1 msdyn_msdyn_sustainabilityproductfootprint_sust
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("msdyn_sustainabilityproductcarbonfootprint")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("msdyn_msdyn_sustainabilityproductfootprint_sust")]
		public Msdyn_SustainabilityProductCarbonFootprint Msdyn_Msdyn_SustainabilityProductFootprint_SuSt
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return GetRelatedEntity<Msdyn_SustainabilityProductCarbonFootprint>("msdyn_msdyn_sustainabilityproductfootprint_sust", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				SetRelatedEntity<Msdyn_SustainabilityProductCarbonFootprint>("msdyn_msdyn_sustainabilityproductfootprint_sust", null, value);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public Msdyn_SustainabilityProductFootprint(object anonymousType) : 
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
                        Attributes["msdyn_sustainabilityproductfootprintid"] = base.Id;
                        break;
                    case "msdyn_sustainabilityproductfootprintid":
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
