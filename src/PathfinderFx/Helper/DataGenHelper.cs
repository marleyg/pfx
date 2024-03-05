using PathfinderFx.Model;

namespace PathfinderFx.Helper;

public static class DataGenHelper
{
    
    
    //return a random decimal between 0.0 and 100.00 as a string
    internal static string GenerateRandomDecimalString()
    {
        var random = new Random();
        var randomDecimal = random.NextDouble() * 100;
        return randomDecimal.ToString("0.00");
    }
    
    internal static decimal GenerateRandomDecimal()
    {
        var random = new Random();
        return new decimal(random.NextDouble() * 100);
    }

    //generate a random 2 word name as a string
    internal static string GenerateRandomName()
    {
        var random = new Random();
        var randomName = $"{RandomWord(random)} {RandomWord(random)}";
        return randomName;
    }

    private static string RandomWord(Random random)
    {
        //generate a random word from a list of words
        var words = new List<string>
        {
            "Able",
            "Acid",
            "Acre",
            "Acts",
            "Aged",
            "Ago",
            "Aid",
            "Aim",
            "Air",
            "Ajar",
            "Alarm",
            "Alias",
            "Alibi",
            "Alien",
            "Alley",
            "Aloe",
            "Alone",
            "Amen",
            "Amend",
            "Amp",
            "Ample",
            "Amuse",
            "Angel",
            "Anger",
            "Angle",
            "Angry",
            "Animal",
            "Ankle",
            "Annoy",
            "Annual",
            "Answer",
            "Ant",
            "Ante",
            "Anti",
            "Antic",
            "Ants",
            "Anvil",
            "Any",
            "Ape",
            "Apex",
            "Aplomb",
            "Appeal",
            "Apple",
            "Apply",
            "Apron",
            "Apt",
            "Aqua",
            "Arbor",
            "Arc",
            "Arcade",
            "Arch",
            "Arched",
            "Area",
            "Arid",
            "Arm",
            "Army",
            "Aroma",
            "Array",
            "Arrow",
            "Arson",
            "Art",
            "Ash",
            "Aspen",
            "Assist",
            "Aster",
            "Ate",
            "Atom",
            "Attic",
            "Audio",
            "Audit",
            "Auger",
            "Aunt",
            "Auntie",
            "Aura",
            "Auto",
            "Autumn",
            "Ava",
            "Avenue",
            "Avert",
            "Avid",
            "Avoid",
            "Avow",
            "Await",
            "Awake",
            "Award",
            "Aware",
            "Awash",
            "Away",
            "Awe",
            "Awful",
            "Awkward",
            "Awning",
            "Axe",
            "Aye",
            "Babe",
            "Baby",
            "Back",
            "Bacon",
            "Bad",
            "Badge",
            "Bag",
            "Bagel",
            "Baggy",
            "Bail",
            "Bait",
            "Bake",
            "Baker",
            "Bald",
            "Cake",
            "Calf",
            "Call",
            "Calm",
            "Calve",
            "Camel",
            "Camp",
            "Can",
            "Cane",
            "Cant",
            "Door",
            "Dope",
            "Dose",
            "Dot",
            "Dough",
            "Dove",
            "Down",
            "Dour",
            "Dove",
            "Eagle",
            "Ear",
            "Earn",
            "Earth",
            "Ease",
            "East",
            "Easy",
            "Eat",
            "Eave",
            "Ebb",
            "Echo",
            "Edge",
            "Eel",
            "Egg",
            "Ego",
            "Fan",
            "Fancy",
            "Fang",
            "Food",
            "Fool",
            "Foot",
            "For",
            "Ford",
            "Form",
            "Fort",
            "Fork",
            "Foul",
            "Four",
            "Fox",
            "Frog",
            "Gag",
            "Gain",
            "Gait",
            "Gala",
            "Gale",
            "Gall",
            "Game",
            "Gang",
            "Gap",
            "Gear",
            "Gee",
            "Gel",
            "Gun",
            "Gut",
            "Gym",
            "Habit",
            "Hack",
            "Hail",
            "Hair",
            "Half",
            "Heal",
            "Heap",
            "Hear",
            "Hope",
            "Hose",
            "Host",
            "Hot",
            "Hour",
            "Halo",
            "Halt",
            "Hen",
            "Herd",
            "Hero",
            "Golf",
            "Gone",
            "Good",
            "Goof",
            "Gore",
            "Hose",
            "Host",
            "Hot",
            "Hour",
            "Idea",
            "Idle",
            "Idol",
            "Igloo",
            "Icy",
            "Juice",
            "Jake",
            "Jury",
            "Just",
            "Kale",
            "Keg",
            "Kept",
            "Kite",
            "Koala",
            "Knot",
            "Knee",
            "Lace",
            "Lack",
            "Lad",
            "Leaf",
            "Lean",
            "Leap",
            "Lear",
            "Make",
            "Male",
            "Mall",
            "Malt",
            "Mind",
            "Monk",
            "Mood",
            "Moon",
            "Moose",
            "Mop",
            "Moral",
            "Name",
            "Nail",
            "Nap",
            "Nape",
            "Napkin",
            "Need",
            "Nest",
            "Net",
            "Old",
            "Olive",
            "Omen",
            "One",
            "Open",
            "Oath",
            "Oats",
            "Ogle",
            "Oily",
            "Pace",
            "Pack",
            "Pad",
            "Pail",
            "Pain",
            "Pair",
            "Palm",
            "Pea",
            "Peak",
            "Pear",
            "Peek",
            "Peel",
            "Peg",
            "Quiz",
            "Quota",
            "Quote",
            "Quack",
            "Quail",
            "Rain",
            "Rake",
            "Rat",
            "Real",
            "Reap",
            "Rear",
            "Reed",
            "Reef",
            "Rust",
            "Sack",
            "Sail",
            "Sake",
            "Sale",
            "Seal",
            "Seat",
            "Seek",
            "Side",
            "Silo",
            "Silt",
            "Soak",
            "Soap",
            "Sole",
            "Soot",
            "Tack",
            "Taco",
            "Tact",
            "Tail",
            "Take",
            "Tale",
            "Tame",
            "Team",
            "Tear",
            "Tee",
            "Toad",
            "Toll",
            "Tomb",
            "Unit",
            "Urn",
            "Use",
            "Ugly",
            "Vain",
            "Vale",
            "Vamp",
            "Vane",
            "Veal",
            "Veil",
            "Vest",
            "Vote",
            "Wade",
            "Wack",
            "Wade",
            "Wage",
            "Wail",
            "Weal",
            "Wean",
            "Wear",
            "Weed",
            "Wind",
            "Wing",
            "Yak",
            "Yam",
            "Yank",
            "Yard",
            "Year",
            "Yell",
            "Yelp",
            "Yoga",
            "Yolk",
            "Yule",
            "Zack",
            "Zany",
            "Zap",
            "Zebra",
            "Zero",
            "Zest",
            "Zig",
            "Zinc",
            "Zip",
            "Zone",
            "Zoo",
        };
        return words[random.Next(words.Count)];
    }

