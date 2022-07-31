using System;
using System.Collections.Generic;
using System.Text;

namespace HenbocaFV.Estructuras
{
    public class Cabecera
    {
        public int Id { get; }
        public string Literal { get; }

        public Cabecera(int id, string literal)
        {
            Id = id;
            Literal = literal;
        }
    }
}
