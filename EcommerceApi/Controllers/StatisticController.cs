using data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StatisticController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("DashbordStatistic")]
        public IActionResult HomeStatistics()
        {
            var statistic = new DashboardStatistcDTO()
            {
                UsersCount = context.Users.Count(),
                ProductsCount = context.Products.Count(),
                CategorysCount = context.Categories.Count(),
            };
            return Ok(statistic);
        }
    }
}
