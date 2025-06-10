using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.TransactionDTO;

namespace SEBO.API.Domain.Interface.Services
{
    public interface ITransactionService
    {
        Task<BaseResponseDTO<TransactionDTO>> AddTransaction(CreateTransactionDTO createTransactionDto);
        Task<BaseResponseDTO<IEnumerable<TransactionDTO>>> GetTransactionsByUserId(int id);
    }
}
