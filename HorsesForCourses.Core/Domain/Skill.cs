namespace HorsesForCourses.Core.Domain;

public record Skill
{
    public string Value { get; }

    private Skill(string value)
    {
        Value = value;
    }

    public static Skill From(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Skill value cannot be empty.");
        return new Skill(value);
    }
}