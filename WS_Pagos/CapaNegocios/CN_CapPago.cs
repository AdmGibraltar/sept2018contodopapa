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
    public class CN_CapPago
    {
        public void InsertarPago(string pago_str, string list_fichas_str, string list_pagos_str, string ConexionCob, string Conexion, ref int verificador, int cd_tipo)
        {
            try
            {
                Pago pago = Deserialize(pago_str);
                List<Banco_Ficha> list_fichas = Deserialize<List<Banco_Ficha>>(list_fichas_str);
                List<PagoDet> list_pagos = Deserialize<List<PagoDet>>(list_pagos_str);

                CD_CapPago claseCapaDatos = new CD_CapPago();

                DbCentro centro = new DbCentro();

                claseCapaDatos.ConsultarCentro(pago.Id_Emp, pago.Id_Cd, cd_tipo, ref centro, ConexionCob);
                SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);

                if (centro.Db_Nombre != null)
                {
                    cnx.InitialCatalog = centro.Db_Nombre;

                    centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(pago.Id_Emp, pago.Id_CdOrigen, cd_tipo, ref centro, ConexionCob);
                    pago.Id_CdOrigenStr = centro.Db_CdNombre;

                    claseCapaDatos.InsertarPago(pago, list_fichas, list_pagos, cnx.ConnectionString, ref verificador);
                    claseCapaDatos.EnviarCorreo_Insertar(pago, cnx.ConnectionString, ref verificador);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPago(string pago_str, string list_fichas_str, string list_pagos_str, string ConexionCob, string Conexion, ref int verificador, int cd_tipo)
        {
            try
            {
                Pago pago = Deserialize(pago_str);
                List<Banco_Ficha> list_fichas = Deserialize<List<Banco_Ficha>>(list_fichas_str);
                List<PagoDet> list_pagos = Deserialize<List<PagoDet>>(list_pagos_str);

                CD_CapPago claseCapaDatos = new CD_CapPago();

                DbCentro centro = new DbCentro();

                claseCapaDatos.ConsultarCentro(pago.Id_Emp, pago.Id_Cd, cd_tipo, ref centro, ConexionCob);
                SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);

                if (centro.Db_Nombre != null)
                {
                    cnx.InitialCatalog = centro.Db_Nombre;

                    centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(pago.Id_Emp, pago.Id_CdOrigen, cd_tipo, ref centro, ConexionCob);
                    pago.Id_CdOrigenStr = centro.Db_CdNombre;

                    claseCapaDatos.ModificarPago(pago, list_fichas, list_pagos, cnx.ConnectionString, ref verificador);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(string pago_str, string ConexionCob, string Conexion, ref int verificador, int? Id_CdExt, int cd_tipo)
        {
            try
            {
                CD_CapPago claseCapaDatos = new CD_CapPago();
                Pago pago = Deserialize(pago_str);

                List<int> List = new List<int>();

                if (Id_CdExt == null)
                {
                    claseCapaDatos.ConsultarCentro(pago.Id_Emp, pago.Id_CdOrigen, pago.Id_Pag, ref List, Conexion);
                }
                else
                {
                    List.Add((int)Id_CdExt);
                }

                foreach (int Id_Cd in List)
                {
                    DbCentro centro = new DbCentro();
                    claseCapaDatos.ConsultarCentro(pago.Id_Emp, Id_Cd, cd_tipo, ref centro, ConexionCob);
                    SqlConnectionStringBuilder cnx = new SqlConnectionStringBuilder(Conexion);
                    if (centro.Db_Nombre != null)
                    {
                        cnx.InitialCatalog = centro.Db_Nombre;

                        claseCapaDatos.Baja(pago, Id_Cd, cnx.ConnectionString, ref verificador);
                        claseCapaDatos.EnviarCorreo_Baja(pago, cnx.ConnectionString, ref verificador);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Pago Deserialize(string xml)
        {
            var xs = new XmlSerializer(typeof(Pago));
            return (Pago)xs.Deserialize(new StringReader(xml));
        }

        private T Deserialize<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new StringReader(xml));
        }
    }
}
