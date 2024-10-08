﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }  // الكمية

        public string UserId { get; set; } // المستخدم الذي يملك المفضلة
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
