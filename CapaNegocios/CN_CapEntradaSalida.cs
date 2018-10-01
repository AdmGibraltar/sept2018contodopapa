using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapEntradaSalida
    {
        //public void ConsultarCantidadEntradasCentroDist(ref int verificador, int Id_Cd, string Conexion)
        //{
        //    try
        //    {
        //        new CD_CapEntradaSalida().ConsultarCantidadEntradasCentroDist(ref verificador, Id_Cd, Conexion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void ConsultarCantidadSalidasCentroDist(ref int verificador, int Id_Cd, string Conexion)
        //{
        //    try
        //    {
        //        new CD_CapEntradaSalida().ConsultarCantidadSalidasCentroDist(ref verificador, Id_Cd, Conexion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void EdicionEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion,
                                      ref int verificador, int tipo_movimiento, int grupoMovimientosActivo, int afecta, bool entrada,
                                      System.Data.DataTable preciosModificar, ref string verificadorStr, int VGEmpresa)
        {
            try
            {
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida(); //xyz
                cd_capEntradaSalida.EditarEntradaSalida(ref entradaSalida, ref detalles, sesion, ref verificador, tipo_movimiento,
                                                           grupoMovimientosActivo, afecta, entrada, preciosModificar, ref verificadorStr, VGEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarConsecutivo(Sesion sesion, int naturaleza, ref int consecutivo)
        {
            try
            {
                //new CD_CapEntradaSalida().ConsultarCantidadEntradasCentroDist(ref verificador, Id_Cd, Conexion); 
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
                cd_capEntradaSalida.ConsultarConsecutivo(sesion, naturaleza, ref consecutivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEntradaSalida(Sesion sesion, int Id_Emp, int Id_Cd_Ver, int Id_Es, int Es_Naturaleza, ref EntradaSalida entradaSalida)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarEntradaSalida(sesion, Id_Emp, Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradaSalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Trae una lista de EntradasSalidas de acuerdo a los parametros de busqueda seleccionados
        /// </summary>
        /// <param name="entradasSalidas"></param>
        /// <param name="entradaSalida"></param>
        /// <param name="sesion"></param>
        /// <param name="NombreCliente"></param>
        /// <param name="ClienteIni"></param>
        /// <param name="ClienteFin"></param>
        /// <param name="ManAut"></param>
        /// <param name="ProveedorIni"></param>
        /// <param name="ProveedorFin"></param>
        /// <param name="Es_Referencia"></param>
        /// <param name="FechaIni"></param>
        /// <param name="FechaFin"></param>
        /// <param name="Estatus"></param>
        /// <param name="NumeroIni">Rango de documentos: Id inicial </param>
        /// <param name="NumeroFin">Rango de documentos: Id final</param>
        public void ConsultarEntradasSalidas(ref List<EntradaSalida> entradasSalidas, ref EntradaSalida entradaSalida, CapaEntidad.Sesion sesion,
            string NombreCliente, int ClienteIni, int ClienteFin, int ManAut, int ProveedorIni, int ProveedorFin, string Es_Referencia,
            DateTime? FechaIni, DateTime? FechaFin,
            string Estatus, int NumeroIni, int NumeroFin)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarEntradasSalidas(ref entradasSalidas, ref entradaSalida, sesion,
                    NombreCliente, ClienteIni, ClienteFin, ManAut, ProveedorIni, ProveedorFin, Es_Referencia,
                    FechaIni, FechaFin,
                    Estatus, NumeroIni, NumeroFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarEncabezadoImprimir(Sesion sesion, int Id_Emp, int Id_Cd_Ver, int Id_Es, int Es_Naturaleza, ref EntradaSalida entradasalida)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarEncabezadoImprimir(sesion, Id_Emp, Id_Cd_Ver, Id_Es, Es_Naturaleza, ref entradasalida);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarEntradaSalida_Estatus(EntradaSalida entradaSalida, string conexion, ref int verificador)
        {
            try
            {
                new CD_CapEntradaSalida().ModificarEntradaSalida_Estatus(entradaSalida, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe el id de un producto y consulta la cantidad que hay disponible
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Prd"></param>
        /// <param name="Disponible"></param>
        public void ConsultarDisponible(Sesion sesion, int Id_Prd, ref int Disponible, ref int invFinal, ref int asignado)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarDisponible(sesion, Id_Prd, ref Disponible, ref invFinal, ref asignado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe un objeto EntradaSalida y consulta sus detalles
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="entsal"></param>
        /// <param name="detalles"></param>
        public void ConsultarEntradaSalidaDetalles(Sesion sesion, EntradaSalida entsal, ref List<EntradaSalidaDetalle> detalles)//, ref DataTable dt)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarEntradaSalidaDetalles(sesion, entsal, ref detalles);//, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Asignarle el estatus de baja desde un principio
        /// </summary>
        /// <param name="entradaSalida"></param>
        /// <param name="detalles"></param>
        /// <param name="sesion"></param>
        /// <param name="verificador"></param>
        /// <param name="afecta"></param>
        /// <param name="entrada"></param>
        public void BajaEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion, ref int verificador,
                                       int afecta, bool entrada, bool actualizacionDocumento)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = null;
                new CD_CapEntradaSalida().BajaEntradaSalida(ref entradaSalida, ref detalles, sesion, ref verificador, afecta, entrada,
                                                            actualizacionDocumento, ref CapaDatos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarEntradaSalida_BajaRemisionesSIAN(ref EntradaSalida entSal, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapEntradaSalida().InsertarEntradaSalida_BajaRemisionesSIAN(ref entSal, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDisponible(EntradaSalida entsal, string Conexion, int producto, int cantidad, ref string verificador)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarDisponible(entsal, Conexion, producto, cantidad, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSaldo(int Id_Emp, int Id_Cd, string Id_PRd, string Id_Ter, string Id_Cte, string Conexion, ref int verificador, string Id_Tm)
        {
            try
            {
                new CD_CapEntradaSalida().ConsultarSaldo(Id_Emp, Id_Cd, Id_PRd, Id_Ter, Id_Cte, Conexion, ref verificador, Id_Tm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Guarda documento entrada salida. "Afecta" 1 remision, 2 orden de compra, 0 nada
        ///// </summary>
        ///// <param name="entradaSalida"></param>
        ///// <param name="detalles"></param>
        ///// <param name="sesion"></param>
        ///// <param name="verificador"></param>
        ///// <param name="tipo_movimiento"></param>
        ///// <param name="grupoMovimientosActivo"></param>
        ///// <param name="afecta">1 remision, 2 orden de compra, 0 nada</param>
        ///// <param name="entrada"></param>
        ///// <param name="actualizacionDeDocumento"></param>
        //public void GuardarEntradaSalida(ref EntradaSalida entradaSalida, ref List<EntradaSalidaDetalle> detalles, Sesion sesion,
        //                                ref int verificador, int tipo_movimiento, int grupoMovimientosActivo, int afecta, bool entrada,
        //                                System.Data.DataTable preciosModificar, ref string verificadorStr, int VGEmpresa)
        //{
        //    try
        //    {
        //        CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
        //        cd_capEntradaSalida.GuardarEntradaSalida(ref entradaSalida, ref detalles, sesion, ref verificador, tipo_movimiento,
        //                                                   grupoMovimientosActivo, afecta, entrada, preciosModificar, ref verificadorStr, VGEmpresa);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        public void GuardarEntradaSalida(EntradaSalida entsal, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion)
        {
            try
            {
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
                cd_capEntradaSalida.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EdicionEntradaSalida(EntradaSalida entsal, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion)
        {
            try
            {
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
                cd_capEntradaSalida.EdicionEntradaSalida(entsal, listaDetalle, verificadorStr, strEmp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaTMov(ref Movimientos mov, string Conexion, string bdCentral)
        {
            try
            {
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
                cd_capEntradaSalida.ConsultaTMov(ref mov, Conexion, bdCentral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaTProveedor(ref Movimientos mov, string Conexion, string bdCentral)
        {
            try
            {
                CD_CapEntradaSalida cd_capEntradaSalida = new CD_CapEntradaSalida();
                cd_capEntradaSalida.ConsultaTProveedor(ref mov, Conexion, bdCentral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void Bitacora_BajaRemisionesSIAN(ref EntradaSalida entSal, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapEntradaSalida().Bitacora_BajaRemisionesSIAN(ref entSal, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}