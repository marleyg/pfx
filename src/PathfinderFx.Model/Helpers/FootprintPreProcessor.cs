using System.Diagnostics.CodeAnalysis;

namespace PathfinderFx.Model.Helpers;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
public static class FootprintPreProcessor
{
    //create a method that takes a ProductFootprintCatcher and converts it to a ProductFootprint that is returned
    public static ProductFootprint ConvertToProductFootprint(ProductFootprintCatcher catcher)
    {
        var footprint = new ProductFootprint
        {
            Id = catcher.Id,
            CompanyName = catcher.CompanyName,
            ProductNameCompany = catcher.ProductNameCompany,
            Comment = catcher.Comment,
            ProductCategoryCpc = catcher.ProductCategoryCpc,
            SpecVersion = catcher.SpecVersion,
            Version = catcher.Version,
            Created = catcher.Created,
            ProductIds = catcher.ProductIds,
            ProductDescription = catcher.ProductDescription
        };
        
        //if the catcher.CompanyIds is not null or accessing it doesn't throw, convert it to a list of strings and assign it to the footprint.CompanyIds
        if(catcher.CompanyIds != null)
            footprint.CompanyIds = ConvertCatcherCompanyIds(catcher.CompanyIds);


        //read the catcher.Status string and find a match in the Status enum
        if (EnumHelper.TryParseStatusEnum(catcher.Status, out var status))
            footprint.Status = status;

        if(catcher.PrecedingPfIds != null)
            footprint.PrecedingPfIds = catcher.PrecedingPfIds;
       
        footprint.Updated = catcher.Updated;
        footprint.StatusComment = catcher.StatusComment;

        if (catcher.ValidityPeriodStart != null)
        {
            footprint.ValidityPeriodStart = catcher.ValidityPeriodStart;
            footprint.ValidityPeriodEnd = catcher.ValidityPeriodEnd;
        }
        
        footprint.Pcf = ConvertCatcherPcfToFootprintPcf(catcher.Pcf);
        if(catcher.Extensions != null)
            footprint.Extensions = ConvertCatcherExtensionToExtensions(catcher.Extensions);

        return footprint;
    }

    private static List<string> ConvertCatcherCompanyIds(IEnumerable<string> catcherCompanyIds)
    {
        return catcherCompanyIds.ToList();
    }

    private static List<Extension> ConvertCatcherExtensionToExtensions(List<ExtensionCatcher> catcherExtensions)
    {
        var extensions = new List<Extension>();
        foreach (var extension in catcherExtensions)
        {
            var newExtension = new Extension
            {
                SpecVersion = extension.SpecVersion,
                DataSchema = extension.DataSchema,
                Documentation = extension.Documentation
            };

            try
            {
                newExtension.Data = extension.Data;
            }
            catch
            {
                newExtension.Data = extension.Data;
            }
            
            extensions.Add(newExtension);
        }

        return extensions;
    }

    private static Pcf ConvertCatcherPcfToFootprintPcf(PcfCatcher catcherPcf)
    {

        var pcf = new Pcf
        {
           AircraftGhgEmissions = catcherPcf.AircraftGhgEmissions,
           BiogenicCarbonContent = catcherPcf.BiogenicCarbonContent,
           AllocationRulesDescription = catcherPcf.AllocationRulesDescription,
           BiogenicCarbonWithdrawal = catcherPcf.BiogenicCarbonWithdrawal,
           BoundaryProcessesDescription = catcherPcf.BoundaryProcessesDescription,
           FossilCarbonContent = catcherPcf.FossilCarbonContent,
           GeographyCountrySubdivision = catcherPcf.GeographyCountrySubdivision,
           LandManagementGhgEmissions = catcherPcf.LandManagementGhgEmissions,
           PackagingGhgEmissions = catcherPcf.PackagingGhgEmissions,
           UncertaintyAssessmentDescription = catcherPcf.UncertaintyAssessmentDescription,
           UnitaryProductAmount = catcherPcf.UnitaryProductAmount
        };
        if(catcherPcf.Assurance != null)
            pcf.Assurance = ConvertCatcherAssuranceToAssurance(catcherPcf.Assurance);
        
        if(!string.IsNullOrEmpty(catcherPcf.BiogenicAccountingMethodology))
            pcf.BiogenicAccountingMethodology = EnumHelper.GetEnumFromText<BiogenicAccountingMethodology>(catcherPcf.BiogenicAccountingMethodology);
        
        if(!string.IsNullOrEmpty(catcherPcf.CharacterizationFactors))
            pcf.CharacterizationFactors = EnumHelper.GetEnumFromText<CharacterizationFactor>(catcherPcf.CharacterizationFactors);
        
        if(catcherPcf.CrossSectoralStandardsUsed != null)
            pcf.CrossSectoralStandardsUsed = ConvertCatcherCrossSectoralStandardToCrossSectoralStandard(catcherPcf.CrossSectoralStandardsUsed);
        
        if(EnumHelper.TryParseDeclaredUnit(catcherPcf.DeclaredUnit, out var unit))
            pcf.DeclaredUnit = unit;
            
        if (EnumHelper.TryParseGeographicCountry(catcherPcf.GeographyCountry, out var country))
            pcf.GeographyCountry = country;
        
        if(EnumHelper.TryParseRegionOrSubregion(catcherPcf.GeographyRegionOrSubregion, out var regionOrSubregion))
            pcf.GeographyRegionOrSubregion = regionOrSubregion;
        
        pcf.PackagingEmissionsIncluded = NullableHelper.GetNullableBool(catcherPcf.PackagingEmissionsIncluded);
        
        if(catcherPcf.PrimaryDataShare != null)
            pcf.PrimaryDataShare = catcherPcf.PrimaryDataShare;
        
        pcf.ReferencePeriodStart = catcherPcf.ReferencePeriodStart;
        pcf.ReferencePeriodEnd = catcherPcf.ReferencePeriodEnd;
        
        if(catcherPcf.SecondaryEmissionFactorSources != null)
            pcf.SecondaryEmissionFactorSources = ConvertCatcherSecondaryEmissionFactorSources(catcherPcf.SecondaryEmissionFactorSources);

        return pcf;
    }

    private static List<SecondaryEmissionFactorSource> ConvertCatcherSecondaryEmissionFactorSources(List<SecondaryEmissionFactorSourceCatcher> catcherPcfSecondaryEmissionFactorSources)
    {
        return catcherPcfSecondaryEmissionFactorSources.Select(source => 
            new SecondaryEmissionFactorSource { Name = source.Name, Version = source.Version }).ToList();
    }

    private static List<CrossSectoralStandard> ConvertCatcherCrossSectoralStandardToCrossSectoralStandard(IEnumerable<string> catcherPcfCrossSectoralStandardsUsed)
    {
        return catcherPcfCrossSectoralStandardsUsed.Select(EnumHelper.GetEnumFromText<CrossSectoralStandard>).ToList();
    }

    private static Assurance ConvertCatcherAssuranceToAssurance(AssuranceCatcher catcherPcfAssurance)
    {
        var assurance = new Assurance
        {
            Boundary = catcherPcfAssurance.Boundary,
            Coverage = catcherPcfAssurance.Coverage,
            Comments = catcherPcfAssurance.Comments,
            HasAssurance = NullableHelper.GetNullableBool(catcherPcfAssurance.AssuranceAssurance),
            Level = catcherPcfAssurance.Level,
            ProviderName = catcherPcfAssurance.ProviderName,
            StandardName = catcherPcfAssurance.StandardName,
            CompletedAt = catcherPcfAssurance.CompletedAt
        };
        return assurance;
    }
}