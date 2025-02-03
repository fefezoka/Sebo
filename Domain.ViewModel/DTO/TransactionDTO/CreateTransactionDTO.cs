namespace SEBO.API.Domain.ViewModel.DTO.TransactionDTO
{
    public class CreateTransactionDTO
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ItemId { get; set; }
        public double TransactionPrice { get; set; }
    }
}
