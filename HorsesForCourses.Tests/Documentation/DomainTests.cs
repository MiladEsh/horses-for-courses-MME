using QuickAcid.Proceedings.ClerksOffice;

namespace HorsesForCourses.Tests.Documentation;

public abstract class DomainTests<TEntity>
{
    protected TEntity Entity { get; }
    protected abstract TEntity CreateCannonicalEntity();

    public DomainTests()
    {
        Entity = CreateCannonicalEntity();
    }
}