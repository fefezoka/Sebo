using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.TransactionDTO;
using SEBO.API.Repository.IdentityAggregate;
using SEBO.API.Repository.ProductAggregate;

namespace SEBO.API.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly UserRepository _userRepository;
        private readonly ItemRepository _itemRepository;
        public TransactionService(TransactionRepository transactionRepository, ItemRepository itemRepository, UserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public async Task<TransactionDTO> AddTransaction(CreateTransactionDTO createTransactionDto)
        {
            var user = await _userRepository.GetUserByIdAsync(createTransactionDto.SellerId);
            if (user == null) throw new NotFoundException("User not found");

            var item = await _itemRepository.GetById(createTransactionDto.ItemId);
            if (item == null) throw new NotFoundException("Item not found");

            var transaction = new Transaction()
            {
                SellerId = createTransactionDto.SellerId,
                BuyerId = createTransactionDto.BuyerId,
                TransactionPrice = createTransactionDto.TransactionPrice,
            };

            return new TransactionDTO(await _transactionRepository.Add(transaction));
        }

        public async Task<IEnumerable<TransactionDTO>> GetByUserId(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found");

            return (await _transactionRepository.GetByUserId(id)).Select(x => new TransactionDTO(x));
        }
    }
}
