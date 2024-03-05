using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class Pcf
{      
    [JsonProperty("declaredUnit")]
    public DeclaredUnit DeclaredUnit { get; set; }

    [JsonProperty("unitaryProductAmount")]
    public string UnitaryProductAmount { get; set; }

    [JsonProperty("pCfExcludingBiogenic")]
    public string PCfExcludingBiogenic { get; set; }
    
    [JsonProperty("pCfIncludingBiogenic", NullValueHandling = NullValueHandling.Ignore)]
    public string PCfIncludingBiogenic { get; set; }

    [JsonProperty("fossilGhgEmissions")]
    public string FossilGhgEmissions { get; set; }

    [JsonProperty("fossilCarbonContent")]
    public string FossilCarbonContent { get; set; }

    [JsonProperty("biogenicCarbonContent")]
    public string BiogenicCarbonContent { get; set; }
    
    [JsonProperty("dLucGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string DLucGhgEmissions { get; set; }
    
    [JsonProperty("landManagementGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string LandManagementGhgEmissions { get; set; }

    [JsonProperty("otherBiogenicGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string OtherBiogenicGhgEmissions { get; set; }

    [JsonProperty("iLucGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string ILucGhgEmissions { get; set; }

    //TODO: this should be a decimal serialized as a json string
    [JsonProperty("biogenicCarbonWithdrawal", NullValueHandling = NullValueHandling.Ignore)]
    public string BiogenicCarbonWithdrawal { get; set; }
    
    [JsonProperty("aircraftGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string AircraftGhgEmissions { get; set; }

    [JsonProperty("characterizationFactors")]
    public CharacterizationFactor CharacterizationFactors { get; set; }

    [JsonProperty("crossSectoralStandardsUsed")]
    public List<CrossSectoralStandard> CrossSectoralStandardsUsed { get; set; }
    
    [JsonProperty("productOrSectorSpecificRules", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<ProductOrSectorSpecificRule> ProductOrSectorSpecificRules { get; set; }
    
    [JsonProperty("biogenicAccountingMethodology", NullValueHandling = NullValueHandling.Ignore)]
    public BiogenicAccountingMethodology BiogenicAccountingMethodology { get; set; }

    [JsonProperty("boundaryProcessesDescription")]
    public string BoundaryProcessesDescription { get; set; }

    [JsonProperty("referencePeriodStart")]
    public DateTimeOffset? ReferencePeriodStart { get; set; }

    [JsonProperty("referencePeriodEnd")]
    public DateTimeOffset? ReferencePeriodEnd { get; set; }

    [JsonProperty("geographyCountrySubdivision", NullValueHandling = NullValueHandling.Ignore)]
    public string GeographyCountrySubdivision { get; set; }
    
    [JsonProperty("geographyCountry", NullValueHandling = NullValueHandling.Ignore)]
    public string GeographyCountry { get; set; }

    [JsonProperty("geographyRegionOrSubregion", NullValueHandling = NullValueHandling.Ignore)]
    public RegionOrSubregion GeographyRegionOrSubregion { get; set; }

    [JsonProperty("secondaryEmissionFactorSources", NullValueHandling = NullValueHandling.Ignore)]
    public List<SecondaryEmissionFactorSource> SecondaryEmissionFactorSources { get; set; }

    [JsonProperty("exemptedEmissionsPercent")]
    public double? ExemptedEmissionsPercent { get; set; }

    [JsonProperty("exemptedEmissionsDescription")]
    public string ExemptedEmissionsDescription { get; set; }

    [JsonProperty("packagingEmissionsIncluded")]
    public bool? PackagingEmissionsIncluded { get; set; }

    [JsonProperty("packagingGhgEmissions", NullValueHandling = NullValueHandling.Ignore)]
    public string PackagingGhgEmissions { get; set; }

    [JsonProperty("allocationRulesDescription", NullValueHandling = NullValueHandling.Ignore)]
    public string AllocationRulesDescription { get; set; }

    [JsonProperty("uncertaintyAssessmentDescription", NullValueHandling = NullValueHandling.Ignore)]
    public string UncertaintyAssessmentDescription { get; set; }

    /// <summary>
    /// Optional until 2025
    /// </summary>
    [JsonProperty("primaryDataShare", NullValueHandling = NullValueHandling.Ignore)]
    public double? PrimaryDataShare { get; set; }
    
    [JsonProperty("dqi", NullValueHandling = NullValueHandling.Ignore)]
    public DataQualityIndicators Dqi { get; set; }

    [JsonProperty("assurance", NullValueHandling = NullValueHandling.Ignore)]
    public Assurance Assurance { get; set; }
}