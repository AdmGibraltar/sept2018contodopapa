using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapOrdenCompra
    {
        public void ConsultaOrdenCompra(ref OrdenCompra ordenCompra, string Conexion)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ConsultaOrdenCompra(ref ordenCompra, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SP_Autoriza_Saldo_OC(int Id_Emp, int Id_Cd, int Id_Ord, int Id_U, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Autoriza_Saldo_OC(Id_Emp, Id_Cd, Id_Ord, Id_U, Conexion, ref Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SP_Consulta_Entradas_OC(int Id_Emp, int Id_Cd, int Id_Ord, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Consulta_Entradas_OC(Id_Emp, Id_Cd, Id_Ord, Conexion, ref  Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SP_Consulta_Saldo_OC(int Id_Emp, int Id_Cd, int Id_Ord, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Consulta_Saldo_OC(Id_Emp, Id_Cd, Id_Ord, Conexion, ref  Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaOrdenCompra_Lista(OrdenCompra ordenCompra, string Conexion, ref List<OrdenCompra> List
            , int Id_Ord_inicio
            , int Id_Ord_fin
            , DateTime Ord_Fecha_inicio
            , DateTime Ord_Fecha_fin
            , string Ord_Estatus)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ConsultaOrdenCompra_Lista(ordenCompra, Conexion, ref List
                    , Id_Ord_inicio
                    , Id_Ord_fin
                    , Ord_Fecha_inicio
                    , Ord_Fecha_fin
                    , Ord_Estatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeneraOrdenCompraAutomatica(string Conexion, ref DataTable dt, string nombreTabla, int Id_Emp, int Id_Cd_Ver, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, int? validador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.GeneraOrdenCompraAutomatica(Conexion, ref dt, nombreTabla, Id_Emp, Id_Cd_Ver, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void GeneraOrdenCompraAutomatica_Lista(OrdenCompraDet ordenCompra, Sesion sesion, int proveedor, int Id_prd_RI, int Id_prd_RF, bool Prd_Transito_Aplica, ref List<OrdenCompraDet> List)
        //{
        //    try
        //    {
        //        CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
        //        claseCapaDatos.GeneraOrdenCompraAutomatica_Lista(ordenCompra, sesion, proveedor, Id_prd_RI, Id_prd_RF, Prd_Transito_Aplica, ref List);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void InsertarOrdenCompra(ref OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.InsertarOrdenCompra(ref ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra_Estatus(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra_Estatus(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarOrdenCompra_EstatusEmision(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ModificarOrdenCompra_EstatusEmision(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarOrdenCompra(OrdenCompra ordenCompra, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.EliminarOrdenCompra(ordenCompra, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarOrdCompraAutoriza(List<AutorizaOrdenCom> listaporAutorizar, Sesion session, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.InsertarOrdCompraAutoriza(listaporAutorizar, session, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public void SP_Autoriza_Saldo_OC(int Id_Emp, int Id_Cd, int Id_Ord, int Id_U, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Autoriza_Saldo_OC(Id_Emp, Id_Cd, Id_Ord, Id_U, Conexion, ref Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        /*
        public void SP_Consulta_Entradas_OC(int Id_Emp, int Id_Cd, int Id_Ord, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Consulta_Entradas_OC(Id_Emp, Id_Cd, Id_Ord, Conexion, ref  Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         */

        /*
        public void SP_Consulta_Saldo_OC(int Id_Emp, int Id_Cd, int Id_Ord, string Conexion, ref string Resultado)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.SP_Consulta_Saldo_OC(Id_Emp, Id_Cd, Id_Ord, Conexion, ref  Resultado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         */

        public void CargaOrdenaAutorizar(string Conexion, int Id_OrdCompra, ref DataTable dtPartidasOrdenAutomatica)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ConultaOrdenaAutorizar(Conexion, Id_OrdCompra, ref dtPartidasOrdenAutomatica);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void actualizarEstatus(int ordCompra, string p, ref int verificador)
        {
            throw new NotImplementedException();
        }

        public void AutorizaOrdenCompra(ref OrdenCompra ordCompra, string conexion, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.AutorizaOrdenCompra(ref ordCompra, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void GuardarURLArchivos(Sesion Sesion, int Id_OrdCompra, string NombreDoc, string URL, ref int verificador)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.GuardarURLArchivos(Sesion, Id_OrdCompra, NombreDoc, URL, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultaArchivosDesc(Sesion sesion, int Id_OrdCompra, ref DataTable dtPartidas)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.consultaArchivosDesc(sesion, Id_OrdCompra, ref dtPartidas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaNivel2(OrdenCompra ordCompra, Sesion session, ref int verificador, ref DataTable DtOrdenCompra)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.ActualizaNivel2(ordCompra, session, ref verificador, ref DtOrdenCompra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarOrdenCompra(ref OrdenCompra ordCompra, string Conexion, ref int verificador, int Partidasnoaceptadas)
        {
            try
            {
                CD_CapOrdenCompra claseCapaDatos = new CD_CapOrdenCompra();
                claseCapaDatos.InsertarOrdenCompra(ref ordCompra, Conexion, ref verificador, Partidasnoaceptadas);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
