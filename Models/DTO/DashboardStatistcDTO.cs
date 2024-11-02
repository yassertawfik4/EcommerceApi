using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DashboardStatistcDTO
    {
        public int Id { get; set; }
        public int UsersCount { get; set; }
        public int ProductsCount { get; set; }
        public int CategorysCount { get; set; }
       
    }
}
