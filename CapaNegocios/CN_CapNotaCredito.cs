using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapNotaCredito
    {
        public void ConsultarCantidadNotaCreditoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapNotaCredito().ConsultarCantidadNotaCreditoCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCredito_Buscar(NotaCredito notaCredito, ref List<NotaCredito> listaNotaCredito, string Conexion
            , string Nombre
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Ncr_Fecha_inicio
            , DateTime? Ncr_Fecha_fin
            , string Ncr_Estatus
            , int? Id_Ncr_inicio
            , int? Id_Ncr_fin
            , int? Id_U)
        {
            try
            {
                new CD_CapNotaCredito().ConsultaNotaCredito_Buscar(notaCredito, ref listaNotaCredito, Conexion
                    , Nombre
                    , Id_Cte_inicio
                    , Id_Cte_fin
                    , Ncr_Fecha_inicio
                    , Ncr_Fecha_fin
                    , Ncr_Estatus
                    , Id_Ncr_inicio
                    , Id_Ncr_fin
                    , Id_U);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNotaCredito(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                new CD_CapNotaCredito().ConsultarNotaCredito(ref notaCredito, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMovsNotaCredito(Movimientos mov, ref List<Movimientos> listaMovimientos, string Conexion)
        {
            try
            {
                new CD_CapNotaCredito().ConsultarMovsNotaCredito(mov, ref listaMovimientos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarEmpleados(int Id_Emp, int Id_Cd, ref List<Usuario> listaUsuarios, string Conexion)
        {
            try
            {
                new CD_CapNotaCredito().ConsultarEmpleados(Id_Emp, Id_Cd, ref listaUsuarios, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNotaCreditoSAT(ref NotaCredito notaCredito, string Conexion, ref object resultado)
        {
            try
            {
                new CD_CapNotaCredito().ConsultarNotaCreditoSAT(ref notaCredito, Conexion, ref resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarNotaCredito(ref NotaCredito notaCredito, string Conexion, ref int verificador, List<AdendaDet> listAdendaCabecera, System.Data.DataTable ListaProductosNotaCredito)
        {
            try
            {
                new CD_CapNotaCredito().InsertarNotaCredito(ref notaCredito, Conexion, ref verificador, listAdendaCabecera, ListaProductosNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCredito(ref NotaCredito notaCredito, string Conexion, ref int verificador, List<AdendaDet> listAdendaCabecera, System.Data.DataTable ListaProductosNotaCredito)
        {
            try
            {
                new CD_CapNotaCredito().ModificarNotaCredito(ref notaCredito, Conexion, ref verificador, listAdendaCabecera, ListaProductosNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCreditoSAT(NotaCredito notaCredito, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapNotaCredito().ModificarNotaCreditoSAT(notaCredito, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarNotaCredito(ref NotaCredito notaCredito, string Conexion, int verificador)
        {
            try
            {
                new CD_CapNotaCredito().EliminarNotaCredito(notaCredito, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAdendaNota(Sesion sesion, int Id_Emp, int Id_Cd, int Id_Cte, ref List<NotaCredito> listNotaCredito)
        {
            try
            {
                CD_CapNotaCredito claseCapaDatos = new CD_CapNotaCredito();
                claseCapaDatos.ConsultaAdendaNota(sesion, Id_Emp, Id_Cd, Id_Cte, ref listNotaCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgregarAdenda(NotaCredito notaCredito, Sesion sesion, ref int verificador)
        {
            try
            {
                CD_CapNotaCredito claseCapaDatos = new CD_CapNotaCredito();
                claseCapaDatos.AgregarAdenda(notaCredito, sesion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotaCredito> ConsultaProductosNotaCredito(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                CD_CapNotaCredito claseCapaDatos = new CD_CapNotaCredito();
                List<NotaCredito> notaCredito1 = claseCapaDatos.ConsultaProductosNotaCredito(ref notaCredito, Conexion);
                return notaCredito1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCreditoEspecialDetalle(ref List<NotaCreditoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Nca,string Id_NcrSerie, int id_Cte)
        {
            try
            {
                CD_CapNotaCredito CDCapNotaCredito = new CD_CapNotaCredito();
                CDCapNotaCredito.ConsultaNotaCreditoEspecialDetalle(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Nca,Id_NcrSerie, id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ArchivoPdf_Xml(ref NotaCredito notaCredito, string Conexion)
        {
            try
            {
                CD_CapNotaCredito claseCapaDatos = new CD_CapNotaCredito();
                claseCapaDatos.ArchivoPdf_Xml(ref notaCredito, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Ncr,string Id_NcrSerie, string Tipo1, string Tipo2, ref List<AdendaDet> listCabT, ref List<AdendaDet> listDetT, string Conexion)
        {
            try
            {
                CD_CapNotaCredito CDCapNotaCredito = new CD_CapNotaCredito();
                CDCapNotaCredito.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Id_Ncr,Id_NcrSerie, Tipo1, Tipo2, ref  listCabT, ref listDetT, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCredito_Estatus(NotaCredito factura2, string p, ref int verificador)
        {
            throw new NotImplementedException();
        }

        public void ValidarEstatusFactura(int Id_Emp, int Id_Cd, int Id_Ncr,string Id_NcrSerie, string Conexion, ref int Verificador)
        {
            try
            {
                CD_CapNotaCredito cd_nc = new CD_CapNotaCredito();

                cd_nc.ValidarEstatusFactura(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie,Conexion, ref Verificador);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public void ValidaMontosImpresion(NotaCredito nc, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool verificador)
        {
            try
            {
                new CD_CapNotaCredito().ValidaMontosImpresion(nc, Id_Cd, Id_Emp, iTipoDocumento, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
