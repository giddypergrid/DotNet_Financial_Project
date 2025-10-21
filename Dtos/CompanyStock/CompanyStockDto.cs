using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.CommentDtoNamespace;
using backend.Models;
using Mapster;

namespace backend.Dtos.CompanyStockDtoNamespace
{
    public class CompanyStockBaseDto
    {
        public string CompanyName { get; set; } = String.Empty;
        public string Symbol { get; set; } = String.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = String.Empty;
        public long MarketCap { get; set; }
        public List<int> CommentIds { get; set; } = new List<int>();
    }
    public class CreateCompanyStockDto : CompanyStockBaseDto
    {
    }
    public class CompanyStockDto : CompanyStockBaseDto
    {
        public int Id { get; set; }
    }
    public static class CompanyStockDtoMapper
    {
        public static CompanyStockDto ToDto(this CompanyStock companyStock)
        {
            CompanyStockDto companyStockDto = companyStock.Adapt<CompanyStockDto>();
            return companyStockDto;
        }

        public static CompanyStock ToModel(this CompanyStockBaseDto createDto)
        {
            CompanyStock companyStock = createDto.Adapt<CompanyStock>();
            return companyStock;
        }
    }
}