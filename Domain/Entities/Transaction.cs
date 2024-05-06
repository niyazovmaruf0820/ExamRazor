namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public Account? FromAccount { get; set; }
    public Account? ToAccount { get; set; }
}