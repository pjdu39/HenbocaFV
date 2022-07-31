using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Estructuras
{
    public class Registro
    {
        public int? IdRegistro { get; private set; }
        public List<Dato> Datos { get; private set; }

        public Registro(List<Dato> datos)
        {
            IdRegistro = null;
            Datos = datos;
        }

        public Registro(int? id, List<Dato> datos)
        {
            IdRegistro = id;
            Datos = datos;
        }

        public void Update(Registro r)
        {
            Datos = r.Datos;
        }

        public void SetId(int id)
        {
            IdRegistro = id;
        }

        // TODO: Método para añadir un dato al registro

        public void AddData(Dato dato)
        {
            Datos.Add(dato);
        }
    }
}
