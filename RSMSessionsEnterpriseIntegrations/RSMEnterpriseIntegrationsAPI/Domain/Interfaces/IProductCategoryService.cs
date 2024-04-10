namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IProductCategoryService
    {
        Task<GetProductCategoryDto?> GetProductCategoryById(int id);
        Task<IEnumerable<GetProductCategoryDto>> GetAll();
        Task<int> CreateProductCategory(CreateProductCategoryDto departmentDto);
        Task<int> UpdateProductCategory(UpdateProductCategoryDto departmentDto);
        Task<int> DeleteProductCategory(int id);
    }
}
