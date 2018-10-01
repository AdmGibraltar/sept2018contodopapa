using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo.SIANCentral;

namespace CapaDatos.SIANCentral
{
    public class CD_CatCuotasCRM
    {
        /// <summary>
        /// Regresa el resultado de la consulta al repositorio Cat_CuotaCrm condicionando por año y mes
        /// </summary>
        /// <param name="anyo">Año de interés para las cuotas</param>
        /// <param name="mes">Mes de interés para las cuotas</param>
        /// <param name="icdSianCentralCtx">Contexto de conexión a la fuente de datos</param>
        /// <returns></returns>
        public IEnumerable<CatCuotasCrm> ConsultarPorAnyoMes(int anyo, int mes, ICD_Contexto icdSianCentralCtx)
        {
            SIANCentralEntities1 ctx = ((ICD_Contexto<SIANCentralEntities1>)icdSianCentralCtx).Contexto;
            var cuotas = from c in ctx.CatCuotasCrms
                         where c.Cuo_Anio==anyo && c.Cuo_Mes==mes
                         select c;
            return cuotas;
        }
    }
}
