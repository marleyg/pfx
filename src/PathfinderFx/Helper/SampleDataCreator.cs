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
            Status = "Active",
            ProductIds = [DataGenHelper.GenerateRandomProductId()],
            Pcf = GetPcf()
        };
        retVal.ProductDescription = retVal.ProductNameCompany + " is a sample product";
        return retVal;
    }

    private static Pcf GetPcf()
    {
        var retVal = new Pcf
        {
            DeclaredUnit = DataGenHelper.GetRandomUnit(),
            AircraftGhgEmissions = DataGenHelper.GenerateRandomDecimalString(),
            GeographyCountry = GeographyCountry.SE.AsText(),
            BiogenicCarbonContent = DataGenHelper.GenerateRandomDecimalString(),
            OtherBiogenicGhgEmissions = DataGenHelper.GenerateRandomDecimalString(),
            FossilCarbonContent = DataGenHelper.GenerateRandomDecimalString(),
            PCfExcludingBiogenic = DataGenHelper.GenerateRandomDecimalString(),
            PCfIncludingBiogenic = DataGenHelper.GenerateRandomDecimalString(),
            GeographyRegionOrSubregion = RegionOrSubregion.Europe.AsText(),
            PackagingEmissionsIncluded = true,
            BiogenicCarbonWithdrawal = "0.0",
            BoundaryProcessesDescription = "End-of-life included",
            ReferencePeriodStart = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            ReferencePeriodEnd = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
            LandManagementGhgEmissions = DataGenHelper.GenerateRandomDecimalString(),
            CharacterizationFactors = "AR5",
            CrossSectoralStandardsUsed = ["GHG Protocol Product standard"],
            ProductOrSectorSpecificRules =
            [
                new ProductOrSectorSpecificRule
                {
                    Operator = ProductOrSectorSpecificRuleOperator.Pef,
                    RuleNames = ["EPD International PCR 2019:01 v2.0"]
                }
            ],
            SecondaryEmissionFactorSources =
            [
                new SecondaryEmissionFactorSource
                {
                    Name = "Ecoinvent",
                    Version = "3.7"

                }
            ],
            ExemptedEmissionsDescription = "None",
            ExemptedEmissionsPercent = 13,
            PrimaryDataShare = 56.12,
            Assurance = new Assurance
            {
                HasAssurance = true,
                Coverage = "product line",
                Level = "reasonable",
                Boundary = "Cradle-to-Gate",
                ProviderName = DataGenHelper.GenerateRandomCompanyName(),
                CompletedAt = new DateTimeOffset(2022, 12, 15, 0, 0, 0, TimeSpan.Zero),
                StandardName = "ISO 14025",
                Comments = "This is a sample assurance comment"
            }
        };
        return retVal;
    }

}