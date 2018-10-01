using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_GestionRentabilidadSimulador
    {
        #region Variables

        string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_Cte"
                                        ,"@Id_Ter"
                                      };


        #endregion



        public void ActualizaGestionRentabilidadAccion(
                      string NId_PrdP,
                      string CDI,
                      string Terr,
                      string Cte,
                      string Conexion,
                      string MesAccion,
                      string AnioAccion,
                      string Accion
        )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { 	
                      "@Id_Emp"
	                , "@Id_Cd"
	                , "@Id_Ter"
	                , "@Id_Cte"
	                , "@Id_Prd"
                    , "@MesAccion"
                    , "@AnioAccion"
                    , "@Accion"
                  };


                object[] Valores = { 1
	                , Convert.ToInt32(CDI)
	                , Convert.ToInt32(Terr)
	                , Convert.ToInt32(Cte)
	                , Convert.ToInt32(NId_PrdP)
	                , Convert.ToString(MesAccion)
	                , Convert.ToString(AnioAccion)
	                , Convert.ToString(Accion)
                 };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_spCapGestionRentabilidad_InformacionSimuladorAccion", ref dr, Parametros, Valores);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

        }

        public void EliminaGestionRentabilidadSimulador(
                      string CDI,
                      string Terr,
                      string Cte,
                      string NId_PrdP,
                      string Conexion,
                      string EVenta,
                      string ECosto,
                      string EUtilidadBruta,
                      string EPorcUBReal,
                      string MesInicial,
                      string AnioInicial,
                      string MesFinal,
                      string AnioFinal,
                      string Ecantidad,
                      string EPrecioVenta,
                      string EPrecioDistribuidor            
            )
        {
            try
            {


                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { 	
                      "@Id_Emp"
	                , "@Id_Cd"
	                , "@Id_Ter"
	                , "@Id_Cte"
	                , "@Id_Prd"
                    , "@venta"
                    , "@Costo"
                    , "@UtilidadBruta"
                    , "@PorcUBReal"
                    , "@MesInicial"
                    , "@AnioInicial"
                    , "@MesFinal"
                    , "@AnioFinal"
	                , "@cantidad"
	                , "@PrecioVenta"
	                , "@PrecioDistribuidor"
                  };


                object[] Valores = { 1
	                , Convert.ToInt32(CDI)
	                , Convert.ToInt32(Terr)
	                , Convert.ToInt32(Cte)
	                , Convert.ToInt32(NId_PrdP)
	                , Convert.ToDecimal(EVenta)
	                , Convert.ToDecimal(ECosto)
	                , Convert.ToDecimal(EUtilidadBruta)
	                , Convert.ToDecimal(EPorcUBReal)
	                , MesInicial
	                , AnioInicial
	                , MesFinal
	                , AnioFinal
	                , Convert.ToInt32(Ecantidad.Replace(".00",""))
	                , Convert.ToDecimal(EPrecioVenta)
	                , Convert.ToDecimal(EPrecioDistribuidor)
                 };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_spCapGestionRentabilidad_AnalisisInformacionSimuladorEliminar", ref dr, Parametros, Valores);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


        }



        public void ActualizaGestionRentabilidadSimulador (
                      string NId_PrdP,
                      string EId_PrdP,
                      string EcantidadP,
                      string EPrd_DescripcionP,
                      string EPrecioVentaP,
                      string EPrecioDistribuidorP,
                      string EventaP,
                      string ECostoP,
                      string EUtilidadBrutaP,
                      string EPorcUBRealP,
                      string CDI,
                      string Terr,
                      string Cte,
                      string Conexion,
                      string comboAccion,
                      string Eventa,
                      string ECosto,
                      string EUtilidadBruta,
                      string EPorcUBReal,
                      string MesInicial,
                      string AnioInicial,
                      string MesFinal,
                      string AnioFinal,
                      string Ecantidad,
                      string EPrecioVenta,
                      string EPrecioDistribuidor
            )
            {
                try
                {
                    
                    SqlDataReader dr = null;
                    CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                    string[] Parametros = { 	"@Id_Emp"
	                , "@Id_Cd"
	                , "@Id_Cte"
	                , "@Id_Ter"
	                , "@Id_Prd"
	                , "@Id_PrdP"
	                , "@Prd_DescripcionP"
	                , "@cantidadP"
	                , "@PrecioVentaP"
	                , "@PrecioDistribuidorP"
	                , "@ventaP"
	                , "@CostoP"
	                , "@UtilidadBrutaP"
	                , "@PorcUBRealP"
                    , "@Accion"
                    , "@venta"
                    , "@Costo"
                    , "@UtilidadBruta"
                    , "@PorcUBReal"
                    , "@MesInicial"
                    , "@AnioInicial"
                    , "@MesFinal"
                    , "@AnioFinal"
	                , "@cantidad"
	                , "@PrecioVenta"
	                , "@PrecioDistribuidor"
                                          };


                    object[] Valores = { 1
	                , Convert.ToInt32(CDI)
	                , Convert.ToInt32(Cte)
	                , Convert.ToInt32(Terr)
	                , Convert.ToInt32(NId_PrdP)
	                , Convert.ToInt32(EId_PrdP)
	                , EPrd_DescripcionP
	                , Convert.ToInt32(EcantidadP)
	                , Convert.ToDecimal(EPrecioVentaP)
	                , Convert.ToDecimal(EPrecioDistribuidorP)
	                , Convert.ToDecimal(EventaP)
	                , Convert.ToDecimal(ECostoP)
	                , Convert.ToDecimal(EUtilidadBrutaP)
	                , Convert.ToDecimal(EPorcUBRealP)
                    ,comboAccion                
	                , Convert.ToDecimal(Eventa)
	                , Convert.ToDecimal(ECosto)
	                , Convert.ToDecimal(EUtilidadBruta)
	                , Convert.ToDecimal(EPorcUBReal)
	                , MesInicial
	                , AnioInicial
	                , MesFinal
	                , AnioFinal
	                , Convert.ToInt32(Ecantidad)
	                , Convert.ToDecimal(EPrecioVenta)
	                , Convert.ToDecimal(EPrecioDistribuidor)
                    };

                    // ------------------------------------
                   // Consultar Gestion de Rentabilidad
                    // ------------------------------------
                    SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_spCapGestionRentabilidad_AnalisisInformacionSimuladorAgregar", ref dr, Parametros, Valores);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        public void ConsultaGestionRentabilidadSimulador_Totales(string Conexion
            , int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , string ConAccion
            , ref Decimal VentaActual
            , ref Decimal CostoActual
            , ref Decimal UtilidadBrutaActual
            , ref Decimal VentaPlanteada
            , ref Decimal CostoPlanteada
            , ref Decimal UtilidadBrutaPlanteada
            , ref Decimal UtilidadBrutaPorcentajeActual
            , ref Decimal ComisionRIKActual
            , ref Decimal UtilidadBrutaPorcentajePlanteada
            , ref Decimal ComisionRIKPlanteada
            , ref Decimal AhorroClientesPesos
            , ref Decimal AhorroClientesPorcentaje
            , ref Decimal UtilidadBrutaMejoraPesos
            , ref Decimal UtilidadBrutaMejoraPorcentaje
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@Id_Cte", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal",  "@Id_U", "@ConAccion" };


                object[] Valores = { Id_Emp, Id_Cd, Convert.ToInt32(Id_Ter), Convert.ToInt32(Id_Cte), MesInicial, AnioInicial, MesFinal, AnioFinal,  Id_U, ConAccion };



                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionSimuladorTotales", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionSimuladorActualizado_Totales", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    VentaActual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaActual")));
                    CostoActual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoActual")));
                    UtilidadBrutaActual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaActual")));
                    VentaPlanteada = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("VentaPlanteada")));
                    CostoPlanteada = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoPlanteada")));
                    UtilidadBrutaPlanteada = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPlanteada")));
                    UtilidadBrutaPorcentajeActual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorcentajeActual")));
                    ComisionRIKActual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ComisionRIKActual")));
                    UtilidadBrutaPorcentajePlanteada = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaPorcentajePlanteada")));
                    ComisionRIKPlanteada = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ComisionRIKPlanteada")));
                    AhorroClientesPesos = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("AhorroClientesPesos")));
                    AhorroClientesPorcentaje = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("AhorroClientesPorcentaje")));
                    UtilidadBrutaMejoraPesos = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaMejoraPesos")));
                    UtilidadBrutaMejoraPorcentaje = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaMejoraPorcentaje")));
                }

                dr.Close();


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtieneEstatusSGR(
                                int Id_Emp
                                , int Id_Cd
                                , int Id_Ter
                                , int Id_Cte
                                , int mesInicial
                                , int anioInicial
                                , int mesFinal
                                , int anioFinal
                                , int Id_U
                                , String Conexion
                                , ref String Resultado
            )
        {

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@Id_Cte", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal",  "@Id_U" };

                object[] Valores    = {   Id_Emp ,    Id_Cd,    Id_Ter,    Id_Cte,    mesInicial,   anioInicial,     mesFinal,    anioFinal,   Id_U   };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                // SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionSimulador", ref dr, Parametros, Valores);


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_ObtenerEstatus_GestionRentabilidad", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Resultado = Convert.ToString(dr.GetValue(dr.GetOrdinal("Resultado"))); 
                }

        }


        public void ConsultaGestionRentabilidadSimulador_Buscar(GestionRentabilidadSimulador gestionRentabilidadSimulador, string Conexion, ref List<GestionRentabilidadSimulador> List,
             int Id_Emp
            , int Id_Cd
            , string Id_Cte
            , string Id_Ter
            , string NombreCliente
            , int MesInicial
            , int AnioInicial
            , int MesFinal
            , int AnioFinal
            , int Id_U
            , string ConAccion
            )
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ter", "@Id_Cte", "@mesInicial", "@anioInicial", "@mesFinal", "@anioFinal", "@Id_U", "@ConAccion" };

                object[] Valores    = { Id_Emp, Id_Cd, Convert.ToInt32(Id_Ter), Convert.ToInt32(Id_Cte), MesInicial, AnioInicial, MesFinal, AnioFinal, Id_U ,ConAccion};

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                // SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionSimulador", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapGestionRentabilidad_AnalisisInformacionSimuladorActualizado", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    GestionRentabilidadSimulador DgestionRentabilidadSimulador = new GestionRentabilidadSimulador();
                    DgestionRentabilidadSimulador.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    DgestionRentabilidadSimulador.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    DgestionRentabilidadSimulador.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    DgestionRentabilidadSimulador.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    DgestionRentabilidadSimulador.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    DgestionRentabilidadSimulador.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    DgestionRentabilidadSimulador.cantidad = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidad")));
                    DgestionRentabilidadSimulador.PrecioVenta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVenta")));
                    DgestionRentabilidadSimulador.PrecioDistribuidor = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidor")));
                    DgestionRentabilidadSimulador.venta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("venta")));
                    DgestionRentabilidadSimulador.Costo = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Costo")));
                    DgestionRentabilidadSimulador.UtilidadBruta = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    DgestionRentabilidadSimulador.PorcUBReal = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBReal")));
                    DgestionRentabilidadSimulador.Cpr_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cpr_Descripcion")));
                    DgestionRentabilidadSimulador.Accion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Accion")));
                    DgestionRentabilidadSimulador.Id_PrdP = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PrdP")));
                    DgestionRentabilidadSimulador.Prd_DescripcionP = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_DescripcionP")));
                    DgestionRentabilidadSimulador.cantidadP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cantidadP")));
                    DgestionRentabilidadSimulador.PrecioVentaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioVentaP")));
                    DgestionRentabilidadSimulador.PrecioDistribuidorP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PrecioDistribuidorP")));
                    DgestionRentabilidadSimulador.ventaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ventaP")));
                    DgestionRentabilidadSimulador.CostoP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoP")));
                    DgestionRentabilidadSimulador.UtilidadBrutaP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("UtilidadBrutaP")));
                    DgestionRentabilidadSimulador.PorcUBRealP = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("PorcUBRealP")));
                    DgestionRentabilidadSimulador.AnioAccion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AnioAccion")));
                    DgestionRentabilidadSimulador.MesAccionNumero = Convert.ToString(dr.GetValue(dr.GetOrdinal("MesAccionNumero")));
                    DgestionRentabilidadSimulador.MesAccionNombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("MesAccionNombre"))); 
                    List.Add(DgestionRentabilidadSimulador);

                }

                dr.Close();


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
