using BlogWebApi.ViewModel.Common;

namespace BlogWebApi.ViewModel
{
    public class CategoryRequestModel: QueryStringParameters
    {
        public string? SearchText { get; set; }
    }
}
