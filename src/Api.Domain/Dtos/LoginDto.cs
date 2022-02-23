using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        public string Email { get; set; }
    }
}
