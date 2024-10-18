namespace SysTINSClass
{
    public class Class1
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        // XML Docs
        /// <summary>
        /// Calcula o valor das parcelas pela divisão do montante pelo número de parcelas
        /// respeitando as RNs 25 e 26.
        /// </summary>
        /// <param name="valor">montante a ser parcelado</param>
        /// <param name="nparc">o número de parcelas (de acordo a RN25)</param>
        /// <returns>o valor de cada parcela (de acordo com a RN26)</returns>
        public double Calcular(double valor, int nparc) 
        {
            return valor / nparc;
        }

        /// <summary>
        /// Insere uma class1
        /// não tem retorno de valor
        /// </summary>
        public void Inserir() // isso aqui insere uma tabela 
        { 
        
        }

    }
}
