namespace SEBO.Domain.Dto.DTO.TransactionDTO
{
    public class CreateTransactionDTO
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ItemId { get; set; }
        public double TransactionPrice { get; set; }
    }
}
