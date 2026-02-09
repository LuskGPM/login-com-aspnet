namespace ApiAuth.Model.Schemas;

public class UserInsertDto
{
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public string? Password { get; set; }
    public DateOnly Data_Nascimento { get; set; }

    public UserInsertDto() { }
    public UserInsertDto(User user) =>
        (Cpf, Email, Nome, Password, Data_Nascimento) = (user.Cpf, user.Email, user.Nome, user.Password, user.Data_Nascimento);
};

public class UserLoginDto
{
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

}

public class UserGetDto
{
    public int Id { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}