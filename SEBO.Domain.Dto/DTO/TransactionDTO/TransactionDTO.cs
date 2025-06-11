using SEBO.Domain.Entities.ProductAggregate;

namespace SEBO.Domain.Dto.DTO.TransactionDTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ItemId { get; set; }
        public int SellerId { get; set; }
        public double TransactionPrice { get; set; }
        public DateTime TransactionDate { get; set; }

        public TransactionDTO(Transaction transaction)
        {
            Id = transaction.Id;
            BuyerId = transaction.BuyerId;
            ItemId = transaction.ItemId;
            SellerId = transaction.SellerId;
            TransactionPrice = transaction.TransactionPrice;
            TransactionDate = transaction.TransactionDate;
        }
    }
}
