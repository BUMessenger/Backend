namespace BUMessenger.Domain.Models.Models.Users;

public record User
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string FatherName { get; init; }
    public required string Email { get; init; }
}