using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdate
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {1} é obrigatório.")]
        [StringLength(60, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {2} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {1} está em formato inválido.")]
        [StringLength(100, ErrorMessage = "O campo {1} precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        public string Email { get; set; }
    }
}
