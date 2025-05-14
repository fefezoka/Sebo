using SEBO.API.Domain.Entities.ProductAggregate;

namespace SEBO.API.Domain.ViewModel.DTO.ItemDTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool isOutOfStock { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }

        public ItemDTO(Item item)
        {
            Id = item.Id;
            Author = item.Author;
            Title = item.Title;
            Description = item.Description;
            Price = item.Price;
            isOutOfStock = item.isOutOfStock;
            SellerId = item.SellerId;
            CategoryId = item.CategoryId;
        }
    }
}
