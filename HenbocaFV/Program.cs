﻿using HenbocaFV.Domain;
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
            // Hacer un método que cree instancias las tablas que se van a usar. Algo como tabla = CreateTable(tablaChild, "path")

            #region Lectura

            //var tabla1 = UnitOfWork.CreateTable("C:\\Celena\\CSV_de_pruebas.csv");

            var tabla1 = new Tabla1();

            var rawHeaders = File.ReadAllLines("C:\\Celena\\CSV_de_pruebas.csv")
                                    .FirstOrDefault();

            tabla1.Cabeceras = UnitOfWork.CreateHeader(rawHeaders);

            foreach (var header in tabla1.Cabeceras)
            {
                Console.WriteLine(header);
            }

            var lines = File.ReadAllLines("C:\\Celena\\CSV_de_pruebas.csv")
                                           .Skip(1)
                                           .ToList();

            foreach (var line in lines)
            {
                var registro = UnitOfWork.FromCsv(line);
                tabla1.Registra(registro);
            }

            foreach (var r in tabla1.Registros)
            {
                Console.WriteLine(r.Datos.Select(x => x.Valor));
            }

            #endregion

            #region Funciones

            tabla1.Potencia();

            #endregion

            #region Escritura

            using StreamWriter writer = new StreamWriter("C:\\Celena\\CVS_pruebas_de_escritura.csv");

            var csv = string.Empty;

            foreach (var c in tabla1.Cabeceras)
            {
                csv += $"{ c.Literal };";
            }

            csv += Environment.NewLine;

            foreach (var r in tabla1.Registros)
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
