namespace PathfinderFx.Model

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var productFootprintCatcher = ProductFootprintCatcher.FromJson(jsonString);

{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ProductFootprintCatchers
    {
        [JsonProperty("data")]
        public List<ProductFootprintCatchers> Data { get; set; }
    }
    
    public partial class ProductFootprintCatcher
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("specVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string SpecVersion { get; set; }

        [JsonProperty("precedingPfIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<Guid> PrecedingPfIds { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public long? Version { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Updated { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("statusComment", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusComment { get; set; }

        [JsonProperty("validityPeriodStart", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidityPeriodStart { get; set; }

        [JsonProperty("validityPeriodEnd", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ValidityPeriodEnd { get; set; }

        [JsonProperty("companyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }

        [JsonProperty("companyIds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> CompanyIds { get; set; }

        [JsonProperty("productDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductDescription { get; set; }

        [JsonProperty("productIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ProductIds { get; set; }

        [JsonProperty("productCategoryCpc", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ProductCategoryCpc { get; set; }

        [JsonProperty("productNameCompany", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductNameCompany { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("pcf", NullValueHandling = NullValueHandling.Ignore)]
        public Pcf Pcf { get; set; }

        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Extension> Extensions { get; set; }
    }

    public partial class Extension
    {
        [JsonProperty("specVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string SpecVersion { get; set; }

        [JsonProperty("dataSchema", NullValueHandling = NullValueHandling.Ignore)]
        public Uri DataSchema { get; set; }

        [JsonProperty("documentation", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Documentation { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("shipmentId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ShipmentId { get; set; }

        [JsonProperty("consignmentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsignmentId { get; set; }

        [JsonProperty("shipmentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ShipmentType { get; set; }

        [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
        public long? Weight { get; set; }

        [JsonProperty("transportChainElementId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TransportChainElementId { get; set; }
    }

    public partial class Pcf
    {
        [JsonProperty("declaredUnit", NullValueHandling = NullValueHandling.Ignore)]
        public string DeclaredUnit { get; set; }

        [JsonProperty("unitaryProductAmount", NullValueHandling = NullValueHandling.Ignore)]
        public string UnitaryProductAmount { get; set; }

        [JsonProperty("pCfExcludingBiogenic", NullValueHandling = NullValueHandling.Ignore)]
        public string PCfExcludingBiogenic { get; set; }

        [JsonProperty("pCfIncludingBiogenic", NullValueHandling = NullValueHandling.Ignore)]
        public string PCfIncludingBiogenic { get; set; }

        [JsonProperty("fossilGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string FossilGhgEmissions { get; set; }

        [JsonProperty("fossilCarbonContent", NullValueHandling = NullValueHandling.Ignore)]
        public string FossilCarbonContent { get; set; }

        [JsonProperty("biogenicCarbonContent", NullValueHandling = NullValueHandling.Ignore)]
        public string BiogenicCarbonContent { get; set; }

        [JsonProperty("dLucGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string DLucGhgEmissions { get; set; }

        [JsonProperty("landManagementGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string LandManagementGhgEmissions { get; set; }

        [JsonProperty("otherBiogenicGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string OtherBiogenicGhgEmissions { get; set; }

        [JsonProperty("iLucGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string ILucGhgEmissions { get; set; }

        [JsonProperty("biogenicCarbonWithdrawal", NullValueHandling = NullValueHandling.Ignore)]
        public string BiogenicCarbonWithdrawal { get; set; }

        [JsonProperty("aircraftGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
        public string AircraftGhgEmissions { get; set; }

        [JsonProperty("characterizationFactors", NullValueHandling = NullValueHandling.Ignore)]
        public string CharacterizationFactors { get; set; }

        [JsonProperty("crossSectoralStandardsUsed", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> CrossSectoralStandardsUsed { get; set; }

        [JsonProperty("productOrSectorSpecificRules", NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductOrSectorSpecificRule> ProductOrSectorSpecificRules { get; set; }

        [JsonProperty("biogenicAccountingMethodology", NullValueHandling = NullValueHandling.Ignore)]
        public string BiogenicAccountingMethodology { get; set; }

        [JsonProperty("boundaryProcessesDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string BoundaryProcessesDescription { get; set; }

        [JsonProperty("referencePeriodStart", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ReferencePeriodStart { get; set; }

        [JsonProperty("referencePeriodEnd", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ReferencePeriodEnd { get; set; }

        [JsonProperty("geographyCountrySubdivision", NullValueHandling = NullValueHandling.Ignore)]
        public string GeographyCountrySubdivision { get; set; }

        [JsonProperty("geographyCountry", NullValueHandling = NullValueHandling.Ignore)]
        public string GeographyCountry { get; set; }

        [JsonProperty("geographyRegionOrSubregion", NullValueHandling = NullValueHandling.Ignore)]
        public string GeographyRegionOrSubregion { get; set; }

        [JsonProperty("secondaryEmissionFactorSources", NullValueHandling = NullValueHandling.Ignore)]
        public List<SecondaryEmissionFactorSource> SecondaryEmissionFactorSources { get; set; }

        [JsonProperty("exemptedEmissionsPercent", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExemptedEmissionsPercent { get; set; }

        [JsonProperty("exemptedEmissionsDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ExemptedEmissionsDescription { get; set; }

        [JsonProperty("packagingEmissionsIncluded", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PackagingEmissionsIncluded { get; set; }

        [JsonProperty("allocationRulesDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string AllocationRulesDescription { get; set; }

        [JsonProperty("uncertaintyAssessmentDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string UncertaintyAssessmentDescription { get; set; }

        [JsonProperty("primaryDataShare", NullValueHandling = NullValueHandling.Ignore)]
        public double? PrimaryDataShare { get; set; }

        [JsonProperty("assurance", NullValueHandling = NullValueHandling.Ignore)]
        public Assurance Assurance { get; set; }
    }

    public partial class Assurance
    {
        [JsonProperty("assurance", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AssuranceAssurance { get; set; }

        [JsonProperty("coverage", NullValueHandling = NullValueHandling.Ignore)]
        public string Coverage { get; set; }

        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public string Level { get; set; }

        [JsonProperty("boundary", NullValueHandling = NullValueHandling.Ignore)]
        public string Boundary { get; set; }

        [JsonProperty("providerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderName { get; set; }

        [JsonProperty("completedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CompletedAt { get; set; }

        [JsonProperty("standardName", NullValueHandling = NullValueHandling.Ignore)]
        public string StandardName { get; set; }

        [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
        public string Comments { get; set; }
    }

    public partial class ProductOrSectorSpecificRule
    {
        [JsonProperty("operator", NullValueHandling = NullValueHandling.Ignore)]
        public string Operator { get; set; }

        [JsonProperty("ruleNames", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RuleNames { get; set; }
    }

    public partial class SecondaryEmissionFactorSource
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Version { get; set; }
    }

    public partial class ProductFootprintCatcher
    {
        public static ProductFootprintCatcher FromJson(string json) => JsonConvert.DeserializeObject<ProductFootprintCatcher>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ProductFootprintCatcher self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
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

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(List<long>);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>)untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
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
