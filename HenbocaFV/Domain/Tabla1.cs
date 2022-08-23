﻿using HenbocaFV.Estructuras;
using HenbocaFV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HenbocaFV.Domain
{
    public class Tabla1 : TablaAbstract
    {
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

                var formula = v * c + ultimaCorriente;

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
