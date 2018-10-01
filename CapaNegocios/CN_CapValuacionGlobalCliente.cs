using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaEntidad;

namespace CapaNegocios
{
    /// <summary>
    /// Clase de negocio para la entidad CapValuacionGlobalCliente
    /// </summary>
    public class CN_CapValuacionGlobalCliente
    {
        /// <summary>
        /// Crea una entrada en el repositorio CapValuacionGlobalCliente
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="datos">Instancia de datos de la entidad CapValuacionGlobalCliente</param>
        /// <param name="ibt">Transacción en la capa de negocio</param>
        /// <returns>CapValuacionGlobalCliente</returns>
        public CapValuacionGlobalCliente Crear(Sesion s, CapValuacionGlobalCliente datos, IBusinessTransaction ibt)
        {
            CD_CapValuacionGlobalCliente cdCapValuacionGlobalCliente = new CD_CapValuacionGlobalCliente();
            return cdCapValuacionGlobalCliente.Insertar(datos, ibt.DataContext);
        }

        /// <summary>
        /// Crea una valuación global con condiciones estándard.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapValuacionGlobalCliente</returns>
        /// 
        // public CrmProspecto InsertarProspecto(CrmProspecto prospecto, ICD_Contexto icdCtx)


        public CapValuacionGlobalCliente CrearParaCliente(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            // # # # # # # # # # # # # # # # # # # # # # # # # # # # 
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            string idStr = CN_Comun.Maximo(s.Id_Emp, s.Id_Cd_Ver, "CapValProyecto", "Id_Vap", ibt.DataContext , "spCatLocal_Maximo"); // ibt.DataContext
            int id_Vap = 0;
            id_Vap = int.Parse(idStr);
            // # # # # # # # # # # # # # # # # # # # # # # # # # # # 


            CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
            CapValProyecto capValProyecto = new CapValProyecto();
            capValProyecto.Id_Emp = s.Id_Emp;
            capValProyecto.Id_Cd = s.Id_Cd;
            capValProyecto.Id_Cte = idCte;
            capValProyecto.Id_Rik = s.Id_Rik;
            capValProyecto.Id_Ter = null;
            capValProyecto.Id_U = s.Id_U;
            capValProyecto.Id_Vap = 0;

            capValProyecto.Vap_Fecha = DateTime.Now;
            capValProyecto.Vap_Estatus = "C";
            capValProyecto.Vap_Estatus2 = 1;

            capValProyecto.Id_Vap = id_Vap;           
            
            capValProyecto = cnCapValProyecto.Crear_(s, capValProyecto, ibt);
            //capValProyecto = cnCapValProyecto.Crear(s, capValProyecto, ibt);

            //Se guarda la transacción (sin haber sido enviada) para generar el último índice generado por la llamada a [Crear]

            
            ibt.Save();

            //Se determinan los proyectos disponibles asociados al cliente para asociar los productos contenidos en dichos proyectos a la valuación
            List<CrmPromociones> List = new List<CrmPromociones>();
            CN_CrmPromocion cls = new CN_CrmPromocion();

            CrmPromociones promocion = new CrmPromociones();
            //filtro1
            promocion.Cds = s.Id_Cd;
            promocion.Representante = s.Id_U;
            promocion.Uen = -1;
            promocion.Segmento = -1;
            promocion.Territorio = -1;
            //filtro2
            promocion.Area = -1;
            promocion.Solucion = -1;
            promocion.Aplicacion = -1;
            promocion.Estatus = -1;
            promocion.Cliente = idCte;
            promocion.Id_Rik = s.Id_Rik.ToString();

            cls.ProyectosDisponiblesParaValuacion(s, promocion, ref List, ibt); //Aquí se obtiene una referencia a todos aquellos proyectos que se asociarán a la valuación global; es importante destacar que hay que modificar la determinación de los proyectos disponibles para que se tome en cuenta 

            //Se asocian los proyectos a la valuación
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            cdCrmValuacionOportunidades.Insertar(capValProyecto.Id_Emp, capValProyecto.Id_Cd, capValProyecto.Id_Cte, capValProyecto.Id_Vap, capValProyecto.Id_Rik.Value, List.Select(cp => cp.Id).ToArray(), ibt.DataContext);

            //Se recorre el listado de proyectos para obtener los productos asociados y actualizar la valuación.
            CN_CrmOportunidadesProductos cnOportunidadesProductos = new CN_CrmOportunidadesProductos();
            CN_ProductoPrecios cnProductoPrecios = new CN_ProductoPrecios();
            List<CapValProyectoDet> productosConciliados = new List<CapValProyectoDet>();
            foreach (var proy in List)
            {
                var productos = cnOportunidadesProductos.ObtenerProductosPorOportunidad(s, proy.Id, proy.Id_Cte, ibt);

                var detalle = (from p in productos
                               select new CapValProyectoDet()
                               {
                                   Id_Emp = p.Id_Emp,
                                   Id_Cd = p.Id_Cd,
                                   Id_Vap = capValProyecto.Id_Vap,
                                   Id_VapDet = 0,
                                   Vap_Tipo = 1,
                                   Id_Prd = p.Id_Prd,
                                   Vap_Cantidad = Convert.ToInt32(p.COP_Cantidad),
                                   Vap_Costo = Math.Round(this.PartidasCalcularPrecioAAA(s.Id_Emp, s.Id_Cd_Ver, idCte, p.Id_Prd, capValProyecto.Id_Vap, ibt), 2),
                                   Vap_Precio = cnProductoPrecios.ConsultarPrecioAAA(s, p.Id_Prd, ibt),
                                   Det_PrecioLista = cnProductoPrecios.ConsultarPrecioLista(s, p.Id_Prd, ibt),
                               }).ToList();
                productosConciliados.AddRange(detalle);
            }

            //Se determinan los valores por defecto para los parámetros
            CD_CatCentroDistribucion cdCatCentroDistribucion = new CD_CatCentroDistribucion();
            CD_CatEmpresa cdCatEmpresa = new CD_CatEmpresa();
            var empresa = cdCatEmpresa.Consultar(s.Id_Emp, ibt.DataContext);
            var cd = cdCatCentroDistribucion.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);

