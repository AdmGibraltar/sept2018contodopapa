using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocios
{
    public class CN_CrmOportunidadesProductos
    {

        public int Update_CrmOportunidadesProductos(
            int Id_Emp,int Id_Cd,
            int Id_Op, int Id_Val, int Id_Cte, int Id_Prd, decimal Cantidad,
            int AplDilucion, decimal DilucionA, decimal DilucionC,
            string CPT_ProductoActual, string CPT_SituacionActual, string CPT_VentajasKey, 
            string CPT_RecursoImagenProductoActual, string CPT_RecursoImagensolucionKey,
            decimal COP_CostoEnUso,
            string Conexion)
        {

            CD_CrmOportunidadesProductos cdOP = new CD_CrmOportunidadesProductos();

            return cdOP.Update_CrmOportunidadesProductos(
                Id_Emp, Id_Cd, Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad,
                AplDilucion, DilucionA, DilucionC, 
                CPT_ProductoActual, CPT_SituacionActual, CPT_VentajasKey, 
                CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
                COP_CostoEnUso,
            Conexion);
        }


        /// <summary>
        /// Obtiene los productos asociados a un proyecto
        /// </summary>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <returns>Conjunto de productos asociados al proyecto especificado</returns>
        public IEnumerable<CrmOportunidadesProducto> ObtenerProductosPorOportunidad(Sesion sesion, int Id_CrmOportunidad, int Id_Cte)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var result=cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, Id_CrmOportunidad, Id_Cte, sesion.Emp_Cnx_EF);
            return result;
        }

        /// <summary>
        /// Obtiene los productos asociados a un proyecto
        /// </summary>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CrmOportunidadesProducto]</returns>
        public IEnumerable<CrmOportunidadesProducto> ObtenerProductosPorOportunidad(Sesion sesion, int Id_CrmOportunidad, int Id_Cte, IBusinessTransaction ibt)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var result = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, Id_CrmOportunidad, Id_Cte, ibt.DataContext);
            return result;
        }

        /// <summary>
        /// Registra una nueva instancia de un producto asociado a un proyecto en la fuente de persistencia.
        /// </summary>
        /// <param name="sesion">Objecto de llave de inicio de sesión</param>
        /// <param name="crmOportunidadesProducto">Información del registro</param>
        /// <returns>CrmOportunidadesProducto. Información del registro</returns>
        public CrmOportunidadesProducto Crear(Sesion sesion, CrmOportunidadesProducto crmOportunidadesProducto, ref int iResult)
        {
            CrmOportunidadesProducto result = null;
            
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(sesion))
            {
                ibt.Begin();
                //Se invoca la llamada a la operación para crear la asociación del producto con el proyecto
                CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                var producto = cdCrmOportunidadesProductos.Insertar(crmOportunidadesProducto, ibt.DataContext, ref iResult);

                //Se valida el estado del proyecto
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, crmOportunidadesProducto.Id_Op, ibt);
                CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                psm.Transaction = ibt;

                var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, crmOportunidadesProducto.Id_Op, crmOportunidadesProducto.Id_Cte, ibt.DataContext);
                proyecto.CrmOportunidadesProducto = productos;
                psm.Update();

                result = producto;

                ibt.Commit();
            }
                       
            return result;
        }

        /// <summary>
        /// Elimina el registro persistente en la fuente de datos.
        /// </summary>
        /// <param name="sesion">Objeto de llave de sesión.</param>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idRik">Identificador del representante</param>
        /// <param name="idOp">Identificador del proyecto</param>
        public void Eliminar(Sesion sesion, int idCte, int idOp, int idPrd)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            cdCrmOportunidadesProductos.Eliminar(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idOp, idPrd, sesion.Emp_Cnx_EF);
        }

        public void Actualizar(Sesion sesion, CrmOportunidadesProducto crmOportunidadesProducto)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            cdCrmOportunidadesProductos.Actualizar(crmOportunidadesProducto, sesion.Emp_Cnx_EF);
        }

        public void Actualizar(Sesion sesion, IEnumerable<CrmOportunidadesProducto> crmOportunidadesProductos)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            foreach (var cop in crmOportunidadesProductos)
            {
                cop.Id_Emp = sesion.Id_Emp;
                cop.Id_Cd = sesion.Id_Cd;
            }
            cdCrmOportunidadesProductos.Actualizar(crmOportunidadesProductos, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// Actualiza los campos de interés en la edición de la propuesta tecno/económica
        /// </summary>
        /// <param name="sesion">Sesión de usuario en la llamada</param>
        /// <param name="crmOportunidadesProductos">Conjunto de productos asociados a la propuesta a modificar</param>
        public void ActualizarEdicionPropuesta(Sesion sesion, IEnumerable<CrmOportunidadesProducto> crmOportunidadesProductos)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            foreach (var cop in crmOportunidadesProductos)
            {
                cop.Id_Emp = sesion.Id_Emp;
                cop.Id_Cd = sesion.Id_Cd;
            }
            cdCrmOportunidadesProductos.ActualizarDesdePropuesta(crmOportunidadesProductos, sesion.Emp_Cnx_EF);
        }

        public IEnumerable<CrmOportunidadesProducto> ObtenerPropuestaEconomica(Sesion s, int idVal, int idCte)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var productos = cdCrmOportunidadesProductos.ConsultarProductosDePropuesta(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, idVal, s.Emp_Cnx_EF);

            return productos;
        }

        public IEnumerable<CrmOportunidadesProducto> ObtenerPropuestaEconomica(Sesion s, int idVal, int idCte, IBusinessTransaction ibt)
        {
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            var productos = cdCrmOportunidadesProductos.ConsultarProductosDePropuesta(s.Id_Emp, s.Id_Cd, s.Id_Rik, idCte, idVal, ibt.DataContext);

            return productos;
        }

    }
}
