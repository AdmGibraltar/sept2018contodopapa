using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CapValuacionProyecto
    {
        public void ConsultarUltimaValuacionProyectoCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarUltimaValuacionProyectoCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Buscar(ValuacionProyecto valuacionProyecto, ref List<ValuacionProyecto> listaValuacionProyecto, string Conexion
            , int? Id_U
            , string Nombre
            , int? Id_Cte_inicio
            , int? Id_Cte_fin
            , DateTime? Vap_Fecha_inicio
            , DateTime? Vap_Fecha_fin)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultaValuacionProyecto_Buscar(valuacionProyecto, ref listaValuacionProyecto, Conexion
                    , Id_U
                    , Nombre
                    , Id_Cte_inicio
                    , Id_Cte_fin
                    , Vap_Fecha_inicio
                    , Vap_Fecha_fin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, string Conexion)
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarValuacionProyecto(ref valuacionProyecto, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarValuacionProyecto_ReporteTotales(ref ValuacionProyecto valuacionProyecto, ref DataTable dt, string Conexion )
        {
            try
            {
                new CD_CapValuacionProyecto().ConsultarValuacionProyecto_ReporteTotales(ref valuacionProyecto, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            try
            {
                new CD_CapValuacionProyecto().InsertarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador,vpactual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarEstado(ValuacionProyecto valuacionProyecto)
        {
            if (valuacionProyecto.Vap_ValorPresenteNeto < 0 || valuacionProyecto.Vap_UtilidadRemanente < 0)
            {
                valuacionProyecto.Estatus2 = 2;
            }
            else if (valuacionProyecto.Vap_ValorPresenteNeto > 0 && valuacionProyecto.Vap_UtilidadRemanente > 0)
            {
                valuacionProyecto.Estatus2 = 1;
            }
        }

        /// <summary>
        /// Crea una nueva entrada persistente de valuación, asociando proyectos.
        /// </summary>
        /// <param name="valuacionProyecto">Instancia de la información de la valuación</param>
        /// <param name="vp">?</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="verificador">Bandera del resultado de la operación</param>
        /// <param name="idOps">Conjunto de proyectos a asociar</param>
        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, Sesion sesion, ref int verificador, int[] idOps)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(sesion))
                {
                    ibt.Begin();
                    //Este bloque debe ejecutarse como una sola transacción
                    ValidarEstado(valuacionProyecto);
                    new CD_CapValuacionProyecto().InsertarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, idOps, ibt.DataContext, sesion.Id_Rik);

                    CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                    cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, valuacionProyecto.Id_Rik.Value, idOps, ibt.DataContext); 

                    CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();

                    foreach (var idOp in idOps)
                    {
                        //Se valida el estado del proyecto

                        var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, idOp);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                        psm.Transaction = ibt;

                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, idOp, valuacionProyecto.Id_Cte, ibt.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;
                        psm.Update();
                    }

                    //CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
                    //cnCrmPropuestaTecnica.GenerarAPartirDeValuacion(sesion, valuacionProyecto.Id_Vap, ibt);

                    ibt.Commit();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida que la propiedad MotivoParaAutorizacion haya sido capturada en caso de que el resultado de la valuación sea negativo.
        /// </summary>
        /// <param name="valuacionProyecto">Instancia de datos de la entidad ValuacionProyecto. Se exige que las propiedades Vap_UtilidadRemanente y Vap_ValorPresenteNeto sean capturadas</param>
        private void ValidarMotivoParaAutorizacion(ValuacionProyecto valuacionProyecto)
        {
            if ((valuacionProyecto.Vap_UtilidadRemanente < 0 || valuacionProyecto.Vap_ValorPresenteNeto < 0))
            {
                if (String.IsNullOrEmpty(valuacionProyecto.MotivoParaAutorizacion))
                {
                    throw new CapturarMotivoParaAutorizarException();
                }

                int nEspacios = valuacionProyecto.MotivoParaAutorizacion.Count(c => c.CompareTo(' ') == 0);
                bool bSonSoloEspacios = valuacionProyecto.MotivoParaAutorizacion.Length == nEspacios;
                if (bSonSoloEspacios)
                {
                    throw new CapturarMotivoParaAutorizarException();
                }

            }
        }

        /// <summary>
        /// Crea una nueva entrada persistente de valuación, asociando proyectos.
        /// </summary>
        /// <param name="valuacionProyecto">Instancia de la información de la valuación</param>
        /// <param name="vp">?</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="verificador">Bandera del resultado de la operación</param>
        /// <param name="idOps">Conjunto de proyectos a asociar</param>
        public void InsertarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, Sesion sesion, ref int verificador, int[] idOps, IBusinessTransaction ibt)
        {
            //Este bloque debe ejecutarse como una sola transacción
            ValidarEstado(valuacionProyecto);

            //Se valida el campo MotivoParaAutorizacion
            ValidarMotivoParaAutorizacion(valuacionProyecto);

            new CD_CapValuacionProyecto().InsertarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, idOps, ibt.DataContext, sesion.Id_Rik);

            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, valuacionProyecto.Id_Rik.Value, idOps, ibt.DataContext);

            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();

            foreach (var idOp in idOps)
            {
                //Se valida el estado del proyecto

                var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, idOp);
                CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                psm.Transaction = ibt;

                var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, idOp, valuacionProyecto.Id_Cte, ibt.DataContext);
                proyecto.CrmOportunidadesProducto = productos;
                psm.Update();
            }

            //Si la utilidad remanente y el valor presente neto son positivos, autorizar la valuación en automático
            if (valuacionProyecto.Vap_UtilidadRemanente > 0 && valuacionProyecto.Vap_ValorPresenteNeto > 0)
            {
                CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
                cnCapValProyecto.Autorizar(sesion, valuacionProyecto.Id_Vap, ibt);
            }


            //CN_CrmPropuestaTecnica cnCrmPropuestaTecnica = new CN_CrmPropuestaTecnica();
            //cnCrmPropuestaTecnica.GenerarAPartirDeValuacion(sesion, valuacionProyecto.Id_Vap, ibt);
        }

        /// <summary>
        /// Crea una nueva entrada persistente de valuación, asociando proyectos.
        /// </summary>
        /// <param name="valuacionProyecto">Instancia de la información de la valuación</param>
        /// <param name="vp">?</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="idOps">Conjunto de proyectos a asociar</param>
        public CapaModelo.CapValProyecto InsertarValuacionProyecto(ValuacionProyecto valuacionProyecto, ValuacionParametros vp, ValuacionParametrosActual p2, Sesion sesion, int[] idOps, IBusinessTransaction ibt)
        {
            //Este bloque debe ejecutarse como una sola transacción
            ValidarEstado(valuacionProyecto);

            //Se valida el campo MotivoParaAutorizacion
            ValidarMotivoParaAutorizacion(valuacionProyecto);

            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            CapaModelo.CapValProyecto capValProyecto = new CapaModelo.CapValProyecto();
            capValProyecto.Id_Emp = sesion.Id_Emp;
            capValProyecto.Id_Cd = sesion.Id_Cd;
            capValProyecto.Id_Cte = valuacionProyecto.Id_Cte;
            capValProyecto.Id_Rik = sesion.Id_Rik;
            capValProyecto.Id_Ter = valuacionProyecto.Id_Ter;
            capValProyecto.Id_U = sesion.Id_U;
            capValProyecto.Id_Vap = valuacionProyecto.Id_Vap;
            capValProyecto.Vap_Fecha = valuacionProyecto.Vap_Fecha;
            capValProyecto.Vap_Nota = valuacionProyecto.Vap_Nota;
            capValProyecto.Vap_Estatus = valuacionProyecto.Vap_Estatus;
            capValProyecto.Vap_UtilidadRemanente = valuacionProyecto.Vap_UtilidadRemanente;
            capValProyecto.Vap_ValorPresenteNeto = valuacionProyecto.Vap_ValorPresenteNeto;
            capValProyecto.Vap_Estatus2 = valuacionProyecto.Estatus2;
            capValProyecto.MotivoParaAutorizacion = valuacionProyecto.MotivoParaAutorizacion;

            capValProyecto = cdCapValuacionProyecto.Insertar(capValProyecto, ibt.DataContext);
            ibt.Save();

            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            int i=1;
            List<CapaModelo.CapValProyectoDet> lstProductos=new List<CapaModelo.CapValProyectoDet>();
            foreach (var prod in valuacionProyecto.ListaProductosValuacionProyecto)
            {
                CapaModelo.CapValProyectoDet capValProyectoDet=new CapaModelo.CapValProyectoDet();
                capValProyectoDet.Id_Emp=sesion.Id_Emp;
                capValProyectoDet.Id_Cd=sesion.Id_Cd;
                capValProyectoDet.Id_Prd=prod.Id_Prd;
                capValProyectoDet.Id_Vap=capValProyecto.Id_Vap;
                capValProyectoDet.Id_VapDet=i++;
                capValProyectoDet.Det_Autorizo=null;
                capValProyectoDet.Det_Estatus=null;
                capValProyectoDet.Det_FecAut=null;
                capValProyectoDet.Det_PrecioLista=prod.Vap_PrecioEspecial;
                capValProyectoDet.Vap_Cantidad=prod.Vap_Cantidad;
                capValProyectoDet.Vap_Costo=prod.Vap_Costo;
                capValProyectoDet.Vap_Precio=prod.Vap_Precio;
                capValProyectoDet.Vap_Tipo=prod.Vap_Tipo;

                lstProductos.Add(capValProyectoDet);
            }
            cdCapValProyectoDet.Insertar(lstProductos, ibt.DataContext);

            CD_CapValProyectoParams cdCapValProyectoParams = new CD_CapValProyectoParams();
            CapaModelo.CapValProyecto_Params parametros1 = new CapaModelo.CapValProyecto_Params();
            parametros1.Id_Emp=sesion.Id_Emp;
            parametros1.Id_Cd=sesion.Id_Cd;
            parametros1.Id_Vap = capValProyecto.Id_Vap;
            parametros1.Vap_Vigencia=vp.Vap_Vigencia;
            parametros1.Vap_Participacion=vp.Vap_Participacion;
            parametros1.Vap_Mano_Obra=vp.Vap_Mano_Obra;
            parametros1.Vap_Amortizacion=vp.Vap_Amortizacion;
            parametros1.Vap_Numero_Entregas=vp.Vap_Numero_Entregas;
            parametros1.Vap_Costo_Entregas=vp.Vap_Costo_Entregas;
            parametros1.Vap_Comision_Factoraje=vp.Vap_Comision_Factoraje;
            parametros1.Vap_Comision_Anden=vp.Vap_Comision_Anden;
            parametros1.Vap_Gasto_Flete_Locales=vp.Vap_Gasto_Flete_Locales;
            parametros1.Vap_IVA=vp.Vap_IVA;
            parametros1.Vap_Plazo_Pago_Cliente=Convert.ToInt32(vp.Vap_Plazo_Pago_Cliente);
            parametros1.Vap_Inventario_Key=vp.Vap_Inventario_Key;
            parametros1.Vap_Inventario_Consignacion=vp.Vap_Inventario_Consignacion;
            parametros1.Vap_Inventario_Papel=vp.Vap_Inventario_Papel;
            parametros1.Vap_Consignacion_Papel=vp.Vap_Consignacion_Papel;
            parametros1.Vap_Credito_Key=vp.Vap_Credito_Key;
            parametros1.Vap_Credito_Papel=vp.Vap_Credito_Papel;
            parametros1.Vap_ISR=vp.Vap_ISR;
            parametros1.Vap_Ucs=vp.Vap_Ucs;
            parametros1.Vap_Cetes=vp.Vap_Cetes;
            parametros1.Vap_Adicional_Cetes=vp.Vap_Adicional_Cetes;
            parametros1.Vap_Costos_Fijos_No_Papel=vp.Vap_Costos_Fijos_No_Papel;
            parametros1.Vap_Costos_Fijos_Papel=vp.Vap_Costos_Fijos_Papel;
            parametros1.Vap_Gastos_Admin=vp.Vap_Gastos_Admin;
            parametros1.Vap_Inversion_Activos=vp.Vap_Inversion_Activos;
            cdCapValProyectoParams.Insertar(parametros1, ibt.DataContext);

            CD_CapValProyecto_Parametros cdCapValProyecto_Parametros = new CD_CapValProyecto_Parametros();
            CapaModelo.CapValProyecto_Parametros capValProyecto_Parametros = new CapaModelo.CapValProyecto_Parametros();
            capValProyecto_Parametros.Id_Emp=sesion.Id_Emp;
            capValProyecto_Parametros.Id_Cd=sesion.Id_Cd;
            capValProyecto_Parametros.Id_Vap=capValProyecto.Id_Vap;
            capValProyecto_Parametros.txtCuentasPorCobrar=p2.txtCuentasPorCobrar;
            capValProyecto_Parametros.txtInventario=p2.txtInventario;
            capValProyecto_Parametros.txtGastosServirCliente=p2.txtGastosServirCliente;
            capValProyecto_Parametros.txtVigencia=p2.txtVigencia;
            capValProyecto_Parametros.txtFleteLocales=p2.txtFleteLocales;
            capValProyecto_Parametros.txtIsr=p2.txtIsr;
            capValProyecto_Parametros.txtCetes=p2.txtCetes;
            capValProyecto_Parametros.txtFinanciamientoproveedores=p2.txtFinanciamientoproveedores;
            capValProyecto_Parametros.txtInversionactivosfijos=p2.txtInversionactivosfijos;
            capValProyecto_Parametros.txtCostodecapital=p2.txtCostodecapital;
            capValProyecto_Parametros.txtManoObra=p2.txtManoObra;
            capValProyecto_Parametros.txtGastosVarAplTerr=p2.txtGastosVarAplTerr;
            capValProyecto_Parametros.txtFletesPagadosalCliente=p2.txtFletesPagadosalCliente;
            cdCapValProyecto_Parametros.Insertar(capValProyecto_Parametros, ibt.DataContext);

            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            cdCrmValuacionOportunidades.Insertar(valuacionProyecto.Id_Emp, valuacionProyecto.Id_Cd, valuacionProyecto.Id_Cte, valuacionProyecto.Id_Vap, valuacionProyecto.Id_Rik.Value, idOps, ibt.DataContext);

            CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();

            foreach (var idOp in idOps)
            {
                //Se valida el estado del proyecto

                var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, idOp);
                CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                psm.Transaction = ibt;

                var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, idOp, valuacionProyecto.Id_Cte, ibt.DataContext);
                proyecto.CrmOportunidadesProducto = productos;
                psm.Update();
            }

            //Si la utilidad remanente y el valor presente neto son positivos, autorizar la valuación en automático
            if (valuacionProyecto.Vap_UtilidadRemanente > 0 && valuacionProyecto.Vap_ValorPresenteNeto > 0)
            {
                CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
                cnCapValProyecto.Autorizar(sesion, valuacionProyecto.Id_Vap, ibt);
            }

            return capValProyecto;
        }

        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, string Conexion, ref int verificador, ValuacionParametrosActual vpactual)
        {
            try
            {
                //Se valida el campo MotivoParaAutorizacion
                ValidarMotivoParaAutorizacion(valuacionProyecto);
                new CD_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, Conexion, ref verificador, vpactual);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Persiste los cambios a la información asociada de una valuación. Esta versión acepta un conjunto de identificadores de proyecto para asociarlos a la valuación.
        /// </summary>
        /// <param name="valuacionProyecto">Información de la instancia de la valuación a persistir</param>
        /// <param name="vp">Información de los parámetros de la valuación</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="verificador">Bandera de resultados</param>
        /// <param name="idOps">Conjunto de proyectos a asociar a la valuación</param>
        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, Sesion sesion, ref int verificador, int[] idOps)
        {
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(sesion))
                {
                    ibt.Begin();

                    ValidarEstado(valuacionProyecto);
                    new CD_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, idOps, sesion.Emp_Cnx_EF, sesion.Id_Rik);

                    CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();

                    foreach (var idOp in idOps)
                    {
                        //Se valida el estado del proyecto

                        var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, idOp);
                        CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                        psm.Transaction = ibt;

                        var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, idOp, valuacionProyecto.Id_Cte, ibt.DataContext);
                        proyecto.CrmOportunidadesProducto = productos;
                        psm.Update();
                    }

                    ibt.Commit();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Persiste los cambios a la información asociada de una valuación. Esta versión acepta un conjunto de identificadores de proyecto para asociarlos a la valuación.
        /// </summary>
        /// <param name="valuacionProyecto">Información de la instancia de la valuación a persistir</param>
        /// <param name="vp">Información de los parámetros de la valuación</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="verificador">Bandera de resultados</param>
        /// <param name="idOps">Conjunto de proyectos a asociar a la valuación</param>
        public void ModificarValuacionProyecto(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, Sesion sesion, ref int verificador, int[] idOps, IBusinessTransaction ibt)
        {
            try
            {
                ValidarEstado(valuacionProyecto);

                //Se valida el campo MotivoParaAutorizacion
                ValidarMotivoParaAutorizacion(valuacionProyecto);

                new CD_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, idOps, sesion.Emp_Cnx_EF, sesion.Id_Rik, ibt.DataContext);

                CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();

                foreach (var idOp in idOps)
                {
                    //Se valida el estado del proyecto

                    var proyecto = cnCrmOportunidad.ObtenerPorId(sesion, idOp);
                    CapaNegocios.FlujosDeEstado.CRM.ProyectoStateMachine psm = new FlujosDeEstado.CRM.ProyectoStateMachine(proyecto, sesion);
                    psm.Transaction = ibt;

                    var productos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, idOp, valuacionProyecto.Id_Cte, ibt.DataContext);
                    proyecto.CrmOportunidadesProducto = productos;
                    psm.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Persiste los cambios a la información asociada de una valuación global. Esta versión acepta un conjunto de identificadores de proyecto para asociarlos a la valuación.
        /// </summary>
        /// <param name="valuacionProyecto">Información de la instancia de la valuación a persistir</param>
        /// <param name="vp">Información de los parámetros de la valuación</param>
        /// <param name="sesion">Llave de inicio de sesión</param>
        /// <param name="verificador">Bandera de resultados</param>
        /// <param name="idOps">Conjunto de proyectos a asociar a la valuación</param>
        public void ModificarValuacionProyectoGlobal(ref ValuacionProyecto valuacionProyecto, ValuacionParametros vp, Sesion sesion, ref int verificador, int[] idOps, IBusinessTransaction ibt)
        {
            try
            {
                //ValidarEstado(valuacionProyecto);

                new CD_CapValuacionProyecto().ModificarValuacionProyecto(ref valuacionProyecto, vp, sesion.Emp_Cnx, ref verificador, idOps, sesion.Emp_Cnx_EF, sesion.Id_Rik, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarValuacionProyecto(ValuacionProyecto valuacionProyecto, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapValuacionProyecto().EliminarValuacionProyecto(valuacionProyecto, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyecto_Autorizacion(ref ValuacionProyecto VP, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ConsultaValuacionProyecto_Autorizacion(ref VP, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaValuacionProyectoList(int Id_Emp, int Id_Cd, int Id_Val, string Conexion, ref List<ValuacionProyectoDetalle> List)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ConsultaValuacionProyectoList(Id_Emp, Id_Cd, Id_Val, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.ModificarValuacionProyectoDetalle(cl, list, Conexion, ref verificador);

                //CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                
                //cdCrmValuacionOportunidades.ConsultarPorValuacion(cl.Id_Emp, cl.Id_Cd, cl.Id_
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarValuacionProyectoDetalle(ValuacionProyectoDetalle cl, List<ValuacionProyectoDetalle> list, string Conexion, ref int verificador, Sesion s)
        {
            try
            {
                using(IBusinessTransaction ibt= CN_FabricaTransaccionNegocios.Default(s))
                {
                    ibt.Begin();
                    CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                    claseCapaDatos.ModificarValuacionProyectoDetalle(cl, list, Conexion, ref verificador, ibt.DataContext);

                    CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();

                    var valuacionesProyectos = cdCrmValuacionOportunidades.ConsultarPorValuacion(cl.Id_Emp, cl.Id_Cd, s.Id_Rik, cl.Id_Vap, ibt.DataContext);
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
                    ibt.Commit();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesActuales(ref ValuacionParametrosActual vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarParametrosActuales(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondiciones(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarParametros(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void consultarCondicionesCentro(ref ValuacionParametros vp, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapValuacionProyecto claseCapaDatos = new CD_CapValuacionProyecto();
                claseCapaDatos.consultarCondicionesCentro(ref vp, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CapaModelo.CapValProyecto Insertar(Sesion s, CapaModelo.CapValProyecto capValProyecto)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.Insertar(capValProyecto, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Obtiene una instancia de valuación existente dado que se proporciona el identificador de la valuación
        /// </summary>
        /// <param name="s">Sesión del RIK en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <returns>Instancia de la valuación representada por el identificador</returns>
        public CapaModelo.CapValProyecto Obtener(Sesion s, int idVal)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVal, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Obtiene una instancia de valuación existente dado que se proporciona el identificador de la valuación. Versión transaccional.
        /// </summary>
        /// <param name="s">Sesión del RIK en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="ibt">Transacción de negocio</param>
        /// <returns>Instancia de la valuación representada por el identificador</returns>
        public CapaModelo.CapValProyecto Obtener(Sesion s, int idVal, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVal, ibt.DataContext);
        }

        public bool EsValuacionValidaParaPropuesta(Sesion s, int idVal)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            var valuacion = cdCapValuacionProyecto.ConsultarPorId(s.Id_Emp, s.Id_Cd, idVal, s.Emp_Cnx_EF);
            return valuacion.Vap_Estatus2 == 3;
        }

        /// <summary>
        /// Obtiene las valuaciones asociadas al RIK en sesion
        /// </summary>
        /// <param name="s">Sesion del operador actual. Tiene que ser RIK</param>
        /// <returns>Conjunto de valuaciones asociadas al proyecto</returns>
        public IEnumerable<CapaModelo.CapValProyecto> ObtenerPorRik(Sesion s)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            return cdCapValuacionProyecto.ConsultarValuacionesPorRik(s.Id_Emp, s.Id_Cd, s.Id_Rik, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Actualiza el estado de la valuación para reflejar el cambio de estado de la valuación a propuesta aceptada
        /// </summary>
        /// <param name="s">Sesión del RIK en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        public void ActualizarAPropuestaAceptada(Sesion s, int idVal)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            cdCapValuacionProyecto.ActualizarAtributoCap_Estatus2(s.Id_Emp, s.Id_Cd, idVal, 4, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Actualiza el estado de la valuación para reflejar el cambio de estado de la valuación a propuesta aceptada. Esta versión exige el uso de una transacción de negocios.
        /// </summary>
        /// <param name="s">Sesión del RIK en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="ibt">Contrato de transacción de negocios</param>
        public void ActualizarAPropuestaAceptada(Sesion s, int idVal, IBusinessTransaction ibt)
        {
            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            cdCapValuacionProyecto.ActualizarAtributoCap_Estatus2(s.Id_Emp, s.Id_Cd, idVal, 4, ibt.DataContext);
        }

        /// <summary>
        /// Formatea el contenido del cuerpo del mensaje, sustituyendo las plantillas por los valores correctos.
        /// </summary>
        /// <param name="cuerpo">Cuerpo del mensaje del mensaje para la notificación de valuacion aceptada</param>
        /// <param name="valuacion">Instancia de la entidad CapValuacionProyecto</param>
        /// <returns>String. Mensaje formateado.</returns>
        protected string FormatearCuerpoValuacionAceptada(string cuerpo, CapaModelo.CapValProyecto valuacion)
        {
            StringBuilder sb = new StringBuilder(cuerpo);
            sb = sb.Replace("[idVal]", valuacion.Id_Vap.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="idVal"></param>
        public void AceptarValuacion(Sesion s, int idVal)
        {
            //Cambiar de estado la valuación
            ActualizarAPropuestaAceptada(s, idVal);
            //Enviar el mensaje de notificación al creador de la valuación
            CapaNegocios.Flujos.Tareas.EnviarCorreo tareaEnviarCorreo = new Flujos.Tareas.EnviarCorreo();
            CN_CatMensaje cnCatMensaje = new CN_CatMensaje(s);
            var valuacion = Obtener(s, idVal);
            string correoRIK = string.Empty;
            CN_CatRik cnCatRik = new CN_CatRik();
            if (valuacion.Id_Rik != null)
            {
                correoRIK = cnCatRik.ObtenerCorreo(s, valuacion.Id_Rik.Value);
                tareaEnviarCorreo.EnviarHtml(s, cnCatMensaje.TituloMensajeValuacionAceptada, correoRIK, FormatearCuerpoValuacionAceptada(cnCatMensaje.CuerpoMensajeValuacionAceptada, valuacion));
            }
            else
            {
                //log? encolar para enviar mas tarde?
            }

            //tareaEnviarCorreo.EnviarHtml(s, cnCatMensaje.TituloMensajeValuacionAceptada,  
        }
    }

    public class CapturarMotivoParaAutorizarException
        : Exception
    {
        /// <summary>
        /// Constructor por defecto. 
        /// </summary>
        public CapturarMotivoParaAutorizarException()
            : base("El resultado de la valuación es negativa, y el campo Motivo Para Autorización debe ser capturado.")
        {
        }
    }

    public class FlujoAnual
    {
        public int Ano
        {
            get;
            set;
        }

        public double Flujo
        {
            get;
            set;
        }

        public double VPFlujo
        {
            get;
            set;
        }
    }
}
