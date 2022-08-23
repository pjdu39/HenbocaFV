using HenbocaFV.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV
{
    public static class UnitOfWork
    {
        public static List<Cabecera> CreateHeader(string csvLine)
        {
            string[] values = csvLine.Split(';');

            var result = new List<Cabecera>();

            for (int i = 0; i < values.Length; i++)
            {
                result.Add(new Cabecera(i, values[i]));
            }

            return result;
        }

        /// <summary>
        /// Crea un registro con todos los datos de una línea del csv. El Id del dato debe coincidir con el Id de su cabecera.
        /// </summary>
        /// <param name="csvLine"></param>
        /// <returns></returns>
        public static Registro FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');

            var datos = new List<Dato>();

            for(int i=0; i<values.Length; i++)
            {
                var value = StringToDecimal(values[i]);

                datos.Add(
                        new Dato(i, value)
                    );
            }

            return new Registro(datos);
        }

        public static decimal? StringToDecimal(string s)
        {
            decimal? result;

            try
            {
                result = Convert.ToDecimal(s);
            }
            catch (Exception e)
            {
                // TODO: Preguntar a Celena cómo se va a operar con este dato cuando sea erróneo
                result = 0;

                if (string.IsNullOrEmpty(s))
                {
                    result = null;
                }

            }

            return result;
        }

        public static decimal? GetLastValue(List<Registro> registros, int idColumna)
        {
            var result = registros
                .Select(x => x.Datos
                    .Where(y => y.Id == idColumna)
                    .Select(y => y.Valor)
                    )
                .LastOrDefault()
                .FirstOrDefault();

            return result;
        }
    }
}
