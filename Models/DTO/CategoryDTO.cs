using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; } // URL to access the image
    }
}
