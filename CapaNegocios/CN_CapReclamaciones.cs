using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapReclamaciones
    {
        /// <summary>
        /// Metodo para insertar los datos de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="dt">data table donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void InsertaReclamaciones(CapaEntidad.Reclamaciones reclamaciones, DataTable dt, string conexion, ref int verificador)
        {
            try
            {
                CD_CapReclamaciones capaDatos = new CD_CapReclamaciones();
                capaDatos.InsertarReclamaciones(reclamaciones, dt, conexion, ref verificador);
            }
            catch (Exception ex)
            {                                
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para modificar los datos de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="dt">data table donde se vaciaran los resultados obtenidos</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Indica si se pudo o no realizar la operacion</param>
        public void ModificaReclamaciones(CapaEntidad.Reclamaciones reclamaciones, DataTable dt, string conexion, ref int verificador)
        {
            try
            {
                CD_CapReclamaciones capaDatos = new CD_CapReclamaciones();
                capaDatos.ModificaReclamaciones(reclamaciones, dt, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Metodo que busca los datos en la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="lista">lista donde se vaciaran los resultados obtenidos</param>
        /// <param name="Id_Emp">Id de la empresa</param>
        /// <param name="Id_Cd">Id de la ciudad</param>
        /// <param name="Id_Rec_Ini">Id de la reclamacion, tomado como un rango inicial</param>
        /// <param name="Id_Rec_Fin">Id de la reclamacion, tomado como un rango final</param>
        /// <param name="Id_Cte_Ini">Id del cliente, tomado como un rango inicial</param>
        /// <param name="Id_Cte_Fin">Id del cliente, tomado como un rango final</param>
        /// <param name="Rec_Estatus">Estatus de las reclamaciones</param>
        /// <param name="Rec_Fecha_Ini">Fecha para buscar desde un rango inicial</param>
        /// <param name="Rec_Fecha_Fin">Fecha para buscar hasta un rango final</param>
        /// <param name="NomCte">Nombre del cliente</param>
        /// <param name="Id_Tipo">Id del tipo de la reclamacion</param>
        public void BuscaReclamaciones(Reclamaciones reclamaciones, string conexion, ref List<Reclamaciones> lista,
            int Id_Emp, int Id_Cd, int Id_Rec_Ini, int Id_Rec_Fin, int Id_Cte_Ini, int Id_Cte_Fin, string Rec_Estatus,
            DateTime Rec_Fecha_Ini, DateTime Rec_Fecha_Fin, string NomCte, int Id_Tipo)
        {
            try
            {
                CD_CapReclamaciones CDCapReclamaciones = new CD_CapReclamaciones();

                CDCapReclamaciones.BuscaReclamaciones(reclamaciones, conexion, ref lista, Id_Emp, Id_Cd, Id_Rec_Ini, 
                    Id_Rec_Fin, Id_Cte_Ini, Id_Cte_Fin, Rec_Estatus, Rec_Fecha_Ini, Rec_Fecha_Fin, NomCte, Id_Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hace una consulta y trae un resultado de la base de datos
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        public void ConsultaReclamaciones(ref Reclamaciones reclamaciones, string conexion)
        {
            try
            {
                CD_CapReclamaciones CDCapReclamaciones = new CD_CapReclamaciones();
                CDCapReclamaciones.ConsultaReclamaciones(ref reclamaciones, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que pone en estatus de Baja una reclamacion
        /// </summary>
        /// <param name="reclamaciones">Entidad de las reclamaciones</param>
        /// <param name="conexion">Cadena de conexion a la base de datos</param>
        /// <param name="verificador">Dijito que indica si se efectuo la operacion correctamente</param>
        public void BajaReclamaciones(Reclamaciones reclamaciones, string conexion, ref int verificador)
        {
            try
            {
                CD_CapReclamaciones CDCapReclamaciones = new CD_CapReclamaciones();
                CDCapReclamaciones.BajaReclamaciones(reclamaciones, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ImprimirReclamaciones(Reclamaciones reclamaciones, string conexion, ref int verificador)
        {
            try
            {
                CD_CapReclamaciones CDCapReclamaciones = new CD_CapReclamaciones();
                CDCapReclamaciones.ImprimirReclamaciones(reclamaciones, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
