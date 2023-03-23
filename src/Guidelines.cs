namespace BrewGuide;

public interface IGuidelines
{
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, IStyleCategory> StyleCategories { get; }
}

public interface IStyleCategory
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, IStyle> Styles { get; }
}

public interface IStyle
{
    public string Id { get; } 
    public string Name { get; }
}
