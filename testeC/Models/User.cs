using System;

namespace TestC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Inicialização direta
public string Email { get; set; } = string.Empty; // Inicialização direta

        public DateTime DataCadastro { get; set; }
    }
}