namespace Domain.DTOs.TransactionDto;

public class AddTransactionDto
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}
