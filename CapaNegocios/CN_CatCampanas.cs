using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CatCampanas
    {
        public void ConsultaCampanas(Campanas Campana, string Conexion, ref List<Campanas> List)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ConsultaCampanas(Campana, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCampanaProducto(Campanas Campana, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ConsultaCampanaProducto(Campana, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCampanas(Campanas Campana,  List<Producto> ListProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.InsertarCampanas(Campana, ListProducto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCampanas(Campanas Campana, List<Producto> ListProducto, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ModificarCampanas(Campana, ListProducto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

      

        public void ConsultaCampanasCombo(ref Campanas Campana, string Conexion)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                //claseCapaDatos.ConsultaCampanasCombo(ref Campana, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCampana(ref Campanas Campana, string Conexion)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ConsultaCampana(ref Campana, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCampanaOportunidad(ref Campanas Campana, string Conexion)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ConsultaCampanaOportunidad(ref Campana, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRuta(ref Campanas Campana, string Conexion)
        {
            try
            {
                CD_CatCampanas claseCapaDatos = new CD_CatCampanas();
                claseCapaDatos.ConsultaRuta(ref Campana, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
