using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_wfrmCampanas
    {
        /// <summary>
        /// Consulta un listado de campañas
        /// </summary>
        public void ConsultaCampanas(string Conexion, int id_Emp, int Id_Cd, ref List<Campanas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { id_Emp, Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampania_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    Campanas campanas = new Campanas();
                    campanas.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    campanas.Id_Cam = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cam")));


                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Nombre"))))
                        campanas.Cam_Nombre = string.Empty;
                    else
                        campanas.Cam_Nombre = dr.GetValue(dr.GetOrdinal("Cam_Nombre")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Jabon"))))
                        campanas.Cam_Jabon = null;
                    else
                        campanas.Cam_Jabon = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Jabon")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Toalla"))))
                        campanas.Cam_Toalla = null;
                    else
                        campanas.Cam_Toalla = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Toalla")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Olores"))))
                        campanas.Cam_Olores = null;
                    else
                        campanas.Cam_Olores = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Olores")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Quimicos"))))
                        campanas.Cam_Quimicos = null;
                    else
                        campanas.Cam_Quimicos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Quimicos")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Tratamiento"))))
                        campanas.Cam_Tratamiento = null;
                    else
                        campanas.Cam_Tratamiento = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Tratamiento")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Bolsa"))))
                        campanas.Cam_Bolsa = null;
                    else
                        campanas.Cam_Bolsa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Bolsa")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Wipers"))))
                        campanas.Cam_Wipers = null;
                    else
                        campanas.Cam_Wipers = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Wipers")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Suplementos"))))
                        campanas.Cam_Suplementos = null;
                    else
                        campanas.Cam_Suplementos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Suplementos")));

                    List.Add(campanas);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consulta una campaña individual
        /// </summary>
        public void ConsultaCampana(ref Campanas campana, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cam" };
                object[] Valores = { campana.Id_Emp, campana.Id_Cam };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaIndividual_Consulta", ref dr, Parametros, Valores);


                if (dr.HasRows)
                {
                    dr.Read();


                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Nombre"))))
                        campana.Cam_Nombre = string.Empty;
                    else
                        campana.Cam_Nombre = dr.GetValue(dr.GetOrdinal("Cam_Nombre")).ToString();

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Jabon"))))
                        campana.Cam_Jabon = null;
                    else
                        campana.Cam_Jabon = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Jabon")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Toalla"))))
                        campana.Cam_Toalla = null;
                    else
                        campana.Cam_Toalla = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Toalla")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Olores"))))
                        campana.Cam_Olores = null;
                    else
                        campana.Cam_Olores = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Olores")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Quimicos"))))
                        campana.Cam_Quimicos = null;
                    else
                        campana.Cam_Quimicos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Quimicos")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Tratamiento"))))
                        campana.Cam_Tratamiento = null;
                    else
                        campana.Cam_Tratamiento = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Tratamiento")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Bolsa"))))
                        campana.Cam_Bolsa = null;
                    else
                        campana.Cam_Bolsa = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Bolsa")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Wipers"))))
                        campana.Cam_Wipers = null;
                    else
                        campana.Cam_Wipers = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Wipers")));

                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cam_Suplementos"))))
                        campana.Cam_Suplementos = null;
                    else
                        campana.Cam_Suplementos = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cam_Suplementos")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaCampanaAplicaciones(string Conexion, int id_Emp, int id_Cd, int id_Cam, ref List<AplicacionCampana> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cam"
                                      };
                object[] Valores = { 
                                       id_Emp 
                                       ,id_Cd
                                       ,id_Cam
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaAplicaciones_Consulta", ref dr, Parametros, Valores);

                List = new List<AplicacionCampana>();
                while (dr.Read())
                {
                    AplicacionCampana aplicacionCampana = new AplicacionCampana();
                    aplicacionCampana.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    aplicacionCampana.Id_Cam = id_Cam;
                    aplicacionCampana.Id_Apl = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Apl")));
                    aplicacionCampana.Apl_Descripcion = dr.GetValue(dr.GetOrdinal("Apl_Descripcion")).ToString();
                    aplicacionCampana.Id_Seg = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Seg")));
                    aplicacionCampana.Seg_Descripcion = dr.GetValue(dr.GetOrdinal("Seg_Descripcion")).ToString();
                    aplicacionCampana.Id_Uen = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Uen")));
                    aplicacionCampana.Uen_Descripcion = dr.GetValue(dr.GetOrdinal("Uen_Descripcion")).ToString();

                    List.Add(aplicacionCampana);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cosnulta un listado de las metas de una campaña
        /// </summary>
        public void ConsultaCampanasMetasLista(string Conexion, int id_Emp, int id_Cd, int id_Cam, ref List<CampanasMetas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Cam"
                                      };
                object[] Valores = { 
                                       id_Emp 
                                       ,id_Cd
                                       ,id_Cam
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaMetas_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    CampanasMetas campanasMetas = new CampanasMetas();
                    campanasMetas.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    campanasMetas.Id_Cam = id_Cam;
                    campanasMetas.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    campanasMetas.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    campanasMetas.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    campanasMetas.MetCam_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MetCam_Cantidad")));
                    campanasMetas.MetCam_Monto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetCam_Monto")));
                    campanasMetas.MetCam_Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("MetCam_Estatus")));

                    List.Add(campanasMetas);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consulta las metas de una campaña, en base a la UEN y los representantes de la UEN
        /// </summary>
        public void ConsultaCampanasMetas(string Conexion, int id_Emp, int id_Cd, int id_Uen, int? id_Cam, ref List<CampanasMetas> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@Id_Emp" 
                                          ,"@Id_Cd"
                                          ,"@Id_Uen"
                                          ,"@Id_Cam"
                                      };
                object[] Valores = { 
                                       id_Emp 
                                       ,id_Cd
                                       ,id_Uen
                                       ,id_Cam
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaUENRepresentantes_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    CampanasMetas campanasMetas = new CampanasMetas();
                    campanasMetas.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    campanasMetas.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    campanasMetas.Id_Cd = id_Cd;
                    campanasMetas.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    campanasMetas.MetCam_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MetCam_Cantidad")));
                    campanasMetas.MetCam_Monto = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("MetCam_Monto")));
                    campanasMetas.MetCam_Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("MetCam_Estatus")));

                    List.Add(campanasMetas);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCampana(int Id_Emp, int Id_Cam, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp" 
                                        ,"@Id_Cam"
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cam
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampania_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCampanaAplicacion(int Id_Emp, int Id_Cam, int Id_Apl, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp"
                                        ,"@Id_Cam"
                                        ,"@Id_Apl"
                                      };
                object[] Valores = { 
                                        Id_Emp
                                        ,Id_Cam
                                        ,Id_Apl
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaAplicacion_Eliminar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCampana(ref Campanas campana, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cam"
                                        ,"@Cam_Nombre"
                                    };
                object[] Valores = {    
                                       campana.Id_Emp
                                       ,campana.Id_Cam
                                       ,campana.Cam_Nombre
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampania_Insertar", ref verificador, Parametros, Valores);
                campana.Id_Cam = verificador; //clave (folio) de la campaña generado



                // --------------------------------
                // Insertar aplicaciones de la campaña
                // --------------------------------
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cam"				
                                            ,"@Id_Apl"
                                            ,"@CamApl_Estatus"
                                      };

                int cont = 0;
                foreach (AplicacionCampana aplicacionCampana in campana.ListaAplicacionCampana)
                {
                    object[] ValoresDetalle = {    
                                            aplicacionCampana.Id_Emp
                                            ,campana.Id_Cam
                                            ,aplicacionCampana.Id_Apl
                                            ,aplicacionCampana.CamApl_Estatus
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaAplicacion_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
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



        public void GuardarCampanaMetas(int id_Cam, ref List<CampanasMetas> listametasCampana, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                // --------------------------------
                // Insertar metas de la campaña
                // --------------------------------
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cam"				
                                            ,"@Id_Rik"
                                            ,"@Id_Cd"
                                            ,"@MetCam_Cantidad"
                                            ,"@MetCam_Monto"
                                            ,"@MetCam_Estatus"
                                      };

                int cont = 0;
                SqlCommand sqlcmd = new SqlCommand();
                foreach (CampanasMetas cm in listametasCampana)
                {
                    object[] ValoresDetalle = {    
                                            cm.Id_Emp
                                            ,id_Cam
                                            ,cm.Id_Rik
                                            ,cm.Id_Cd
                                            ,cm.MetCam_Cantidad
                                            ,cm.MetCam_Monto
                                            ,cm.MetCam_Estatus
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaMeta_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
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


        public void ModificarCampana(ref Campanas campana, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                string[] Parametros = {
                                        "@Id_Emp"
                                        ,"@Id_Cam"
                                        ,"@Cam_Nombre"
                                    };
                object[] Valores = {    
                                       campana.Id_Emp
                                       ,campana.Id_Cam
                                       ,campana.Cam_Nombre
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampania_Modificar", ref verificador, Parametros, Valores);


                // --------------------------------
                // Insertar aplicaciones de la campaña
                // --------------------------------
                string[] ParametrosDetalle = {
                                            "@Id_Emp"			
                                            ,"@Id_Cam"				
                                            ,"@Id_Apl"
                                            ,"@CamApl_Estatus"
                                      };

                int cont = 0;
                foreach (AplicacionCampana aplicacionCampana in campana.ListaAplicacionCampana)
                {
                    object[] ValoresDetalle = {    
                                            aplicacionCampana.Id_Emp
                                            ,campana.Id_Cam
                                            ,aplicacionCampana.Id_Apl
                                            ,aplicacionCampana.CamApl_Estatus
                                          };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCRMCampaniaAplicacion_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    cont++;
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
    }
}
