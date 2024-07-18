﻿using GeekShopping.ProductAPI.Data.DTO_s;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
		[Authorize]
		public async Task<ActionResult<ProductDTO>> GetById([FromRoute] long id)
        {
            var product = await _repository.GetById(id);

            if(product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO dto)
        {
            if(dto == null) return BadRequest();

            var product = await _repository.Create(dto);
            return Ok(product);
        }

        [HttpPut]
		[Authorize]
		public async Task<ActionResult<ProductDTO>> Update([FromBody] ProductDTO dto)
        {
            if(dto == null) return BadRequest();

            var product = await _repository.Update(dto);
            return Ok(product);
        }

        [HttpDelete("{id}")]
		[Authorize(Roles = Role.Admin)]
		public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var status = await _repository.Delete(id);

            if (!status)
                return NotFound();

            return Ok();
        }
    }
}
