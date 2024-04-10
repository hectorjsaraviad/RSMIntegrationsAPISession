namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _salesOrderHeaderRepository;
        public SalesOrderHeaderService(ISalesOrderHeaderRepository repository)
        {
            _salesOrderHeaderRepository = repository;
        }

        public async Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto createSalesOrderHeaderDto )
        {
            if (createSalesOrderHeaderDto is null )
            {
                throw new BadRequestException("SalesOrderHeader info is not valid.");
            }

            SalesOrderHeader salesOrderHeader = new()
            {
                RevisionNumber = createSalesOrderHeaderDto.RevisionNumber,
                OrderDate = createSalesOrderHeaderDto.OrderDate,
                DueDate = createSalesOrderHeaderDto.DueDate,
                Status = createSalesOrderHeaderDto.Status,
                OnlineOrderFlag = createSalesOrderHeaderDto.OnlineOrderFlag,
                BillToAddressID = createSalesOrderHeaderDto.BillToAddressID,
                ShipToAddressID = createSalesOrderHeaderDto.ShipToAddressID,
                ShipMethodID = createSalesOrderHeaderDto.ShipMethodID,
                SubTotal = createSalesOrderHeaderDto.SubTotal,
                TaxAmt = createSalesOrderHeaderDto.TaxAmt,
                Freight = createSalesOrderHeaderDto.Freight,
                CustomerID = createSalesOrderHeaderDto.CustomerID
            };

            return await _salesOrderHeaderRepository.CreateSalesOrderHeader(salesOrderHeader);
        }

        public async Task<int> DeleteSalesOrderHeader(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }
            var orderHeader = await ValidateSalesOrderHeaderExistence(id);
            return await _salesOrderHeaderRepository.DeleteSalesOrderHeader(orderHeader);
        }

        public async Task<IEnumerable<GetSalesOrderHeaderDto>> GetAll()
        {
            var ordersHeader = await _salesOrderHeaderRepository.GetAllSalesOrderHeaders();
            List<GetSalesOrderHeaderDto> ordersHeaderDto = [];

            foreach (var orderHeader in ordersHeader)
            {
                GetSalesOrderHeaderDto dto = new()
                {
                    SalesOrderID = orderHeader.SalesOrderID,
                    RevisionNumber = orderHeader.RevisionNumber,
                    OrderDate = orderHeader.OrderDate,
                    DueDate = orderHeader.DueDate,
                    Status = orderHeader.Status,
                    OnlineOrderFlag = orderHeader.OnlineOrderFlag,
                    CustomerID = orderHeader.CustomerID,
                    BillToAddressID = orderHeader.BillToAddressID,
                    ShipToAddressID = orderHeader.ShipToAddressID,
                    ShipMethodID = orderHeader.ShipMethodID,
                    SubTotal = orderHeader.SubTotal,
                    TaxAmt = orderHeader.TaxAmt,
                    Freight = orderHeader.Freight
                };

                ordersHeaderDto.Add(dto);
            }
          
            return ordersHeaderDto; 
        }

        public async Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("SalesOrderHeaderId is not valid");
            }

            var orderHeader = await ValidateSalesOrderHeaderExistence(id);
            
            GetSalesOrderHeaderDto dto = new()
            {
                SalesOrderID = orderHeader.SalesOrderID,
                RevisionNumber = orderHeader.RevisionNumber,
                OrderDate = orderHeader.OrderDate,
                DueDate = orderHeader.DueDate,
                Status = orderHeader.Status,
                OnlineOrderFlag = orderHeader.OnlineOrderFlag,
                CustomerID = orderHeader.CustomerID,
                BillToAddressID = orderHeader.BillToAddressID,
                ShipToAddressID = orderHeader.ShipToAddressID,
                ShipMethodID = orderHeader.ShipMethodID,
                SubTotal = orderHeader.SubTotal,
                TaxAmt = orderHeader.TaxAmt,
                Freight = orderHeader.Freight
            };
            return dto;
        }

        public async Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto salesOrderHeaderDto)
        {
            if(salesOrderHeaderDto is null)
            {
                throw new BadRequestException("SalesOrderHeader info is not valid.");
            }
            var orderHeader = await ValidateSalesOrderHeaderExistence(salesOrderHeaderDto.SalesOrderID);
            orderHeader.SubTotal = salesOrderHeaderDto.SubTotal ?? orderHeader.SubTotal;
            orderHeader.TaxAmt = salesOrderHeaderDto.TaxAmt ?? orderHeader.TaxAmt;
            orderHeader.Freight = salesOrderHeaderDto.Freight ?? orderHeader.Freight;
            
            return await _salesOrderHeaderRepository.UpdateSalesOrderHeader(orderHeader);
        }

        private async Task<SalesOrderHeader> ValidateSalesOrderHeaderExistence(int id)
        {
            var existingOrderHeader = await _salesOrderHeaderRepository.GetSalesOrderHeaderById(id)
                ?? throw new NotFoundException("SalesOrderHeader not found.");

            return existingOrderHeader;
        }

    }
}
