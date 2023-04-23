using Microsoft.AspNetCore.Identity;
namespace IWantApp.Domain.Users;

public class ApplicationUser : IdentityUser

{
    public string EnderecoCompleto { get; set; }
    public string Cpf { get; set; }
}
