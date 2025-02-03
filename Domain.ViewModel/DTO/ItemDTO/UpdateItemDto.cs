namespace SEBO.API.Domain.ViewModel.DTO.ItemDTO
{
    public class UpdateItemDto
    {
        public int ItemId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool isOutOfStock { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
    }
}
