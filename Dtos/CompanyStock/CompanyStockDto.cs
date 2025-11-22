using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.CommentDtoNamespace;
using backend.Models;
using Mapster;

namespace backend.Dtos.CompanyStockDtoNamespace
{
    public class CreateCompanyStockDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string CompanyName { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Symbol is required")]
        [StringLength(10, ErrorMessage = "Symbol cannot exceed 10 characters")]
        public string Symbol { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Purchase price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Purchase price must be 0 or greater")]
        public decimal Purchase { get; set; }
        
        [Required(ErrorMessage = "Last dividend is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Last dividend must be 0 or greater")]
        public decimal LastDiv { get; set; }
        
        [Required(ErrorMessage = "Industry is required")]
        [StringLength(50, ErrorMessage = "Industry cannot exceed 50 characters")]
        public string Industry { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Market cap is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Market cap must be greater than 0")]
        public long MarketCap { get; set; }
    }
    public class CompanyStockDto : CreateCompanyStockDto
    {
        public int Id { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }

    public class CompanyStockDtoNoComments : CreateCompanyStockDto
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
        public static CompanyStockDtoNoComments ToDtoNoComments(this CompanyStock companyStock){
            CompanyStockDtoNoComments companyStockDtoNoComments = companyStock.Adapt<CompanyStockDtoNoComments>();
            return companyStockDtoNoComments;
        }
        public static CompanyStock ToModel(this CreateCompanyStockDto createDto)
        {
            CompanyStock companyStock = createDto.Adapt<CompanyStock>();
            return companyStock;
        }
    }
}