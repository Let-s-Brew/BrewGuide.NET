namespace BrewGuide.NET;

public interface IGuidelines
{
    public string Name { get; }
    public string Description { get; }
    public IEnumerable<IStyleCategory> StyleCategories { get; }


}

public interface IStyleCategory
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public IEnumerable<IStyle> Styles { get; }
}

public interface IStyle
{
    public int Id { get; } 
    public string Name { get; }

    
}
