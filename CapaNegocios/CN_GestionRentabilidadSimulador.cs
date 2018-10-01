using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
namespace CapaNegocios
{
    public class CN_GestionRentabilidadSimulador 
    {

        public void ActualizaGestionRentabilidadSimuladorAcciones(
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
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.ActualizaGestionRentabilidadAccion(
                      NId_PrdP
                      , CDI
                      , Terr
                      , Cte
                      , Conexion
                      , MesAccion
                      , AnioAccion
                      , Accion
                 );
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
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.EliminaGestionRentabilidadSimulador(
                      CDI
                      , Terr
                      , Cte
                      , NId_PrdP
                      , Conexion
                      , Eventa
                      , ECosto
                      , EUtilidadBruta
                      , EPorcUBReal
                      , MesInicial
                      , AnioInicial
                      , MesFinal
                      , AnioFinal
                      , Ecantidad
                      , EPrecioVenta
                      , EPrecioDistribuidor
                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ActualizaGestionRentabilidadSimulador(
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
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.ActualizaGestionRentabilidadSimulador(
                      NId_PrdP
                      , EId_PrdP
                      , EcantidadP
                      , EPrd_DescripcionP
                      , EPrecioVentaP
                      , EPrecioDistribuidorP
                      , EventaP
                      , ECostoP
                      , EUtilidadBrutaP
                      , EPorcUBRealP
                      , CDI
                      , Terr
                      , Cte
                      , Conexion
                      , comboAccion
                      , Eventa
                      , ECosto
                      , EUtilidadBruta
                      , EPorcUBReal
                      , MesInicial
                      , AnioInicial
                      , MesFinal
                      , AnioFinal
                      , Ecantidad
                      , EPrecioVenta
                      , EPrecioDistribuidor
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ConsultaGestionRentabilidadSimulador_Totales(
            string Conexion
            ,int Id_Emp
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
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.ConsultaGestionRentabilidadSimulador_Totales(
             Conexion
            ,Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ConAccion
            , ref VentaActual
            , ref CostoActual
            , ref UtilidadBrutaActual
            , ref VentaPlanteada
            , ref CostoPlanteada
            , ref UtilidadBrutaPlanteada
            , ref UtilidadBrutaPorcentajeActual
            , ref ComisionRIKActual
            , ref UtilidadBrutaPorcentajePlanteada
            , ref ComisionRIKPlanteada
            , ref AhorroClientesPesos
            , ref AhorroClientesPorcentaje
            , ref UtilidadBrutaMejoraPesos
            , ref UtilidadBrutaMejoraPorcentaje
            );
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
                    , ref string Resultado
            )
        {


            try
            {
                  
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.ObtieneEstatusSGR(
                      Id_Emp
                    , Id_Cd
                    , Id_Ter
                    , Id_Cte
                    , mesInicial
                    , anioInicial
                    , mesFinal
                    , anioFinal
                    , Id_U
                    , Conexion
                    , ref Resultado
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public void ConsultaGestionRentabilidadSimulador_Buscar(GestionRentabilidadSimulador gestionRentabilidadSimulador, string Conexion, ref List<GestionRentabilidadSimulador> List
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
            ,string ConAccion
            )
        {
            try
            {
                CD_GestionRentabilidadSimulador claseCapaDatos = new CD_GestionRentabilidadSimulador();

                claseCapaDatos.ConsultaGestionRentabilidadSimulador_Buscar(gestionRentabilidadSimulador, Conexion, ref List
            , Id_Emp
            , Id_Cd
            , Id_Cte
            , Id_Ter
            , NombreCliente
            , MesInicial
            , AnioInicial
            , MesFinal
            , AnioFinal
            , Id_U
            , ConAccion 
            );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
         public void ConsultaGestionRentabilidad_Buscar(GestionRentabilidad gestionRentabilidad, string p, ref List<GestionRentabilidad> listGestionRentabilidad, int p_2, int p_3, Telerik.Web.UI.RadTextBox radTextBox, Telerik.Web.UI.RadTextBox radTextBox_2, Telerik.Web.UI.RadTextBox radTextBox_3)
         {
             throw new NotImplementedException();
         }*/
    }
}
