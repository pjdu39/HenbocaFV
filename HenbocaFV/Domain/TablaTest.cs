using HenbocaFV.Estructuras;
using HenbocaFV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Domain
{
    public class TablaTest
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

        private int GetId(string literal)
        {
            return Cabeceras
                .Where(x => x.Literal == literal)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private decimal? GetValue(Registro r, int id)
        {
            return r.Datos
                .Where(x => x.Id == id)
                .Select(x => x.Valor)
                .FirstOrDefault();
        }

        #endregion

        // PRUEBAS

        public void Potencia()
        {
            var idVoltaje = GetId("Voltaje");
            var idCorriente = GetId("Corriente");
            var idColumnaResultado = GetId("Potencia");

            var registrosNuevos = new List<Registro>();

            foreach (var r in Registros)
            {
                var v = GetValue(r, idVoltaje);
                var c = GetValue(r, idCorriente);

                var resultado = v * c;

                r.Apply(idColumnaResultado, resultado);

                var registroFinal = new Registro(r.IdRegistro, r.Datos);

                registrosNuevos.Add(registroFinal);
            }
            
            foreach (var r in registrosNuevos)
            {
                Registra(r);
            }
        }

        public void Corriente2()
        {
            var idVoltaje = GetId("Voltaje");
            var idPotencia = GetId("Potencia");
            var idColumnaResultado = GetId("Corriente2");

            var registrosNuevos = new List<Registro>();

            foreach (var r in Registros)
            {
                var v = GetValue(r, idVoltaje);
                var p = GetValue(r, idPotencia);

                var resultado = p / v;

                r.Apply(idColumnaResultado, resultado);

                var registroFinal = new Registro(r.IdRegistro, r.Datos);

                registrosNuevos.Add(registroFinal);
            }

            foreach (var r in registrosNuevos)
            {
                Registra(r);
            }
        }
    }
}
