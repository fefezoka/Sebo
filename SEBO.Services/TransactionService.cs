using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Utility.Exceptions;
using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.TransactionDTO;
using SEBO.Domain.Interface.Repository.IdentityAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;
using SEBO.Domain.Interface.Services;

namespace SEBO.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        public TransactionService(ITransactionRepository transactionRepository, IItemRepository itemRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponseDTO<TransactionDTO>> AddTransaction(CreateTransactionDTO createTransactionDto)
        {
            var responseDTO = new BaseResponseDTO<TransactionDTO>();

            var (_, user) = await _userRepository.GetUserByIdAsync(createTransactionDto.SellerId);
            if (user == null) throw new NotFoundException("User not found");

            var item = await _itemRepository.GetById(createTransactionDto.ItemId);
            if (item == null) throw new NotFoundException("Item not found");

            var transaction = new Transaction()
            {
                SellerId = createTransactionDto.SellerId,
                BuyerId = createTransactionDto.BuyerId,
                TransactionPrice = createTransactionDto.TransactionPrice,
            };

            return responseDTO.AddContent(new TransactionDTO(await _transactionRepository.Add(transaction)));
        }

        public async Task<BaseResponseDTO<IEnumerable<TransactionDTO>>> GetTransactionsByUserId(int id)
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<TransactionDTO>>();

            var (result, user) = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found");

            var transactions = (await _transactionRepository.GetByUserId(id)).Select(x => new TransactionDTO(x));

            return responseDTO.AddContent(transactions);
        }
    }
}
