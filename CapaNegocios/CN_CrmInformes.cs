using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CrmInformes
    {
        public void ConsultarUENSegmentosTerritoriosSucursal(int Id_Emp, int Id_Cd, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.ConsultarUENSegmentosTerritoriosSucursal(Id_Emp, Id_Cd, ref ds, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarUENS(int Id_Emp, int Id_Cd, int Id_Rik, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarUENS(Id_Emp, Id_Cd, Id_Rik, ref dt, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarSegmentos(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Uen, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarSegmentos(Id_Emp, Id_Cd, Id_Rik, Id_Uen, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarAreasSegmento(bool activo, int Id_Emp, int Id_Seg, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarAreasSegmento(activo, Id_Emp, Id_Seg, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarTerritoriosRik(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Seg, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarTerritoriosRik(Id_Emp, Id_Cd, Id_Rik, Id_Seg, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarSolucionesArea(int Id_Emp, int Id_Area, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarSolucionesArea(Id_Emp, Id_Area, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarAplicacionesSoluciones(int Id_Emp, int Id_Solucion, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.CargarAplicacionesSoluciones(Id_Emp, Id_Solucion, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GenerarControlPromocion_Limpieza(int Id_Emp, int Id_Cd, string Id_Rik, int IntConsulta, double monto1, double monto2, ref DataSet ds, string Conexion)
        //{
        //    try
        //    {
        //        CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
        //        claseCapaDatos.GenerarControlPromocion_Limpieza(Id_Emp, Id_Cd, Id_Rik, IntConsulta, monto1, monto2, ref ds, Conexion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void GenerarControlPromocion_LimpiezaAplicacion(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, bool PNuevo, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.GenerarControlPromocion_LimpiezaAplicacion(Id_Emp, Id_Cd,Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, PNuevo,ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GenerarCierreMes(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, bool PNuevo, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.GenerarCierreMes(Id_Emp, Id_Cd, Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, PNuevo, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void GenerarControlPromocion(int Id_Emp, int Id_Cd, int Id_U, string Id_Rik, int periodo, int IntConsulta, string monto1, string monto2, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.GenerarControlPromocion(Id_Emp, Id_Cd, Id_U, Id_Rik, periodo, IntConsulta, monto1, monto2, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_GteSegmento(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, int IntConsulta, int Id_GteSeg, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.spCRM_ControlPromocion_GteSegmento(Id_Emp, Id_Cd, periodo, Id_Rik, IntConsulta, Id_GteSeg, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlEntrada(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.spCRM_ControlEntrada(Id_Emp, Id_Cd, periodo, Id_Rik, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_DII(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.spCRM_ControlPromocion_DII(Id_Emp, Id_Cd, periodo, Id_Rik, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_ControlPromocion_DIINumero(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.spCRM_ControlPromocion_DIINumero(Id_Emp, Id_Cd, periodo, Id_Rik, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCRM_Campana(int Id_Emp, int Id_Cd, int periodo, string Id_Rik, ref DataSet ds, string Conexion)
        {
            try
            {
                CD_CrmInformes claseCapaDatos = new CD_CrmInformes();
                claseCapaDatos.spCRM_Campana(Id_Emp, Id_Cd, periodo, Id_Rik, ref ds, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
