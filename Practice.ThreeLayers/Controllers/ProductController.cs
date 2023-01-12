using Enities.Dtos;
using Enities.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;
using Services;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.ThreeLayers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /*[Authorize]*/
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> GetAll()
        {
            try
            {
                var res = await _productService.GetAll();
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> GetProductById(int id)
        {
            try
            {
                var res = await _productService.GetProductById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpDelete(Name = "DeleteProduct")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteProduct(int id)
        {
            try
            {
                var res = await _productService.DeleteProductById(id);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpPut(Name = "UpdateProduct")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateProduct(Product product)
        {
            try
            {
                var res = await _productService.UpdateProduct(product);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> CreateProduct([FromBody] Product product)
        {
            try
            {
                var res = await _productService.CreateProduct(product);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
} 
