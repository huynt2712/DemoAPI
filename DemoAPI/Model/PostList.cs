namespace DemoAPI.Model
{
    public class PostList
    {
        public static List<Post> GetListPost(string searchField)
        {
            var products = new List<Post> { new Post{Id = 1, Name = "Nvidia", Description = "Ra mắt card đồ họa RTX3090"},
            new Post{ Id=2,Name="Intel",Description="Chip core i9-12900k mạnh mẽ nhất của intel, đối thủ mạnh mẽ của AMD" },
            new Post{Id=3, Name="AMD",Description="AMD vừa hợp tác với SamSung cho ra chip có GPU xử lý mạnh mẽ nhất" } };
            var tempProducts = new List<Post>();
            if (!string.IsNullOrEmpty(searchField))
            {
                var productSearch = products.Where(p => p.Name.ToLower().Contains(searchField.ToLower())|| p.Description.ToLower().Contains(searchField.ToLower())).Select(p => p);
                foreach (var product in productSearch)
                {
                    tempProducts.Add(product);
                }
                return tempProducts;
            }
            return products;
        }
    }
}
