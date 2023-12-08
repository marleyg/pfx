// <auto-generated />
//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using PathfinderFx;
//
//    var productFootprints = ProductFootprints.FromJson(jsonString);
#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace PathfinderFx.Model
{
    using System;
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Globalization;

    public partial class ProductFootprints
    {
        [JsonPropertyName("data")]
        public List<ProductFootprint> Data { get; set; }
    }

    public partial class ProductFootprint
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("specVersion")]
        public string SpecVersion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("precedingPfIds")]
        public List<string> PrecedingPfIds { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("version")]
        public long? Version { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("created")]
        public DateTimeOffset? Created { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("statusComment")]
        public string StatusComment { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validityPeriodStart")]
        public DateTimeOffset? ValidityPeriodStart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validityPeriodEnd")]
        public DateTimeOffset? ValidityPeriodEnd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("companyIds")]
        public List<string> CompanyIds { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("productIds")]
        public List<string> ProductIds { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("productCategoryCpc")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProductCategoryCpc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("productNameCompany")]
        public string ProductNameCompany { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pcf")]
        public Pcf Pcf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extensions")]
        public List<Extension> Extensions { get; set; }
    }

    public partial class Extension
    {
        
        [JsonPropertyName("specVersion")]
        public string SpecVersion { get; set; }

        [JsonPropertyName("dataSchema")]
        public Uri DataSchema { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("documentation")]
        public Uri Documentation { get; set; }

        [JsonPropertyName("data")]
        public ShipmentExtension Data { get; set; }
    }

    public partial class ShipmentExtension
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("shipmentId")]
        public string ShipmentId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("consignmentId")]
        public string ConsignmentId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("shipmentType")]
        public string ShipmentType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("weight")]
        public long? Weight { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transportChainElementId")]
        public string TransportChainElementId { get; set; }
    }

    public partial class Pcf
    {      
        [JsonPropertyName("declaredUnit")]
        public string DeclaredUnit { get; set; }

        [JsonPropertyName("unitaryProductAmount")]
        public string UnitaryProductAmount { get; set; }

        [JsonPropertyName("pCfExcludingBiogenic")]
        public string PCfExcludingBiogenic { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pCfIncludingBiogenic")]
        public string PCfIncludingBiogenic { get; set; }

        [JsonPropertyName("fossilGhgEmissions")]
        public string FossilGhgEmissions { get; set; }

        [JsonPropertyName("fossilCarbonContent")]
        public string FossilCarbonContent { get; set; }

        [JsonPropertyName("biogenicCarbonContent")]
        public string BiogenicCarbonContent { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dLucGhgEmissions")]
        public string DLucGhgEmissions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("landManagementGhgEmissions")]
        public string LandManagementGhgEmissions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("otherBiogenicGhgEmissions")]
        public string OtherBiogenicGhgEmissions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("iLucGhgEmissions")]
        public string ILucGhgEmissions { get; set; }

        //TODO: this should be a decimal serialized as a json string
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("biogenicCarbonWithdrawal")]
        public string BiogenicCarbonWithdrawal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("aircraftGhgEmissions")]
        public string AircraftGhgEmissions { get; set; }

        [JsonPropertyName("characterizationFactors")]
        public string CharacterizationFactors { get; set; }

        [JsonPropertyName("crossSectoralStandardsUsed")]
        public List<string> CrossSectoralStandardsUsed { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("productOrSectorSpecificRules")]
        public List<ProductOrSectorSpecificRule> ProductOrSectorSpecificRules { get; set; }

        //TODO: this should be an enum PEF, ISO, GHGP, Quantis
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("biogenicAccountingMethodology")]
        public string BiogenicAccountingMethodology { get; set; }

        [JsonPropertyName("boundaryProcessesDescription")]
        public string BoundaryProcessesDescription { get; set; }

        [JsonPropertyName("referencePeriodStart")]
        public DateTimeOffset? ReferencePeriodStart { get; set; }

        [JsonPropertyName("referencePeriodEnd")]
        public DateTimeOffset? ReferencePeriodEnd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("geographyCountrySubdivision")]
        public string GeographyCountrySubdivision { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("geographyCountry")]
        public string GeographyCountry { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("geographyRegionOrSubregion")]
        public string GeographyRegionOrSubregion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("secondaryEmissionFactorSources")]
        public List<SecondaryEmissionFactorSource> SecondaryEmissionFactorSources { get; set; }

        [JsonPropertyName("exemptedEmissionsPercent")]
        public decimal? ExemptedEmissionsPercent { get; set; }

        [JsonPropertyName("exemptedEmissionsDescription")]
        public string ExemptedEmissionsDescription { get; set; }

        [JsonPropertyName("packagingEmissionsIncluded")]
        public bool? PackagingEmissionsIncluded { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("packagingGhgEmissions")]
        public string PackagingGhgEmissions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("allocationRulesDescription")]
        public string AllocationRulesDescription { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("uncertaintyAssessmentDescription")]
        public string UncertaintyAssessmentDescription { get; set; }

        /// <summary>
        /// Optional until 2025
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("primaryDataShare")]
        public double? PrimaryDataShare { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dqi")]
        public DataQualityIndicators Dqi { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("assurance")]
        public Assurance Assurance { get; set; }
    }

    public partial class Assurance
    {
        [JsonPropertyName("assurance")]
        public bool HasAssurance { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("coverage")]
        public string Coverage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("boundary")]
        public string Boundary { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("completedAt")]
        public DateTimeOffset? CompletedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("standardName")]
        public string StandardName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("comments")]
        public string Comments { get; set; }
    }

    public partial class DataQualityIndicators
    {
        [JsonPropertyName("coveragePercent")]
        public long? CoveragePercent { get; set; }

        [JsonPropertyName("technologicalDQR")]
        public double? TechnologicalDqr { get; set; }

        [JsonPropertyName("temporalDQR")]
        public double? TemporalDqr { get; set; }

        [JsonPropertyName("geographicalDQR")]
        public long? GeographicalDqr { get; set; }

        [JsonPropertyName("completenessDQR")]
        public double? CompletenessDqr { get; set; }

        [JsonPropertyName("reliabilityDQR")]
        public double? ReliabilityDqr { get; set; }
    }

    public partial class ProductOrSectorSpecificRule
    {
        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("ruleNames")]
        public List<string> RuleNames { get; set; }
    }

    public partial class SecondaryEmissionFactorSource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public partial class ProductFootprints
    {
        public static ProductFootprints FromJson(string json) => JsonSerializer.Deserialize<ProductFootprints>(json, PathfinderFx.Model.Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this ProductFootprints self) => JsonSerializer.Serialize(self, PathfinderFx.Model.Converter.Settings);
    }

    public static class Converter
    {
        public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
        };
    }

    internal class ParseStringConverter : JsonConverter<long>
    {
        public override bool CanConvert(Type t) => t == typeof(long);

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;
        public DateOnlyConverter() : this(null) { }

        public DateOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string? _dateTimeFormat;
        private CultureInfo? _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string? DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            string text;


            if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
            {
                value = value.ToUniversalTime();
            }

            text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

            writer.WriteStringValue(text);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateText = reader.GetString();

            if (string.IsNullOrEmpty(dateText) == false)
            {
                if (!string.IsNullOrEmpty(_dateTimeFormat))
                {
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                }
            }
            else
            {
                return default(DateTimeOffset);
            }
        }


        public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
    }
}
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
