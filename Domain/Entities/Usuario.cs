using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace SOS_Natureza.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; private set; }

        [Required, EmailAddress]
        public string Email { get; private set; }

        [Required]
        [MinLength(7, ErrorMessage = "A senha deve conter no mínimo 7 caracteres")]
        public string Password { get; private set; }

        public ICollection<Denuncia> Denuncias { get; private set; } = new List<Denuncia>();

        public Usuario() { }

        public Usuario(string name, string email, string password)
        {
            SetNome(name);
            SetEmail(email);
            SetSenha(password);
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome é obrigatório.");

            Name = nome.Trim();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
                throw new ArgumentException("Email inválido.");

            Email = email.Trim().ToLower();
        }

        public void SetSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 7)
                throw new ArgumentException("A senha deve conter no mínimo 7 caracteres.");

            Password = senha;
        }

        public void Update(string name, string email, string? password = null)
        {
            SetNome(name);
            SetEmail(email);

            if (!string.IsNullOrWhiteSpace(password))
                SetSenha(password);
        }

    }
}