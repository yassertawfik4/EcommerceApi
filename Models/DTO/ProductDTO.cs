using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }

        public string? Image { get; set; }
    }
}
