namespace Eggy.Domain.Models;

public record Project(string Id, string Name, string Type, string Category)
{
    public static readonly Project Empty = new(string.Empty, string.Empty, string.Empty, string.Empty);

    public void Deconstruct(out string id, out string name, out string type, out string category)
    {
        id = Id;
        name = Name;
        type = Type;
        category = Category;
    }

    public sealed override string ToString() => string.Join(" ", Type, Category, Name, Id);
}