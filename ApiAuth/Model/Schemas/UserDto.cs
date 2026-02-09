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
        (Cpf, Email, Nome, Password, Data_Nascimento) = (user.Cpf, user.Email, user.Nome, user.PasswordHash, user.Data_Nascimento);
};

public class UserUpdateDto
{
    public string? Cpf { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }
};

public class UserGetDto
{
    public string? Id { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }

    public UserGetDto() { }
    public UserGetDto(User user) =>
        (Id, Cpf, Email, Nome, Data_Nascimento) = (user.Id, user.Cpf, user.Email, user.Nome, user.Data_Nascimento);
};