using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Correos
    {
        public void Guardar(CapaEntidad.Correos correo, string Conexion, ref int verificador)
        {
            try
            {
                CD_Correos cd_correos = new CD_Correos();
                cd_correos.Guardar(correo, Conexion, ref verificador);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Consultar(ref CapaEntidad.Correos correo, string Conexion)
        {
            try
            {
                CD_Correos cd_correos = new CD_Correos();
                cd_correos.Consultar(ref correo, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
