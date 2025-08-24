using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Documentation;

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