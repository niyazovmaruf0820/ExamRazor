namespace Domain.DTOs.AccountDto;

public class AddAccountDto
{
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public string? Type { get; set; }
}
