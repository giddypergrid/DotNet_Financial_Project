using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Repository.Interface;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepository.GetAllStocks();
            return Ok(stocks.Select(s => s.ToDto()));
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
            var existingStock = await _stockRepository.GetStockBySymbol(createCompanyStockDto.Symbol);
            if (existingStock != null)
            {
                return BadRequest("Stock with this symbol already exists.");
            }

            var companyStock = createCompanyStockDto.ToModel();
            await _stockRepository.CreateStock(companyStock);
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