    public static DeclaredUnit GetRandomUnit()
    {
        //select a random unit from the enum Unit and return it as a string
        var values = Enum.GetValues(typeof(DeclaredUnit));
        var random = new Random();
        var randomUnit = (DeclaredUnit)(values.GetValue(random.Next(values.Length)) ?? DeclaredUnit.Kilogram);
        return randomUnit;
    }

    public static string GenerateRandomCompanyId()
    {
        //create a random certificate id string
        var random = new Random();
        var randomCertificateId = $"{random.Next(10000000, 99999999)}";
        return randomCertificateId;
    }

    //return a random company name as a string
    public static string GenerateRandomCompanyName()
    {
        var random = new Random();
        var randomCompanyName = $"{RandomWord(random)}" + " " + GetRandomCompanyPostfix();
        return randomCompanyName;
    }

    private static string GetRandomCompanyPostfix()
    {
        var postFix = new List<string>
        {
            "Inc",
            "Corp",
            "Ltd",
            "LLC",
            "GmbH",
            "AG",
            "S.A.",
            "S.p.A.",
            "S.L.",
            "S.L.U.",
            "S.A.S."
        };

        var random = new Random();
        return postFix[random.Next(postFix.Count)];
    }

    public static string GenerateRandomProductId()
    {
        //create a random product id string that is 16 characters long with a random 2 letter prefix and 10 random numbers separated by hyphens
        var random = new Random();  
        var randomProductId = $"{RandomWord(random)[..2]}-{random.Next(1000000000, unchecked((int)9999999999))}";
        return randomProductId;

    }
    
    public static List<CrossSectoralStandard> GetRandomListOfCrossSectoralStandards()
    {
        //select a random number of CrossSectoralStandards and return them as a list
        var values = Enum.GetValues(typeof(CrossSectoralStandard));
        var random = new Random();
        var randomCount = random.Next(1, values.Length);
        var randomStandards = new List<CrossSectoralStandard>();
        for (var i = 0; i < randomCount; i++)
        {
            var randomStandard = (CrossSectoralStandard)(values.GetValue(random.Next(values.Length)) ?? CrossSectoralStandard.GhgProtocolProductStandard);
            randomStandards.Add(randomStandard);
        }
        return randomStandards;
    }

