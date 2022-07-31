using HenbocaFV.Estructuras;
using HenbocaFV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Domain
{
    public abstract class TablaAbstract
    {
        public List<Cabecera> Cabeceras { get; set; } = new List<Cabecera>();
        public List<Registro> Registros { get; set; } = new List<Registro>();

        public void Registra(Registro r)
        {
            var id = r.IdRegistro;

            if (Registros.Select(x => x.IdRegistro).Contains(id) && id != null)
            {
                // Modifica un registro existente
                Registros.Where(x => x.IdRegistro == id).FirstOrDefault().Update(r);
            }
            else
            {
                // Crea un nuevo registro
                var newId = Registros.Count();
                r.SetId(newId);
                Registros.Add(r);
            }
        }

        #region Utilidades comunes

        protected int GetId(string literal)
        {
            return Cabeceras
                .Where(x => x.Literal == literal)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        protected decimal? GetValue(Registro r, int id)
        {
            return r.Datos
                .Where(x => x.Id == id)
                .Select(x => x.Valor)
                .FirstOrDefault();
        }

        #endregion
    }
}
