using AutoMapper;
using Enities.Dtos;
using Enities.Models;
using Repositories.IRepository;
using Services.IService;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>{
            cfg.AddProfile(new MappingProfile());
        });
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<int>> CreateProduct(Product product)
        {
            try
            {
                await _productRepository.Insert(product);
                return new ServiceResponse<int>
                { 
                    Data = product.ProductId,
                    Message = "Created Successfully",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> DeleteProductById(int id)
        {
            try
            {
                var pro = await _productRepository.GetById(id);
                if (pro == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Not found",
                        StatusCode = 200,
                        Success = true
                    };
                }
                await _productRepository.Delete(pro);
                return new ServiceResponse<string>
                { 
                    Message = "Delete Successfully",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAll()
        {
            try
            {
                List<Expression<Func<Product, object>>> includes = new List<Expression<Func<Product, object>>>()
                {
                    x => x.Category
                };
                var lst = await _productRepository.GetAllWithCondition(null, includes, x => x.ProductId, true);
                var mapper = config.CreateMapper();
                var lstDto = mapper.Map<IEnumerable<ProductDto>>(lst);
                if (lstDto.Count() > 0)
                {
                    return new ServiceResponse<IEnumerable<ProductDto>>
                    {
                        Data = lstDto,
                        Message = "Successfully",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<ProductDto>>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async Task<ServiceResponse<ProductDto>> GetProductById(int id)
        {
            try
            {
                List<Expression<Func<Product, object>>> includes = new List<Expression<Func<Product, object>>>()
                {
                    x => x.Category
                };
                var pro = await _productRepository.GetByIdWithCondition(x => x.ProductId == id, includes, true);
                var mapper = config.CreateMapper();
                var proDto = mapper.Map<ProductDto>(pro);
                if (proDto == null)
                {
                    return new ServiceResponse<ProductDto>
                    {
                        Message = "Not Found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<ProductDto>
                {
                    Data = proDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> UpdateProduct(Product product)
        {
            var pro = await _productRepository.GetById(product.ProductId);
            if (pro == null)
            {
                return new ServiceResponse<string>
                {
                    Message = "Not found",
                    StatusCode = 200,
                    Success = true
                };
            }
            await _productRepository.Update(product);
            return new ServiceResponse<string>
            {
                Message = "Update Successfully",
                StatusCode = 204,
                Success = true
            };
        }
    }
}
