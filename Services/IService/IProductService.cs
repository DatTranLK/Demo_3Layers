using Enities.Dtos;
using Enities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAll();
        Task<ServiceResponse<ProductDto>> GetProductById(int id);
        Task<ServiceResponse<string>> DeleteProductById(int id);
        Task<ServiceResponse<int>> CreateProduct(Product product);
        Task<ServiceResponse<string>> UpdateProduct(Product product);
    }
}
