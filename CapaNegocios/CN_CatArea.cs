using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatArea
    {

        public void Lista(Area area, string Conexion, ref List<Area> List)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Lista(area, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Area area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Insertar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Area area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Modificar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Borrar(Area area, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatArea claseCapaDatos = new CD_CatArea();
                claseCapaDatos.Borrar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Regresa el conjunto de áreas asociadas al segmento especificado
        /// </summary>
        /// <param name="s">Sesión del usario en operación</param>
        /// <param name="idUen">Identificador del centro de negocio</param>
        /// <param name="idSegmento">Identificador del segmento</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CatArea]</returns>
        public IEnumerable<CatArea> Obtener(Sesion s, int idUen, int idSegmento, IBusinessTransaction ibt)
        {
            CD_CatArea cdCatArea = new CD_CatArea();
            return cdCatArea.Consultar(s.Id_Emp, idUen, idSegmento, ibt.DataContext);
        }
    }
}
