using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Helper
{
    public class PagedList<T> //Generic
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages; 

        public List<T> Items { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count * 1.0 / pageSize);
            PageSize = pageSize;
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, 
            int pageNumber, 
            int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
