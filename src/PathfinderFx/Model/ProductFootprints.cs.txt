// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Microsoft.PathfinderFx;
//
//    var productFootprint = ProductFootprints.FromJson(jsonString);

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model
{
    public partial class ProductFootprints
    {
        [Required]
        [JsonProperty("data")]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [Required]
        [JsonProperty("id", Required = Required.Always)]
        public Guid Id { get; set; }
        
        [Required]
        [JsonProperty("specVersion", Required = Required.Always)]
        public string SpecVersion { get; set; }

        [Required]
        [JsonProperty("version", Required = Required.Always)]
        public long Version { get; set; }

        [Required]
        [JsonProperty("created", Required = Required.Always)]
        public DateTimeOffset Created { get; set; }
        
  
        [JsonProperty("updated")]
        public string Updated { get; set; }

        [Required]
        [JsonProperty("companyName", Required = Required.Always)]
        public string CompanyName { get; set; }

        [Required]
        [JsonProperty("companyIds", Required = Required.Always)]
        public string[] CompanyIds { get; set; }

        [Required]
        [JsonProperty("productDescription", Required = Required.Always)]
        public string ProductDescription { get; set; }

        [Required]
        [JsonProperty("productIds", Required = Required.Always)]
        public string[] ProductIds { get; set; }

        [Required]
        [JsonProperty("productCategoryCpc", Required = Required.Always)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProductCategoryCpc { get; set; }

        [Required]
        [JsonProperty("productNameCompany", Required = Required.Always)]
        public string ProductNameCompany { get; set; }

        [Required]
        [JsonProperty("comment", Required = Required.AllowNull)]
        public string Comment { get; set; }

        [Required]
        [JsonProperty("pcf", Required = Required.Always)]
        public Pcf Pcf { get; set; }

        [JsonProperty("extensions")]
        public Extension[] Extensions { get; set; }
    }

    public partial class Extension
    {
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [Required]
        [JsonProperty("version", Required = Required.Always)]
        public string Version { get; set; }
        
        [Required]
        [JsonProperty("urn", Required = Required.AllowNull)]
        public string URN { get; set; }

        [Required]
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }
        
        [Required]
        [JsonProperty("data", Required = Required.Always)]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [Required]
        [JsonProperty("key", Required = Required.Always)]
        public string Key { get; set; }
        
        [Required]
        [JsonProperty("value", Required = Required.AllowNull)]
        public string Value { get; set; }
    }

    
    public enum Region{Africa, Americas, Asia, Europe, Oceania};
    public enum Subregion {AustraliaAndNewZealand, Caribbean, CentralAmerica, CentralAsia, EasternAfrica, EasternAsia, EasternEurope, Melanesia, Micronesia, MiddleAfrica, NorthernAfrica, NorthernAmerica, NorthernEurope, Polynesia, SouthAmerica, SouthEasternAsia, SouthernAfrica, SouthernAsia, SouthernEurope, WesternAfrica, WesternAsia, WesternEurope};
    public partial class Pcf
    {
        [Required]
        [JsonProperty("declaredUnit", Required = Required.Always)]
        public string DeclaredUnit { get; set; }
        
        [Required]
        [JsonProperty("unitaryProductAmount", Required = Required.Always)]
        public string UnitaryProductAmount { get; set; }

        [Required]
        [JsonProperty("pCfExcludingBiogenic", Required = Required.Always)]
        public string PCfExcludingBiogenic { get; set; }

        [JsonProperty("pCfIncludingBiogenic")]
        public string PCfIncludingBiogenic { get; set; }

        [Required]
        [JsonProperty("fossilGhgEmissions", Required = Required.Always)]
        public string FossilGhgEmissions { get; set; }

        [Required]
        [JsonProperty("fossilCarbonContent", Required = Required.Always)]
        public string FossilCarbonContent { get; set; }

        [JsonProperty("biogenicEmissions")]
        public BiogenicEmissions BiogenicEmissions { get; set; }

        [Required]
        [JsonProperty("biogenicCarbonContent", Required = Required.Always)]
        public string BiogenicCarbonContent { get; set; }

        [JsonProperty("biogenicCarbonWithdrawal")]
        public string BiogenicCarbonWithdrawal { get; set; }

        [Required]
        [JsonProperty("characterizationFactors", Required = Required.Always)]
        public string CharacterizationFactors { get; set; }

        [JsonProperty("biogenicAccountingMethodology")]
        public string BiogenicAccountingMethodology { get; set; }

        [Required]
        [JsonProperty("reportingPeriodStart", Required = Required.Always)]
        public DateTimeOffset ReportingPeriodStart { get; set; }

        [Required]
        [JsonProperty("reportingPeriodEnd", Required = Required.Always)]
        public DateTimeOffset ReportingPeriodEnd { get; set; }

        [JsonProperty("geographyCountrySubdivision")]
        public string GeographyCountrySubdivision { get; set; }

        [JsonProperty("geography_country", Required = Required.Always)]
        [JsonConverter((typeof(GeographyCountryConverter)))]
        public GeographyCountry GeographyCountry { get; set; }

        [JsonProperty("geographyRegionOrSubregion", Required = Required.Always)]
        public string GeographyRegionOrSubregion { get; set; }

        [JsonProperty("primaryDataShare")]
        public double PrimaryDataShare { get; set; }

        [JsonProperty("emissionFactorSources")]
        public EmissionFactorSource[] EmissionFactorSources { get; set; }

        [JsonProperty("boundaryProcessesDescription")]
        public string BoundaryProcessesDescription { get; set; }

        [Required]
        [JsonProperty("exemptedEmissionsPercent", Required = Required.Always)]
        public long ExemptedEmissionsPercent { get; set; }

        [Required]
        [JsonProperty("exemptedEmissionsDescription", Required = Required.Always)]
        public string ExemptedEmissionsDescription { get; set; }

        [Required]
        [JsonProperty("packagingEmissionsIncluded", Required = Required.Always)]
        public bool PackagingEmissionsIncluded { get; set; }

        [JsonProperty("packagingGhgEmissions")]
        public string PackagingGhgEmissions { get; set; }

        [Required]
        [JsonProperty("crossSectoralStandardsUsed", Required = Required.Always)]
        public string[] CrossSectoralStandardsUsed { get; set; }

        [Required]
        [JsonProperty("productOrSectorSpecificRules", Required = Required.Always)]
        public ProductOrSectorSpecificRule[] ProductOrSectorSpecificRules { get; set; }

        [JsonProperty("allocationRulesDescription")]
        public string AllocationRulesDescription { get; set; }

        [JsonProperty("uncertaintyAssessmentDescription")]
        public string UncertaintyAssessmentDescription { get; set; }

        [JsonProperty("dqi")]
        public Dqi Dqi { get; set; }

        [JsonProperty("assurance")]
        public Assurance Assurance { get; set; }
    }

    public partial class Assurance
    {
        [Required]
        [JsonProperty("coverage", Required = Required.Always)]
        public string Coverage { get; set; }

        [Required]
        [JsonProperty("level", Required = Required.Always)]
        public string Level { get; set; }

        [Required]
        [JsonProperty("boundary", Required = Required.Always)]
        public string Boundary { get; set; }

        [Required]
        [JsonProperty("providerName", Required = Required.Always)]
        public string ProviderName { get; set; }

        [Required]
        [JsonProperty("completedAt", Required = Required.Always)]
        public DateTimeOffset CompletedAt { get; set; }

        [Required]
        [JsonProperty("standard", Required = Required.Always)]
        public string Standard { get; set; }

        [Required]
        [JsonProperty("statementOrSignature", Required = Required.Always)]
        public StatementOrSignature StatementOrSignature { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }

    public enum AssuranceProof{Document, DigitalSignature}
    public partial class StatementOrSignature
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(AssuranceProofConverter))]
        public AssuranceProof Type { get; set; } //enum me

        [JsonProperty("value", Required = Required.AllowNull)]
        public string Value { get; set; }
    }

    public partial class BiogenicEmissions
    {
        [JsonProperty("landUseEmissions")]
        public string LandUseEmissions { get; set; }

        [JsonProperty("iLucGhgEmissions")]
        public string ILucGhgEmissions { get; set; }

        [JsonProperty("dLucGhgEmissions")]
        public string DLucGhgEmissions { get; set; }

        [JsonProperty("otherGhgEmissions")]
        public string OtherGhgEmissions { get; set; }
    }

    public partial class Dqi
    {
        [JsonProperty("coveragePercent")]
        public long CoveragePercent { get; set; }

        [JsonProperty("technologicalDQR")]
        public double TechnologicalDqr { get; set; }

        [JsonProperty("temporalDQR")]
        public double TemporalDqr { get; set; }

        [JsonProperty("geographicalDQR")]
        public long GeographicalDqr { get; set; }

        [JsonProperty("completenessDQR")]
        public double CompletenessDqr { get; set; }

        [JsonProperty("reliabilityDQR")]
        public double ReliabilityDqr { get; set; }
    }

    public partial class EmissionFactorSource
    {
        [Required]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [Required]
        [JsonProperty("version", Required = Required.Always)]
        public string Version { get; set; }
    }

    public partial class ProductOrSectorSpecificRule
    {
        
        [Required]
        [JsonProperty("operator", Required = Required.Always)]
        public string Operator { get; set; } //enum PEF, EPD International, Other

        [Required]
        [JsonProperty("ruleNames", Required = Required.Always)]
        public string[] RuleNames { get; set; }
        
        [JsonProperty("otherOperatorName")]
        public string OtherOperatorName { get; set; }
        
    }

    public partial class ProductFootprints
    {
        public static ProductFootprints FromJson(string json) => JsonConvert.DeserializeObject<ProductFootprints>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ProductFootprints self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
    
    internal class AssuranceProofConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AssuranceProof) || t == typeof(AssuranceProof?);

        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Document")
            {
                return AssuranceProof.Document;
            }
            if(value=="DigitalSignature")
            {
                return AssuranceProof.DigitalSignature;
            }
            throw new Exception("Cannot unmarshal type GeospatialSourceConverter");
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AssuranceProof)untypedValue;
            if (value == AssuranceProof.Document)
            {
                serializer.Serialize(writer, "Document");
                return;
            }
            {
                serializer.Serialize(writer, "DigitalSignature");
            }
        }

        public static readonly AssuranceProofConverter Singleton = new AssuranceProofConverter();
    }
    
    internal class GeographicCountryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeographyCountry) || t == typeof(GeographyCountry?);

        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            fixe this HubEndpointRouteBuilderExtensions with the enum.ToString()
            if (value == "Document")
            {
                return AssuranceProof.Document;
            }
            if(value=="DigitalSignature")
            {
                return AssuranceProof.DigitalSignature;
            }
            throw new Exception("Cannot unmarshal type GeospatialSourceConverter");
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AssuranceProof)untypedValue;
            if (value == AssuranceProof.Document)
            {
                serializer.Serialize(writer, "Document");
                return;
            }
            {
                serializer.Serialize(writer, "DigitalSignature");
            }
        }

        public static readonly AssuranceProofConverter Singleton = new AssuranceProofConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
