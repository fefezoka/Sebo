using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.Base;
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
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<TransactionDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<TransactionDTO>>> PostTransaction([FromBody] CreateTransactionDTO createTransactionDto) => Ok(await _transactionService.AddTransaction(createTransactionDto));

        [HttpGet("user/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<IEnumerable<TransactionDTO>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<IEnumerable<TransactionDTO>>>> GetByUserId([FromRoute] int id) => Ok(await _transactionService.GetByUserId(id));
    }
}
