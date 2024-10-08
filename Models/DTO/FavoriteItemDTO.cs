using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FavoriteItemDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } // المستخدم الذي يملك المفضلة
        public int ProductId { get; set; }
        public int Quantity { get; set; }  // الكمية
    }
}
