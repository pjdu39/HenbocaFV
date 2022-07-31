using HenbocaFV.Estructuras;
using HenbocaFV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Domain
{
    public class TablaTest : TablaAbstract
    {
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

                var formula = v * c;

                r.Apply(idColumnaResultado, formula);

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
