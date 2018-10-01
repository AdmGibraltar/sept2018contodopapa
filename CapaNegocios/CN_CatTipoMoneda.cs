using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatTipoMoneda
    {
        public void ConsultaTipoMoneda(TipoMoneda tipoMoneda, int id_Emp, string Conexion, ref List<TipoMoneda> List)
        {
            try
            {
                CD_CatTipoMoneda claseCapaDatos = new CD_CatTipoMoneda();
                claseCapaDatos.ConsultaTipoMoneda(tipoMoneda, Conexion, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta un contexto de conexión a la fuente de datos.
        /// </summary>
        /// <param name="tipoMoneda"></param>
        /// <param name="id_Emp"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <param name="List"></param>
        public void ConsultaTipoMoneda(TipoMoneda tipoMoneda, int id_Emp, IBusinessTransaction ibt, ref List<TipoMoneda> List)
        {
            try
            {
                CD_CatTipoMoneda claseCapaDatos = new CD_CatTipoMoneda();
                claseCapaDatos.ConsultaTipoMoneda(tipoMoneda, ibt.DataContext, id_Emp, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoMonedaIndividual(ref TipoMoneda tipoMoneda, string Conexion)
        {
            try
            {
                CD_CatTipoMoneda claseCapaDatos = new CD_CatTipoMoneda();
                claseCapaDatos.ConsultaTipoMonedaIndividual(ref tipoMoneda, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoMoneda(TipoMoneda tipoMoneda, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoMoneda claseCapaDatos = new CD_CatTipoMoneda();
                claseCapaDatos.InsertarTipoMoneda(tipoMoneda, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarTipoMoneda(TipoMoneda tipoMoneda, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatTipoMoneda claseCapaDatos = new CD_CatTipoMoneda();
                claseCapaDatos.ModificarTipoMoneda(tipoMoneda, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
