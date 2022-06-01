namespace Dell.Entities
{
    public class Medicamento
    {
        public Medicamento(string substancia, string produto, string apresentacao, decimal valorPf, string ean, string comercializacao, decimal pmc, string listaConcessaoDeCredito)
        {
            Substancia = substancia;
            Produto = produto;
            Apresentacao = apresentacao;
            ValorPf = valorPf;
            Ean = ean;
            Comercializacao = comercializacao;
            Pmc = pmc;
            ListaConcessaoDeCredito = listaConcessaoDeCredito;
        }

        public string Substancia { get; private set; }
        public string Produto { get; private set; }
        public string Apresentacao { get; private set; }
        public decimal ValorPf { get; private set; }
        public string Ean { get; private set; }
        public string Comercializacao { get; private set; }
        public decimal Pmc { get; private set; }
        public string ListaConcessaoDeCredito { get; set; }
    }
}
