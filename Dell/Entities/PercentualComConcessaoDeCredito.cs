using System.Globalization;

namespace Dell.Entities
{
    public class PercentualComConcessaoDeCredito
    {
        public PercentualComConcessaoDeCredito(decimal negativa, decimal neutra, decimal positiva)
        {
            Negativa = negativa;
            Neutra = neutra;
            Positiva = positiva;
        }

        public decimal Negativa { get; private set; }
        public decimal Neutra { get; private set; }
        public decimal Positiva { get; private set; }

        CultureInfo enUs = new CultureInfo("pt-BR");

        public string TotalTexto
        { 
            get 
            { 
                return $"{Negativa}%"; 
            }  
        }

        public string NegativaTexto
        {
            get
            {
                return $"{Negativa}%";
            }
        }

        public string NeutraTexto
        {
            get
            {
                return $"{Neutra}%";
            }
        }

        public string PositivaTexto
        {
            get
            {
                return $"{Positiva}%";
            }
        }
    }
}