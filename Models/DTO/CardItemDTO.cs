using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CardItemDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } // المستخدم الذي يملك الكارت
        public int ProductId { get; set; }
        public int Quantity { get; set; }  // الكمية
    }
}
