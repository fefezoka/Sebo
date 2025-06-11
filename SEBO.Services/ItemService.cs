using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Services.Identity;
using SEBO.Domain.Utility.Exceptions;
using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.ItemDTO;
using SEBO.Domain.Interface.Repository.IdentityAggregate;
using SEBO.Domain.Interface.Repository.ProductAggregate;
using SEBO.Domain.Interface.Services;

namespace SEBO.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IUserService userService)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<BaseResponseDTO<ItemDTO>> AddItem(CreateItemDTO createItemDTO)
        {
            var responseDTO = new BaseResponseDTO<ItemDTO>();

            var (result, user) = await _userRepository.GetUserByIdAsync(createItemDTO.SellerId);
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

            return responseDTO.AddContent(new ItemDTO(await _itemRepository.Add(item)));
        }

        public async Task<BaseResponseDTO<ItemDTO>> UpdateItem(UpdateItemDTO updateItemDTO)
        {
            var responseDTO = new BaseResponseDTO<ItemDTO>();
            var item = await _itemRepository.GetById(updateItemDTO.ItemId);

            if (item == null) throw new NotFoundException("Item not found");

            item.SellerId = updateItemDTO.SellerId;
            item.Author = updateItemDTO.Author;
            item.isOutOfStock = updateItemDTO.isOutOfStock;
            item.Description = updateItemDTO.Description;
            item.Title = updateItemDTO.Title;

            return responseDTO.AddContent(new ItemDTO(await _itemRepository.Update(item)));
        }

        public async Task<BaseResponseDTO<IEnumerable<ItemDTO>>> GetAllItems()
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<ItemDTO>>();

            var items = (await _itemRepository.GetAll()).Select(x => new ItemDTO(x)) ?? Enumerable.Empty<ItemDTO>();

            return responseDTO.AddContent(items);
        }

        public async Task<BaseResponseDTO<ItemDTO>> GetItemById(int id)
        {
            var responseDTO = new BaseResponseDTO<ItemDTO>();

            var item = await _itemRepository.GetById(id) ?? throw new NotFoundException("Item not found");
            return responseDTO.AddContent(new ItemDTO(item));
        }

        public async Task DeleteItemById(int id)
        {
            var item = await _itemRepository.GetById(id) ?? throw new NotFoundException("Item not found");
            var userId = _userService.GetUserIdFromClaims();

            if (item.SellerId != userId) throw new BadRequestException("User isn't item owner");

            await _itemRepository.DeleteById(id);
        }

    }
}
