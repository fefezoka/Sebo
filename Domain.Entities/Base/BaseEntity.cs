namespace SEBO.API.Domain.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreateDate { get; set; }
        public DateTime? AlterDate { get; set; }
    }
}