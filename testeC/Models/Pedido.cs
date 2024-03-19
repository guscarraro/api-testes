using System.ComponentModel.DataAnnotations.Schema;

namespace TestC.Models
{
    public class Pedido
    {
        [Column("id")] // Defina explicitamente o nome da coluna como "id"
        public int Id { get; set; }

        [Column("cliente_nome")] // Defina explicitamente o nome da coluna como "cliente_nome"
        public string ClienteNome { get; set; }

        [Column("data_pedido")] // Defina explicitamente o nome da coluna como "data_pedido"
        public DateTime DataPedido { get; set; }

        [Column("valor_total")] // Defina explicitamente o nome da coluna como "valor_total"
        public decimal ValorTotal { get; set; }
    }
}
