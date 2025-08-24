using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Domain;

[DocFile]
[DocFileHeader("Horses For Courses")]
public class CreateDoc
{
    [Fact]
    public void GenerateDoc()
    {
        Explain.This<CreateDoc>("domain-invariants.md");
    }
}