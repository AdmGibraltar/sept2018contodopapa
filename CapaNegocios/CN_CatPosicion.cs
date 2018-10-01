using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatPosicion
    {
        public void Lista(Posicion area, string Conexion, ref List<Posicion> List)
        {
            try
            {
                CD_CatPosicion claseCapaDatos = new CD_CatPosicion();
                claseCapaDatos.Lista(area, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Posicion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatPosicion claseCapaDatos = new CD_CatPosicion();
                claseCapaDatos.Insertar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Posicion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatPosicion claseCapaDatos = new CD_CatPosicion();
                claseCapaDatos.Modificar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
