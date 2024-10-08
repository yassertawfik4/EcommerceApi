﻿using data.Repository.IRepository;
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
    public class FavoriteItemsController : ControllerBase
    {
        private readonly IFavoriteItemRepository favoriteItemRepository;

        public FavoriteItemsController(IFavoriteItemRepository favoriteItemRepository)
        {
            this.favoriteItemRepository = favoriteItemRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = favoriteItemRepository.GetAll();

            List<FavoriteItemDTO>favoriteItems = new List<FavoriteItemDTO>();
            foreach (var item in items)
            {
                favoriteItems.Add(new FavoriteItemDTO
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserId=item.UserId,
                });
            }
            if (items == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(favoriteItems);
            }
        }
        [HttpPost]
        public IActionResult Create(FavoriteItemDTO item)
        {
            if (ModelState.IsValid)
            {
                FavoriteItem favoriteItem = new FavoriteItem()
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserId = item.UserId,

                };
                favoriteItemRepository.Create(favoriteItem);
                favoriteItemRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, FavoriteItemDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                var item=favoriteItemRepository.GetOne(t=>t.Id == id);
                item.Id = itemDTO.Id;
                item.ProductId = itemDTO.ProductId;
                item.Quantity = itemDTO.Quantity;
                item.UserId = itemDTO.UserId;
               favoriteItemRepository.Update(item);
                favoriteItemRepository.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = favoriteItemRepository.GetOne(t =>t.Id==id);
            favoriteItemRepository.Delete(res);
            favoriteItemRepository.Save();
            return Ok();
        }
    }
}