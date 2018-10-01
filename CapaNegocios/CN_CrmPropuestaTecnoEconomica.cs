using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaModelo;
using CapaNegocios;

namespace CapaNegocios
{
    /// <summary>
    /// Clase de manejo de operaciones de reglas de negocio concerniente al instrumento de propuestas tecno-económicas
    /// </summary>
    public class CN_CrmPropuestaTecnoEconomica
    {
        /// <summary>
        /// Cambia el estado de la valuación de interés para reflejar que la propuesta asociada a dicha valuación ha sido aceptada. Se desencadena la generación del ACYS a partir de los productos asociados a la valuación.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idVal">Identificador de la valuación a la cual se aceptará su propuesta</param>
        public Acys Aceptar(Sesion s, int idVal)
        {
            
            try
            {
                Acys acys = null;
                using (var businessTransaction = CN_FabricaTransaccionNegocios.Default(s))
                {
                    businessTransaction.Begin();
                    //TODO: ejecutar el bloque como una sola transacción
                    CN_CapValuacionProyecto cnCapValuacionProyecto = new CN_CapValuacionProyecto();
                    //Se actualiza la valuación para reflejar que la propuesta asociada ha sido aceptada.
                    cnCapValuacionProyecto.ActualizarAPropuestaAceptada(s, idVal, businessTransaction);

                    CN_CrmValuacionOportunidades cnCrmValuacionOportunidades = new CN_CrmValuacionOportunidades();
                    var valuacion = cnCapValuacionProyecto.Obtener(s, idVal, businessTransaction);
                    var proyectos = cnCrmValuacionOportunidades.ObtenerPorValuacion(s, valuacion.Id_Cte, idVal, businessTransaction);

                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                    CapaDatos.CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CapaDatos.CD_CrmOportunidadesProductos();

                    /* ORIGNAL 
                    //Varios ACYS?
                    foreach(var p in proyectos)
                    {
                        //Validar el flujo
                        var proyecto = cnCrmOportunidad.ObtenerPorId(s, p.Id_Op, businessTransaction);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, s);
                        psm.AlCrearAcys = (a) =>
                        {
                            acys = a;
                        };
                        psm.Transaction = businessTransaction;

                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(s.Id_Emp, s.Id_Cd, proyecto.Id_Op, valuacion.Id_Cte, businessTransaction.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;
                        
                        psm.Update();
                    }
                    */

                    foreach (var p in proyectos)
                    {
                        //Validar el flujo
                        var proyecto = cnCrmOportunidad.ObtenerPorId(s, p.Id_Op, businessTransaction);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, s);
                        psm.AlCrearAcys = (a) =>
                        {
                            acys = a;
                        };
                        psm.Transaction = businessTransaction;

                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(s.Id_Emp, s.Id_Cd, proyecto.Id_Op, valuacion.Id_Cte, businessTransaction.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;
                        
                        psm.Update(); //RFH

                    }
                    businessTransaction.Commit();
                }
                
                return acys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Genera la estructura lógica de un acys a partir de una valuación.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <returns>Acys. En caso exitoso; null en caso contrario</returns>
        protected Acys GenerarACYS(Sesion s, CapValProyecto valuacion)
        {
            Acys acys=new Acys();
            
            PrepararSeccionGeneralACYS(s, valuacion, acys);
            PrepararSeccionVisitasACYS(acys);
            PrepararSeccionContactosACYS(acys);
            return acys;
        }

        /// <summary>
        /// Inicializa la parte de la estructura lógica del acys que pertenece a la sección general.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionGeneralACYS(Sesion s, CapValProyecto valuacion, Acys acys)
        {
            acys.Id_Emp = s.Id_Emp;
            acys.Id_Cd = s.Id_Cd;
            if (valuacion.Id_Ter == null)
            {
                throw new ValuacionSinTerritorioException();
            }
            CN_CatCliente cnCatCliente = new CN_CatCliente();
            var cliente = cnCatCliente.Obtener(s, valuacion.Id_Cte);
            acys.Id_Ter = valuacion.Id_Ter.Value;
            acys.Id_Rik = s.Id_Rik;
            acys.Id_Cte = valuacion.Id_Cte;
            acys.Cte_Nombre = cliente.Cte_NomComercial;
            acys.Id_AcsVersion = 1;
            acys.Acs_Fecha = DateTime.Now;
            acys.Acs_FechaInicioDocumento = DateTime.Now;
            acys.Acs_FechaFinDocumento = DateTime.Now;
            acys.Acs_Proveedor = "Sin especificar";
            acys.Acs_RutaEntrega = 0;
            acys.Acs_RutaServicio = 0;
            acys.Acs_VigenciaIni = DateTime.Now;
            acys.Acs_Semana = 0;
            acys.Acs_RecPedCorreo = false;

            acys.Acs_RecPedFax = false;
            acys.Acs_RecPedTel = false;
            acys.Acs_RecPedRep = false;
            acys.Acs_RecPedOtroStr = string.Empty;

            acys.Acs_PedidoEncargadoEnviar = string.Empty;
            acys.Acs_PedidoPuesto = string.Empty;
            acys.Acs_PedidoTelefono = string.Empty;
            acys.Acs_PedidoEmail = string.Empty;


            acys.Acs_ReqOrdenCompra = false;
            acys.Acs_RecDocReposicion = false;
            acys.Acs_RecDocFolio = false;
            acys.Acs_RecDocOtro = string.Empty;
            acys.Id_U = s.Id_U;
        }

        /// <summary>
        /// Inicializa la parte lógica del acys que refleja la sección de visitas
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionVisitasACYS(Acys acys)
        {
            acys.Vis_Frecuencia = 0;
            acys.Acs_VisitaOtro = string.Empty;

            acys.Acs_ReqServAsesoria = false;
            acys.Acs_ReqServTecnicoRelleno = false;
            acys.Acs_ReqServMantenimiento = false;

            string Modalidad = "A";
            acys.Acs_Modalidad = Modalidad;

            acys.IdCte_DirEntrega = 0;
        }

        /// <summary>
        /// Inicializa la parte lógica del acys que reflaja la sección de contactos.
        /// </summary>
        /// <param name="acys">Instancia de la entidad [CapAcys]</param>
        protected void PrepararSeccionContactosACYS(Acys acys)
        {
            acys.Acs_Notas = string.Empty;

            acys.Acs_ContactoRepVenta = 0;
            acys.Acs_ContactoRepVentaTel = string.Empty;
            acys.Acs_ContactoRepVentaEmail = string.Empty;

            acys.Acs_ContactoRepServ = 0;
            acys.Acs_ContactoRepServTel = string.Empty;
            acys.Acs_ContactoRepServEmail = string.Empty;


            acys.Acs_ContactoJefServ = 0;
            acys.Acs_ContactoJefServTel = string.Empty;
            acys.Acs_ContactoJefServEmail = string.Empty;


            acys.Acs_ContactoAseServ = 0;
            acys.Acs_ContactoAseServTel = string.Empty;
            acys.Acs_ContactoAseServEmail = string.Empty;

            acys.Acs_ContactoJefOper = 0;
            acys.Acs_ContactoJefOperTel = string.Empty;
            acys.Acs_ContactoJefOperEmail = string.Empty;


            acys.Acs_ContactoCAlmRep = 0;
            acys.Acs_ContactoCAlmRepTel = string.Empty;
            acys.Acs_ContactoCAlmRepEmail = string.Empty;

            acys.Acs_ContactoCServTec = 0;
            acys.Acs_ContactoCServTecTel = string.Empty;
            acys.Acs_ContactoCServTecEmail = string.Empty;

            acys.Acs_ContactoCCreCob = 0;
            acys.Acs_ContactoCCreCobTel = string.Empty;
            acys.Acs_ContactoCCreCobEmail = string.Empty;


            acys.Acs_Contacto2 = string.Empty;
            acys.Acs_Telefono2 = 0;
            acys.Acs_Correo2 = string.Empty;

            acys.Acs_Contacto3 = string.Empty;
            acys.Acs_Telefono3 = 0;
            acys.Acs_Correo3 = string.Empty;

            acys.Acs_Contacto4 = string.Empty;
            acys.Acs_Telefono4 = 0;
            acys.Acs_Correo4 = string.Empty;

            acys.Acs_Contacto5 = string.Empty;
            acys.Acs_Telefono5 = 0;
            acys.Acs_Correo5 = string.Empty;

            acys.Acs_Contacto6 = string.Empty;
            acys.Acs_Telefono6 = 0;
            acys.Acs_Correo6 = string.Empty;
            
        }

        /// <summary>
        /// Regresa el listado de productos asociados a una valuación, preparados para ser asociados a la estructura lógica del ACYS.
        /// </summary>
        /// <param name="valuacion">Instancia de la entidad [CapValProyecto]</param>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <returns>AcysPrd[]. Conjunto de productos asociados a una valuación en una llamada satisfactoria; null de otro modo</returns>
        protected List<AcysPrd> ObtenerListadoDeProductos(CapValProyecto valuacion, Sesion s)
        {
            CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
            var dets=cnCapValProyecto.ObtenerDetalle(s, valuacion.Id_Vap);
            List<AcysPrd> productos = new List<AcysPrd>();
            CN_CatProducto cnCatProducto = new CN_CatProducto();
            foreach (var cvpd in dets)
            {
                var producto = cnCatProducto.ObtenerPorId(s, cvpd.Id_Prd);
                AcysPrd prd=new AcysPrd() 
                { 
                    Acs_Doc = string.Empty, 
                    Acys_Cantidad = cvpd.Vap_Cantidad, 
                    Acys_CantTotal = cvpd.Vap_Cantidad, 
                    Acys_FechaFin=DateTime.Now,
                    Acys_FechaInicio=DateTime.Now,
                    Acys_Frecuencia = 1,
                    Acys_Jueves=false,
                    Acys_Lunes=false,
                    Acys_Martes=false,
                    Acys_Miercoles=false,
                    Acys_Sabado=false,
                    Acys_UltACtp=0,
                    Acys_UltSCtp=0,
                    Acys_Viernes=false,
                    Id_Prd = cvpd.Id_Prd,
                    Id_TG=0,
                    Prd_Descripcion = producto.Prd_Descripcion,
                    Prd_Precio = cvpd.Vap_Precio,
                    Prd_Presentacion = producto.Prd_Presentacion,
                    Prd_UniNom = producto.Prd_UniNe
                };
                productos.Add(prd);
            }
            
            return productos;
        }

        public class ValuacionSinTerritorioException
            : Exception
        {
            public ValuacionSinTerritorioException()
                : base("Generación de ACYS cancelada. La valuación no cuenta con un territorio asociado.")
            {
            }
        }
    }
}
