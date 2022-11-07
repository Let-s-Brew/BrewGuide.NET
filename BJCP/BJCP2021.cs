using BrewGuide.NET;
namespace BrewGuide.NET.BJCP;

internal class BJCP2021 : IGuidelines
{
    public string Name => "BJCP 2021 Style Guidelines";
    public string Description => "The 2021 BJCP Style Guidelines are a minor revision to the 2015 edition, itself a major update of the 2008 edition. The goals of the 2015 edition were to better address world beer styles as found in their local markets, keep pace with emerging craft beer market trends, describe historical beers now finding a following, better describe the sensory characteristics of modern brewing ingredients, take advantage of new research and references, and help competition organizers better manage the complexity of their events. These goals have not changed in the 2021 edition.";

    public IEnumerable<IStyleCategory> StyleCategories { get; } = new List<IStyleCategory>();
}
