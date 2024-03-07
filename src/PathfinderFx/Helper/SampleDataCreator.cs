using PathfinderFx.Model;

namespace PathfinderFx.Helper;

public static class SampleDataCreator
{
    internal static ProductFootprint GetProductFootprint(string organizationName,
        string organizationId, bool getAllOptionalFields = false)
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
            Pcf = GetPcf(DataGenHelper.GetRandomBool(), getAllOptionalFields)
        };
        retVal.ProductDescription = retVal.ProductNameCompany + " is a sample product";

        //get optional fields if GetRandomBool() returns true or getAllOptionalFields is true
        if (DataGenHelper.GetRandomBool() || getAllOptionalFields)
        {
            retVal.PrecedingPfIds = [Guid.NewGuid().ToString()];
            retVal.Updated = DateTime.UtcNow.Subtract(TimeSpan.FromDays(90));
            retVal.StatusComment = getAllOptionalFields ? "This sample has all optional fields" : "This is a sample status comment";
        }


        if (DataGenHelper.GetRandomBool() || getAllOptionalFields)
        {
            retVal.ValidityPeriodStart = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365));
            retVal.ValidityPeriodEnd = DateTime.UtcNow.Add(TimeSpan.FromDays(365));
        }
        
        if(DataGenHelper.GetRandomBool() || getAllOptionalFields)
            retVal.Extensions = DataGenHelper.GetRandomListOfExtensions();

        return retVal;
    }

    private static Pcf GetPcf(bool optionals, bool getAllOptionalFields = false)
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
            PackagingEmissionsIncluded = DataGenHelper.GetRandomBool(),
            ExemptedEmissionsDescription = "None",
            ExemptedEmissionsPercent = 13,
            PrimaryDataShare = 56.12,
        };

        //if optionals is false and getAllOptionalFields is false, return pcf
        if (!optionals && !getAllOptionalFields) return pcf;
        pcf.BiogenicAccountingMethodology = DataGenHelper.GetRandomEnumValue<BiogenicAccountingMethodology>();

        if (DataGenHelper.GetRandomBool() || getAllOptionalFields)
        {
            pcf.ProductOrSectorSpecificRules = DataGenHelper.GetRandomProductOrSectorSpecificRules();
        }
        
        pcf.ILucGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.AircraftGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.DLucGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.LandManagementGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.OtherBiogenicGhgEmissions = DataGenHelper.GenerateRandomDecimalString();
        pcf.PCfIncludingBiogenic = DataGenHelper.GenerateRandomDecimalString();
        pcf.BiogenicCarbonWithdrawal = DataGenHelper.GenerateRandomDecimalString();
        pcf.AllocationRulesDescription = "This is a sample allocation rules description";
        pcf.GeographyCountrySubdivision = "Sample country subdivision";
        pcf.PrimaryDataShare = (double?)DataGenHelper.GenerateRandomDecimal();
            
        if(DataGenHelper.GetRandomBool() || getAllOptionalFields)
            pcf.SecondaryEmissionFactorSources = DataGenHelper.GetRandomListOfSecondaryEmissionFactorSources();
        
        if (!DataGenHelper.GetRandomBool() && !getAllOptionalFields) return pcf;
        pcf.Assurance = DataGenHelper.GenerateRandomAssurance();
        pcf.UncertaintyAssessmentDescription = "This is a sample uncertainty assessment description";
        
        return pcf;
    }

}