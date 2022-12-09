// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Microsoft.PathfinderFx;
//
//    var productFootprint = ProductFootprint.FromJson(jsonString);

namespace Microsoft.PathfinderFx
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ProductFootprint
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("specVersion")]
        public string SpecVersion { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyIds")]
        public string[] CompanyIds { get; set; }

        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("productIds")]
        public string[] ProductIds { get; set; }

        [JsonProperty("productCategoryCpc")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ProductCategoryCpc { get; set; }

        [JsonProperty("productNameCompany")]
        public string ProductNameCompany { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("pcf")]
        public Pcf Pcf { get; set; }

        [JsonProperty("extensions")]
        public Extension[] Extensions { get; set; }
    }

    public partial class Extension
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("myKey")]
        public string MyKey { get; set; }
    }

    public partial class Pcf
    {
        [JsonProperty("declaredUnit")]
        public string DeclaredUnit { get; set; }

        [JsonProperty("unitaryProductAmount")]
        public string UnitaryProductAmount { get; set; }

        [JsonProperty("pCfExcludingBiogenic")]
        public string PCfExcludingBiogenic { get; set; }

        [JsonProperty("pCfIncludingBiogenic")]
        public string PCfIncludingBiogenic { get; set; }

        [JsonProperty("fossilGhgEmissions")]
        public string FossilGhgEmissions { get; set; }

        [JsonProperty("fossilCarbonContent")]
        public string FossilCarbonContent { get; set; }

        [JsonProperty("biogenicEmissions")]
        public BiogenicEmissions BiogenicEmissions { get; set; }

        [JsonProperty("biogenicCarbonContent")]
        public string BiogenicCarbonContent { get; set; }

        [JsonProperty("biogenicCarbonWithdrawal")]
        public string BiogenicCarbonWithdrawal { get; set; }

        [JsonProperty("characterizationFactors")]
        public string CharacterizationFactors { get; set; }

        [JsonProperty("biogenicAccountingMethodology")]
        public string BiogenicAccountingMethodology { get; set; }

        [JsonProperty("reportingPeriodStart")]
        public DateTimeOffset ReportingPeriodStart { get; set; }

        [JsonProperty("reportingPeriodEnd")]
        public DateTimeOffset ReportingPeriodEnd { get; set; }

        [JsonProperty("geographyCountrySubdivision")]
        public string GeographyCountrySubdivision { get; set; }

        [JsonProperty("geography_country")]
        public string GeographyCountry { get; set; }

        [JsonProperty("geographyRegionOrSubregion")]
        public string GeographyRegionOrSubregion { get; set; }

        [JsonProperty("primaryDataShare")]
        public double PrimaryDataShare { get; set; }

        [JsonProperty("emissionFactorSources")]
        public EmissionFactorSource[] EmissionFactorSources { get; set; }

        [JsonProperty("boundaryProcessesDescription")]
        public string BoundaryProcessesDescription { get; set; }

        [JsonProperty("exemptedEmissionsPercent")]
        public long ExemptedEmissionsPercent { get; set; }

        [JsonProperty("exemptedEmissionsDescription")]
        public string ExemptedEmissionsDescription { get; set; }

        [JsonProperty("packagingEmissionsIncluded")]
        public bool PackagingEmissionsIncluded { get; set; }

        [JsonProperty("packagingGhgEmissions")]
        public string PackagingGhgEmissions { get; set; }

        [JsonProperty("crossSectoralStandardsUsed")]
        public string[] CrossSectoralStandardsUsed { get; set; }

        [JsonProperty("productOrSectorSpecificRules")]
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
        [JsonProperty("coverage")]
        public string Coverage { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("boundary")]
        public string Boundary { get; set; }

        [JsonProperty("providerName")]
        public string ProviderName { get; set; }

        [JsonProperty("completedAt")]
        public DateTimeOffset CompletedAt { get; set; }

        [JsonProperty("standard")]
        public string Standard { get; set; }

        [JsonProperty("statementOrSignature")]
        public StatementOrSignature StatementOrSignature { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }

    public partial class StatementOrSignature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
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
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class ProductOrSectorSpecificRule
    {
        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("ruleNames")]
        public string[] RuleNames { get; set; }
    }

    public partial class ProductFootprint
    {
        public static ProductFootprint FromJson(string json) => JsonConvert.DeserializeObject<ProductFootprint>(json, Microsoft.PathfinderFx.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ProductFootprint self) => JsonConvert.SerializeObject(self, Microsoft.PathfinderFx.Converter.Settings);
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