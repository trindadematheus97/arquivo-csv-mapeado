using CsvHelper;
using CsvHelper.Configuration;
using Dell.CsvConfig;
using Dell.Entities;
using System.Globalization;
using System.Text;

namespace Dell.Service
{
    public class CsvService
    {
        public static List<Medicamento> CsvMedicamentos()
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    Delimiter = ";"
                };

                var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Data\TA_PRECO_MEDICAMENTO.csv"));

                using (var reader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        var medicamentosDTO = csv.GetRecords<MedicamentoDTO>().ToList();

                        var medicamentos = new List<Medicamento>();

                        foreach (var item in medicamentosDTO)
                        {
                            medicamentos.Add(new Medicamento(item.Produto, 
                                                             item.Substancia, 
                                                             item.Apresentacao, 
                                                             item.ValorPf.GetValueOrDefault(), 
                                                             item.Ean, 
                                                             item.Comercializacao, 
                                                             item.Pmc.GetValueOrDefault(), 
                                                             item.ListaConcessaoDeCredito));
                        }

                        return medicamentos;
                    };
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<Medicamento>().ToList();
            }
        }
    }
}
