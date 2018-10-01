using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsecutivoFE
    {
        public void ConsultaConsecutivo(int Id_Emp, int Id_Cd, string Conexion, ref List<ConsecutivoFE> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Id_Emp, Id_Cd };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConsecutivoFactElec_Consulta", ref dr, Parametros, Valores);

                ConsecutivoFE FactElect = default(ConsecutivoFE);
                while (dr.Read())
                {
                    FactElect = new ConsecutivoFE();
                    FactElect.Empresa = dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    FactElect.CentroDistribucion = dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    FactElect.Id = dr.GetValue(dr.GetOrdinal("Id_Cfe"));
                    FactElect.NombreAcuse = dr.GetValue(dr.GetOrdinal("Cfe_NombreAcuse"));
                    FactElect.FolioSAT = dr.GetValue(dr.GetOrdinal("Cfe_Llave"));
                    FactElect.Año = dr.GetValue(dr.GetOrdinal("Cfe_Año"));
                    FactElect.RazonSocial = dr.GetValue(dr.GetOrdinal("Cfe_RazonSocial"));
                    FactElect.NumRazonSocial = dr.GetValue(dr.GetOrdinal("Cfe_NumCertificado"));
                    FactElect.UltimoFolio = dr.IsDBNull(dr.GetOrdinal("Cfe_UltFolio")) ? (double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cfe_UltFolio")));
                    FactElect.RangoInicial = dr.GetValue(dr.GetOrdinal("Cfe_FolioIni"));
                    FactElect.RangoFinal = dr.GetValue(dr.GetOrdinal("Cfe_FolioFin"));
                    FactElect.RangoFecha = dr.GetValue(dr.GetOrdinal("Cfe_FechaVigencia"));
                    FactElect.TipoMovimiento = dr.GetValue(dr.GetOrdinal("Cfe_TMov"));
                    FactElect.FolioAprovacion = dr.GetValue(dr.GetOrdinal("Cfe_FolioAprobacion"));
                    FactElect.Estatus = dr.GetValue(dr.GetOrdinal("Cfe_Estatus"));
                    if (Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cfe_Estatus"))))
                    {
                        FactElect.EstatusStr = "Activo";
                    }
                    else
                    {
                        FactElect.EstatusStr = "Inactivo";
                    }
                    list.Add(FactElect);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaConsecutivo(int Id_Emp, int Cfe_Tmov, string Cfe, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Cfe_Tmov", "@Cfe" };
                object[] Valores = { Id_Emp, Cfe_Tmov, Cfe };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConsecutivoFactElec_ConsultaExistencia", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void InsertarConsecutivo(ref ConsecutivoFE FactElect, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                          "@Empresa", 
                                          "@Centro", 
                                          "@NombreAcuse",  
                                          "@FolioSAT", 
                                          "@Año", 
                                          "@RazonSocial", 
                                          "@NumRazonSocial", 
                                          "@UltimoFolio", 
                                          "@RangoInicial",   
                                          "@RangoFinal", 
                                          "@RangoFecha",  
                                          "@TipoMovimiento", 
                                          "@FolioAprobacion", 
                                          "@Estatus"
                                      };
                object[] Valores = {
                                       FactElect.Empresa, 
                                       FactElect.CentroDistribucion,  
                                       FactElect.NombreAcuse, 
                                       FactElect.FolioSAT,  
                                       FactElect.Año,  
                                       FactElect.RazonSocial,
                                       FactElect.NumRazonSocial,  
                                       FactElect.UltimoFolio,
                                       FactElect.RangoInicial,
                                       FactElect.RangoFinal,     
                                       FactElect.RangoFecha,  
                                       FactElect.TipoMovimiento, 
                                       FactElect.FolioAprovacion,
                                       FactElect.Estatus, 
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatConsecutivo_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConsecutivo(ref ConsecutivoFE FactElect, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);
                string[] Parametros = { 
                                          "@Empresa", 
                                          "@Centro", 
                                          "@NombreAcuse",  
                                          "@FolioSAT", 
                                          "@Año", 
                                          "@RazonSocial", 
                                          "@NumRazonSocial", 
                                          "@UltimoFolio", 
                                          "@RangoInicial",   
                                          "@RangoFinal", 
                                          "@RangoFecha",  
                                          "@TipoMovimiento", 
                                          "@FolioAprobacion", 
                                          "@Estatus",
                                          "@Id_Cfe",
                                          "@TipoMovimientoOld"
                                      };
                object[] Valores = {
                                       FactElect.Empresa, 
                                       FactElect.CentroDistribucion,  
                                       FactElect.NombreAcuse, 
                                       FactElect.FolioSAT,  
                                       FactElect.Año,  
                                       FactElect.RazonSocial,
                                       FactElect.NumRazonSocial,  
                                       FactElect.UltimoFolio,
                                       FactElect.RangoInicial,
                                       FactElect.RangoFinal,     
                                       FactElect.RangoFecha,  
                                       FactElect.TipoMovimiento, 
                                       FactElect.FolioAprovacion,
                                       FactElect.Estatus, 
                                       FactElect.Id,
                                       FactElect.TipoMovimientoOld
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatConsecutivo_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
