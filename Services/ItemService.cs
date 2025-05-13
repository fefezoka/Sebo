using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.ItemDTO;
using SEBO.API.Repository.IdentityAggregate;
using SEBO.API.Repository.ProductAggregate;

namespace SEBO.API.Services
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly UserRepository _userRepository;

        public ItemService(ItemRepository itemRepository, CategoryRepository categoryRepository, UserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<Item> AddItem(CreateItemDTO createItemDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(createItemDTO.SellerId);
            if (user == null) throw new NotFoundException("User not found");

            var category = await _categoryRepository.GetById(createItemDTO.CategoryId);
            if (category == null) throw new NotFoundException("Category not found");

            var item = new Item()
            {
                Price = createItemDTO.Price,
                Author = createItemDTO.Author,
                CategoryId = createItemDTO.CategoryId,
                isOutOfStock = false,
                Description = createItemDTO.Description,
                SellerId = createItemDTO.SellerId,
                Title = createItemDTO.Title,
            };

            return await _itemRepository.Add(item);
        }

        public async Task<Item> UpdateItem(UpdateItemDTO updateItemDTO)
        {
            var item = await _itemRepository.GetById(updateItemDTO.ItemId);

            if (item == null) throw new NotFoundException("Item not found");

            item.SellerId = updateItemDTO.SellerId;
            item.Author = updateItemDTO.Author;
            item.isOutOfStock = updateItemDTO.isOutOfStock;
            item.Description = updateItemDTO.Description;
            item.Title = updateItemDTO.Title;

            return await _itemRepository.Update(item);
        }

        public async Task<IEnumerable<Item>> GetItems() => await _itemRepository.GetAll() ?? Enumerable.Empty<Item>();

        public async Task<Item> GetById(int id) => await _itemRepository.GetById(id) ?? throw new NotFoundException("Item not found");

        public async Task DeleteById(int id) => await _itemRepository.DeleteById(id);
    }
}
