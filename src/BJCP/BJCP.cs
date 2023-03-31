using System.Text.Json;
using System.Reflection;

namespace BrewCode.BrewGuide.BJCP;

public static class Guidelines
{
    public static readonly BJCPGuidelines BJCP2021Guidelines = LoadFromJSON();

    private static BJCPGuidelines LoadFromJSON()
    {
        var guildlines = new BJCPGuidelines
        {
            Name = "BJCP 2021 Guidelines",
            Description = "The 2021 BJCP Style BJCPGuidelines are a minor revision to the 2015 edition, itself a major update of the 2008 edition. The goals of the 2015 edition were to better address world beer styles as found in their local markets, keep pace with emerging craft beer market trends, describe historical beers now finding a following, better describe the sensory characteristics of modern brewing ingredients, take advantage of new research and references, and help competition organizers better manage the complexity of their events. These goals have not changed in the 2021 edition."
        };

        using (JsonDocument doc = JsonDocument.Parse(Assembly.GetAssembly(typeof(Guidelines)).GetManifestResourceStream($"{Assembly.GetAssembly(typeof(Guidelines)).GetName().Name}.2021styles.json")))
        {
            
            if(doc == null)
            {
                Console.WriteLine("Error loading BJCP Styles json file.");
                return null;
            }

            foreach( JsonElement style in doc.RootElement.EnumerateArray() ) {
                //Check if we have the category already
                var catId = style.GetProperty("categorynumber").GetString();
                IStyleCategory category;
                if( guildlines.StyleCategories.ContainsKey(catId) ) 
                { 
                    category = guildlines.StyleCategories[catId];
                } else // create category if we don't already have it
                {
                    category = new BJCPStyleCategory(catId, style.GetProperty("category").GetString());
                    guildlines.StyleCategories[catId] = category;
                }

                var bjcpStyle = new BJCPStyle(style, category);
                category.Styles.Add(bjcpStyle.Id, bjcpStyle);
            }
        }
        return guildlines;
    }
}
public class BJCPGuidelines : IGuidelines
{
    public Dictionary<string, IStyleCategory> StyleCategories { get; } = new();
    public string Name { get; init; } = string.Empty;
    public string Description {get; init; } = string.Empty;
}

public class BJCPStyleCategory : IStyleCategory
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public Dictionary<string, IStyle> Styles => new();

    public BJCPStyleCategory(string id, string name, string description = "") {
        Id = id;
        Name = name;
        Description = description;
    }
}

public record BJCPStyle : IStyle
{
    public BJCPStyle(JsonElement json, IStyleCategory category)
    {
        if (!json.TryGetProperty("number", out var id))
        {
            throw new ArgumentNullException("Id", "Unable to locate Id from Style JSON (key: \"number\")");
        }
        Id = id.GetString();
        if (!json.TryGetProperty("name", out var name))
        {
            throw new ArgumentNullException("Name", "Unable to locate Name from Style JSON (key: \"name\")");
        }
        Name = name.GetString();

        Category = category;
        OverallImpression = json.TryGetProperty("overallimpression", out var impression) ? impression.GetString() : String.Empty;
        Aroma = json.TryGetProperty("armoa", out var armoa) ? armoa.GetString() : String.Empty;
        Appearance = json.TryGetProperty("appearance", out var app) ? app.GetString() : String.Empty;
        Flavor = json.TryGetProperty("flavor", out var flavor) ? flavor.GetString() : String.Empty;
        Mouthfeel = json.TryGetProperty("mouthfeel", out var mouth) ? mouth.GetString() : String.Empty;
        Comments = json.TryGetProperty("comments", out var comm) ? comm.GetString() : String.Empty;
        History = json.TryGetProperty("history", out var hist) ? hist.GetString() : String.Empty;
        CharacteristicIngredients = json.TryGetProperty("characteristicingredients", out var ci) ? ci.GetString() : String.Empty;
        StyleComparison = json.TryGetProperty("stylecomparison", out var sc) ? sc.GetString() : String.Empty;
        CommercialExamples = json.TryGetProperty("commercialexamples", out var ce) ? ce.GetString() : String.Empty;
        Tags = json.TryGetProperty("tags",out var tags) ? tags.GetString().Split(", ").ToList() : new List<string>();
        Stats = new VitalStats()
        {
            IBULow = json.TryGetProperty("ibumin", out var ibumin) ? int.Parse(ibumin.GetString()) : 0,
            IBUHigh = json.TryGetProperty("ibumax", out var ibumax) ? int.Parse(ibumax.GetString()) : 0,
            OGLow = json.TryGetProperty("ogmin", out var ogmin) ? float.Parse(ogmin.GetString()) : 0,
            OGHigh = json.TryGetProperty("ogmax", out var ogmax) ? float.Parse(ogmax.GetString()) : 0,
            FGLow = json.TryGetProperty("fgmin", out var fgmin) ? float.Parse(fgmin.GetString()) : 0,
            FGHigh = json.TryGetProperty("fgmax", out var fgmax) ? float.Parse(fgmax.GetString()) : 0,
            ABVLow = json.TryGetProperty("abvmin", out var abvmin) ? float.Parse(abvmin.GetString()) : 0,
            ABVHigh = json.TryGetProperty("abvmax", out var abvmax) ? float.Parse(abvmax.GetString()) : 0,
            SRMLow = json.TryGetProperty("srmmin", out var srmmin) ? float.Parse(srmmin.GetString()) : 0,
            SRMHigh = json.TryGetProperty("srmmax", out var srmmax) ? float.Parse(srmmax.GetString()) : 0
        };
        
    }

    public string Id { get; init; }
    public string Name { get; init; }
    public IStyleCategory Category { get; init; }
    public string OverallImpression { get; init; }
    public string Aroma { get; init; }
    public string Appearance { get; init; }
    public string Flavor { get; init; }
    public string Mouthfeel { get; init; }
    public string Comments { get; init; }
    public string History { get; init; }
    public string CharacteristicIngredients { get; init; }
    public string StyleComparison { get; init; }
    public VitalStats Stats { get; init; }
    public string CommercialExamples { get; init; }
    public string EntryInstructions { get; init; }
    public List<string> Tags { get; init; }

}

public readonly record struct VitalStats (
    int IBULow,
    int IBUHigh,
    float SRMLow,
    float SRMHigh,
    float OGLow,
    float OGHigh,
    float FGLow,
    float FGHigh,
    float ABVLow,
    float ABVHigh
);
