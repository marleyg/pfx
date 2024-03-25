using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class ProductOrSectorSpecificRule
{
    [JsonProperty("operator")]
    public ProductOrSectorSpecificRuleOperator Operator { get; set; }

    [JsonProperty("ruleNames")]
    public List<string> RuleNames { get; set; }
    
    [JsonProperty("otherOperatorName", NullValueHandling = NullValueHandling.Ignore)]
    public string OtherOperatorName { get; set; }
}