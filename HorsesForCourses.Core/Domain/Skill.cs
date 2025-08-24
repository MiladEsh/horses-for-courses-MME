namespace HorsesForCourses.Core.Domain;

public record Skill
{
    private string value = string.Empty;

    public string Value => value;

    private Skill() { }

    private Skill(string value)
    {
        this.value = value;
    }

    public static Skill From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Skill value cannot be empty.");
        return new Skill(value);
    }
}
