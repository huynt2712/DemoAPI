using System;
using System.Collections.Generic;

namespace EFCoreDemo.Model
{
    public partial class Post
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
