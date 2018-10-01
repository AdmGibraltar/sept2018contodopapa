using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CrmContacto
    {
        public void Insertar(CapaEntidad.Contacto contacto, ref int verificador, string Conexion)
        {
            try
            {
                CD_CrmContacto cd_catcliente = new CD_CrmContacto();
                cd_catcliente.Insertar(contacto, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(CapaEntidad.Contacto contacto, ref int verificador, string Conexion)
        {
            try
            {
                CD_CrmContacto cd_catcliente = new CD_CrmContacto();
                cd_catcliente.Modificar(contacto, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Consulta(Contacto contacto, ref System.Data.DataSet dsContacto, string Conexion)
        {
            try
            {
                CD_CrmContacto cd_catcliente = new CD_CrmContacto();
                cd_catcliente.Consulta(contacto, ref dsContacto, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
    }
}
