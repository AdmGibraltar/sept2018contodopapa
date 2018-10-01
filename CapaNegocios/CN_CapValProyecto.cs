using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapValProyecto
    {
        /// <summary>
        /// Crea una nueva entrada en el repositorio CapValProyecto.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="datos">Instancia de datos de la entidad CapValProyecto</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>CapValProyecto</returns>
        public CapValProyecto Crear(Sesion s, CapValProyecto datos, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            string idStr = CN_Comun.Maximo(s.Id_Emp, s.Id_Cd_Ver, "CapValProyecto", "Id_Vap", ibt.DataContext, "spCatLocal_Maximo");
            int id = 0;
            id = int.Parse(idStr);
            datos.Id_Vap = id;
            return cdCapValuacionProyecto.Insertar(datos, ibt.DataContext);
        }

        public CapValProyecto Crear_(Sesion s, CapValProyecto datos, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            /*CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            string idStr = CN_Comun.Maximo(s.Id_Emp, s.Id_Cd_Ver, "CapValProyecto", "Id_Vap", ibt.DataContext, "spCatLocal_Maximo");
            int id = 0;
            id = int.Parse(idStr);
            datos.Id_Vap = id;*/
            return cdCapValuacionProyecto.Insertar(datos, ibt.DataContext);
        }


        /// <summary>
        /// Devuelve el detalle de los productos de una valuación, dado el identificador de la valuación
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <returns>IEnumerable[CapValProyectoDet]. Conjunto de productos asociados a la valuación</returns>
        public IEnumerable<CapValProyectoDet> ObtenerDetalle(Sesion s, int idVal)
        {
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            return cdCapValProyectoDet.ConsultarPorCapValProyectoId(s.Id_Emp, s.Id_Cd, idVal, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve el detalle de los productos de una valuación, dado el identificador de la valuación
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CapValProyectoDet]. Conjunto de productos asociados a la valuación</returns>
        public IEnumerable<CapValProyectoDet> ObtenerDetalle(Sesion s, int idVal, IBusinessTransaction ibt)
        {
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            return cdCapValProyectoDet.ConsultarPorCapValProyectoId(s.Id_Emp, s.Id_Cd, idVal, ibt.DataContext);
        }

        /// <summary>
        /// Indica en términos absolutos si una valuación tiene asociada la estructura necesaria para persistir su propuesta técnica.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="ibt">Transacción de nivel de negocio</param>
        /// <returns>True en caso de que tenga la estructura asociada; falso en caso contrario</returns>
        public bool TienePropuestaTecnica(Sesion s, int idCte, int idVal, IBusinessTransaction ibt)
        {
            CD_CrmPropuestaTecnica cdCrmPropuestaTecnica = new CD_CrmPropuestaTecnica();
            var detalle = cdCrmPropuestaTecnica.ConsultarDetallePropuestaTecnica(s.Id_Emp, s.Id_Cd, idCte, s.Id_Rik, idVal, ibt.DataContext);
            return detalle.Count() > 0;
        }

        public CapValProyecto ObtenerPorId(Sesion s, int idVap)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVap, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve la instancia de CapValProyecto dado el identificador de la instancia
        /// </summary>
        /// <param name="s">Sesión de usuario en operación</param>
        /// <param name="idVap">Identificador de la valuación</param>
        /// <param name="ibt">Transacción de negocios</param>
        /// <returns>CapValProyecto</returns>
        public CapValProyecto ObtenerPorId(Sesion s, int idVap, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVap, ibt.DataContext);
        }

        /// <summary>
        /// Devuelve el conjunto de valuaciones a autorizar por parte del gerente del CDI.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <returns>IEnumerable de CapValProyecto.</returns>
        public IEnumerable<CapValProyecto> AAutorizar(Sesion s)
        {
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(s))
            {
                ibt.Begin();
                CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
                return cdCapValuacionProyecto.ConsultarValuacionesAAutorizar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
            }
        }

        /// <summary>
        /// Devuelve el conjunto de valuaciones a autorizar por parte del gerente del CDI.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción a nivel de capa de negocios</param>
        /// <returns>IEnumerable de CapValProyecto.</returns>
        public IEnumerable<CapValProyecto> AAutorizar(Sesion s, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarValuacionesAAutorizar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
        }

        //
        // 11 Sep 2018 
        // 
        public List<eCapValProyecto> CRM2_ConsultarValuacionesAAutorizar(Sesion s)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.CRM2_ConsultarValuacionesAAutorizar(s.Id_Emp, s.Id_Cd, s.Emp_Cnx);
        }


        /// <summary>
        /// Autoriza una valuación. Marca la utilidad remanente como aceptada y al mismo tiempo marca el detalle de la 
        /// valuación como autorizada.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void Autorizar(Sesion s, int idVal, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            cdCapValuacionProyecto.ActualizarAtributoCap_Estatus2(s.Id_Emp, s.Id_Cd, idVal, 3, ibt.DataContext);
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();

            //Se marca el detalle como autorizado
            var detalle = cdCapValProyectoDet.ConsultarPorCapValProyectoId(s.Id_Emp, s.Id_Cd, idVal, ibt.DataContext);
            foreach (var d in detalle)
            {
                cdCapValProyectoDet.ActualizarAtributosDeAutorizacion(s.Id_Emp, s.Id_Cd, d.Id_Vap, d.Id_VapDet, "A", DateTime.Now, s.Id_U, ibt.DataContext);
            }

            //Se marca la valuación como autorizada
            var valuacion = cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVal, ibt.DataContext);
            valuacion.Vap_Estatus = "A";

            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            var valuacionesProyectos = cdCrmValuacionOportunidades.ConsultarPorValuacion(s.Id_Emp, s.Id_Cd, s.Id_Rik, idVal, ibt.DataContext);
            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            foreach (var vp in valuacionesProyectos)
            {
                var proyecto = cnCrmOportunidad.ObtenerPorId(s, vp.Id_Op, ibt);
                CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, s);
                psm.Transaction = ibt;

                var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(s.Id_Emp, s.Id_Cd, proyecto.Id_Op, vp.CrmOportunidade.Id_Cte.Value, ibt.DataContext);
                proyecto.CrmOportunidadesProducto = productos;
                psm.Update();
            }
        }

        public void Autorizar(Sesion s, int idVal)
        {
            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(s))
            {
                ibt.Begin();
                var valuacion = ObtenerPorId(s, idVal, ibt);
                if (valuacion.Vap_Estatus2 == 2)
                {
                    Autorizar(s, idVal, ibt);
                    ibt.Commit();
                }
                else
                {
                    throw new ValuacionInhabilitadaParaAutorizarException();
                }
            }
        }

        public class ValuacionInhabilitadaParaAutorizarException
            : Exception
        {
            public ValuacionInhabilitadaParaAutorizarException()
                : base("La valuación se encuentra en un estado en el cual la autorización no debe proceder")
            {
            }
        }
    }
}