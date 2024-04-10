namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }
        public async Task<int> CreateProduct(CreateProductDto productDto)
        {
            if (productDto is null
            ||
            string.IsNullOrWhiteSpace(productDto.Name)
            ||
            string.IsNullOrWhiteSpace(productDto.ProductNumber)
             || productDto.MakeFlag == null
             || productDto.FinishedGoodsFlag == null
             || productDto.SafetyStockLevel == null
             || productDto.ReorderPoint == null
             || productDto.StandardCost == null
             || productDto.ListPrice == null
             )
            {
                throw new BadRequestException("Product data is not provided.");
            }
            Product product = new()
            {
                Name = productDto.Name,
                ListPrice = productDto.ListPrice ?? 0,
                ProductNumber = productDto.ProductNumber,
                StandardCost = productDto.StandardCost ?? 0,
                Weight = productDto.Weight,
                Color = productDto.Color,
                FinishedGoodsFlag = productDto.FinishedGoodsFlag ?? false,
                MakeFlag = productDto.MakeFlag ?? false,
                ReorderPoint = productDto.ReorderPoint ?? 0,
                SafetyStockLevel = productDto.SafetyStockLevel ?? 0,
                Size = productDto.Size,
                DaysToManufacture = productDto.DaysToManufacture,
                SellStartDate = productDto.SellStartDate,
            };
            return await _productRepository.CreateProduct(product);
        }
        public async Task<int> UpdateProduct(UpdateProductDto productDto)
        {
            if (productDto is null)
            {
                throw new BadRequestException("Product data is not provided.");
            }
            var product = await ValidateProductExistence(productDto.ProductID);

            product.Name = string.IsNullOrWhiteSpace(productDto.Name) ? product.Name : productDto.Name;
            product.ListPrice = productDto.ListPrice ?? product.ListPrice;
            product.ProductNumber = string.IsNullOrWhiteSpace(productDto.ProductNumber) ? product.ProductNumber : productDto.ProductNumber;
            product.StandardCost = productDto.StandardCost ?? product.StandardCost;
            product.Weight = productDto.Weight ?? product.Weight;
            product.Color = string.IsNullOrWhiteSpace(productDto.Color) ? product.Color : productDto.Color;
            product.FinishedGoodsFlag = productDto.FinishedGoodsFlag ?? product.FinishedGoodsFlag;
            product.MakeFlag = productDto.MakeFlag ?? product.MakeFlag;
            product.ReorderPoint = productDto.ReorderPoint ?? product.ReorderPoint;
            product.SafetyStockLevel = productDto.SafetyStockLevel ?? product.SafetyStockLevel;
            product.Size = string.IsNullOrWhiteSpace(productDto.Size) ? product.Size : productDto.Size;
            product.DaysToManufacture = productDto.DaysToManufacture ?? product.DaysToManufacture;
            product.SellStartDate = productDto.SellStartDate ?? product.SellStartDate;
            return await _productRepository.UpdateProduct(product);
        }


        public async Task<int> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }
            var product = await ValidateProductExistence(id);
            return await _productRepository.DeleteProduct(product);
        }

        public async Task<IEnumerable<GetProductDto>> GetAll()
        {
            var products = await _productRepository.GetAllProducts();
            List<GetProductDto> productsDto = new();
            foreach (var product in products)
            {
                GetProductDto dto = new()
                {
                    Name = product.Name,
                    ListPrice = product.ListPrice,
                    ProductID = product.ProductID,
                    ProductNumber = product.ProductNumber,
                    StandardCost = product.StandardCost,
                    Weight = product.Weight,
                    Color = product.Color,
                    FinishedGoodsFlag = product.FinishedGoodsFlag,
                    MakeFlag = product.MakeFlag,
                    ReorderPoint = product.ReorderPoint,
                    SafetyStockLevel = product.SafetyStockLevel,
                    Size = product.Size,
                    SellStartDate = product.SellStartDate,
                    DaysToManufacture = product.DaysToManufacture,

                };
                productsDto.Add(dto);
            }
            return productsDto;
        }

        public async Task<GetProductDto?> GetProductByID(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }

            var product = await ValidateProductExistence(id);

            GetProductDto dto = new()
            {
                Name = product.Name,
                ListPrice = product.ListPrice,
                ProductID = product.ProductID,
                ProductNumber = product.ProductNumber,
                StandardCost = product.StandardCost,
                Weight = product.Weight,
                Color = product.Color,
                FinishedGoodsFlag = product.FinishedGoodsFlag,
                MakeFlag = product.MakeFlag,
                ReorderPoint = product.ReorderPoint,
                SafetyStockLevel = product.SafetyStockLevel,
                Size = product.Size,
            };
            return dto;

        }



        private async Task<Product> ValidateProductExistence(int id)
        {
            var existingProduct = await _productRepository.GetProductByID(id)
                ?? throw new NotFoundException($"Product with Id: {id} was not found.");

            return existingProduct;
        }

    }
}