            //Se persisten los parámetros de la valuación
            CapValProyecto_Params parametros = new CapValProyecto_Params();
            parametros.Id_Emp = s.Id_Emp;
            parametros.Id_Cd = s.Id_Cd;
            parametros.Id_Vap = capValProyecto.Id_Vap;

            //Las constantes usadas en el siguiente bloque de asignación son extraídas del procedimiento almacenado spValCondicionesCentro_Consultar
            parametros.Vap_Vigencia = 1;
            parametros.Vap_Mano_Obra = 0;
            parametros.Vap_Amortizacion = 5.67;
            parametros.Vap_Numero_Entregas = 0;
            parametros.Vap_Costo_Entregas = 0;
            parametros.Vap_Comision_Factoraje = 0;
            parametros.Vap_Comision_Anden = 0;
            parametros.Vap_Gasto_Flete_Locales = empresa.Emp_GastoLocal == null ? 0 : empresa.Emp_GastoLocal.Value;
            parametros.Vap_IVA = cd.Cd_IvaPedidosFacturacion == null ? 0 : cd.Cd_IvaPedidosFacturacion.Value;
            parametros.Vap_Plazo_Pago_Cliente = 30;
            parametros.Vap_Inventario_Key = cd.Cd_Dias == null ? 0 : cd.Cd_Dias.Value;
            parametros.Vap_Inventario_Consignacion = 0;
            parametros.Vap_Credito_Key = cd.Cd_CreditoPapel == null ? 0 : cd.Cd_CreditoPapel.Value;
            parametros.Vap_Credito_Papel = cd.Cd_CreditoKey == null ? 0 : cd.Cd_CreditoKey.Value;

            parametros.Vap_ISR = empresa.Emp_Isr == null ? 0 : empresa.Emp_Isr.Value;
            parametros.Vap_Ucs = empresa.Emp_Ucs == null ? 0 : empresa.Emp_Ucs.Value;
            parametros.Vap_Cetes = empresa.Emp_Cetes == null ? 0 : empresa.Emp_Cetes.Value;
            parametros.Vap_Adicional_Cetes = empresa.Emp_AdicionalCetes == null ? 0 : empresa.Emp_AdicionalCetes.Value;
            parametros.Vap_Costos_Fijos_No_Papel = empresa.Emp_ContribucionNoPapel == null ? 0 : empresa.Emp_ContribucionNoPapel.Value;
            parametros.Vap_Costos_Fijos_Papel = empresa.Emp_ContribucionPapel == null ? 0 : empresa.Emp_ContribucionPapel.Value;
            parametros.Vap_Gastos_Admin = empresa.Emp_GastoAdmin == null ? 0 : empresa.Emp_GastoAdmin.Value;
            parametros.Vap_Inversion_Activos = empresa.Emp_Inversion == null ? 0 : empresa.Emp_Inversion.Value;

            CD_CapValProyectoParams cdCapValProyectoParams = new CD_CapValProyectoParams();
            cdCapValProyectoParams.Insertar(parametros, ibt.DataContext);

            CapValProyecto_Parametros capValProyecto_Parametros = new CapValProyecto_Parametros();
            CN_CatCliente clsCliente = new CN_CatCliente();
            double DiasRotacion = 0;
            clsCliente.CatClienteCondPago(s.Id_Emp, s.Id_Cd_Ver, idCte, ref DiasRotacion, ibt);
            capValProyecto_Parametros.txtCuentasPorCobrar = DiasRotacion;
            capValProyecto_Parametros.txtInventario = parametros.Vap_Inventario_Key;
            capValProyecto_Parametros.txtGastosServirCliente = cd.Cd_ComisionRik;
            capValProyecto_Parametros.txtGastosVarAplTerr = 0;
            capValProyecto_Parametros.txtVigencia = parametros.Vap_Vigencia;
            capValProyecto_Parametros.txtFleteLocales = parametros.Vap_Gasto_Flete_Locales;
            capValProyecto_Parametros.txtIsr = parametros.Vap_ISR;
            capValProyecto_Parametros.txtCetes = parametros.Vap_Cetes;
            capValProyecto_Parametros.txtFinanciamientoproveedores = cd.Cd_DiasFinanciaProv;
            capValProyecto_Parametros.txtInversionactivosfijos = cd.Cd_FactorConvActFijo;
            capValProyecto_Parametros.txtCostodecapital = cd.Cd_TasaIncCostoCapital;
            capValProyecto_Parametros.txtManoObra = 0;
            capValProyecto_Parametros.txtFletesPagadosalCliente = 0;
            capValProyecto_Parametros.Id_Emp = s.Id_Emp;
            capValProyecto_Parametros.Id_Cd = s.Id_Cd;
            capValProyecto_Parametros.Id_Vap = capValProyecto.Id_Vap;
            CD_CapValProyecto_Parametros cdCapValProyecto_Parametros = new CD_CapValProyecto_Parametros();
            cdCapValProyecto_Parametros.Insertar(capValProyecto_Parametros, ibt.DataContext);

