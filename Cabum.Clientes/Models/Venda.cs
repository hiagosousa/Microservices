namespace Cabum.Clientes.Models;

    public class Venda
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdFuncionario { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
    }