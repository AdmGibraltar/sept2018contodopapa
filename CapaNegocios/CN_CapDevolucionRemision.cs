using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapDevolucionRemision
    {
        public void Consulta_Lista(DevolucionRemision dvolRemision, string Conexion, ref List<DevolucionRemision> List)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.Consulta_Lista(dvolRemision, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRemisionSaldo(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.ConsultaRemisionSaldo(rd, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaDevolucionHistorico(DevolucionRemision devolRemision, string Conexion, ref List<DevolucionRemisionDet> List)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.ConsultaDevolucionHistorico(devolRemision, Conexion, ref List);
        }


        public void Consulta(ref DevolucionRemision devolRemision, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.Consulta(ref devolRemision, Conexion,   Id_Emp,  Id_Cd,  folio);
        }

        public void ConsultaPorFactura(ref DevolucionRemision devolRemision, ref bool pHasRows, string Conexion, int Id_Emp, int Id_Cd, int Id_Fac)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.ConsultaPorFactura(ref devolRemision, ref pHasRows, Conexion, Id_Emp, Id_Cd, Id_Fac);
        }

        public void ConsultaDetalle(ref DevolucionRemision devolRemision, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.ConsultaDetalle(ref devolRemision, Conexion, Id_Emp, Id_Cd, folio);
        }

        public void ConsultaDetallePorRemision(ref List<DevolucionRemisionDet> devolRemision, string Conexion, int Id_Emp, int Id_Cd, int Id_Rem)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.ConsultaDetallePorRemision(ref devolRemision, Conexion, Id_Emp, Id_Cd, Id_Rem);
        }

        public void CancelaEntradas(string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
            claseCapaDatos.CancelaEntradas(Conexion, Id_Emp, Id_Cd, folio);
        }
        
        public void ConsultaRemisionProductoSaldoDetalle(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
             try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.ConsultaRemisionProductoSaldoDetalle(rd, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRemisionProductoSaldoDetalleTotal(DevolucionRemision rd, string Conexion, ref List<RemisionDet> List)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.ConsultaRemisionProductoSaldoDetalleTotal(rd, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ConsultaMovInverso(int id_emp, int id_tm, string Conexion)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                return  claseCapaDatos.ConsultaMovInverso(id_emp, id_tm, Conexion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarEntradaSalida(DevolucionRemision devRem,ref string verificadorStr, int strEmp, string Conexion, ref int id_DevRem)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.GuardarEntradaSalida(devRem, ref verificadorStr, strEmp, Conexion, ref id_DevRem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         public void GuardarEntradaSalidaAjuste(EntradaSalida entsal,ref string verificadorStr, string Conexion)
        {
            try
            {
                CD_CapDevolucionRemision claseCapaDatos = new CD_CapDevolucionRemision();
                claseCapaDatos.GuardarEntradaSalidaAjuste(entsal, ref verificadorStr, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
