namespace BrewCode.BrewGuide;

public interface IGuidelines<TStyleCat, TStyle> 
    where TStyleCat : IStyleCategory<TStyle> 
    where TStyle : IStyle
{
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, TStyleCat> StyleCategories { get; }

    public TStyle? GetStyleById(string id);
}

public interface IStyleCategory<TStyle>
    where TStyle: IStyle
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, TStyle> Styles { get; }
}

public interface IStyle
{
    public string Id { get; } 
    public string Name { get; }
}
