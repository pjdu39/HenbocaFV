using HenbocaFV.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HenbocaFV
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Lectura

            var tablaTest = new TablaTest();

            var rawHeaders = File.ReadAllLines("C:\\Celena\\CSV_de_pruebas.csv")
                                    .FirstOrDefault();

            tablaTest.Cabeceras = UnitOfWork.CreateHeader(rawHeaders);

            foreach (var header in tablaTest.Cabeceras)
            {
                Console.WriteLine(header);
            }

            var lines = File.ReadAllLines("C:\\Celena\\CSV_de_pruebas.csv")
                                           .Skip(1)
                                           .ToList();

            foreach (var line in lines)
            {
                var registro = UnitOfWork.FromCsv(line);
                tablaTest.Registra(registro);
            }

            foreach (var r in tablaTest.Registros)
            {
                Console.WriteLine(r.Datos.Select(x => x.Valor));
            }

            #endregion

            #region Funciones

            tablaTest.Potencia();
            tablaTest.Corriente2();

            #endregion

            #region Escritura

            using StreamWriter writer = new StreamWriter("C:\\Celena\\CVS_pruebas_de_escritura.csv");

            var csv = string.Empty;

            foreach (var c in tablaTest.Cabeceras)
            {
                csv += $"{ c.Literal };";
            }

            csv += Environment.NewLine;

            foreach (var r in tablaTest.Registros)
            {
                foreach (var dato in r.Datos)
                {
                    csv += $"{ dato.Valor };";
                }

                csv += Environment.NewLine;
            }

            writer.WriteLine(csv);

            #endregion
        }
    }
}
