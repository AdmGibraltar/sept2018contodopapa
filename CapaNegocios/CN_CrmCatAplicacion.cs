using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmCatAplicacion
    {
        public void Lista(CapaEntidad.Aplicacion aplicacion, string Conexion, ref List<CapaEntidad.Aplicacion> List)
        {
            try
            {
                CD_CrmCatAplicacion cd_aplicacion = new CD_CrmCatAplicacion();
                cd_aplicacion.Lista(aplicacion, ref List, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(CapaEntidad.Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CrmCatAplicacion cd_aplicacion = new CD_CrmCatAplicacion();
                cd_aplicacion.Insertar(aplicacion, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Eliminar(CapaEntidad.Aplicacion aplicacion, ref int verificador, string Conexion)
        {
            try
            {
                CD_CrmCatAplicacion cd_aplicacion = new CD_CrmCatAplicacion();
                cd_aplicacion.Eliminar(aplicacion, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(CapaEntidad.Aplicacion aplicacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CrmCatAplicacion cd_aplicacion = new CD_CrmCatAplicacion();
                cd_aplicacion.Modificar(aplicacion, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
