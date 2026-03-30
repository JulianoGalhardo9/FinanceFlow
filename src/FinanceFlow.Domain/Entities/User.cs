namespace FinanceFlow.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    private User() { }

    public static User Create(string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome é obrigatório.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O email é obrigatório.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("A senha é obrigatória.", nameof(passwordHash));

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email.ToLowerInvariant(),
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };
    }
}