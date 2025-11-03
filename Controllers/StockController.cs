using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Repository.Interface;
using backend.Constants;
using backend.Helpers;
using backend.Dtos.General;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStocks([FromQuery] queryStockObject queryStockObject)
        {
            var (stocks, totalCount) = await _stockRepository.SearchStocks(queryStockObject);
            
            bool hasMoreData = stocks.Count > (queryStockObject.PageSize ?? 10);
            if (hasMoreData)
            {
                stocks.RemoveAt(stocks.Count - 1);
            }
            
            string? nextCursorSymbol = null;
            string? nextCursorCompanyName = null;
            int? nextCursorId = null;
            
            if (hasMoreData && stocks.Count > 0)
            {
                var lastStock = stocks.Last();
                nextCursorSymbol = lastStock.Symbol;
                nextCursorCompanyName = lastStock.CompanyName;
                nextCursorId = lastStock.Id;
            }
            
            var paginatedResponse = new paginationDto<CompanyStockDto>
            {
                Data = stocks.Select(s => s.ToDto()).ToList(),
                TotalCount = totalCount,
                PageIndex = queryStockObject.PageIndex ?? 1,
                PageSize = queryStockObject.PageSize ?? 10,
                NextStringId = nextCursorSymbol,
                NextIntId = nextCursorId,
                HasMoreData = hasMoreData
            };
            
            return Ok(paginatedResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var stock = await _stockRepository.GetStockById(id);
            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            return Ok(stock.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(CreateCompanyStockDto createCompanyStockDto)
        {

            var (companyStock, statusCode) = await _stockRepository.CreateStock(createCompanyStockDto);
            if (statusCode == StatusCodeConstants.COMMENT_EXIST_WHEN_CREATE)
            {
                return Conflict("Stock with this symbol already exists");
            }
            if (companyStock == null)
            {
                return BadRequest("Failed to create stock");
            }
            return CreatedAtAction(nameof(GetStockById), new { id = companyStock.Id }, companyStock.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] CreateCompanyStockDto updateCompanyStockDto){
            var updateStock = await _stockRepository.UpdateStock(id, updateCompanyStockDto);
            if (updateStock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            return Ok(updateStock.ToDto());            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id){
            var deletedStock = await _stockRepository.DeleteStock(id);
            if (deletedStock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }
            return NoContent();
        }
    }
}