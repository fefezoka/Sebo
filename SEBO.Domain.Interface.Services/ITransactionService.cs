using SEBO.Domain.Dto.DTO.TransactionDTO;
using SEBO.Domain.Dto.DTO.Base;

namespace SEBO.Domain.Interface.Services
{
    public interface ITransactionService
    {
        Task<BaseResponseDTO<TransactionDTO>> AddTransaction(CreateTransactionDTO createTransactionDto);
        Task<BaseResponseDTO<IEnumerable<TransactionDTO>>> GetTransactionsByUserId(int id);
    }
}
