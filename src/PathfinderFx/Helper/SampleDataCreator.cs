using PathfinderFx.Model;

namespace PathfinderFx.Helper;

public static class SampleDataCreator
{
    internal static ProductFootprint GetProductFootprint(string organizationName,
        string organizationId)
    {
        var retVal = new ProductFootprint
        {
            Id = Guid.NewGuid(),
            CompanyName = organizationName,
            CompanyIds = [organizationId],
            ProductNameCompany = DataGenHelper.GenerateRandomName(),
            Comment = "This is a sample Product Footprint",
            ProductCategoryCpc = 3342,
            SpecVersion = "2.1",
            Version = 1,
            Created = DateTime.UtcNow,
            Status = DataGenHelper.GetRandomEnumValue<Status>(),
            ProductIds = [DataGenHelper.GenerateRandomProductId()],
            Pcf = GetPcf(DataGenHelper.GetRandomBool())
        };
        retVal.ProductDescription = retVal.ProductNameCompany + " is a sample product";

        if (DataGenHelper.GetRandomBool())
        {
            retVal.PrecedingPfIds = [Guid.NewGuid().ToString()];
            retVal.Updated = DateTime.UtcNow.Subtract(TimeSpan.FromDays(90));
            retVal.StatusComment = "This is a sample status comment";
        }

        if (DataGenHelper.GetRandomBool())
        {
            retVal.ValidityPeriodStart = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365));
            retVal.ValidityPeriodEnd = DateTime.UtcNow.Add(TimeSpan.FromDays(365));
        }
        
        if(DataGenHelper.GetRandomBool())
            retVal.Extensions = DataGenHelper.GetRandomListOfExtensions();

        return retVal;
    }

    private static Pcf GetPcf(bool optionals)
    {
        var pcf = new Pcf
        {
            DeclaredUnit = DataGenHelper.GetRandomUnit(),
            UnitaryProductAmount = DataGenHelper.GenerateRandomDecimalString(),
            GeographyCountry = GeographyCountry.SE.AsText(),
            BiogenicCarbonContent = DataGenHelper.GenerateRandomDecimalString(),
            FossilGhgEmissions = DataGenHelper.GenerateRandomDecimalString(),
            FossilCarbonContent = DataGenHelper.GenerateRandomDecimalString(),
            PCfExcludingBiogenic = DataGenHelper.GenerateRandomDecimalString(),
            GeographyRegionOrSubregion = DataGenHelper.GetRandomEnumValue<RegionOrSubregion>(),
            BiogenicCarbonWithdrawal = "0.0",
            BoundaryProcessesDescription = "End-of-life included",
            ReferencePeriodStart = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            ReferencePeriodEnd = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
            LandManagementGhgEmissions = DataGenHelper.GenerateRandomDecimalString(),
            CharacterizationFactors = DataGenHelper.GetRandomEnumValue<CharacterizationFactor>(),
            CrossSectoralStandardsUsed = DataGenHelper.GetRandomListOfCrossSectoralStandards(),
            ExemptedEmissionsDescription = "None",
            ExemptedEmissionsPercent = 13,
            PrimaryDataShare = 56.12,
        };

        if (!optionals) return pcf;
        pcf.BiogenicAccountingMethodology = DataGenHelper.GetRandomEnumValue<BiogenicAccountingMethodology>();

        if (DataGenHelper.GetRandomBool())
        {
            pcf.ProductOrSectorSpecificRules = DataGenHelper.GetRandomProductOrSectorSpecificRules();
        }
            
        pcf.AircraftGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.DLucGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.LandManagementGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.OtherBiogenicGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.PCfIncludingBiogenic = DataGenHelper.GenerateRandomDecimalString();
        pcf.BiogenicCarbonWithdrawal = DataGenHelper.GenerateRandomDecimalString();
            
        if(DataGenHelper.GetRandomBool())
            pcf.SecondaryEmissionFactorSources = DataGenHelper.GetRandomListOfSecondaryEmissionFactorSources();
            
        pcf.PackagingEmissionsIncluded = DataGenHelper.GetRandomBool();
        pcf.AllocationRulesDescription = "This is a sample allocation rules description";

        pcf.PrimaryDataShare = (double?)DataGenHelper.GenerateRandomDecimal();

        if (!DataGenHelper.GetRandomBool()) return pcf;
        pcf.Assurance = DataGenHelper.GenerateRandomAssurance();
        pcf.UncertaintyAssessmentDescription = "This is a sample uncertainty assessment description";
        return pcf;
    }

}