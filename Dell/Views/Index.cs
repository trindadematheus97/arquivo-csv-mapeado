using static System.Console;
using Dell.Service;
using System.Text;
using Dell.Entities;

namespace Dell.Views
{
    public static class Index
    {
        public static void StartProgram()
        {
            try
            {
                var opcao = 0;

                while (opcao != 4)
                {
                    Console.Clear();
                    MenuPrincipal();
                    opcao = int.Parse(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            MenuBuscaProdutoPorNome();
                            break;
                        case 2:
                            MenuBuscaProdutoPorEan();
                            break;
                        case 3:
                            MenuListaPercentualConcessaoDeCredito();
                            break;
                        case 4:
                            Console.WriteLine("[PROGRAMA ENCERRADO]");
                            break;
                        default:
                            Console.WriteLine("[!!! OPÇÃO INVÁLIDA !!!]");
                            Console.WriteLine("[PRESSIONE UMA TECLA PARA VOLTAR AO MENU PRINCIPAL]");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("[OCORREU UM ERRO DURANTE A EXECUCAO, TENTE NOVAMENTE]");
                Console.WriteLine("[PROGRAMA ENCERRADO]");
            }    
        }

        public static void MenuPrincipal()
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("* PROGRAMA PARA BUSCA DE PRODUTOS *");
            Console.WriteLine("***********************************");
            Console.WriteLine("[1 - BUSCAR PELO NOME]");
            Console.WriteLine("[2 - BUSCAR POR CÓDIGO DE BARRAS]");
            Console.WriteLine("[3 - COMPARATIVO DA LISTA DE CONCESSÃO DE CRÉDITO TRIBUTÁRIO (PIS/COFINS)]");
            Console.WriteLine("[4 - SAIR]");
            Console.Write("=> ");
        }

        public static void MenuBuscaProdutoPorNome()
        {
            Console.Clear();
            Console.Write("DIGITE O NOME DO PRODUTO: ");
            var nomeProduto = Console.ReadLine();
            Console.WriteLine();

            var lista = MedicamentosService.BuscaProdutoPorNome(nomeProduto);

            ListarProdutos(lista);
        }

        public static void MenuBuscaProdutoPorEan()
        {
            Console.Clear();
            Console.Write("DIGITE O CÓDIGO DE BARRAS DO PRODUTO: ");
            var ean = Console.ReadLine();
            Console.WriteLine();

            var produto = MedicamentosService.BuscaProdutoPorEan(ean);
            var listaPmc = MedicamentosService.BuscaProdutoMaiorEMenorPmc();
            var difencaPmc = MedicamentosService.DiferencaProdutoMaiorEMenorPmc();

            ListarProdutos(listaPmc, produto, difencaPmc);
        }

        public static void MenuListaPercentualConcessaoDeCredito()
        {
            Console.Clear();

            var percentual = MedicamentosService.PercentualConcessaoDeCredito();

            GraficoPercentualConcessaoDeCredito(percentual);
        }

        private static void ListarProdutos(List<Medicamento> lista)
        {
            foreach (var item in lista)
            {
                var str = new StringBuilder();
                str.AppendLine($"[PRODUTO: {item.Produto}]");
                str.AppendLine($"[SUBSTÂNCIA: {item.Substancia}]");
                str.AppendLine($"[APRESENTAÇÃO: {item.Apresentacao.Trim()}]");
                str.AppendLine($"[VALOR PESSOA FÍSICA: {item.ValorPf.ToString("C")}]");

                Console.WriteLine(str);
            };

            Console.WriteLine("[PRESSIONE UMA TECLA PARA VOLTAR AO MENU PRINCIPAL]");
            Console.ReadKey();
        }

        private static void ListarProdutos(List<Medicamento> lista, Medicamento medicamento, decimal diferencaPmc)
        {
            var str = new StringBuilder();
            str.AppendLine($"[PRODUTO: {medicamento.Produto}]");
            str.AppendLine($"[SUBSTÂNCIA: {medicamento.Substancia}]");
            str.AppendLine($"[APRESENTAÇÃO: {medicamento.Apresentacao.Trim()}]");
            str.AppendLine($"[VALOR PESSOA FÍSICA: {medicamento.ValorPf.ToString("C")}]");
            Console.WriteLine(str);

            Console.WriteLine("[***PRODUTOS COM MAIOR E MENOR PMC***]");
            Console.WriteLine();
            foreach (var item in lista)
            {
                var strLista = new StringBuilder();
                strLista.AppendLine($"[PRODUTO: {item.Produto}]");
                strLista.AppendLine($"[SUBSTÂNCIA: {item.Substancia}]");
                strLista.AppendLine($"[APRESENTAÇÃO: {item.Apresentacao.Trim()}]");
                strLista.AppendLine($"[VALOR PESSOA FÍSICA: {item.ValorPf.ToString("C")}]");
                Console.WriteLine(strLista);
            };

            Console.WriteLine($"[DIFERENÇA ENTRE O PMC: {diferencaPmc.ToString("C")}]");
            Console.WriteLine();

            Console.WriteLine("[PRESSIONE UMA TECLA PARA VOLTAR AO MENU PRINCIPAL]");
            Console.ReadKey();
        }

        private static void GraficoPercentualConcessaoDeCredito(PercentualComConcessaoDeCredito percentual)
        {
            var str = new StringBuilder();

            // Header
            str.Append("CLASSIFICACAO");
            str.Append("    ");
            str.Append("PERCENTUAL");
            str.Append("    ");
            str.AppendLine("GRAFICO");

            // Classificacao negativa
            str.Append("Negativa");
            str.Append("          ");
            str.Append($"{percentual.NegativaTexto}");
            str.Append("       ");
            for (int i = 1; i < percentual.Negativa; i++)
            {
                str.Append("*");
            }
            str.AppendLine();

            // Classificacao neutra
            str.Append("Neutra");
            str.Append("            ");
            str.Append($"{percentual.NeutraTexto}");
            str.Append("        ");
            for (int i = 1; i < percentual.Neutra; i++)
            {
                str.Append("*");
            }
            str.AppendLine();

            // Classificacao positiva
            str.Append("Positiva");
            str.Append("          ");
            str.Append($"{percentual.PositivaTexto}");
            str.Append("       ");
            for (int i = 1; i < percentual.Positiva; i++)
            {
                str.Append("*");
            }
            str.AppendLine();

            // Footer
            str.Append("TOTAL");
            str.Append("             ");
            str.AppendLine($"{percentual.TotalTexto}");

            Console.WriteLine(str);

            Console.WriteLine("[PRESSIONE UMA TECLA PARA VOLTAR AO MENU PRINCIPAL]");
            Console.ReadKey();
        }
    }
}
