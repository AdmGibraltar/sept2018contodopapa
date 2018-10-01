using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ProCompraLocal
    {

        public void InsertarSolicitud(CompraLocal compralocal, DataTable dt, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;
                CapaDatos.StartTrans();
                int ver = 0;

                //Guarda la cabezera
                Parametros = new string[] { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Comp_FechaSol",
                                        "@Comp_Solicito"
                                      };
                Valores = new object[]{ 
                                        compralocal.Id_Emp,
                                        compralocal.Id_Cd,
                                        compralocal.FechaSol,
                                        compralocal.Comp_Solicito
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Insertar", ref verificador, Parametros, Valores);
                ver = verificador;

                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Id_CompDet", "@Id_Prd", "@Det_Costo", "@Det_Estatus", "@Accion" };
                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    Valores = new object[] {
                                            compralocal.Id_Emp,
                                            compralocal.Id_Cd,
                                            ver,
                                            x +1,
                                            dt.Rows[x]["Num"],
                                            Convert.ToDouble(dt.Rows[x]["Costo"]),
                                            dt.Rows[x]["Estatus"],
                                            x

                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocalDet_Insertar", ref verificador, Parametros, Valores);

                }
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                verificador = ver;
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarSolicitud(CompraLocal compralocal, DataTable dt, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;

                CapaDatos.StartTrans();

                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Id_CompDet", "@Id_Prd", "@Det_Costo", "@Det_Estatus", "@Accion" };
                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    Valores = new object[] {
                                            compralocal.Id_Emp,
                                            compralocal.Id_Cd,
                                            compralocal.Id_Comp,
                                            x+1,
                                            dt.Rows[x]["Num"],
                                            Convert.ToDouble(dt.Rows[x]["Costo"]),
                                            dt.Rows[x]["Estatus"],
                                            x
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocalDet_Insertar", ref verificador, Parametros, Valores);

                }
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarSolicitud(CompraLocal compralocal, DataTable dt, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Comp" };
                object[] Valores = { compralocal.Id_Emp, compralocal.Id_Cd, compralocal.Id_Comp };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocalDet_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    dt.Rows.Add(new object[] { 
                                               dr.GetValue(dr.GetOrdinal("Id_Prd")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                                               Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Det_Costo"))).ToString("#,##0.00"),
                                               dr.GetValue(dr.GetOrdinal("Det_Estatus")),
                                               Estatus(dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString())
                    });
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object Estatus(string p)
        {
            switch (p.ToUpper())
            {
                case "0": return "Sin autorizar";
                case "A": return "Autorizado";
                case "R": return "Rechazado";
                default: return "N/A";
            }
        }
        
        public void ConsultaCompraLocalList(int Id_Emp, int Id_Cd, int Id_Sol, string Conexion, ref List<ProductoLocal> List)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Comp" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Sol };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocalDet_Consultar", ref dr, Parametros, Valores);
                ProductoLocal pl = default(ProductoLocal);
                while (dr.Read())
                {
                    pl = new ProductoLocal();
                    pl.Id_Det = dr.GetInt32(dr.GetOrdinal("Id_CompDet"));
                    pl.Autorizado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "A" ? true : false;
                    pl.Rechazado = dr.GetValue(dr.GetOrdinal("Det_Estatus")).ToString().ToUpper() == "R" ? true : false;
                    pl.CompraEnfocada = dr.IsDBNull(dr.GetOrdinal("Det_Enfocada")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Det_Enfocada")));
                    pl.Costo = dr.IsDBNull(dr.GetOrdinal("Det_Costo")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Det_Costo")));
                    pl.Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    pl.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Det_FecAut")));
                    pl.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    List.Add(pl);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarSolicitud(ref CompraLocal cl, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Com" };
                object[] Valores = { cl.Id_Emp, cl.Id_Cd, cl.Id_Comp };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocal_Consulta", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    cl.Cd_Nombre = dr.IsDBNull(dr.GetOrdinal("Cd_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    cl.Comp_Solicito = dr.IsDBNull(dr.GetOrdinal("Comp_Solicito")) ? 0 : dr.GetInt32(dr.GetOrdinal("Comp_Solicito"));
                    cl.Solicito_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    cl.FechaAut = dr.IsDBNull(dr.GetOrdinal("Det_FecAut")) ? "" : dr.GetDateTime(dr.GetOrdinal("Det_FecAut")).ToString("dd/MM/yyyy hh:mm:ss tt").ToUpper();
                    cl.FechaSol = dr.GetDateTime(dr.GetOrdinal("Comp_FechaSol"));
                    verificador = 1;
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCompraLocal(CompraLocal cl, List<ProductoLocal> list, string Conexion, ref int verificador)
        {
            CD_Datos CapaDatos = default(CD_Datos);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros;
                object[] Valores;

                CapaDatos.StartTrans();

                //Guardar los Detalles
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Comp", "@Id_CompDet", "@Det_Estatus", "@Det_FecAut", "@Det_Enfocada", "@Det_Autorizo" };
                foreach (ProductoLocal prodlocal in list)
                {
                    Valores = new object[] {
                                            cl.Id_Emp,
                                            cl.Id_Cd,
                                            cl.Id_Comp,
                                            prodlocal.Id_Det,
                                            prodlocal.Estatus,
                                            prodlocal.FechaAut==null ? (object)null: Convert.ToDateTime(prodlocal.FechaAut),
                                            prodlocal.CompraEnfocada,
                                            prodlocal.Autorizo
                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProCompraLocalDet_Modificar", ref verificador, Parametros, Valores);

                }
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultarPrdCompraLocal(Sesion sesion, int prd, ref List<ProductoLocal> List)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Prd" };
                object[] Valores = { sesion.Id_Emp, sesion.Id_Cd_Ver, prd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatProductoLocal_Consultar", ref dr, Parametros, Valores);

                ProductoLocal pl = new ProductoLocal();
                if (dr.HasRows)
                {
                    dr.Read();
                    pl = new ProductoLocal();
                    pl.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    pl.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    List.Add(pl);
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
