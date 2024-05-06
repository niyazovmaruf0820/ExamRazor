namespace Domain.DTOs.TransactionDto;

public class GetTransactionsDto
{
    public int Id { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
