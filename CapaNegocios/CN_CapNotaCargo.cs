using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapNotaCargo
    {
        public void ConsultaNotaCargo(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                new CD_CapNotaCargo().ConsultaNotaCargo(ref notaCargo, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargo_Encabezado(ref NotaCargo notaCargo, string Conexion, ref bool encontrado)
        {
            try
            {
                new CD_CapNotaCargo().ConsultaNotaCargo_Encabezado(ref notaCargo, Conexion, ref encontrado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargo_Buscar(NotaCargo notaCargo, ref List<NotaCargo> listaNotaCargo, string Conexion
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Nca_Fecha_inicio
            , DateTime? Nca_Fecha_fin
            , string Nca_Estatus
            , int? Id_Nca_inicio
            , int? Id_Nca_fin
            , int? Id_U)
        {
            try
            {
                new CD_CapNotaCargo().ConsultaNotaCargo_Buscar(notaCargo, ref listaNotaCargo, Conexion
                    , Id_Cte_inicio
                    , Id_Cte_fin
                    , Nca_Fecha_inicio
                    , Nca_Fecha_fin
                    , Nca_Estatus
                    , Id_Nca_inicio
                    , Id_Nca_fin
                    , Id_U);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarMovsNotaCargo(Movimientos mov, ref List<Movimientos> listaMovimientos, string Conexion)
        {
            try
            {
                new CD_CapNotaCargo().ConsultarMovsNotaCargo(mov, ref listaMovimientos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCantidadNotaCargoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapNotaCargo().ConsultarCantidadNotaCargoCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPagoFicha(ref NotaCargo ficha, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.ConsultaPagoFicha(ref ficha, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNotaCargoSAT(ref NotaCargo notaCredito, string Conexion, ref object resultado)
        {
            try
            {
                new CD_CapNotaCargo().ConsultarNotaCargoSAT(ref notaCredito, Conexion, ref resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarNotaCargo(ref NotaCargo notaCargo, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listaFacturaDet)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.InsertarNotaCargo(ref notaCargo, Conexion, ref verificador, ListCab, listaFacturaDet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCargo(ref NotaCargo notaCargo, string Conexion, ref int verificador, List<AdendaDet> ListCab, DataTable listaFacturaDet)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.ModificarNotaCargo(ref notaCargo, Conexion, ref verificador, ListCab, listaFacturaDet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ModificarNotaCargo_Estatus(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.ModificarNotaCargo_Estatus(notaCargo, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarNotaCargoSAT(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapNotaCargo().ModificarNotaCargoSAT(notaCargo, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ArchivoPdf_Xml(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.ArchivoPdf_Xml(ref notaCargo, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarNotaCargo(NotaCargo notaCargo, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.EliminarNotaCargo(notaCargo, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rastreo(ref NotaCargo nca, string Conexion, int tipoBusqueda)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.Rastreo(nca, Conexion, tipoBusqueda);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAdendaNota(Sesion sesion, int Id_Emp, int Id_Cd, int Id_Cte, ref List<NotaCargo> listNotaCargo)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.ConsultaAdendaNota(sesion, Id_Emp, Id_Cd, Id_Cte, ref listNotaCargo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarAdenda(NotaCargo notaCargo, Sesion sesion, ref int verificador)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                claseCapaDatos.AgregarAdenda(notaCargo, sesion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotaCargo> ConsultaProductosNotaCargo(ref NotaCargo notaCargo, string Conexion)
        {
            try
            {
                CD_CapNotaCargo claseCapaDatos = new CD_CapNotaCargo();
                List<NotaCargo> NotaCargo = claseCapaDatos.ConsultaProductosNotaCargo(ref notaCargo, Conexion);
                return NotaCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargoEspecialDetalle(ref List<NotaCargoDet> listaFacturaProductos, string Conexion, int id_Emp, int id_Cd, int id_Nca,string Id_NcaSerie, int id_Cte)
        {
            try
            {
                CD_CapNotaCargo CDCapNotaCargo = new CD_CapNotaCargo();
                CDCapNotaCargo.ConsultaNotaCargoEspecialDetalle(ref listaFacturaProductos, Conexion, id_Emp, id_Cd, id_Nca,Id_NcaSerie, id_Cte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_Nca, string Id_NcaSerie, string Tipo1, string Tipo2, ref List<AdendaDet> listCabT, ref List<AdendaDet> listDetT, string Conexion)
        {
            try
            {
                CD_CapNotaCargo CDCapNotaCargo = new CD_CapNotaCargo();
                CDCapNotaCargo.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Id_Nca, Id_NcaSerie,Tipo1, Tipo2, ref  listCabT, ref listDetT, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaMontosImpresion(NotaCargo nc, int Id_Cd, int Id_Emp, int iTipoDocumento, string conexion, ref bool verificador)
        {
            try
            {
                new CD_CapNotaCargo().ValidaMontosImpresion(nc, Id_Cd, Id_Emp, iTipoDocumento, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
