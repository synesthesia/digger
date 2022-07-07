public class TagCollection
{
    public string[] Tags {get; set;}

    public TagCollection(string[] tags) => Tags = tags;

    public TagCollection() => Tags = new string[0];

    public override string ToString()
    {
        return string.Join(",", Tags);
    }
}
