using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatSegmentos
    {
        public void ConsultaSegmentos(int Id_Emp, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmentos(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarSegmentos(Segmentos segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.InsertarSegmentos(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarSegmentos(Segmentos segmento, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ModificarSegmentos(segmento, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmentos(int Id_Emp, int Id_Seg, string Conexion, ref List<Segmentos> List)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmentos(Id_Emp, Id_Seg, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSegmento_Usuario(ref List<Segmentos> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmento_Usuario(ref list, Id_Emp, Id_U, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSegmentoTer(int Id_Emp, int Id_Cd, int Id_Ter, string Conexion, ref Segmentos segmento)
        {
            try
            {
                CD_CatSegmentos claseCapaDatos = new CD_CatSegmentos();
                claseCapaDatos.ConsultaSegmentoTer(Id_Emp, Id_Cd, Id_Ter, Conexion, ref segmento);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatSegmento> ObtenerSegmentosPorUen(int idEmp, int idUen, Sesion sesion)
        {
            CD_CatSegmentos cdCatSegmentos = new CD_CatSegmentos();
            var result = cdCatSegmentos.ConsultarSegmentosPorUen(idEmp, idUen, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// Regresa el conjunto de segmentos asociados a la unidad de negocio
        /// </summary>
        /// <param name="idUen">Identificador de la unidad de negocio</param>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CatSegmento]</returns>
        public IEnumerable<CatSegmento> ObtenerSegmentosPorUen(int idUen, Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CatSegmentos cdCatSegmentos = new CD_CatSegmentos();
            var result = cdCatSegmentos.ConsultarSegmentosPorUen(sesion.Id_Emp, idUen, ibt.DataContext);
            return result;
        }
    }
}
