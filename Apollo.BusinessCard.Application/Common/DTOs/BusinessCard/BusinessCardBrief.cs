namespace Apollo.BusinessCard.Application.Common.DTOs;

public record BusinessCardBrief
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } 
    public string Phone { get; set; } 
    public string Address { get; set; } 

    public string Photo { get; set; }
    public Guid AttachtmentId { get; set; }

    public int GenderId { get; set; }
    public string GenderName { get; set; }
}
