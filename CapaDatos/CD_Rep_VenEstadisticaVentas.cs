using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Rep_VenEstadisticaVentas
    {
        #region Parametros
        string[] Parametros={
                              "@Id_Emp"   
                             ,"@Id_Cd"  
                             ,"@Fecha" 
                             ,"@Territorio"
                             ,"@Cliente" 
                             ,"@Producto" 
                             ,"@Tipo"      
                             ,"@NivelCliente"
                             ,"@NivelProducto" 
                            };
        
        #endregion

        public void ConsultaVentaSem_Territorio(VentaSemanal semanal, string Conexion, ref List<VentaSemanal> List,int Id_Emp, int Id_Cd, 
                                                string Fecha, string Territorio, string Cliente, string Producto, 
                                                int Tipo, int NivelCliente, int NivelProducto, int Mov_80)
        {
            try 
            {
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Id_Emp"   
                             ,"@Id_Cd"  
                             ,"@Fecha" 
                             ,"@Territorio"
                             ,"@Cliente" 
                             ,"@Producto" 
                             ,"@Tipo"      
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@Mov_80"
                            };
                object[] Valores ={
                                   Id_Emp
                                  ,Id_Cd
                                  ,Fecha
                                  ,Territorio
                                  ,Cliente
                                  ,Producto
                                  ,Tipo
                                  ,NivelCliente
                                  ,NivelProducto
                                  ,Mov_80
                                  };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentasSemanal", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    semanal = new VentaSemanal();
                    semanal.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    semanal.Nom_Ter = dr.GetValue(dr.GetOrdinal("Nom_Ter")).ToString();
                    semanal.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    semanal.Nom_Cte = dr.GetValue(dr.GetOrdinal("Nom_Cte")).ToString();
                    semanal.Id_prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    semanal.Nom_Prd = dr.GetValue(dr.GetOrdinal("Nom_Prd")).ToString();
                    if (Tipo == 0)
                    {
                        semanal.Unidades = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Unidades")));
                        semanal.Importe = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Importe")));
                    }
                    if(Tipo == 1)
                        semanal.Importe = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Importe")));
                    if (Tipo == 2)
                        semanal.Unidades = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Unidades")));
                        
                    semanal.Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sem")));
                    semanal.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    semanal.Mes = dr.GetValue(dr.GetOrdinal("Mes")).ToString();
                    List.Add(semanal);
                }
                  
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex) 
            {
                throw ex;
            }           
        }
    }
}
