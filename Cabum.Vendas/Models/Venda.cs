namespace Cabum.Vendas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
}