using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_ContratoComodato
    {
        public void ConsultarCantidadContratoComCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(
                    "spContratoComodatoCantidadEnCd_Consultar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarContratoComodato_BaseInstalada(ContratoComodato contratoComodato, ref List<ContratoComodato> listaContratoCom
            , DateTime fecha1, DateTime fecha2, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                        "@Id_Emp"
                                        ,"@Id_Cd"
                                        ,"@Id_U"
                                        ,"@Id_Ter"
                                        ,"@Id_Cte"
                                        ,"@Fecha1"
                                        ,"@Fecha2"
                                      };
                object[] Valores = { 
                                       contratoComodato.Id_Emp
                                       ,contratoComodato.Id_Cd
                                       ,contratoComodato.Id_U
                                       ,contratoComodato.Id_Ter
                                       ,contratoComodato.Id_Cte
                                       ,fecha1.Year == 1 ? (DateTime?)null : fecha1 
                                       ,fecha2.Year == 1 ? (DateTime?)null : fecha2
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spContratoComodato_ConsultarBaseInstalada", ref dr, Parametros, Valores);
                listaContratoCom = new List<ContratoComodato>();
                while (dr.Read())
                {
                    ContratoComodato cc = new ContratoComodato();
                    cc.ListaContratoComodatoDetalle = new List<ContratoComodatoDetalle>();

                    cc.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    cc.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    //cc.Id_Cco = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cco")));
                    //cc.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    //cc.Rem_Estatus = dr.GetValue(dr.GetOrdinal("Rem_Estatus")).ToString();
                    //cc.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    //cc.Cco_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Cco_Fecha")));

                    cc.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    cc.Ter_Nombre = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Rik"))))
                        cc.Id_Rik = null;
                    else
                        cc.Id_Rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    if (Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Rik_Nombre"))))
                        cc.Rik_Nombre = null;
                    else
                        cc.Rik_Nombre = dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    cc.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    cc.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();

                    cc.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    cc.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();

                    cc.Contrato = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Contrato")));
                    cc.Saldo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Saldo")));
                    cc.Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Contrato")));
                    //cc.ContratoComodatoDetalle = new ContratoComodatoDetalle();
                    //cc.ContratoComodatoDetalle.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    //cc.ContratoComodatoDetalle.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    //cc.ContratoComodatoDetalle.Cco_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cco_Cantidad")));

                    listaContratoCom.Add(cc);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarContratoComodato_FechaContrato(ref List<ContratoComodato> listaContratoCom, ref int verificador, string Conexion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();

                SqlCommand sqlcmd = null;
                string[] Parametros = { "@Id_Emp"
		                             ,"@Id_Cd"
		                             ,"@Id_Cte"
		                             ,"@Id_Ter"
		                             ,"@Id_Prd"
		                             ,"@Cantidad"
		                             ,"@FechaIni"
		                             ,"@FechaFin"
		                             ,"@Contador"
		                             ,"@Fecha"
		                             ,"@Id_U"};

                foreach (ContratoComodato contratoComodato in listaContratoCom)
                {
                    object[] Valores = { 
                                           contratoComodato.Id_Emp, 
                                           contratoComodato.Id_Cd, 
                                           contratoComodato.Id_Cte,
                                           contratoComodato.Id_Ter,
                                           contratoComodato.Id_Prd,
                                           contratoComodato.Cantidad,
                                           contratoComodato.Cco_FechaIni,
                                           contratoComodato.Cco_FechaFin,
                                           verificador,
                                           contratoComodato.Cco_Fecha,
                                           contratoComodato.Id_U
                                       };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spContratoComodato_ActualizarFechaContrato", ref verificador, Parametros, Valores);
                }

                CapaDatos.CommitTrans();

                if (sqlcmd != null)
                {
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ModificarContratoComodato_FechaContrato(int Id_Emp, int Id_Cd, int folio,ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cco", "@Tipo" };
                object[] Valores = { Id_Emp, Id_Cd, folio, 1 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRemisionDetContratoComodato_Consulta", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
