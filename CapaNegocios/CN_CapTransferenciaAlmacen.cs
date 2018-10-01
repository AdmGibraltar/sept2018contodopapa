using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapTransferenciaAlmacen
    {
        public void ConsultaTransferenciaAlmacen(ref TransferenciaAlmacen TransferenciaAlmacen, string Conexion)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.ConsultaTransferenciaAlmacen(ref TransferenciaAlmacen, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTransferenciaAlmacen_Lista(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref List<TransferenciaAlmacen> List
            , int Id_Trans_inicio
            , int Id_Trans_fin
            , DateTime Trans_Fecha_inicio
            , DateTime Trans_Fecha_fin
            , string Trans_Estatus)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.ConsultaTransferenciaAlmacen_Lista(TransferenciaAlmacen, Conexion, ref List
                    , Id_Trans_inicio
                    , Id_Trans_fin
                    , Trans_Fecha_inicio
                    , Trans_Fecha_fin
                    , Trans_Estatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraTransferenciaAlmacenAutomatica(string Conexion, ref DataTable dt, string nombreTabla, int Id_Emp, int Id_Cd_Ver, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, int? validador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.GeneraTransferenciaAlmacenAutomatica(Conexion, ref dt, nombreTabla, Id_Emp, Id_Cd_Ver, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GeneraTransferenciaAlmacenAutomatica_Lista(TransferenciaAlmacenDet TransferenciaAlmacen, Sesion sesion, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, ref List<TransferenciaAlmacenDet> List)
        //{
        //    try
        //    {
        //        CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
        //        claseCapaDatos.GeneraTransferenciaAlmacenAutomatica_Lista(TransferenciaAlmacen, sesion, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, ref List);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /*
        public void InsertarTransferenciaAlmacen(ref TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.InsertarTransferenciaAlmacen(ref TransferenciaAlmacen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        /*
        public void ModificarTransferenciaAlmacen(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.ModificarTransferenciaAlmacen(TransferenciaAlmacen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        /*
        public void ModificarTransferenciaAlmacen_Estatus(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.ModificarTransferenciaAlmacen_Estatus(TransferenciaAlmacen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

      /*  public void ModificarTransferenciaAlmacen_EstatusEmision(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.ModificarTransferenciaAlmacen_EstatusEmision(TransferenciaAlmacen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarTransferenciaAlmacen(TransferenciaAlmacen TransferenciaAlmacen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapTransferenciaAlmacen claseCapaDatos = new CD_CapTransferenciaAlmacen();
                claseCapaDatos.EliminarTransferenciaAlmacen(TransferenciaAlmacen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
    }
}
