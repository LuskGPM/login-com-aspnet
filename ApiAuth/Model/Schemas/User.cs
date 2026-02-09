namespace ApiAuth.Model.Schemas;

public class User
{
    public int Id { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public string? Password { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}