            //Se persisten los productos asociados a la valuación
            CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
            cdCapValProyectoDet.Insertar(productosConciliados, ibt.DataContext);

            //Se procede a calcular el resultado de la valuación
            ResultadosValuacion resultadosValuacion = new ResultadosValuacion();
            GeneraReporteVP(resultadosValuacion, productosConciliados, parametros, capValProyecto_Parametros, s, ibt);

            capValProyecto.Vap_UtilidadRemanente = resultadosValuacion.UtilidadRemanente;
            capValProyecto.Vap_ValorPresenteNeto = resultadosValuacion.ValorPresenteNeto;

            CapValuacionGlobalCliente capValuacionGlobalCliente = new CapValuacionGlobalCliente();
            capValuacionGlobalCliente.Id_Emp = s.Id_Emp;
            capValuacionGlobalCliente.Id_Cd = s.Id_Cd;
            capValuacionGlobalCliente.Id_Cte = idCte;
            capValuacionGlobalCliente.Id_Vap = capValProyecto.Id_Vap;

            CD_CapValuacionGlobalCliente cdCapValuacionGlobalCliente = new CD_CapValuacionGlobalCliente();

            capValuacionGlobalCliente = cdCapValuacionGlobalCliente.Insertar(capValuacionGlobalCliente, ibt.DataContext);
            ibt.Save(); //Se generan los índices de las entradas recién creadas.
            ibt.DataContext.ReloadEntity(capValuacionGlobalCliente, cvc => cvc.CapValProyecto);
            return capValuacionGlobalCliente;
        }

