using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }  
        public string? ProfileImageUrl { get; set; }
        public ICollection<CardItem> cardItems { get; set; }
        public ICollection<FavoriteItem> favoriteItems { get; set; }
    }
}
