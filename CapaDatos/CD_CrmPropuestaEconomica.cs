using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CrmPropuestaEconomica
    {
        public void Insertar(IEnumerable<CrmPropuestaEconomica> detalle, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                ctx.CrmPropuestaEconomicas.AddRange(detalle);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<CrmPropuestaEconomica> ConsultarPorValuacion(int idEmp, int idCd, int idCte, int idRik, int idVal, string cadenaConexionEF)
        {
            IEnumerable<CrmPropuestaEconomica> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var productos = (from pe in ctx.CrmPropuestaEconomicas
                                 where pe.Id_Emp == idEmp && pe.Id_Cd == idCd && pe.Id_Cte == idCte && pe.Id_Rik == idRik && pe.Id_Val == idVal
                                 select pe).ToList().Select(cpe => 
                                 {
                                     cpe.ProductoSerializable = cpe.CatProducto;
                                     return cpe; 
                                 }).ToList();
                result = productos;
            }
            return result;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        public eCapValProyecto spCRMCapValProyecto(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Rik, int Id_Val, string Conexion)
        {
            eCapValProyecto Obj = new eCapValProyecto();

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                          ,"@Id_Rik"
                                          ,"@Id_Vap"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Cte
                                       ,Id_Rik
                                       ,Id_Val                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ObtenerCapValProyecto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Obj.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Obj.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Obj.Id_Vap = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Vap")));
                    Obj.Vap_Fecha = dr.GetValue(dr.GetOrdinal("Vap_Fecha")).ToString();
                    Obj.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    Obj.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Obj.Vap_Nota = dr.GetValue(dr.GetOrdinal("Vap_Nota")).ToString();
                    Obj.Vap_Estatus = dr.GetValue(dr.GetOrdinal("Vap_Estatus")).ToString();

                    Obj.Vap_UtilidadRemanente = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_UtilidadRemanente")));
                    Obj.Vap_ValorPresenteNeto = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_ValorPresenteNeto")));

                    Obj.Vap_Estatus2 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Vap_Estatus2")));
                    Obj.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    Obj.MotivoParaAutorizacion = dr.GetValue(dr.GetOrdinal("MotivoParaAutorizacion")).ToString();                    
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                Obj = null;
            }

            return Obj;
        }

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        public List<ePropuestaTecnoEconomicaDetalle> CRM_ObtenerPropuestaEconomica(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Rik, int Id_Val, string Conexion)
        {
            List<ePropuestaTecnoEconomicaDetalle> Lst = new List<ePropuestaTecnoEconomicaDetalle>();

            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cte"
                                          ,"@Id_Rik"
                                          ,"@Id_Val"
                                      };
                object[] Valores = { 
                                       Id_Emp
                                       ,Id_Cd
                                       ,Id_Cte
                                       ,Id_Rik
                                       ,Id_Val                                       
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ObtenerPropuestaEconomica", ref dr, Parametros, Valores);

                CapaEntidad.Informe1 a;
                while (dr.Read())
                {
                    ePropuestaTecnoEconomicaDetalle obj = new ePropuestaTecnoEconomicaDetalle();

                    obj.Id_Val = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Val")));
                    obj.Id_Op = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Op")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Id_VapDet= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_VapDet")));
                    obj.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();

                    obj.Vap_Precio = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_Precio")));
                    obj.Vap_Cantidad = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Vap_Cantidad")));
                    
                    obj.Prd_Presentacion = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));
                    obj.COP_ConsumoMensual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("COP_ConsumoMensual")));
                    obj.ConsumoMensualL = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ConsumoMensualL")));
                    obj.GastoMensual = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("GastoMensual")));
                                                                                               
                    obj.COP_DilucionAntecedente = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("COP_DilucionAntecedente")));
                    obj.COP_DilucionConsecuente = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("COP_DilucionConsecuente")));
                    obj.ConsumoMensualLDiluidos = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("ConsumoMensualLDiluidos")));

                    obj.CostoEnUso = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CostoEnUso")));

                    obj.CPT_ProductoActual = dr.GetValue(dr.GetOrdinal("CPT_ProductoActual")).ToString();
                    obj.CPT_RecursoImagenProductoActual = dr.GetValue(dr.GetOrdinal("CPT_RecursoImagenProductoActual")).ToString();
                    obj.CPT_SituacionActual = dr.GetValue(dr.GetOrdinal("CPT_SituacionActual")).ToString();
                    obj.ProductoKey = dr.GetValue(dr.GetOrdinal("ProductoKey")).ToString();
                    obj.CPT_RecursoImagenSolucionKey = dr.GetValue(dr.GetOrdinal("CPT_RecursoImagenSolucionKey")).ToString();
                    obj.CPT_VentajasKey = dr.GetValue(dr.GetOrdinal("CPT_VentajasKey")).ToString();

                    obj.COP_EsQuimico = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COP_EsQuimico")));
                    obj.Prd_UniEmp = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Prd_UniEmp")));
                    obj.AplDilucion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AplDilucion")));
                    
                    Lst.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                Lst = null;
            }

            return Lst;
        }
        //
    }
}