        /// <summary>
        /// Regresa el conjunto de valuaciones globales asociadas al cliente idCte. En caso de que no existan valuaciones globales asociadas, crea una por defecto.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de negocio</param>
        /// <returns>IEnumerable[CapValuacionGlobalCliente]</returns>
        public IEnumerable<CapValuacionGlobalCliente> ObtenerPorCliente(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            CD_CapValuacionGlobalCliente cdCapValuacionGlobalCliente = new CD_CapValuacionGlobalCliente();
            var resultado=cdCapValuacionGlobalCliente.Consultar(s.Id_Emp, s.Id_Cd, idCte, ibt.DataContext);
            if (resultado.Count() == 0)
            {
                //El cliente no tiene asociado una valuación global. Crearla.
                CN_CapValProyecto cnCapValProyecto = new CN_CapValProyecto();
                CapValProyecto capValProyecto = new CapValProyecto();
                capValProyecto.Id_Emp = s.Id_Emp;
                capValProyecto.Id_Cd = s.Id_Cd;
                capValProyecto.Id_Cte = idCte;
                capValProyecto.Id_Rik = s.Id_Rik;
                capValProyecto.Id_Ter = null;
                capValProyecto.Id_U = s.Id_U;
                capValProyecto.Id_Vap = 0;

                capValProyecto.Vap_Fecha = DateTime.Now;
                capValProyecto.Vap_Estatus = "C";
                capValProyecto.Vap_Estatus2 = 1;

                capValProyecto = cnCapValProyecto.Crear(s, capValProyecto, ibt);

                //Se guarda la transacción (sin haber sido enviada) para generar el último índice generado por la llamada a [Crear]
                ibt.Save();

                //Se determinan los proyectos disponibles asociados al cliente para asociar los productos contenidos en dichos proyectos a la valuación
                List<CrmPromociones> List = new List<CrmPromociones>();
                CN_CrmPromocion cls = new CN_CrmPromocion();

                CrmPromociones promocion = new CrmPromociones();
                //filtro1
                promocion.Cds = s.Id_Cd;

                promocion.Representante = s.Id_U;
                promocion.Uen = -1;
                promocion.Segmento = -1;

                promocion.Territorio = -1;
                //filtro2
                promocion.Area = -1;
                promocion.Solucion = -1;
                promocion.Aplicacion = -1;
                promocion.Estatus = -1;
                promocion.Cliente = idCte;
                promocion.Id_Rik = s.Id_Rik.ToString();

                cls.ProyectosDisponiblesParaValuacion(s, promocion, ref List, ibt); //Aquí se obtiene una referencia a todos aquellos proyectos que se asociarán a la valuación global; es importante destacar que hay que modificar la determinación de los proyectos disponibles para que se tome en cuenta 

                //Se asocian los proyectos a la valuación
                CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                cdCrmValuacionOportunidades.Insertar(capValProyecto.Id_Emp, capValProyecto.Id_Cd, capValProyecto.Id_Cte, capValProyecto.Id_Vap, capValProyecto.Id_Rik.Value, List.Select(cp=>cp.Id).ToArray(), ibt.DataContext);

                //Se recorre el listado de proyectos para obtener los productos asociados y actualizar la valuación.
                CN_CrmOportunidadesProductos cnOportunidadesProductos = new CN_CrmOportunidadesProductos();
                CN_ProductoPrecios cnProductoPrecios = new CN_ProductoPrecios();
                List<CapValProyectoDet> productosConciliados = new List<CapValProyectoDet>();
                foreach (var proy in List)
                {
                    var productos=cnOportunidadesProductos.ObtenerProductosPorOportunidad(s, proy.Id, proy.Id_Cte, ibt);

                    var detalle = (from p in productos
                                   select new CapValProyectoDet()
                                   {
                                       Id_Emp = p.Id_Emp,
                                       Id_Cd = p.Id_Cd,
                                       Id_Vap = capValProyecto.Id_Vap,
                                       Id_VapDet = 0,
                                       Vap_Tipo = 1,
                                       Id_Prd = p.Id_Prd,
                                       Vap_Cantidad = Convert.ToInt32(p.COP_Cantidad),
                                       Vap_Costo = Math.Round(this.PartidasCalcularPrecioAAA(s.Id_Emp, s.Id_Cd_Ver, idCte, p.Id_Prd, capValProyecto.Id_Vap, ibt), 2),
                                       Vap_Precio = cnProductoPrecios.ConsultarPrecioAAA(s, p.Id_Prd, ibt),
                                       Det_PrecioLista = cnProductoPrecios.ConsultarPrecioLista(s, p.Id_Prd, ibt),
                                   }).ToList();
                    productosConciliados.AddRange(detalle);
                }

                //Se determinan los valores por defecto para los parámetros
                CD_CatCentroDistribucion cdCatCentroDistribucion = new CD_CatCentroDistribucion();
                CD_CatEmpresa cdCatEmpresa = new CD_CatEmpresa();
                var empresa = cdCatEmpresa.Consultar(s.Id_Emp, ibt.DataContext);
                var cd = cdCatCentroDistribucion.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);

                //Se persisten los parámetros de la valuación
                CapValProyecto_Params parametros = new CapValProyecto_Params();
                parametros.Id_Emp = s.Id_Emp;
                parametros.Id_Cd = s.Id_Cd;
                parametros.Id_Vap = capValProyecto.Id_Vap;

                //Las constantes usadas en el siguiente bloque de asignación son extraídas del procedimiento almacenado spValCondicionesCentro_Consultar
                parametros.Vap_Vigencia = 1;
                parametros.Vap_Mano_Obra = 0;
                parametros.Vap_Amortizacion = 5.67;
                parametros.Vap_Numero_Entregas = 0;
                parametros.Vap_Costo_Entregas = 0;
                parametros.Vap_Comision_Factoraje = 0;
                parametros.Vap_Comision_Anden = 0;
                parametros.Vap_Gasto_Flete_Locales = empresa.Emp_GastoLocal == null ? 0 : empresa.Emp_GastoLocal.Value;
                parametros.Vap_IVA = cd.Cd_IvaPedidosFacturacion == null ? 0 : cd.Cd_IvaPedidosFacturacion.Value;
                parametros.Vap_Plazo_Pago_Cliente = 30;
                parametros.Vap_Inventario_Key = cd.Cd_Dias == null ? 0 : cd.Cd_Dias.Value;
                parametros.Vap_Inventario_Consignacion = 0;
                parametros.Vap_Credito_Key = cd.Cd_CreditoPapel == null ? 0 : cd.Cd_CreditoPapel.Value;
                parametros.Vap_Credito_Papel = cd.Cd_CreditoKey == null ? 0 : cd.Cd_CreditoKey.Value;

                parametros.Vap_ISR = empresa.Emp_Isr == null ? 0 : empresa.Emp_Isr.Value;
                parametros.Vap_Ucs = empresa.Emp_Ucs == null ? 0 : empresa.Emp_Ucs.Value;
                parametros.Vap_Cetes = empresa.Emp_Cetes == null ? 0 : empresa.Emp_Cetes.Value;
                parametros.Vap_Adicional_Cetes = empresa.Emp_AdicionalCetes == null ? 0 : empresa.Emp_AdicionalCetes.Value;
                parametros.Vap_Costos_Fijos_No_Papel = empresa.Emp_ContribucionNoPapel == null ? 0 : empresa.Emp_ContribucionNoPapel.Value;
                parametros.Vap_Costos_Fijos_Papel = empresa.Emp_ContribucionPapel == null ? 0 : empresa.Emp_ContribucionPapel.Value;
                parametros.Vap_Gastos_Admin = empresa.Emp_GastoAdmin == null ? 0 : empresa.Emp_GastoAdmin.Value;
                parametros.Vap_Inversion_Activos = empresa.Emp_Inversion == null ? 0 : empresa.Emp_Inversion.Value;

                CD_CapValProyectoParams cdCapValProyectoParams = new CD_CapValProyectoParams();
                cdCapValProyectoParams.Insertar(parametros, ibt.DataContext);

                CapValProyecto_Parametros capValProyecto_Parametros = new CapValProyecto_Parametros();
                CN_CatCliente clsCliente = new CN_CatCliente();
                double DiasRotacion = 0;
                clsCliente.CatClienteCondPago(s.Id_Emp, s.Id_Cd_Ver, idCte, ref DiasRotacion, ibt);
                capValProyecto_Parametros.txtCuentasPorCobrar = DiasRotacion;
                capValProyecto_Parametros.txtInventario = parametros.Vap_Inventario_Key;
                capValProyecto_Parametros.txtGastosServirCliente = cd.Cd_ComisionRik;
                capValProyecto_Parametros.txtGastosVarAplTerr = 0;
                capValProyecto_Parametros.txtVigencia = parametros.Vap_Vigencia;
                capValProyecto_Parametros.txtFleteLocales = parametros.Vap_Gasto_Flete_Locales;
                capValProyecto_Parametros.txtIsr = parametros.Vap_ISR;
                capValProyecto_Parametros.txtCetes = parametros.Vap_Cetes;
                capValProyecto_Parametros.txtFinanciamientoproveedores = cd.Cd_DiasFinanciaProv;
                capValProyecto_Parametros.txtInversionactivosfijos = cd.Cd_FactorConvActFijo;
                capValProyecto_Parametros.txtCostodecapital = cd.Cd_TasaIncCostoCapital;
                capValProyecto_Parametros.txtManoObra = 0;
                capValProyecto_Parametros.txtFletesPagadosalCliente = 0;
                capValProyecto_Parametros.Id_Emp = s.Id_Emp;
                capValProyecto_Parametros.Id_Cd = s.Id_Cd;
                capValProyecto_Parametros.Id_Vap = capValProyecto.Id_Vap;
                CD_CapValProyecto_Parametros cdCapValProyecto_Parametros = new CD_CapValProyecto_Parametros();
                cdCapValProyecto_Parametros.Insertar(capValProyecto_Parametros, ibt.DataContext);

                //Se persisten los productos asociados a la valuación
                CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
                cdCapValProyectoDet.Insertar(productosConciliados, ibt.DataContext);

                //Se procede a calcular el resultado de la valuación
                ResultadosValuacion resultadosValuacion=new ResultadosValuacion();
                GeneraReporteVP(resultadosValuacion, productosConciliados, parametros, capValProyecto_Parametros, s, ibt);

                capValProyecto.Vap_UtilidadRemanente = resultadosValuacion.UtilidadRemanente;
                capValProyecto.Vap_ValorPresenteNeto = resultadosValuacion.ValorPresenteNeto;

                CapValuacionGlobalCliente capValuacionGlobalCliente = new CapValuacionGlobalCliente();
                capValuacionGlobalCliente.Id_Emp = s.Id_Emp;
                capValuacionGlobalCliente.Id_Cd = s.Id_Cd;
                capValuacionGlobalCliente.Id_Cte = idCte;
                capValuacionGlobalCliente.Id_Vap = capValProyecto.Id_Vap;

                capValuacionGlobalCliente = cdCapValuacionGlobalCliente.Insertar(capValuacionGlobalCliente, ibt.DataContext);
                ibt.Save(); //Se generan los índices de las entradas recién creadas.
                ibt.DataContext.ReloadEntity(capValuacionGlobalCliente, cvc => cvc.CapValProyecto);

                resultado = new List<CapValuacionGlobalCliente>() { capValuacionGlobalCliente };
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los proyectos asociados a una valuación global
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idVal">Identificador de la valuación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CrmOportunidade[]</returns>
        public IEnumerable<CrmOportunidade> ObtenerProyectosAsociados(Sesion s, int idVal, int idCte, IBusinessTransaction ibt)
        {
            CD_CapValuacionGlobalCliente cdCapValuacionGlobalCliente = new CD_CapValuacionGlobalCliente();
            return cdCapValuacionGlobalCliente.ConsultarProyectosAsociados(s.Id_Emp, s.Id_Cd, idVal, idCte, ibt.DataContext);
        }

        public double PartidasCalcularPrecioAAA(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Prd, int idVap, IBusinessTransaction ibt)
        {
            double precioProductoAceptado = 0;
            //obtener precio especial del producto 
            //para el cliente actual de la factura
            //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
            VentanaPrecioEspecialPro precioEspecialPro = null;
            new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, ibt
                , Id_Emp, Id_Cd, Id_Cte, Id_Prd /* , Convert.ToInt32(cmbMoneda.SelectedValue) */);
            if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
            {
                /*
                    * NOTA: si el precio está en dólares u otro tipo de moneda, 
                    * se hace la conversión al tipo de moneda de la Valuacion de proyectos
                    */
                if (precioEspecialPro.Id_Mon != 1) // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                { //Consultar tipo de cambio
                    double tipoCambioFactura = 1; // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                    double tipoCambioPrecioEspecial = 0;

                    TipoMoneda tipoMoneda = new TipoMoneda();
                    List<TipoMoneda> lista = new List<TipoMoneda>();
                    new CN_CatTipoMoneda().ConsultaTipoMoneda(tipoMoneda, Id_Emp
                        , ibt, ref lista);

                    foreach (TipoMoneda tm in lista)
                    {
                        if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                            tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                    }
                    precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                }
                else
                    precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
            }
            else
            {
                //Si no hay un precio especial en SOLICITUD DE PRECIOS ESPECIALES
                //va por el precio del catalogo CLIENTE-PRODUCTO, si no hay toma el precio AAA normal del producto
                //obtener precio AAA
                float precioAAA = 0;
                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, Id_Emp, Id_Cd, Id_Prd, ibt);