    public static T GetRandomEnumValue<T>()
    {
        //select a random value from the enum T and return it as a string
        var values = Enum.GetValues(typeof(T));
        var random = new Random();
        var randomValue = (T)(values.GetValue(random.Next(values.Length)) ?? default(T));
        return randomValue;
    }

    public static IEnumerable<ProductOrSectorSpecificRule> GetRandomProductOrSectorSpecificRules()
    {
        
        var random = new Random();
        var randomCount = random.Next(1, 5);
        var randomRules = new List<ProductOrSectorSpecificRule>();
        for (var i = 0; i < randomCount; i++)
        {
            var randomRule = new ProductOrSectorSpecificRule
            {
                Operator = GetRandomEnumValue<ProductOrSectorSpecificRuleOperator>(),
                RuleNames = [GetRandomRuleName()]
            };
            randomRules.Add(randomRule);
        }
        return randomRules;
    }

    private static string GetRandomRuleName()
    {
        //return something like this "EPD International PCR 2019:01 v2.0"
        var random = new Random();
        var randomRuleName = $"{RandomWord(random)} {RandomWord(random)} {random.Next(2000, 2023)}:{random.Next(1, 13)} v{random.Next(1, 10)}.{random.Next(1, 10)}";
        return randomRuleName;
    }

    public static List<SecondaryEmissionFactorSource> GetRandomListOfSecondaryEmissionFactorSources()
    {
        var random = new Random();
        var randomCount = random.Next(1, 5);
        var randomSecondaries = new List<SecondaryEmissionFactorSource>();
        for (var i = 0; i < randomCount; i++)
        {
            var randomSecond = new SecondaryEmissionFactorSource
            {
                Name = GenerateRandomName(),
                Version = random.Next(1, 10).ToString()
            };
            randomSecondaries.Add(randomSecond);
        }
        return randomSecondaries;
    }

    public static bool GetRandomBool()
    {
        var random = new Random();
        return random.Next(0, 2) == 1;
    }

    public static Assurance GenerateRandomAssurance()
    {
        //create a random Assurance object with random values

        var assurance = new Assurance
        {
            HasAssurance = true,
            Coverage = "product line",
            Level = "reasonable",
            Boundary = "Cradle-to-Gate",
            ProviderName = GenerateRandomCompanyName(),
            CompletedAt = new DateTimeOffset(2022, 12, 15, 0, 0, 0, TimeSpan.Zero),
            StandardName = GenerateRandomStandardName(),
            Comments = "This is a sample assurance comment"
        };
        return assurance;

    }

    private static string GenerateRandomStandardName()
    {
        //return something like this "ISO 14025"
        var random = new Random();
        var randomStandardName = $"{RandomWord(random)} {random.Next(1000, 9999)}";
        return randomStandardName;
    }

    public static List<Extension> GetRandomListOfExtensions()
    {
        //create a random list of extensions
        var random = new Random();
        var randomCount = random.Next(1, 2);
        var randomExtensions = new List<Extension>();
        for (var i = 0; i < randomCount; i++)
        {
            var randomExtension = new Extension
            {
                SpecVersion = GenerateRandomVersion()
            };
            
            if (GetRandomBool())
            {
                randomExtension.Documentation = new Uri("https://www.example.com/shipment-extension");
                randomExtension.DataSchema = new Uri("https://www.schema.com/shipment-extension.json");
                randomExtension.Data = new ShipmentExtension
                {
                    ConsignmentId = GenerateRandomProductId(),
                    ShipmentId = GenerateRandomCompanyId(),
                    ShipmentType = "Pickup",
                    Weight = (long?)GenerateRandomDecimal(),
                    TransportChainElementId = GenerateRandomCompanyId()
                };
            }
            else
            {
                randomExtension.Documentation = new Uri("https://www.example.com/green-steel-extension");
                randomExtension.DataSchema = new Uri("https://www.schema.com/green-steel-extension.json");
                randomExtension.Data = new GreenSteelExtension
                {
                    HeatTreatment = GenerateRandomName(),
                    SteelProductionCountry = GetRandomEnumValue<GeographyCountry>(),
                    SteelProductionDate = DateTime.Now.Subtract(TimeSpan.FromDays(423)),
                    SteelProductionLocation = GenerateRandomName(),
                    SteelProductionProcess = GenerateRandomName(),
                    SteelType = "HSLA Green Steel"
                };
            }
            
            randomExtensions.Add(randomExtension);
        }
        return randomExtensions;
    }

    private static string GenerateRandomVersion()
    {
        //return something like this "2.1"
        var random = new Random();
        var randomVersion = $"{random.Next(1, 10)}.{random.Next(1, 10)}";
        return randomVersion;
    }
}