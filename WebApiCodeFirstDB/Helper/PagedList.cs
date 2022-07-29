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

        //20 items all pages TotalCount
        //pagesize: 5 items PageSize
        //Total pages: 20/5 = 4


        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source,  //danh sách iqueryable
            int pageNumber, //hiện tại đang ở page nào = 3
            int pageSize) //có bao nhiêu page
        {
            var count = source.Count(); //tổng số phần tử (items) trên toàn bộ pages
            var totalPages = (int)Math.Ceiling(count * 1.0 / pageSize); //20/5 = 4 pages
            //Có bao nhiêu page

            if (pageNumber > totalPages) pageNumber = totalPages; 

            var items = await source.Skip((pageNumber - 1) * pageSize) // 20 items => skip ((3-1) * 5 = 10
                //bỏ qua 10 items đầu tiên 
                .Take(pageSize) //lấy 5 items tiếp theo
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
