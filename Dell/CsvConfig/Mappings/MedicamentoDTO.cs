using CsvHelper.Configuration.Attributes;

namespace Dell.CsvConfig
{
    public class MedicamentoDTO
    {
        [Name("PRODUTO")]
        public string Produto { get; set; }

        [Name("SUBSTÂNCIA")]
        public string Substancia { get; set; }

        [Name("APRESENTAÇÃO")]
        public string Apresentacao { get; set; }

        [Name("PF Sem Impostos")]
        public decimal? ValorPf { get; set; }

        [Name("EAN 1")]
        public string Ean { get; set; }

        [Name("COMERCIALIZAÇÃO 2020")]
        public string Comercializacao { get; set; }

        [Name("PMC 0%")]
        public decimal? Pmc { get; set; }

        [Name("LISTA DE CONCESSÃO DE CRÉDITO TRIBUTÁRIO (PIS/COFINS)")]
        public string ListaConcessaoDeCredito { get; set; }
    }
}
