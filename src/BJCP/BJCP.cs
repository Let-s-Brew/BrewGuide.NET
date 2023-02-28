using BrewGuide.NET;
using System.Text.Json;
using System.IO;
using System.Reflection;

namespace BrewGuide.NET.BJCP;

public static class BJCP
{
    public static readonly BJCP21Guidelines BJCP2021Guidelines = LoadFromJSON();

    private static BJCP21Guidelines LoadFromJSON()
    {
        var guildlines = new BJCP21Guidelines();
    using (JsonDocument doc = JsonDocument.Parse(Assembly.GetExecutingAssembly().GetManifestResourceStream("2021styles.json")))
    {

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
                var bjcpStyle = new BJCPStyle() { 
                    Id = style.GetProperty("number").GetString(),
                    Name = style.GetProperty("name").GetString(),
                    OverallImpression = style.GetProperty("overallimpression").GetString(),
                    Aroma = style.GetProperty("aroma").GetString(),
                    Appearance = style.GetProperty("appearance").GetString(),
                    Flavor = style.GetProperty("flavor").GetString(),
                    Mouthfeel = style.GetProperty("mouthfeel").GetString(),
                    Comments = style.GetProperty("comments").GetString(),
                    History = style.GetProperty("history").GetString(),
                    CharacteristicIngredients = style.GetProperty("characteristicingredients").GetString(),
                    StyleComparison = style.GetProperty("stylecomparison").GetString(),
                    CommercialExamples = style.GetProperty("commercialexamples").GetString(),
                    Tags = style.GetProperty("").GetString().Split(", ").ToList(),
                    Stats = new VitalStats()
                    {
                        IBUHigh = int.Parse(style.GetProperty("ibumax").GetString()),
                        IBULow = int.Parse(style.GetProperty("ibumin").GetString()),
                        OGHigh = float.Parse(style.GetProperty("ogmax").GetString()),
                        OGLow = float.Parse(style.GetProperty("ogmin").GetString()),
                        FGHigh = float.Parse(style.GetProperty("fgmax").GetString()),
                        FGLow = float.Parse(style.GetProperty("fgmin").GetString()),
                        ABVHigh = float.Parse(style.GetProperty("abvmax").GetString()),
                        ABVLow = float.Parse(style.GetProperty("abvmin").GetString()),
                        SRMHigh = int.Parse(style.GetProperty("srmmax").GetString()),
                        SRMLow = int.Parse(style.GetProperty("srmmin").GetString())
                    }
                };
                category.Styles.Add(bjcpStyle.Id, bjcpStyle);
            }
    }
        return guildlines;
    }
}
public class BJCP21Guidelines : IGuidelines
{
    public string Name => "BJCP 2021 Style Guidelines";
    public string Description => "The 2021 BJCP Style Guidelines are a minor revision to the 2015 edition, itself a major update of the 2008 edition. The goals of the 2015 edition were to better address world beer styles as found in their local markets, keep pace with emerging craft beer market trends, describe historical beers now finding a following, better describe the sensory characteristics of modern brewing ingredients, take advantage of new research and references, and help competition organizers better manage the complexity of their events. These goals have not changed in the 2021 edition.";

    public Dictionary<string, IStyleCategory> StyleCategories { get; } = new();
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
    public List<string> Tags { get; init; }

}

public readonly record struct VitalStats (
    int IBULow,
    int IBUHigh,
    int SRMLow,
    int SRMHigh,
    float OGLow,
    float OGHigh,
    float FGLow,
    float FGHigh,
    float ABVLow,
    float ABVHigh
);
