using PathfinderFx.Integration.Model.Entities;

namespace PathfinderFx.Integration.Model;

public class ProductFootprintEntityCollection
{
    public string? HostName { get; set; } = "";
    public Msdyn_SustainabilityProduct? Msdyn_SustainabilityProduct { get; set; }
    public Msdyn_SustainabilityProductIdentifier? Msdyn_SustainabilityProductIdentifier { get; set; }
    public Msdyn_SustainabilityProductFootprint? Msdyn_SustainabilityProductFootprint { get; set; }
    public Msdyn_SustainabilityProductCarbonFootprint? Msdyn_SustainabilityProductCarbonFootprint { get; set; }
    public Msdyn_ProductCarbonFootprintAssurance? Msdyn_ProductCarbonFootprintAssurance { get; set; }
    public List<Msdyn_ProductOrSectorSpecificRule>? Msdyn_ProductOrSectorSpecificRule { get; set; }
    //rule mappings are added during insert/updates in the DataverseClient
}