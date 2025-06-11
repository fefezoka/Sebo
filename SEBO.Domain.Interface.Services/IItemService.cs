using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.ItemDTO;

namespace SEBO.Domain.Interface.Services
{
    public interface IItemService
    {
        Task<BaseResponseDTO<ItemDTO>> AddItem(CreateItemDTO createItemDTO);
        Task<BaseResponseDTO<ItemDTO>> UpdateItem(UpdateItemDTO updateItemDTO);
        Task<BaseResponseDTO<IEnumerable<ItemDTO>>> GetAllItems();
        Task<BaseResponseDTO<ItemDTO>> GetItemById(int id);
        Task DeleteItemById(int id);
    }
}
