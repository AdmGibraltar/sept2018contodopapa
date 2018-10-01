using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatMeta
    {
        public void Lista(Meta meta, string Conexion, ref List<Meta> List)
        {
            try
            {
                CD_CatMeta claseCapaDatos = new CD_CatMeta();
                claseCapaDatos.Lista(meta, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Meta meta, Sesion sesion, ref int verificador)
        {
            try
            {
                CD_CatMeta claseCapaDatos = new CD_CatMeta();
                claseCapaDatos.Modificar(meta, sesion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(int Id_U, ref Meta meta, string Conexion)
        {
            try
            {
                CD_CatMeta claseCapaDatos = new CD_CatMeta();
                claseCapaDatos.Consultar(Id_U, ref meta, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
