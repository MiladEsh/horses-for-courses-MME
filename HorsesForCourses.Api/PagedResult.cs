namespace HorsesForCourses.Api
{
    public sealed record PagedResult<T>(
        IReadOnlyList<T> Items,
        int TotalCount,
        int PageNumber,
        int PageSize)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Total number of pages: Ex: 11/5 = Math.Ceiling(2.2) => 3
        public bool HasPrevious => PageNumber > 1; // If Current page > 1 return true, it's means you can get previous page objects
        public bool HasNext => PageNumber < TotalPages; // If Current page < Total pages return true, it's means you can get next page objects
    }
}