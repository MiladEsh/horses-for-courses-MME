using QuickPulse.Explains;

namespace HorsesForCourses.Tests.Domain;

[DocFile]
public class DomainInvariants
{
    [Fact]
    public void GenerateDoc()
    {
        Explain.This<DomainInvariants>("domain-invariants.md");
    }
}