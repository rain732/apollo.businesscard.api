namespace Apollo.BusinessCard.Application.Common.DTOs;

public record CreateBusinessCardDto
{
    public string Name { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
    public string Adress { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int GenderId { get; set; }
}
