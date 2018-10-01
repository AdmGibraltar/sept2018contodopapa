using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapRemision
    {
        public bool ConsultaRemisionFacturacion(ref Remision remision, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();

                return claseCapaDatos.ConsultaRemisionFacturacion(ref remision, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ConsultaCantidadRemision(Sesion sesion, int prd, string remisiones)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();

                return claseCapaDatos.ConsultaCantidadRemision(sesion, prd, remisiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionDetalleContratoComodato(ref Remision remision, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaRemisionDetalleContratoComodato(ref remision, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionDetalleFacturacion(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaRemisionDetalleFacturacion(ref remision, ref listaFacrutaRemision, Id_Rem_Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDevolucionRemisionDetalleFacturacion(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaDevolucionRemisionDetalleFacturacion(ref remision, ref listaFacrutaRemision, Id_Rem_Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDevolucionRemisionDetalleFacturacionAgrupado(ref Remision remision, ref DataTable listaFacrutaRemision, string Id_Rem_Lista, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaDevolucionRemisionDetalleFacturacionAgrupado(ref remision, ref listaFacrutaRemision, Id_Rem_Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesVencidas(Sesion session, Remision pRem, ref List<RemisionesVencidas> pRemList)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaRemisionesVencidas(session, pRem, ref pRemList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarRemision(Remision remision, List<RemisionDet> detalles, Sesion sesion,
                        ref int verificador, bool actualizacionDocumento, bool Gen_Contrato, ref int Id_Rem, ref bool tipoMovimento, ref string mensaje)
        {
            try
            {
                new CD_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, actualizacionDocumento, Gen_Contrato, ref Id_Rem, ref tipoMovimento, ref mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta las remisiones de acuerdo a los filtros de busqueda de la pantalla de remisiones_lista
        /// </summary>
        /// <param name="remisiones"></param>
        /// <param name="remision"></param>
        /// <param name="sesion"></param>
        /// <param name="NombreCliente"></param>
        /// <param name="ClienteIni"></param>
        /// <param name="ClienteFin"></param>
        /// <param name="ManAut"></param>
        /// <param name="FechaIni"></param>
        /// <param name="FechaFin"></param>
        /// <param name="Estatus"></param>
        /// <param name="NumeroIni"></param>
        /// <param name="NumeroFin"></param>
        /// <param name="Pedido"></param>
        public void ConsultarRemisiones(ref List<Remision> remisiones, ref Remision remision, CapaEntidad.Sesion sesion,
            string NombreCliente, int ClienteIni, int ClienteFin, int ManAut,
            DateTime? FechaIni, DateTime? FechaFin, string Estatus, int NumeroIni, int NumeroFin, int Pedido, string OrdenDeCompra, int IdTm)
        {
            try
            {
                new CD_CapRemision().ConsultarRemisiones(ref remisiones, ref remision, sesion, NombreCliente,
                                                        ClienteIni, ClienteFin, ManAut, FechaIni, FechaFin,
                                                        Estatus, NumeroIni, NumeroFin, Pedido, OrdenDeCompra, IdTm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta los datos para el encabezado del reporte a imprimir
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd_Ver"></param>
        /// <param name="Id_Rem"></param>
        /// <param name="remision"></param>
        public void ConsultarEncabezadoImprimir(Sesion sesion, /*int Id_Emp, int Id_Cd_Ver,*/ int Id_Rem, ref Remision remision, int grabarContrato)
        {
            try
            {
                new CD_CapRemision().ConsultarEncabezadoImprimir(sesion, /*Id_Emp, Id_Cd_Ver,*/ Id_Rem, ref remision, grabarContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemision_Estatus(Remision remision, string conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemision().ModificarRemision_Estatus(remision, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemisiones_Estatus(int Id_Emp, int Id_Cd, string remisiones, string estatus, string conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemision().ModificarRemisiones_Estatus(Id_Emp, Id_Cd, remisiones, estatus, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesDetalle(Sesion sesion, Remision remision, ref List<RemisionDet> detalles)
        {
            try
            {
                new CD_CapRemision().ConsultarRemisionesDetalle(sesion, remision, ref detalles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void BajaRemision(ref Remision remision, ref List<RemisionDet> detalles, Sesion sesion, ref int verificador)
        {

            try
            {
                new CD_CapRemision().BajaRemision(ref remision, ref detalles, sesion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ConsultarReferenciaDet(Sesion sesion, int Id_Rem, int Id_Prd, ref int verificador)
        //{
        //    //RM
        //    try
        //    {
        //        new CD_CapRemision().ConsultarReferenciaDet(sesion, Id_Rem, Id_Prd, ref verificador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// consulta la cantidad de partidas de una remision, que tienen saldo disponible para devolucion
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="id_remision"></param>
        /// <param name="partidasConSaldo"></param>     
        public void ConsultarPartidasConSaldo(Sesion sesion, int id_remision, ref int partidasConSaldo)
        {//RM
            try
            {
                new CD_CapRemision().ConsultarPartidasConSaldo(sesion, id_remision, ref partidasConSaldo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Remision> ConsultaProductosNotaCargo(ref Remision remision, string Conexion)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                List<Remision> NotaCargo = claseCapaDatos.ConsultaProductosRemision(ref remision, Conexion);
                return NotaCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionEspecialDetalle(ref List<RemisionDet> listaRemisionProductos, string Conexion, int id_Emp, int id_Cd, int id_Nca, int id_Cte)
        {
            try
            {
                CD_CapRemision CDCapRemision = new CD_CapRemision();
                CDCapRemision.ConsultaRemisionEspecialDetalle(ref listaRemisionProductos, Conexion, id_Emp, id_Cd, id_Nca, id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidad(int Id_Emp, int Id_Cd, string refe, string folio, int Prd_AgrupadoSpo, ref int cantidad, string conexion)
        {
            try
            {
                CD_CapRemision CDCapRemision = new CD_CapRemision();
                CDCapRemision.ConsultarRemisionesCantidad(Id_Emp, Id_Cd, refe, folio, Prd_AgrupadoSpo, ref cantidad, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidadRem(int Id_Emp, int Id_Cd, string _ref, int id_prd, ref int cantidadES, string conexion)
        {
            try
            {
                CD_CapRemision CDCapRemision = new CD_CapRemision();
                CDCapRemision.ConsultarRemisionesCantidadRem(Id_Emp, Id_Cd, _ref, id_prd, ref cantidadES, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRemisionesCantidadRemCantidad(int Id_Emp, int Id_Cd, int Id_ES, int id_prd, string nat, ref int cantidadES2, string conexion)
        {
            try
            {
                CD_CapRemision CDCapRemision = new CD_CapRemision();
                CDCapRemision.ConsultarRemisionesCantidadRemCantidad(Id_Emp, Id_Cd, Id_ES, id_prd, nat, ref cantidadES2, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesxFactura(Sesion sesion, int Factura, ref List<Remision> remision)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaRemisionesxFactura(sesion, Factura, ref remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaRemisionesxPedido(Sesion sesion, int Pedido, ref List<Remision> remision)
        {
            try
            {
                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ConsultaRemisionesxPedido(sesion, Pedido, ref remision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarPermitirModificar(int Id_Rem, int Id_Emp, int Id_Cd, string conexion, ref int verificador)
        {
            try
            {
                CD_CapRemision CDCapRemision = new CD_CapRemision();
                CDCapRemision.ConsultarPermitirModificar(Id_Rem, Id_Emp, Id_Cd, conexion, ref  verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarRemision_PDF(Remision remision, string conexion, ref int verificador)
        {
            try
            {
                new CD_CapRemision().ModificarRemision_PDF(remision, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidaMontosImpresion(Remision remision, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool verificador)
        {
            try
            {
                new CD_CapRemision().ValidaMontosImpresion(remision, Id_Cd, Id_Emp, iTipoDocumento, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EsMargenMetaExcedido(int? id_Ter, int? id_Cte, int? id_TG, DateTime? fechaInicial, DateTime? fechaFinal, Sesion s)
        {
            CD_CapRemision cdCapRemision = new CD_CapRemision();
            var upr = cdCapRemision.CalcularUtilidadPrimaReal(s.Id_Emp, s.Id_Cd, id_Ter, id_Cte, id_TG, fechaInicial, fechaFinal, s.Emp_Cnx_EF);
            CN_CapAcysDatosGarantia cnCapAcysDatosGarantia = new CN_CapAcysDatosGarantia();
            var datosAcys = cnCapAcysDatosGarantia.Consultar(s.Id_Emp, s.Id_Cd, id_Ter, id_Cte, id_TG, s);

            return (Decimal)datosAcys.UPrimaNeta.Value < upr * 100;
        }


        public void ValidarRemisionesPendientes(Sesion sesion, int Pedido, ref List<Remision> remision)
        {
            try
            {

                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ValidarRemisionesPendientes(sesion, Pedido, ref remision);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ValidarRemisionesPendientesCliente(Sesion sesion, Remision remision, ref List<Remision> remisiones)
        {
            try
            {

                CD_CapRemision claseCapaDatos = new CD_CapRemision();
                claseCapaDatos.ValidarRemisionesPendientesCliente(sesion, remision, ref remisiones);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
