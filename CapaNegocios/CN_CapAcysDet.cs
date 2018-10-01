using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CapAcysDet
    {
        public CN_CapAcysDet(IBusinessTransaction ibt)
        {
            _ibt = ibt;
        }

        public IEnumerable<CapAcysDet> AgregarProductosAACYS(IEnumerable<CapAcysDet> productos)
        {
            //Este método solo llama a la operación de datos para agregar las entidades al repositorio de datos.
            //Debiera de correrse una validación para determinar el estado del ACYS, y determinar si es posible
            //este movimiento.
            //Es posible que esta operación deba de disparar de nuevo el flujo de autorización del ACYS.
            CD_CapAcysDet cdCapAcysDet = new CD_CapAcysDet(_ibt.DataContext);
            return cdCapAcysDet.Insertar(productos);
        }

        /// <summary>
        /// Devuelve el conjunto de productos asociados a un ACYS. Se utiliza la última versión del ACYS.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idAcys">Identificador del ACYS</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CapAcysDet]</returns>
        public IEnumerable<CapAcysDet> ObtenerProductosDeACYS(Sesion s, int idAcys, int idCte, int idTerritorio, IBusinessTransaction ibt)
        {
            CD_CapAcysDet cdCapAcysDet = new CD_CapAcysDet(_ibt.DataContext);
            return cdCapAcysDet.ConsultarPorAcys(s.Id_Emp, s.Id_Cd, idAcys, idCte, idTerritorio, ibt.DataContext);
        }

        //
        // RFH 06 04 2018 
        // Reemplaza la consulta por EF
        //
        public List<CapaEntidad.eCapAcysDet> Consulta_ProductosDeACYS(int idEmp, int idCd, int idCte, int idAcs, int idTer, Sesion sesion)
        {
            List<CapaEntidad.eCapAcysDet> lst = new List<CapaEntidad.eCapAcysDet>();
            try
            {
                CD_CapAcysDet clsCD = new CD_CapAcysDet(sesion.Emp_Cnx);
                lst = clsCD.Consulta_ProductosDeACYS(idEmp, idCd, idCte, idAcs, idTer);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null; 
            }
            return lst;
        }

        private IBusinessTransaction _ibt=null;
    }
}
