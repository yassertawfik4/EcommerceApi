using data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardItemController : ControllerBase
    {
        private readonly ICardItemRepository cardItemRepository;

        public CardItemController(ICardItemRepository cardItemRepository)
        {
            this.cardItemRepository = cardItemRepository;
        }
        //display
        [HttpGet]
        public IActionResult AllCardItem()
        {
            var res = cardItemRepository.GetAll();

            List<CardItemDTO> cardItemList = new List<CardItemDTO>();
            foreach (var item in res)
            {
                cardItemList.Add(new CardItemDTO
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserId = item.UserId,
                });
            }
            if (res == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(cardItemList);
            }
        }
        [HttpPost]
        public IActionResult Create(CardItemDTO cardItemDTO)
        {
            if (ModelState.IsValid)
            {
                CardItem cardItem = new CardItem()
                {
                    Id = cardItemDTO.Id,
                    ProductId = cardItemDTO.ProductId,
                    Quantity = cardItemDTO.Quantity,
                    UserId = cardItemDTO.UserId,

                };
                cardItemRepository.Create(cardItem);
                cardItemRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult EditCard(int id,CardItemDTO cardItemDTO)
        {
            if (ModelState.IsValid)
            {
                var res=cardItemRepository.GetOne(t=>id == t.Id);
                
                res.Id = cardItemDTO.Id;
                res.ProductId = cardItemDTO.ProductId;
                res.Quantity = cardItemDTO.Quantity;
                res.UserId = cardItemDTO.UserId;

                cardItemRepository.Update(res);
                cardItemRepository.Save();
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCardItem(int id)
        {
            var res=cardItemRepository.GetOne(t=>id == t.Id);
            cardItemRepository.Delete(res);
            cardItemRepository.Save();
            return Ok();
        }
        [HttpDelete("DeleteAllCardItems")]
        public IActionResult DeleteAllCardItems()
        {
            var cardItems = cardItemRepository.GetAll();

            if (cardItems == null || !cardItems.Any())
            {
                return NotFound("No card items found.");
            }

            foreach (var item in cardItems)
            {
                cardItemRepository.Delete(item);
            }

            cardItemRepository.Save();

            return Ok("All card items have been deleted.");
        }
    }
}
