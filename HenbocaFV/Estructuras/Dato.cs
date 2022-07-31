using System;
using System.Collections.Generic;
using System.Text;

namespace HenbocaFV.Estructuras
{
    public class Dato
    {
        public int Id { get; private set; }
        public decimal? Valor { get; private set; }

        public Dato(int id, decimal? valor)
        {
            Id = id;
            Valor = valor;
        }

        public void SetData(int id, decimal? valor)
        {
            Id = id;
            Valor = valor;
        }
    }
}
