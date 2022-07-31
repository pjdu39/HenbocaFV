using HenbocaFV.Estructuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Extensions
{
    public static class Extensions
    {
        public static void SetValue<T>(this IEnumerable<T> query, Action<T> action)
            where T : class
        {
            foreach (var item in query)
            {
                action(item);
            }
        }

        public static void Apply(this Registro r, int id, decimal? valor)
        {
            foreach (var dato in r.Datos.Where(x => x.Id == id))
            {
                dato.SetData(id, valor);
            }
        }
    }
}
