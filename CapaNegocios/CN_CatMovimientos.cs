using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatMovimientos
    {
        //public void ConsultaMovimientos(int Empresa, string Conexion, ref List<Movimientos> List)
        //{
        //    La esclavitud no se abolió, se cambió a 10 hrs. diarias.
        //}

        public void InsertarMovimientos(Movimientos mv, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatMovimientos claseCapaDatos = new CD_CatMovimientos();
                claseCapaDatos.InsertarMovimientos(mv, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarMovimientos(Movimientos mv, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatMovimientos claseCapaDatos = new CD_CatMovimientos();
                claseCapaDatos.ModificarMovimientos(mv, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que consulta si se afecta a un cliente(0) o a un proveedor(1).....  
        /// El amor eterno dura aproximadamente 3 meses.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Tm"></param>
        /// <param name="Tm_Afecta"></param>
        public void ConsultaTmovimientoAfecta(Sesion sesion, int Id_Tm, ref int Tm_Afecta)
        {
            //ric
            try
            {
                CD_CatMovimientos cn_catmov = new CD_CatMovimientos();
                cn_catmov.ConsultarTmovimientoAfecta(sesion, Id_Tm, ref Tm_Afecta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe Id_Tm, TmNatMov (naturaleza movimiento (1)inventario o Cobranza).  
        /// Consulta si requiere referencia y que tipo de documento es el de la referencia. (1) Remision , (2) Factura
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Tm"></param>
        /// <param name="Tm_NatMov"></param>
        /// <param name="Tm_ReqReferencia"></param>
        /// <param name="Tm_ReqTDoc"></param>
        public void ConsultarTmovimientoReqReferencia(Sesion sesion, int Id_Tm, int Tm_NatMov, ref bool Tm_ReqReferencia, ref int Tm_ReqTDoc)
        {//ric
            try
            {
                new CD_CatMovimientos().ConsultarTmovimientoReqReferencia(sesion, Id_Tm, Tm_NatMov, ref Tm_ReqReferencia, ref Tm_ReqTDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que verifica si el tipo de movimiento afecta orden de compra.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="Id_Tm"></param>
        /// <param name="Tm_Afecta"></param>
        public void ConsultaTmovimientoAfectaOrdCom(Sesion sesion, int Id_Tm, ref bool Tm_AfectaOrdCom)
        {
            //ric
            try
            {
                CD_CatMovimientos cn_catmov = new CD_CatMovimientos();
                cn_catmov.ConsultarTmovimientoAfectaOrdCom(sesion, Id_Tm, ref Tm_AfectaOrdCom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaMovimientos(bool inventario, int Empresa, string Conexion, ref List<Movimientos> List)
        {
            try
            {
                CD_CatMovimientos claseCapaDatos = new CD_CatMovimientos();
                claseCapaDatos.ConsultaMovimientos(inventario, Empresa, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTmovimientoReqSpo(Sesion sesion, int Id_Tm, ref bool Tm_ReqSpo)
        {
            //rm //Metodo que verifica si el tipo de movimiento requiere sistema de propietarios
            try
            {
                new CD_CatMovimientos().ConsultarTmovimientoReqSpo(sesion, Id_Tm, ref Tm_ReqSpo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
