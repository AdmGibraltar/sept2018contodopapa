using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;
using CapaNegocios.FlujosDeEstado.CRM;

namespace CapaNegocios
{
    public class CN_CrmOportunidad
    {
        public void ComboSegmento(Sesion sesion, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ComboSegmento(sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ComboArea(Sesion sesion, int segmento, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ComboArea(sesion, segmento, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarSolucion(Sesion sesion, int area, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaSolucion(sesion, area, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaAplicacion(Sesion sesion, int solucion, ref List<CrmOportunidades> list)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaAplicacion(sesion, solucion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencial(Sesion sesion, CrmOportunidades registros, int tipo, ref double VPotencial)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaVPotencial(sesion, registros, tipo, ref VPotencial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVPotencialCliente(Sesion sesion, CrmOportunidades registros, ref double valorTeorico, ref double valorObservado, ref double? Teorico, ref double? Observado)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaVPotencialCliente(sesion, registros, ref valorTeorico, ref valorObservado, ref  Teorico, ref  Observado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta el detalle de una oportunidad, además de cargar las aplicaciones asociadas.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="cd"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="list"></param>
        public void ConsultaOportunidad(Sesion sesion, int cd, int idOportunidad, ref List<CrmOportunidades> list, bool cargarColeccionObjectosOportunidadAplicaciones = false)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.ConsultaOportunidad(sesion, cd, idOportunidad, ref list);

                CD_CrmOportunidadesAplicacion cdOpsAp = new CD_CrmOportunidadesAplicacion();
                var listaAplicaciones = cdOpsAp.ConsultarPorOportunidad(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, idOportunidad, sesion.Emp_Cnx_EF);
                if (list.Count > 0)
                {
                    list[0].Aplicaciones = listaAplicaciones.Select(obj => obj.Id_Apl).ToArray();
                }

                if (cargarColeccionObjectosOportunidadAplicaciones)
                {
                    CN_CrmOportunidadesAplicacion cnCrmOportunidadesAplicacion = new CN_CrmOportunidadesAplicacion();
                    list[0].CrmOportunidadesAplicaciones = cnCrmOportunidadesAplicacion.ObtenerPorOportunidad(sesion, list[0].Id_Cte, list[0].Id_Op).ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOportunidad(Sesion sesion, CrmOportunidades registros, ref int validador)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.UpdateOportunidad(sesion, registros, ref validador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void tipoUsuario(Sesion sesion, ref string tipoUsuario)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.tipoUsuario(sesion, ref tipoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOportunidad(int Id_emp, int Id_Cd, int Id_Op, string conexion)
        {
            try
            {
                CD_CrmOportunidad claseCRM = new CD_CrmOportunidad();
                claseCRM.DeleteOportunidad(Id_emp, Id_Cd, Id_Op, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Regresa el conjunto de proyectos asociados a un RIK.
        /// </summary>
        /// <param name="sesion">Sesión del operador</param>
        /// <returns>CrmOportunidade[] en una llamada satisfactoria; null en caso contrario</returns>
        public IEnumerable<CrmOportunidade> ObtenerPorRik(Sesion sesion)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarPorRIK(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// Regresa el conjunto de proyectos asociados a un RIK. Esta versión utiliza una transacción de capa de negocio para poder trabajar con las propiedades de navegación de las entidades.
        /// </summary>
        /// <param name="sesion">Sesión del operador</param>
        /// <returns>CrmOportunidade[] en una llamada satisfactoria; null en caso contrario</returns>
        public IEnumerable<CrmOportunidade> ObtenerPorRik(Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarPorRIK(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, ibt.DataContext);
        }

        /// <summary>
        /// Regresa el conjunto de proyectos originados en CRMII y asociados a un RIK. Esta versión utiliza 
        /// una transacción de capa de negocio para poder trabajar con las propiedades de navegación de las entidades.
        /// </summary>
        /// <param name="sesion">Sesión del operador</param>
        /// <returns>CrmOportunidade[] en una llamada satisfactoria; null en caso contrario</returns>
        public IEnumerable<CrmOportunidade> ObtenerSoloCRMIIPorRik(Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarSoloCRMIIPorRik(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_Rik, ibt.DataContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRik"></param>
        /// <param name="sesion"></param>
        /// <param name="ibt"></param>
        /// <returns></returns>
        public IEnumerable<CrmOportunidade> ObtenerSoloCRMII_PorRik(int IdRik, Sesion sesion, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarSoloCRMIIPorRik(sesion.Id_Emp, sesion.Id_Cd, IdRik, ibt.DataContext);
        }

        /// <summary>
        /// Regresa los proyectos asociados a una valuación.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>CrmOportunidade[] en caso de una llamada exitosa.</returns>
        public IEnumerable<CrmOportunidade> ObtenerProyectosEnValuaciones(Sesion s, int idCte)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarProyectosEnValuaciones(s.Id_Emp, s.Id_Cd, idCte, s.Id_Rik, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Regresa los proyectos asociados a una valuación.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns></returns>
        public IEnumerable<CrmOportunidade> ObtenerProyectosEnValuaciones(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarProyectosEnValuaciones(s.Id_Emp, s.Id_Cd, idCte, s.Id_Rik, ibt.DataContext);
        }

        /// <summary>
        /// Valida y actualiza las fases del proyecto, basado en las condiciones actuales del proyecto.
        /// </summary>
        /// <param name="s">Sesión del llamador</param>
        /// <param name="proyecto">Instancia de la entidad [CrmOportunidades]</param>
        public void ValidarFases(Sesion s, CrmOportunidade proyecto)
        {
            ProyectoStateMachine proyectoStateMachine = new ProyectoStateMachine(proyecto, s);
            proyectoStateMachine.Update();
        }

        /// <summary>
        /// Devuelve la instancia CrmOportunidade dado el identificador de proyecto idOp.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idTer">Identificador del territorio asociado al proyecto idOp</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <returns>CrmOportunidade.</returns>
        public CrmOportunidade ObtenerPorId(Sesion s, int idOp)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarPorId(s.Id_Emp, s.Id_Cd, idOp, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Devuelve la instancia CrmOportunidade dado el identificador de proyecto idOp.
        /// </summary>
        /// <param name="s">Sesión del operador</param>
        /// <param name="idTer">Identificador del territorio asociado al proyecto idOp</param>
        /// <param name="idOp">Identificador del proyecto</param>
        /// <param name="ibt">Transaccion de negocio</param>
        /// <returns>CrmOportunidade.</returns>
        public CrmOportunidade ObtenerPorId(Sesion s, int idOp, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.ConsultarPorId(s.Id_Emp, s.Id_Cd, idOp, ibt.DataContext);
        }

        /// <summary>
        /// Regresa las evidencias registradas para el proyecto [entidad] dado su fase actual
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="entidad">Instancia de datos de la entidad CrmOportunidades</param>
        /// <param name="ibt">Transacción de regla de negocio</param>
        /// <returns>IEnumerable<CapProyectoFaseEvidencia></returns>
        public IEnumerable<CapProyectoFaseEvidencia> ObtenerEvidencias(Sesion s, CrmOportunidade entidad, IBusinessTransaction ibt)
        {
            CD_CapProyectoFaseEvidencia cdCapProyectoFaseEvidencia = new CD_CapProyectoFaseEvidencia();
            CN_CatBiblioteca cnCatBiblioteca = new CN_CatBiblioteca();
            var biblioteca = cnCatBiblioteca.ObtenerBibliotecaDefaultDeUsuario(s, ibt);
            return cdCapProyectoFaseEvidencia.ConsultarPorProyecto(s.Id_Emp, s.Id_Cd, s.Id_U, biblioteca.Id_Biblioteca, entidad.Id_Op, entidad.Estatus.Value, ibt.DataContext);
        }

        public CapaEntidad.eResultadoValuacion Calcular_ResultadoValuacion(int Id_Emp, int Id_Cd, int Id_Op, Sesion sesion)
        {
            eResultadoValuacion eRV = new eResultadoValuacion();
            CD_CrmOportunidad cd_crmOp = new CD_CrmOportunidad();

            eRV = cd_crmOp.Calcular_ResultadoValuacion(Id_Emp, Id_Cd, Id_Op, sesion.Emp_Cnx);

            return eRV;
        }


        public eResultadoValuacion Consulta_ResultadoValuacion(int Id_Emp, int Id_Cd, int Id_Cte, int Id_Vap, IBusinessTransaction ibt)
        {
            eResultadoValuacion RV = new eResultadoValuacion();

            CD_CapValProyectoDet cdCVPD = new CD_CapValProyectoDet();

            RV = cdCVPD.Consultar_ResultadoValuacion(Id_Emp, Id_Cd, Id_Cte, Id_Vap, ibt.DataContext);

            return RV;
        }

        public CapaEntidad.eCapValProyecto Consulta_ResultadoValuacion(int Cte, int Val, Sesion sesion, IBusinessTransaction ibt)
        {
            CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();
            CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
            Obj = cnPE.spCRMCapValProyecto(sesion.Id_Emp, sesion.Id_Cd, Cte, sesion.Id_Rik, Cte, sesion);
            return Obj;
        }

        /// <summary>
        /// Calcula el resultado de la valuación, generando los valores de utilidad remanente y valor presente neto
        /// </summary>
        /// <param name="resultadosValuacion">Instancia de la clase ResultadosValuacion que almacenará el resultado de la valuación</param>
        /// <param name="proyecto">Instancia de datos de la entidad CrmPromociones</param>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public ResultadosValuacion CalcularResultadoValuacion(CrmPromociones proyecto, Sesion sesion, IBusinessTransaction ibt)
        {
            try
            {

                ResultadosValuacion resultadosValuacion = new ResultadosValuacion();
                CD_CapValProyectoDet cdCapValProyectoDet = new CD_CapValProyectoDet();
                CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
                CN_CapValuacionGlobalCliente cnCapValuacionGlobalCliente = new CN_CapValuacionGlobalCliente();
                CN_ProductoPrecios cnProductoPrecios = new CN_ProductoPrecios();
                var proyectoValuaciones = cdCrmValuacionOportunidades.ConsultarPorProyecto(sesion.Id_Emp, sesion.Id_Cd, proyecto.Id_Cte, proyecto.Id, ibt.DataContext);
                if (proyectoValuaciones == null)
                {
                    throw new ProyectoNoAsociadoAValuacionException(proyecto.Id);
                }

                CD_CrmOportunidadesProductos cdCrmOportunidadesProductos = new CD_CrmOportunidadesProductos();
                var proyectoProductos = cdCrmOportunidadesProductos.ConsultarPorOportunidadYCliente(sesion.Id_Emp, sesion.Id_Cd, proyectoValuaciones.Id_Op, proyectoValuaciones.Id_Cte, ibt.DataContext);

                var productosValuacion = cdCapValProyectoDet.ConsultarPorCapValProyectoId(proyectoValuaciones.Id_Emp, proyectoValuaciones.Id_Cd, proyectoValuaciones.Id_Val, ibt.DataContext).ToList().Where(cvpd =>
                {
                    return proyectoProductos.Where(cop => cop.Id_Prd == cvpd.Id_Prd).Count() > 0;
                })/*.Select(p =>
            {
                return new CapValProyectoDet()
                {
                    Id_Emp = p.Id_Emp,
                    Id_Cd = p.Id_Cd,
                    Id_Vap = p.Id_Vap,
                    Id_VapDet = 0,
                    Vap_Tipo = 1,
                    Id_Prd = p.Id_Prd,
                    Vap_Cantidad = p.Vap_Cantidad,
                    Vap_Costo = Math.Round(cnCapValuacionGlobalCliente.PartidasCalcularPrecioAAA(sesion.Id_Emp, sesion.Id_Cd_Ver, proyecto.Id_Cte, p.Id_Prd, p.Id_Vap, ibt), 2),
                    Vap_Precio = cnProductoPrecios.ConsultarPrecioAAA(sesion, p.Id_Prd, ibt),
                    Det_PrecioLista = cnProductoPrecios.ConsultarPrecioLista(sesion, p.Id_Prd, ibt),
                };             
            })*/;

                IEnumerable<CapValProyectoDet> lista = productosValuacion;

                CD_CapValProyecto_Parametros cdCapValProyecto_Parametros = new CD_CapValProyecto_Parametros();
                var pars = cdCapValProyecto_Parametros.ConsultarPorValuacion(sesion.Id_Emp, sesion.Id_Cd, proyectoValuaciones.Id_Val, ibt.DataContext);

                CapValProyecto_Parametros parametros2 = pars;

                CapValProyecto_Params parametros = null;
                CD_CapValProyectoParams cdCapValProyectoParams = new CD_CapValProyectoParams();
                parametros = cdCapValProyectoParams.Consultar(sesion.Id_Emp, sesion.Id_Cd, proyectoValuaciones.Id_Val, ibt.DataContext);

                if (parametros2 == null)
                {
                    throw new CapValProyecto_ParametrosIndefinidosException();
                }

                if (parametros == null)
                {
                    throw new CapValProyecto_ParamsIndefinidosException();
                }

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
                return resultadosValuacion;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Obtiene todos los proyectos registrados desde la plataforma CRMII
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>IEnumerable[CrmOportunidade]</returns>
        public IEnumerable<CrmOportunidade> ObtenerTodosDeCRMII(Sesion s, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            return cdCrmOportunidad.Consultar(crmOp =>
            {
                return crmOp.Id_CrmProspecto != null;
            }, ibt.DataContext);
        }

        /// <summary>
        /// Cancela un proyecto
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente asociado al proyecto idOp</param>
        /// <param name="idOp">Identificador del proyecto que se desea cancelar</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void Cancelar(Sesion s, int idCte, int idOp, int idCausa, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            var op = cdCrmOportunidad.ConsultarPorId(s.Id_Emp, s.Id_Cd, idOp, ibt.DataContext);

            //
            // La cancelacion de proyecto se aplica
            // en cualquier fase del proyecto.
            //
            // 11 Sep 2018 RFH
            //

            /*if (op.Estatus == 3)
            {
                // CANCELAR EL PROYECT 
            }*/
            // CUANDO ESTA EN CANCELADO o ACEPTADA la propuesta ya no permite cancelar.
            /*
            if (op.Estatus == 4 && op.Estatus == 5) 
            //if (op.Estatus > 3 || op.Estatus < 1)
            {                
                throw new CancelacionNoPermitidaException();
            }
            */

            CD_CapValuacionProyecto cdCapValuacionProyecto = new CD_CapValuacionProyecto();
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            var valuacionProyecto = cdCrmValuacionOportunidades.ConsultarPorProyecto(s.Id_Emp, s.Id_Cd, idCte, idOp, ibt.DataContext);

            /*
             *  // CANCELAR EL PROYECT 
            if (valuacionProyecto != null)
            {
                throw new CancelacionNoPermitidaProyectoEnValuacionGlobalException();
            }
            */

            op.Estatus = 5; //Una enumeración no caería mal.
            op.Id_Causa = idCausa;
            op.FechaCancelacion = DateTime.Now;

        }

        public class ResultadosValuacion
        {
            public ResultadosValuacion()
            {
                UtilidadRemanente = null;
                ValorPresenteNeto = null;
            }

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

        public class CapValProyecto_ParamsIndefinidosException
            : Exception
        {
            public CapValProyecto_ParamsIndefinidosException()
                : base()
            {
            }
        }

        public class CapValProyecto_ParametrosIndefinidosException
            : Exception
        {
            public CapValProyecto_ParametrosIndefinidosException()
                : base()
            {
            }
        }

        /// <summary>
        /// Clase de excepción que representa la ausencia de la asociación de un proyecto a una valuación
        /// </summary>
        public class ProyectoNoAsociadoAValuacionException
            : Exception
        {
            /// <summary>
            /// Constructor que acepta un identificador de proyecto
            /// </summary>
            /// <param name="id"></param>
            public ProyectoNoAsociadoAValuacionException(int id)
                : base(string.Format("El proyecto {0} no ha sido asociado a una valuación", id))
            {
            }
        }

        public class CancelacionNoPermitidaException
            : Exception
        {
            public CancelacionNoPermitidaException()
                : base("El proyecto se encuentra en una etapa en donde no está permitida la cancelación")
            {
            }
        }

        public class CancelacionNoPermitidaProyectoEnValuacionGlobalException
            : Exception
        {
            public CancelacionNoPermitidaProyectoEnValuacionGlobalException()
                : base("El proyecto se encuentra actualmente en una valuación global. Por favor, primero quite la asociación del proyecto.")
            {
            }
        }
    }
}