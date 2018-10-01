using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatEstatus
    {
        public void Insertar(CapaEntidad.Estatus estatus, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatEstatus claseCapaDatos = new CD_CatEstatus();
                claseCapaDatos.Insertar(estatus, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(CapaEntidad.Estatus estatus, string Conexion, ref List<CapaEntidad.Estatus> List)
        {
            try
            {
                CD_CatEstatus claseCapaDatos = new CD_CatEstatus();
                claseCapaDatos.Lista(estatus, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Borrar(CapaEntidad.Estatus estatus, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatEstatus claseCapaDatos = new CD_CatEstatus();
                claseCapaDatos.Borrar(estatus, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
