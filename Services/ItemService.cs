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

        public async Task<Item> AddItem(CreateItemDto createItemDto)
        {
            var user = await _userRepository.GetUserByIdAsync(createItemDto.SellerId);
            if (user == null) throw new NotFoundException("User not found");

            var category = await _categoryRepository.GetById(createItemDto.CategoryId);
            if (category == null) throw new NotFoundException("Category not found");

            var item = new Item()
            {
                Price = createItemDto.Price,
                Author = createItemDto.Author,
                CategoryId = createItemDto.CategoryId,
                isOutOfStock = false,
                Description = createItemDto.Description,
                SellerId = createItemDto.SellerId,
                Title = createItemDto.Title,
            };

            return await _itemRepository.Add(item);
        }

        public async Task<Item> UpdateItem(UpdateItemDto updateItemDto)
        {
            var item = await _itemRepository.GetById(updateItemDto.ItemId);

            if (item == null) throw new NotFoundException("Item not found");

            item.SellerId = updateItemDto.SellerId;
            item.Author = updateItemDto.Author;
            item.isOutOfStock = updateItemDto.isOutOfStock;
            item.Description = updateItemDto.Description;
            item.Title = updateItemDto.Title;

            return await _itemRepository.Update(item);
        }

        public async Task<IEnumerable<Item>> GetItems() => await _itemRepository.GetAll() ?? Enumerable.Empty<Item>();

        public async Task<Item> GetById(int id) => await _itemRepository.GetById(id) ?? throw new NotFoundException("Item not found");

        public async Task DeleteById(int id) => await _itemRepository.DeleteById(id);
    }
}
