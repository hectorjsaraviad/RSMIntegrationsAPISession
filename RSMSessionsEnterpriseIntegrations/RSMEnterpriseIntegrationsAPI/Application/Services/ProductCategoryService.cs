namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository repository)
        {
            _productCategoryRepository = repository;
        }

        public async Task<int> CreateProductCategory(CreateProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto is null 
                || string.IsNullOrWhiteSpace(productCategoryDto.Name))
            {
                throw new BadRequestException("Product Category info is not valid.");
            }

            ProductCategory productCategory = new()
            {
                Name = productCategoryDto.Name,
            };

            return await _productCategoryRepository.CreateProductCategory(productCategory);
        }

        public async Task<int> DeleteProductCategory(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }
            var productCategory = await ValidateProductCategoryExistence(id);
            return await _productCategoryRepository.DeleteProductCategory(productCategory);
        }

        public async Task<IEnumerable<GetProductCategoryDto>> GetAll()
        {
            var productCategories = await _productCategoryRepository.GetAllProductCategories();
            List<GetProductCategoryDto> productCategoryDto = [];

            foreach (var productCategory in productCategories)
            {
                GetProductCategoryDto dto = new()
                {
                    Name = productCategory.Name,
                    ProductCategoryID = productCategory.ProductCategoryID
                };

                productCategoryDto.Add(dto);
            }

            return productCategoryDto; 
        }

        public async Task<GetProductCategoryDto?> GetProductCategoryById(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }

            var productCategory = await ValidateProductCategoryExistence(id);
            
            GetProductCategoryDto dto = new()
            {
                Name = productCategory.Name,
            };
            return dto;
        }

        public async Task<int> UpdateProductCategory(UpdateProductCategoryDto productCategoryDto)
        {
            if(productCategoryDto is null)
            {
                throw new BadRequestException("Product Category info is not valid.");
            }
            var productCategory = await ValidateProductCategoryExistence(productCategoryDto.ProductCategoryID);
            
            productCategory.Name = string.IsNullOrWhiteSpace(productCategoryDto.Name) ? productCategory.Name : productCategoryDto.Name;
           
            return await _productCategoryRepository.UpdateProductCategory(productCategory);
        }

        private async Task<ProductCategory> ValidateProductCategoryExistence(int id)
        {
            var existingProductCategory = await _productCategoryRepository.GetProductCategoryById(id) 
                ?? throw new NotFoundException("Product Category not found.");

            return existingProductCategory;
        }

    }
}
