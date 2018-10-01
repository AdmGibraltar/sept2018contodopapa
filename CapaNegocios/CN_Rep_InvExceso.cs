using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Collections;
using System.Data;

namespace CapaNegocios
{
    public class CN_Rep_InvExceso
    {
        public void Consulta(RepExcesos exceso, string Conexion, ref List<RepExcesos> List)
        {
            try
            {
                CD_Rep_InvExceso claseCapaDatos = new CD_Rep_InvExceso();
                claseCapaDatos.Consulta(exceso, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Consulta3(RepExcesos exceso, string Conexion, ref List<RepExcesos> List)
        {
            try
            {
                CD_Rep_InvExceso claseCapaDatos = new CD_Rep_InvExceso();
                claseCapaDatos.Consulta3(exceso, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaGrafica(RepExcesos exceso, ref  DataTable valores, string Conexion)
        {
            try
            {
                CD_Rep_InvExceso claseCapaDatos = new CD_Rep_InvExceso();
                claseCapaDatos.ConsultaGrafica(exceso, ref valores, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
