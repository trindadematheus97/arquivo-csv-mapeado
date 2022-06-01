using Dell.Entities;

namespace Dell.Service
{
    public static class MedicamentosService
    {
        public static List<Medicamento> BuscaProdutoPorNome(string nomeProduto)
        {
        
            var todosMedicamentos = CsvService.CsvMedicamentos();

            nomeProduto = nomeProduto.Trim().ToUpper();
           
            var listaProdutosComercializados = todosMedicamentos
                .Where(x => x.Produto.Contains(nomeProduto) && x.Comercializacao.Contains("Sim"))
                .OrderBy(x => x.Produto)
                .ToList();

            return listaProdutosComercializados;
        }

        public static Medicamento BuscaProdutoPorEan(string ean)
        {
            var todosMedicamentos = CsvService.CsvMedicamentos();

            ean = ean.Trim();

            var produto = todosMedicamentos.FirstOrDefault(x => x.Ean.Contains(ean));

            return produto;
        }

        public static List<Medicamento> BuscaProdutoMaiorEMenorPmc()
        {
            var lista = CsvService.CsvMedicamentos().OrderBy(x => x.Pmc);

            var pmcMin = lista.FirstOrDefault(x => x.Pmc != decimal.Zero);
            var pmcMax = lista.LastOrDefault();

            var listaMedicamentosFiltrados = new List<Medicamento>
            {
                pmcMin,
                pmcMax
            };

            return listaMedicamentosFiltrados;
        }

        public static decimal DiferencaProdutoMaiorEMenorPmc()
        {
            var lista = BuscaProdutoMaiorEMenorPmc();

            var menorPmc = lista.FirstOrDefault();
            var maiorPmc = lista.LastOrDefault();
            var diferencaPmc = maiorPmc.Pmc - menorPmc.Pmc;

            return diferencaPmc;
        }

        public static PercentualComConcessaoDeCredito PercentualConcessaoDeCredito()
        {
            var todosMedicamentos = CsvService.CsvMedicamentos();
            var listaProdutosComercializados = todosMedicamentos.Where(x => x.Comercializacao.Contains("Sim")).ToList();
            var itensComPercentualNegativo = listaProdutosComercializados.Where(x => x.ListaConcessaoDeCredito.Contains("Negativa")).Count();
            var itensComPercentualNeutro = listaProdutosComercializados.Where(x => x.ListaConcessaoDeCredito.Contains("Neutra")).Count();
            var itensComPercentualPositivo = listaProdutosComercializados.Where(x => x.ListaConcessaoDeCredito.Contains("Positiva")).Count();

            var percentualNegativo = Math.Round((decimal)(itensComPercentualNegativo * 100) / listaProdutosComercializados.Count(), 2);
            var percentualNeutro = Math.Round((decimal)(itensComPercentualNeutro * 100) / listaProdutosComercializados.Count(), 2);
            var percentualPositivo = Math.Round((decimal)(itensComPercentualPositivo * 100) / listaProdutosComercializados.Count(), 2);

            var percentualComConcessaoDeCredito = new PercentualComConcessaoDeCredito(percentualNegativo, percentualNeutro, percentualPositivo);

            return percentualComConcessaoDeCredito;
        }        
    }
}
