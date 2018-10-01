using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatSolucion
    {
        public void Lista(Solucion area, string Conexion, ref List<Solucion> List)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Lista(area, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Solucion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Insertar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Solucion area, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatSolucion claseCapaDatos = new CD_CatSolucion();
                claseCapaDatos.Modificar(area, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatSolucion> ObtenerPorEmpresaYArea(int idEmp, int idArea, Sesion sesion)
        {
            CD_CatSolucion cdCatSolucion = new CD_CatSolucion();
            var res = cdCatSolucion.ConsultarPorEmpresaYArea(idEmp, idArea, sesion.Emp_Cnx_EF);
            return res;
        }

        /// <summary>
        /// Regresa el conjunto de soluciones asociados a un área
        /// </summary>
        /// <param name="idEmp">Identificador de la emmpresa</param>
        /// <param name="idArea">Identificador del área</param>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CatSolucion]</returns>
        public IEnumerable<CatSolucion> ObtenerPorEmpresaYArea(int idEmp, int idArea, Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CatSolucion cdCatSolucion = new CD_CatSolucion();
            var res = cdCatSolucion.ConsultarPorEmpresaYArea(idEmp, idArea, ibt.DataContext);
            return res;
        }
    }
}
