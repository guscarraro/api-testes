using System;

namespace TestC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Adicionado
        public DateTime DataCadastro { get; set; }
    }
}