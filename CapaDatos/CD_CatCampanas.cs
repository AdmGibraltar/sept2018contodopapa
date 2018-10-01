using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Globalization;

namespace CapaDatos
{
    public class CD_CatCampanas
    {
        public void ConsultaCampanas(Campanas campana, string Conexion, ref List<Campanas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd"};
                object[] Valores = { campana.Id_Emp, campana.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampana_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    campana = new Campanas();
                    campana.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    campana.Id_Cam = (int)dr.GetValue(dr.GetOrdinal("Id_Cam"));                    
                    campana.Id_Uen = (int)dr.GetValue(dr.GetOrdinal("Id_Uen"));
                    campana.Uen = (string)dr.GetValue(dr.GetOrdinal("Uen")); 
                    campana.Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Segmento"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_Segmento"));
                    campana.Segmento = (string)dr.GetValue(dr.GetOrdinal("Segmento")); 
                    campana.Id_Sol = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Solucion"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_Solucion"));
                    campana.Solucion = (string)dr.GetValue(dr.GetOrdinal("Solucion")); 
                    campana.Id_Area = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Area"))) ? -1 : (int)dr.GetValue(dr.GetOrdinal("Id_Area"));
                    campana.Area = (string)dr.GetValue(dr.GetOrdinal("Area")); 
                    campana.Id_Aplicacion = (int)dr.GetValue(dr.GetOrdinal("Id_Aplicacion"));
                    campana.Aplicacion = (string)dr.GetValue(dr.GetOrdinal("Aplicacion")); 
                    campana.Cam_Nombre = (string)dr.GetValue(dr.GetOrdinal("Cam_Descripcion"));     
                    campana.Cam_FechaInicio = (DateTime)dr.GetValue(dr.GetOrdinal("Cam_FechaInicio"));
                    campana.Cam_FechaFin = (DateTime)dr.GetValue(dr.GetOrdinal("Cam_FechaFin"));
                    
                    

                   
                    campana.Cam_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Activo")));
                    if (Convert.ToBoolean(campana.Cam_Activo))
                    {
                        campana.EstatusStr = "Activo";
                    }
                    else
                    {
                        campana.EstatusStr = "Inactivo";
                    }
                    List.Add(campana);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCampanas(Campanas campana, List<Producto> ListProducto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                       
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cam",
	                                    "@Id_Uen", 
	                                    "@Id_Segmento",	                                  
	                                    "@Id_Area", 
	                                    "@Id_Solucion", 
	                                    "@Id_Aplicacion", 
                                        "@Cam_Descripcion", 
                                        "@Cam_FechaInicio",
                                        "@Cam_FechaFin",
	                                    "@Cam_Activo",
                                        "@Aplicacion"
                                      };
                object[] Valores = { 
                                        campana.Id_Emp,
                                        campana.Id_Cd,
                                        campana.Id_Cam,
                                        campana.Id_Uen,
                                        campana.Id_Seg,                                      
                                        campana.Id_Area,
                                        campana.Id_Sol,
                                        campana.Id_Aplicacion,
                                        campana.Cam_Nombre,
                                        campana.Cam_FechaInicio, 
                                        campana.Cam_FechaFin,
                                        campana.Cam_Activo,
                                        campana.Aplicacion
                                    
                                        
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampana_Insertar", ref verificador, Parametros, Valores);
               // CapaDatos.LimpiarSqlcommand(ref sqlcmd);




                Parametros = new string[]{
                                            "@Id_Cam",  
                                            "@Id_Prd",  
                                            "@Prd_cuota"  
                                           
		                                };


           
                foreach (Producto producto in ListProducto)
                {
                    Valores = new object[]{
                                                campana.Id_Cam,
                                                producto.Id_Prd,
                                                producto.Prd_Cuota
                                               
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampanaProducto_Insertar", ref verificador, Parametros, Valores);
                }

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();   
                throw ex;
            }
        }

        public void ModificarCampanas(Campanas campana, List<Producto> ListProducto, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {


                CapaDatos.StartTrans();
                
                string[] Parametros = { 
	                                    "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cam",
	                                    "@Id_Uen", 
	                                    "@Id_Segmento",	                                  
	                                    "@Id_Area", 
	                                    "@Id_Solucion", 
	                                    "@Id_Aplicacion", 
                                        "@Cam_Descripcion", 
                                        "@Cam_FechaInicio",
                                        "@Cam_FechaFin",
	                                    "@Cam_Activo",
                                        "@Aplicacion"
                                       
                                      };
                object[] Valores = { 
                                        campana.Id_Emp,
                                        campana.Id_Cd,
                                        campana.Id_Cam,
                                        campana.Id_Uen,
                                        campana.Id_Seg,                                       
                                        campana.Id_Area,
                                        campana.Id_Sol,
                                        campana.Id_Aplicacion ,
                                        campana.Cam_Nombre,
                                        campana.Cam_FechaInicio, 
                                        campana.Cam_FechaFin,                                        
                                        campana.Cam_Activo,
                                        campana.Aplicacion
                                       
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampana_Modificar", ref verificador, Parametros, Valores);


                  Parametros =  new string[] { "@Id_Cam"};

                  Valores = new object[] { campana.Id_Cam };

                  sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampanaProducto_Eliminar", ref verificador, Parametros, Valores);


                Parametros = new string[]{
                                            "@Id_Cam",  
                                            "@Id_Prd",  
                                            "@Prd_cuota"                                             
		                                };

                foreach (Producto producto in ListProducto)
                {
                    Valores = new object[]{
                                                campana.Id_Cam,
                                                producto.Id_Prd,
                                                producto.Prd_Cuota
                                               
		                                    };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampanaProducto_Insertar", ref verificador, Parametros, Valores);
                }

                CapaDatos.CommitTrans();         
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();   
                throw ex;
            }
        }

        

       

        public void ConsultaCampana(ref Campanas catCampana, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cam" };
                object[] Valores = { catCampana.Id_Emp, catCampana.Id_Cd, catCampana.Id_Cam };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampana_Consultar", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;

                if (dr.HasRows)
                {
                    dr.Read();
                    catCampana.Id_Cam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cam"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cam")));
                    catCampana.Cam_Nombre = dr.GetValue(dr.GetOrdinal("Cam_Descripcion")).ToString();
                    catCampana.Id_Uen = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Uen"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    catCampana.Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Seg"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    catCampana.Id_Area = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Area"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Area")));
                    catCampana.Id_Sol = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Solucion"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Solucion")));
                    catCampana.Id_Aplicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Aplicacion"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Aplicacion")));                 
                  
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCampanaOportunidad(ref Campanas catCampana, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Uen", "@Id_Segmento", "@Id_Area", "@Id_Solucion", "@Id_Aplicacion", "@Cam_Aplicacion" };
                object[] Valores = { catCampana.Id_Emp, catCampana.Id_Cd, catCampana.Id_Uen, catCampana.Id_Seg, catCampana.Id_Area,catCampana.Id_Sol, catCampana.Id_Aplicacion, catCampana.Cam_Aplicacion };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSelCampana", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;

                if (dr.HasRows)
                {
                    dr.Read();
                    catCampana.Id_Cam = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cam"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cam")));
                    catCampana.Cam_Nombre = dr.GetValue(dr.GetOrdinal("Cam_Descripcion")).ToString();
                    catCampana.Cam_FechaInicio = (DateTime)dr.GetValue(dr.GetOrdinal("Cam_FechaInicio"));
                    catCampana.Cam_FechaFin = (DateTime)dr.GetValue(dr.GetOrdinal("Cam_FechaFin"));
                   
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAreaCombo(ref Territorios catterr, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1", "@Id2", "@Id3", "@Id4" };
                object[] Valores = { catterr.Id_Emp, catterr.Id_Cd, catterr.Id_Ter, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatRikTerr_Combo", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;
                if (dr.HasRows)
                {
                    dr.Read();
                    catterr.Id_Rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    catterr.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCampanaProducto(Campanas campanas, string Conexion, ref List<Producto> list_producto)
        {
            try
            {
                CD_Datos CapaDatos = new CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cam" };
                object[] Valores = { campanas.Id_Cam };
                DataSet ds = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCampaniaProducto_Consultar", ref ds, Parametros, Valores);

                Producto OneProducto;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    OneProducto = new Producto();

                    OneProducto.Id_Prd = Convert.ToInt32(dr["Id_Prd"].ToString());
                    OneProducto.Prd_Descripcion = dr["Prd_Descripcion"].ToString();
                    OneProducto.Prd_Cuota = Convert.ToInt32(dr["Prd_Cuota"]);

                    list_producto.Add(OneProducto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaRuta(ref Campanas catCampana, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Aplicacion", "@Id_Uen" };
                object[] Valores = { catCampana.Id_Emp, catCampana.Id_Cd, catCampana.Cam_Nombre, catCampana.Id_Uen };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMRuta_Consultar", ref dr, Parametros, Valores);

                CultureInfo ci = CultureInfo.CurrentCulture;

                if (dr.HasRows)
                {
                    dr.Read();
                    catCampana.Id_Cam = 0;
                   
                   
                    catCampana.Id_Seg = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Seg"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    catCampana.Id_Area = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Area"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Area")));
                    catCampana.Id_Sol = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Sol"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sol")));
                    catCampana.Id_Aplicacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Apl"))) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Apl")));

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
