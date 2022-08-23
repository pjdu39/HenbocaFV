using HenbocaFV.Estructuras;
using HenbocaFV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Domain
{
    public class Tabla1 : TablaAbstract
    {
        private decimal? P_MPP_STC = 460;

        public void Potencia()
        {
            var idVoltaje = GetId("Voltaje");
            var idCorriente = GetId("Corriente");
            var idColumnaResultado = GetId("Potencia");

            var registrosNuevos = new List<Registro>();

            var ultimaCorriente = Registros
                .Select(x => x.Datos
                    .Where(y => y.Id == idVoltaje)
                    .Select(y => y.Valor)
                    )
                .LastOrDefault()
                .FirstOrDefault();

            foreach (var r in Registros)
            {
                var v = GetValue(r, idVoltaje);
                var c = GetValue(r, idCorriente);
                decimal? formula; //? significa que admite nulos

                if (c>2)
                {
                    formula = v * c;
                }
                else
                {
                    formula = 0;
                }

                r.Apply(idColumnaResultado, formula);

                var registroFinal = new Registro(r.IdRegistro, r.Datos);

                registrosNuevos.Add(registroFinal);
            }
            
            foreach (var r in registrosNuevos)
            {
                Registra(r);
            }
        }
        public void Energia()
        {
            var idPAC = GetId("P_AC (W)");
            //var idCorriente = GetId("Corriente");
            var idColumnaResultado = GetId("Energia");

            var registrosNuevos = new List<Registro>();

            decimal? registroAnterior = 0;

            foreach (var r in Registros)
            {
                var v = GetValue(r, idPAC);
                //var c = GetValue(r, idCorriente);
                decimal? formula; //? significa que admite nulos

                formula = v * 5 / 3600; 
                
                registroAnterior += formula; //+= significa que añade formula al valor que ya tenía, se va acumulando

                r.Apply(idColumnaResultado, registroAnterior); //OJO: depende del caso poner formula o registroAnterior

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