                double precioAAA_CLIENTEPRODUCTO = 0;
                ClienteProd clienteProd = new ClienteProd();
                clienteProd.Id_Emp = Id_Emp;
                clienteProd.Id_Cd = Id_Cd;
                clienteProd.Id_Cte = Id_Cte;
                clienteProd.Id_Prd = Id_Prd;
                clienteProd.Id_Vap = idVap;
                new CN_CatClienteProd().ConsultaClienteProdPrecioEspecialibt(clienteProd, ref precioAAA_CLIENTEPRODUCTO, ibt);

                precioProductoAceptado = precioAAA_CLIENTEPRODUCTO > 0 ? precioAAA_CLIENTEPRODUCTO : precioAAA;



            }
            return precioProductoAceptado;
        }

        public void GeneraReporteVP(ResultadosValuacion resultadosValuacion, IEnumerable<CapValProyectoDet> lista, CapValProyecto_Params parametros, CapValProyecto_Parametros parametros2, Sesion sesion, IBusinessTransaction ibt)
        {

            Double VentaNeta = 0;
            Double VentaNetaPapel = 0;
            Double VentaNetaOtros = 0;
            Double CostoMaterial = 0;
            Double CostoMaterialNOPapel = 0;
            Double AmortizacionTotal = 0;
            Double Prd_PesConTecnico = 0;
            Double UtilidadBruta = 0;

            String EsPapel = "";
            Int32 Prd_Mes = 0;
            Double Prd_PesosAAA = 0;
            Double Prd_PesosConTecnico = 0;
            Int32 MaxMeses = 0;

            double TotalInversionComodatos = 0;

            foreach (var valProyectoDet in lista)
            {

                EsPapel = "";
                Prd_PesosConTecnico = 0;
                Prd_Mes = 0;
                Prd_PesosAAA = 0;

                CN_CatProducto clsProducto = new CN_CatProducto();
                clsProducto.CatProducto_Informacion_VP(valProyectoDet.Id_Prd, sesion.Emp_Cnx, ref EsPapel, ref Prd_PesosConTecnico, ref Prd_Mes, ref Prd_PesosAAA, ibt);

                if (valProyectoDet.Vap_Tipo == 1)   //Si es Consumible
                {

                    VentaNeta = VentaNeta + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    CostoMaterial = CostoMaterial + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Costo);

                    if (EsPapel == "S")   //Si Es Papel
                    {
                        VentaNetaPapel = VentaNetaPapel + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    }
                    else  //Si es Diferente de Papel
                    {
                        CostoMaterialNOPapel = CostoMaterialNOPapel + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Costo);
                        VentaNetaOtros = VentaNetaOtros + (valProyectoDet.Vap_Cantidad * valProyectoDet.Vap_Precio);
                    }
                }
                else // Si es Sistemas Propietarios
                {

                    if (MaxMeses < Prd_Mes)
                    {
                        MaxMeses = Prd_Mes;
                    }
                    Prd_PesConTecnico = Prd_PesConTecnico + (valProyectoDet.Vap_Cantidad * Prd_PesosConTecnico);
                    AmortizacionTotal = AmortizacionTotal + ((valProyectoDet.Vap_Cantidad * Prd_PesosAAA) / Prd_Mes);
                    TotalInversionComodatos = TotalInversionComodatos + (valProyectoDet.Vap_Cantidad * Prd_PesosAAA);
                }
            }

            UtilidadBruta = VentaNeta - CostoMaterial;

            CentroDistribucion cd = new CentroDistribucion();
            new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, ibt);

            double FactorFijos = 0;
            double FactorUCS = 0;

            if (VentaNeta < 5000) FactorFijos = 17.5;
            if (VentaNeta >= 5000 && VentaNeta < 10000) FactorFijos = 16.84;
            if (VentaNeta >= 10000 && VentaNeta < 15000) FactorFijos = 16.18;
            if (VentaNeta >= 15000 && VentaNeta < 20000) FactorFijos = 15.53;
            if (VentaNeta >= 20000 && VentaNeta < 25000) FactorFijos = 14.87;
            if (VentaNeta >= 25000 && VentaNeta < 30000) FactorFijos = 14.21;
            if (VentaNeta >= 30000 && VentaNeta < 35000) FactorFijos = 13.55;
            if (VentaNeta >= 35000 && VentaNeta < 40000) FactorFijos = 12.89;
            if (VentaNeta >= 40000 && VentaNeta < 45000) FactorFijos = 12.24;
            if (VentaNeta >= 45000 && VentaNeta < 50000) FactorFijos = 11.58;
            if (VentaNeta >= 50000 && VentaNeta < 55000) FactorFijos = 10.92;
            if (VentaNeta >= 55000 && VentaNeta < 60000) FactorFijos = 10.26;
            if (VentaNeta >= 60000 && VentaNeta < 65000) FactorFijos = 9.61;
            if (VentaNeta >= 65000 && VentaNeta < 70000) FactorFijos = 8.95;
            if (VentaNeta >= 70000 && VentaNeta < 75000) FactorFijos = 8.29;
            if (VentaNeta >= 75000 && VentaNeta < 80000) FactorFijos = 7.63;
            if (VentaNeta >= 80000 && VentaNeta < 85000) FactorFijos = 6.97;
            if (VentaNeta >= 85000 && VentaNeta < 90000) FactorFijos = 6.32;
            if (VentaNeta >= 90000 && VentaNeta < 100000) FactorFijos = 5.66;
            if (VentaNeta >= 100000) FactorFijos = 5.0;

            if (VentaNeta < 5000) FactorUCS = 3.5;
            if (VentaNeta >= 5000 && VentaNeta < 10000) FactorUCS = 3.0;
            if (VentaNeta >= 10000 && VentaNeta < 25000) FactorUCS = 2.5;
            if (VentaNeta >= 25000 && VentaNeta < 50000) FactorUCS = 2;
            if (VentaNeta >= 50000 && VentaNeta < 100000) FactorUCS = 1.5;
            if (VentaNeta >= 100000) FactorUCS = 1;

            double Cte_CarMP = parametros.Vap_Mano_Obra.Value;
            double Cte_GasVarT = 0;
            double Cte_FletePaga = 0;


            double DiasRotacion = 0;


            DiasRotacion = parametros2.txtCuentasPorCobrar.Value;


            //calcular financiamiento de proveedores
            double financiamientoProveedores = (((((CostoMaterial / 30) * parametros2.txtInventario.Value) / parametros2.txtInventario.Value) * parametros2.txtFinanciamientoproveedores.Value) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
            if (double.IsNaN(financiamientoProveedores) || double.IsInfinity(financiamientoProveedores))
            {
                financiamientoProveedores = 0;
            }

            double inversionTotalActivos
                = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
                + ((CostoMaterial / 30) * parametros2.txtInventario.Value)
                + ((VentaNeta / 30) * parametros2.txtInversionactivosfijos.Value);



            if (double.IsNaN(inversionTotalActivos) || double.IsInfinity(inversionTotalActivos))
            {
                inversionTotalActivos = 0;
            }

            //calcular utilidad bruta
            UtilidadBruta =
                VentaNeta
                - CostoMaterial
                - Cte_CarMP
                - (/*CostoMaterial*/ CostoMaterialNOPapel * (parametros2.txtFleteLocales.Value / 100)) //flete
                - AmortizacionTotal
                - Prd_PesConTecnico;



            if (double.IsNaN(UtilidadBruta) || double.IsInfinity(UtilidadBruta))
            {
                UtilidadBruta = 0;
            }
            
            double UtilidadMarginal =
                UtilidadBruta
                - (UtilidadBruta * (parametros2.txtGastosServirCliente.Value / 100))
                - Cte_GasVarT
                - Cte_FletePaga;

            if (double.IsNaN(UtilidadMarginal) || double.IsInfinity(UtilidadMarginal))
            {
                UtilidadMarginal = 0;
            }

            //calcular Uafir mensual
            double UafirMensual =
                UtilidadMarginal
                - (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))
                - (VentaNeta * (Convert.ToSingle(FactorUCS) / 100));
            if (double.IsNaN(UafirMensual) || double.IsInfinity(UafirMensual))
            {
                UafirMensual = 0;
            }
            //calcular Costo de capital

            double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (parametros2.txtCetes.Value + parametros2.txtCostodecapital.Value) / 100;
            
            if (double.IsNaN(CostoCapital) || double.IsInfinity(CostoCapital))
            {
                CostoCapital = 0;
            }
            //calcular Uafir después de impuestos

            double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (parametros2.txtIsr.Value / 100));


            if (double.IsNaN(UafirDespuesImpuestos) || double.IsInfinity(UafirDespuesImpuestos))
            {
                UafirDespuesImpuestos = 0;
            }
            //calcular porcentaje de utilidad remanente

            double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (parametros2.txtCetes.Value + parametros2.txtCostodecapital.Value);


            if (double.IsNaN(UtilidadRemanentePorc) || double.IsInfinity(UtilidadRemanentePorc))
            {
                UtilidadRemanentePorc = 0;
            }

            double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));

            if (double.IsNaN(ctaPorCobrar) || double.IsInfinity(ctaPorCobrar))
            {
                ctaPorCobrar = 0;
            }

            double txtUafirActivos = UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100;
            if (double.IsNaN(txtUafirActivos) || double.IsInfinity(txtUafirActivos))
            {
                txtUafirActivos = 0;
            }

            double txtISRyPTUMon = (UafirMensual * 12) * (parametros2.txtIsr.Value / 100);

            if (double.IsNaN(txtISRyPTUMon) || double.IsInfinity(txtISRyPTUMon))
            {
                txtISRyPTUMon = 0;
            }

            double txtGastosVariablesPorc = (Cte_GasVarT / VentaNeta) * 100;

            if (double.IsNaN(txtGastosVariablesPorc) || double.IsInfinity(txtGastosVariablesPorc))
            {
                txtGastosVariablesPorc = 0;
            }
            double txtOtrosGastosVariablesPorc = 0;
            if (double.IsNaN(txtOtrosGastosVariablesPorc) || double.IsInfinity(txtOtrosGastosVariablesPorc))
            {
                txtOtrosGastosVariablesPorc = 0;
            }
            double txtFletesPagadosPorc = (Cte_FletePaga / VentaNeta) * 100;
            if (double.IsNaN(txtFletesPagadosPorc) || double.IsInfinity(txtFletesPagadosPorc))
            {
                txtFletesPagadosPorc = 0;
            }

            double txtCargoUCSPorc = ((VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)) / VentaNeta) * 100;

            if (double.IsNaN(txtCargoUCSPorc) || double.IsInfinity(txtCargoUCSPorc))
            {
                txtCargoUCSPorc = 0;
            }
            double txtUafirMensualPorc = (UafirMensual / VentaNeta) * 100;
            if (double.IsNaN(txtUafirMensualPorc) || double.IsInfinity(txtUafirMensualPorc))
            {
                txtUafirMensualPorc = 0;
            }
            double txtContribucionGastosFijosPapelPorc = ((VentaNeta * (Convert.ToSingle(FactorFijos) / 100)) / VentaNeta) * 100;
            if (double.IsNaN(txtContribucionGastosFijosPapelPorc) || double.IsInfinity(txtContribucionGastosFijosPapelPorc))
            {
                txtContribucionGastosFijosPapelPorc = 0;
            }

            double txtContribucionGastosFijosOtrosPorc = ((VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)) / VentaNeta) * 100;

            if (double.IsNaN(txtContribucionGastosFijosOtrosPorc) || double.IsInfinity(txtContribucionGastosFijosOtrosPorc))
            {
                txtContribucionGastosFijosOtrosPorc = 0;
            }
            double txtAmortizacionPorc = (AmortizacionTotal / VentaNeta) * 100;
            if (double.IsNaN(txtAmortizacionPorc) || double.IsInfinity(txtAmortizacionPorc))
            {
                txtAmortizacionPorc = 0;
            }
            double txtCostoServEquipoPorc = (Prd_PesConTecnico / VentaNeta) * 100;
            if (double.IsNaN(txtCostoServEquipoPorc) || double.IsInfinity(txtCostoServEquipoPorc))
            {
                txtCostoServEquipoPorc = 0;
            }
            //double txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)) / VentaNeta) * 100;
            double txtComisionRepPorc = ((UtilidadBruta * (parametros2.txtGastosServirCliente.Value / 100)) / VentaNeta) * 100;

            if (double.IsNaN(txtComisionRepPorc) || double.IsInfinity(txtComisionRepPorc))
            {
                txtComisionRepPorc = 0;
            }
            double txtUtilidadPorc = (UtilidadBruta / VentaNeta) * 100;
            if (double.IsNaN(txtUtilidadPorc) || double.IsInfinity(txtUtilidadPorc))
            {
                txtUtilidadPorc = 0;
            }
            double txtManoObraPorc = (parametros2.txtManoObra.Value / VentaNeta) * 100;

            if (double.IsNaN(txtManoObraPorc) || double.IsInfinity(txtManoObraPorc))
            {
                txtManoObraPorc = 0;
            }

            double txtFletePorc2 = ((CostoMaterialNOPapel * (parametros2.txtFleteLocales.Value / 100)) / VentaNeta) * 100;


            if (double.IsNaN(txtFletePorc2) || double.IsInfinity(txtFletePorc2))
            {
                txtFletePorc2 = 0;
            }
            double txtCostoMaterialPorc = (CostoMaterial / VentaNeta) * 100;
            if (double.IsNaN(txtCostoMaterialPorc) || double.IsInfinity(txtCostoMaterialPorc))
            {
                txtCostoMaterialPorc = 0;
            }
            double txtUtilidadMarginalPorc = (UtilidadMarginal / VentaNeta) * 100;
            if (double.IsNaN(txtUtilidadMarginalPorc) || double.IsInfinity(txtUtilidadMarginalPorc))
            {
                txtUtilidadMarginalPorc = 0;
            }

            Int32 AnioIndice = 0;
            Double Flujo = 0;
            Double VPFlujo = 0;
            Double TotalVPFlujo = 0;

            if (MaxMeses == 0)
            {
                MaxMeses = 1;
            }
            MaxMeses = Convert.ToInt32(parametros2.txtVigencia.Value) * 12;

            while (AnioIndice < Convert.ToInt32(MaxMeses / 12) + 1)
            {
                if (AnioIndice == 0)
                {
                    Flujo = (inversionTotalActivos + TotalInversionComodatos) * -1;
                    VPFlujo = (inversionTotalActivos + TotalInversionComodatos) * -1;
                }
                else
                {
                    Flujo = ((UafirMensual + AmortizacionTotal) * 12);
                    VPFlujo = Flujo / Math.Pow((((cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100) + 1), AnioIndice);
                }
                TotalVPFlujo = TotalVPFlujo + VPFlujo;

                AnioIndice++;
            }

            resultadosValuacion.UtilidadRemanente = Convert.ToDecimal((UafirDespuesImpuestos - CostoCapital));
            resultadosValuacion.ValorPresenteNeto = Convert.ToDecimal(TotalVPFlujo);
        }

        public class ResultadosValuacion
        {
            public decimal? UtilidadRemanente
            {
                get;
                set;
            }

            public decimal? ValorPresenteNeto
            {
                get;
                set;
            }

            /// <summary>
            /// Regresa true en caso de que la utilidad remanente y el valor presente neto MAYOR A CERO; false en caso contrario
            /// </summary>
            public bool EsPositiva
            {
                get
                {
                    if (UtilidadRemanente != null)
                    {
                        if (ValorPresenteNeto != null)
                        {
                            return UtilidadRemanente > 0 && ValorPresenteNeto > 0;
                        }
                    }
                    //Se asume que es negativa en caso de que la utilidad remanente o el valor presente neto no se encuentren presentes
                    return false;
                }
            }
        }
    }
}
