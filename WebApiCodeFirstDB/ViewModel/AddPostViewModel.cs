namespace BlogWebApi.ViewModel
{
    public class AddPostViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public string Image { get; set; }

        public int PostCategoryId { get; set; }
    }
}
