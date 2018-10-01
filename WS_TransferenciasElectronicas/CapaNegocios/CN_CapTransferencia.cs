using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace CapaNegocios
{
    public class CN_CapTransferencia
    {
        public void InsertarTransferencia(string transferencia_str, string list_transferencias_str, string ConexionCen, string Conexion, ref int verificador)
        {
            try
            {
                Transferencia transferencia = Deserialize(transferencia_str);

                List<TransferenciaDet> list_transferencias = Deserialize<List<TransferenciaDet>>(list_transferencias_str);

                CD_CapTransferencia claseCapaDatos = new CD_CapTransferencia();

                DbCentro centro = new DbCentro();

                claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, transferencia.Id_Cd,  ref centro, ConexionCen);
                SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);

                if (centro.Db_Nombre != null)
                {
                    cnx.InitialCatalog = centro.Db_Nombre;

                    centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, transferencia.Id_CdOrigen, ref centro, ConexionCen);
                    transferencia.Id_CdOrigenStr = centro.Db_CdNombre;

                    claseCapaDatos.InsertarTransferencia(transferencia,  list_transferencias, cnx.ConnectionString, ref verificador);
                    claseCapaDatos.EnviarCorreo_Insertar(transferencia, cnx.ConnectionString, ref verificador);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      /*  public void Modificartransferencia(string transferencia_str, string list_fichas_str, string list_transferencias_str, string ConexionCob, string Conexion, ref int verificador, int cd_tipo)
        {
            try
            {
                transferencia transferencia = Deserialize(transferencia_str);
                List<Banco_Ficha> list_fichas = Deserialize<List<Banco_Ficha>>(list_fichas_str);
                List<TransferenciaDet> list_transferencias = Deserialize<List<transferenciaDet>>(list_transferencias_str);

                CD_Captransferencia claseCapaDatos = new CD_Captransferencia();

                DbCentro centro = new DbCentro();

                claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, transferencia.Id_Cd, cd_tipo, ref centro, ConexionCob);
                SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);

                if (centro.Db_Nombre != null)
                {
                    cnx.InitialCatalog = centro.Db_Nombre;

                    centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, transferencia.Id_CdOrigen, cd_tipo, ref centro, ConexionCob);
                    transferencia.Id_CdOrigenStr = centro.Db_CdNombre;

                    claseCapaDatos.Modificartransferencia(transferencia, list_fichas, list_transferencias, cnx.ConnectionString, ref verificador);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public void Baja(string transferencia_str, string ConexionCob, string Conexion, ref int verificador, int? Id_CdExt, int cd_tipo)
        {
            try
            {
                CD_CapTransferencia claseCapaDatos = new CD_CapTransferencia();
                Transferencia transferencia = Deserialize(transferencia_str);

                List<int> List = new List<int>();

                if (Id_CdExt == null)
                {
                    claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, transferencia.Id_CdOrigen, transferencia.Id_Trans, ref List, Conexion);
                }
                else
                {
                    List.Add((int)Id_CdExt);
                }

                foreach (int Id_Cd in List)
                {
                    DbCentro centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(transferencia.Id_Emp, Id_Cd,  ref centro, ConexionCob);
                    SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);
                    if (centro.Db_Nombre != null)
                    {
                        cnx.InitialCatalog = centro.Db_Nombre;

                        claseCapaDatos.Baja(transferencia, Id_Cd, cnx.ConnectionString, ref verificador);
                        claseCapaDatos.EnviarCorreo_Baja(transferencia, cnx.ConnectionString, ref verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Transferencia Deserialize(string xml)
        {
            var xs = new XmlSerializer(typeof(Transferencia));
            return (Transferencia)xs.Deserialize(new StringReader(xml));
        }

        private T Deserialize<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new StringReader(xml));
        }
    }
}
