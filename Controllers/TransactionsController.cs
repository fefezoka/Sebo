using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.ViewModel.DTO.TransactionDTO;
using SEBO.API.Services;

namespace SEBO.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> PostTransaction([FromBody] CreateTransactionDTO createTransactionDto)
            => Ok(await _transactionService.AddTransaction(createTransactionDto));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetByUserId([FromRoute] int id) => Ok(await _transactionService.GetByUserId(id));
    }
}
