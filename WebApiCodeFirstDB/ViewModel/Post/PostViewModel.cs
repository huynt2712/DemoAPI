namespace BlogWebApi.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }
        
        public string Slug { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int PostCategoryId { get; set; }
    }
}
