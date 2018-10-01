using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo;
using CapaModelo.SIANWebCentral;
using CapaEntidad;
using CapaDatos.SIANCentral;
using CapaModelo.SIANCentral;
using CapaModelo.SIANWebCentral;

namespace CapaNegocios
{
    public class CN_ReporteVentaCRM
    {
        public IEnumerable<EntradaCDReporteVentaCRM> GenerarVistaGeneral(Sesion s, int cdTipo, int anyo, int mes, bool bTrimestral, /*IBusinessTransaction ibt,*/ IBusinessTransaction ibtSianCentral)
        {
            var transaccionesNegocioSIANCentral = CN_FabricaTransaccionNegocios.ObtenerParaSIANCentral(s);
            //Se requiere la información de los representantes con sus proyectos.
            //Todos los proyectos de un solo repositorio pertenecen a un mismo centro de distribución

            CD_CatCuotasCRM cdCatCuotasCRM = new CD_CatCuotasCRM();
            var cuotas = cdCatCuotasCRM.ConsultarPorAnyoMes(anyo, mes, ibtSianCentral.DataContext);

            CapaDatos.SIANCentral.CD_CatCDI cdCatCDI = new CapaDatos.SIANCentral.CD_CatCDI();
            var cdis = cdCatCDI.Consultar(ibtSianCentral.DataContext).Where(cdi => cdi.Id_Cd != 710 && cdi.Cd_Tipo == cdTipo);

            List<EntradaCDReporteVentaCRM> resultado = new List<EntradaCDReporteVentaCRM>();

            foreach (var cdi in cdis)
            {
                
                try
                {
                    IBusinessTransaction t = CN_FabricaTransaccionNegocios.Obtener(s, cdi.Cd_DescCorta);

                    /**/
                    IBusinessTransaction ibt = t;
                    CN_CatCalendario cnCatCalendario = new CN_CatCalendario();
                    
                    // Validacion de contexto
                    if (ibt.DataContext != null)
                    {
                        var calendarioActual = cnCatCalendario.CalendarioActual(s, ibt);
                        int periodoActual = 0;
                        if (calendarioActual != null)
                        {
                            if (anyo == calendarioActual.Cal_Año && mes == calendarioActual.Cal_Mes)
                            {
                                periodoActual = 1;
                            }
                        }
                        else
                        {
                            periodoActual = 0;
                        }

                        var calendarioFechaFin = cnCatCalendario.PorAnyoMes(s, anyo, mes, ibt);

                        DateTime? fechaFin = DateTime.Now;
                        DateTime? fechaPeriodoInicial = DateTime.Now;

                        if (calendarioFechaFin != null)
                        {
                            fechaFin = calendarioFechaFin.Cal_FechaFin;
                            fechaPeriodoInicial = calendarioFechaFin.Cal_FechaIni;
                            if (periodoActual == 1 && DateTime.Now < calendarioFechaFin.Cal_FechaFin)
                            {
                                fechaFin = DateTime.Now;
                            }
                        }
                        else
                        {
                            fechaFin = DateTime.Now;
                        }
                        
                        int mesInicial = mes;
                        int anyoInicial = anyo;

                        if (bTrimestral)
                        {
                            //aplicable solo para Febrero y Enero
                            if (mesInicial - 2 < 1)
                            {
                                anyoInicial = anyoInicial - 1;
                                mesInicial = 12 + (mesInicial - 2);
                            }
                        }

                        var calendarioFechaInicial = cnCatCalendario.PorAnyoMes(s, anyoInicial, mesInicial, ibt);

                        if (calendarioFechaInicial != null)
                        {

                            /**/
                            var subResultado = ProcesarEntradaParaCentroDeDistribucion(s, calendarioFechaInicial.Cal_FechaIni.Value, fechaFin.Value, fechaPeriodoInicial.Value, cuotas, t);
                            var listaSubresultado = subResultado.ToList();

                            if (listaSubresultado.Count == 0)
                            {
                                EntradaCDReporteVentaCRM entradaVacia = new EntradaCDReporteVentaCRM()
                                {
                                    Id_Cd = cdi.Id_Cd,
                                    Cd_Nombre = cdi.Cd_Nombre,
                                    VI_ProyectosIngresadosNumeroProyectos = 0,
                                    VI_ProyectosIngresadosImporteProyecto = 0,
                                    VI_ProyectosPromocionNumeroProyecto = 0,
                                    VI_ProyectosPromocionImporteProyecto = 0,
                                    VI_CierreNumeroProyectos = 0,
                                    VI_CierreImporteProyectos = 0,
                                    VI_CanceladoNumProy = 0,
                                    VI_CanceladoImporteProy = 0,

                                    VE_ProyectosIngresadosNumeroProyectos = 0,
                                    VE_ProyectosIngresadosImporteProyecto = 0,
                                    VE_ProyectosPromocionNumeroProyecto = 0,
                                    VE_ProyectosPromocionImporteProyecto = 0,
                                    VE_CierreNumeroProyectos = 0,
                                    VE_CierreImporteProyectos = 0,
                                    VE_CanceladoNumProy = 0,
                                    VE_CanceladoImporteProy = 0,

                                    CD_Zona = cdi.Cd_Region,
                                    Indice = 1
                                };
                                //listaSubresultado.Add(entradaVacia);
                                listaSubresultado.Add(entradaVacia);
                            }

                            //subResultado = subResultado.ToList();
                            // lstECDR
                            foreach (var sr in listaSubresultado)
                            //foreach (var sr in listaSubresultado)
                            {
                                sr.Cd_Nombre = cdi.Cd_Nombre;
                                sr.CD_Zona = cdi.Cd_Region;
                                sr.Indice = 1;
                            }

                            resultado.AddRange(listaSubresultado);
                        }
                        
                    } else {

                        EntradaCDReporteVentaCRM entradaVacia = new EntradaCDReporteVentaCRM()
                        {
                            Id_Cd = cdi.Id_Cd,
                            Cd_Nombre = cdi.Cd_Nombre,
                            VI_ProyectosIngresadosNumeroProyectos = 0,
                            VI_ProyectosIngresadosImporteProyecto = 0,
                            VI_ProyectosPromocionNumeroProyecto = 0,
                            VI_ProyectosPromocionImporteProyecto = 0,
                            VI_CierreNumeroProyectos = 0,
                            VI_CierreImporteProyectos = 0,
                            VI_CanceladoNumProy = 0,
                            VI_CanceladoImporteProy = 0,

                            VE_ProyectosIngresadosNumeroProyectos = 0,
                            VE_ProyectosIngresadosImporteProyecto = 0,
                            VE_ProyectosPromocionNumeroProyecto = 0,
                            VE_ProyectosPromocionImporteProyecto = 0,
                            VE_CierreNumeroProyectos = 0,
                            VE_CierreImporteProyectos = 0,
                            VE_CanceladoNumProy = 0,
                            VE_CanceladoImporteProy = 0,

                            CD_Zona = cdi.Cd_Region,
                            Indice = 1
                        };
                        //listaSubresultado.Add(entradaVacia);
                        //lstECDR.Add(entradaVacia); 
                        resultado.Add(entradaVacia);

                    }
                    
                }
                catch (CapaNegocios.SIANNoEncontradoException sneEx)
                {
                    //La configuración del SIAN no fué encontrado
                    EntradaCDReporteVentaCRM entradaVacia = new EntradaCDReporteVentaCRM()
                    {
                        Id_Cd = cdi.Id_Cd,
                        Cd_Nombre = cdi.Cd_Nombre,

                        VI_ProyectosIngresadosNumeroProyectos = 0,
                        VI_ProyectosIngresadosImporteProyecto = 0,
                        VI_ProyectosPromocionNumeroProyecto = 0,
                        VI_ProyectosPromocionImporteProyecto = 0,
                        VI_CierreNumeroProyectos = 0,
                        VI_CierreImporteProyectos = 0,
                        VI_CanceladoNumProy = 0,
                        VI_CanceladoImporteProy = 0,

                        VE_ProyectosIngresadosNumeroProyectos = 0,
                        VE_ProyectosIngresadosImporteProyecto = 0,
                        VE_ProyectosPromocionNumeroProyecto = 0,
                        VE_ProyectosPromocionImporteProyecto = 0,
                        VE_CierreNumeroProyectos = 0,
                        VE_CierreImporteProyectos = 0,
                        VE_CanceladoNumProy = 0,
                        VE_CanceladoImporteProy = 0,

                        Indice = 1,
                        CD_Zona = cdi.Cd_Region
                    };
                    resultado.Add(entradaVacia);
                }

            }

            var agrupacionPorZona = from sr in resultado
                                    group sr by sr.CD_Zona into grp
                                    select new EntradaCDReporteVentaCRM()
                                    {
                                        Cd_Nombre = grp.Key,

                                        VI_ProyectosIngresadosNumeroProyectos = grp.Sum(e => e.VI_ProyectosIngresadosNumeroProyectos),
                                        VI_ProyectosIngresadosImporteProyecto = grp.Sum(e => e.VI_ProyectosIngresadosImporteProyecto),
                                        VI_ProyectosPromocionNumeroProyecto = grp.Sum(e => e.VI_ProyectosPromocionNumeroProyecto),
                                        VI_ProyectosPromocionImporteProyecto = grp.Sum(e => e.VI_ProyectosPromocionImporteProyecto),
                                        VI_CierreNumeroProyectos = grp.Sum(e => e.VI_CierreNumeroProyectos),
                                        VI_CierreImporteProyectos = grp.Sum(e => e.VI_CierreImporteProyectos),
                                        VI_CanceladoNumProy = grp.Sum(e => e.VI_CanceladoNumProy),
                                        VI_CanceladoImporteProy = grp.Sum(e => e.VI_CanceladoImporteProy),

                                        VE_ProyectosIngresadosNumeroProyectos = grp.Sum(e => e.VE_ProyectosIngresadosNumeroProyectos),
                                        VE_ProyectosIngresadosImporteProyecto = grp.Sum(e => e.VE_ProyectosIngresadosImporteProyecto),
                                        VE_ProyectosPromocionNumeroProyecto = grp.Sum(e => e.VE_ProyectosPromocionNumeroProyecto),
                                        VE_ProyectosPromocionImporteProyecto = grp.Sum(e => e.VE_ProyectosPromocionImporteProyecto),
                                        VE_CierreNumeroProyectos = grp.Sum(e => e.VE_CierreNumeroProyectos),
                                        VE_CierreImporteProyectos = grp.Sum(e => e.VE_CierreImporteProyectos),
                                        VE_CanceladoNumProy = grp.Sum(e => e.VE_CanceladoNumProy),
                                        VE_CanceladoImporteProy = grp.Sum(e => e.VE_CanceladoImporteProy),

                                        CD_Zona = grp.Key,
                                        Indice = 2,
                                        EsZona = true
                                    };
            resultado.AddRange(agrupacionPorZona);

            resultado = resultado.OrderBy(e => e.CD_Zona).ThenBy(e => e.Indice).ToList();
            return resultado;
        }

        public IEnumerable<EntradaCDReporteVentaCRM> GenerarVistaRIK(Sesion s, int anyo, int mes, int idCd, bool bTrimestral, IBusinessTransaction ibtSianCentral)
        {
            var transaccionesNegocioSIANCentral = CN_FabricaTransaccionNegocios.ObtenerParaSIANCentral(s);
            //Se requiere la información de los representantes con sus proyectos.
            //Todos los proyectos de un solo repositorio pertenecen a un mismo centro de distribución

            CD_CatCuotasCRM cdCatCuotasCRM = new CD_CatCuotasCRM();
            var cuotas = cdCatCuotasCRM.ConsultarPorAnyoMes(anyo, mes, ibtSianCentral.DataContext);

            CapaDatos.SIANCentral.CD_CatCDI cdCatCDI = new CapaDatos.SIANCentral.CD_CatCDI();
            var cdis = cdCatCDI.Consultar(ibtSianCentral.DataContext);
            cdis = from cd in cdis
                   where cd.Id_Cd == idCd
                   select cd;

            if (cdis.Count() > 0)
            {
                var cdi = cdis.First();
                List<EntradaCDReporteVentaCRM> resultado = new List<EntradaCDReporteVentaCRM>();
                try
                {
                    Sesion sesionContextoDeDatos = new Sesion();
                    sesionContextoDeDatos.Id_Emp = s.Id_Emp;
                    sesionContextoDeDatos.Id_Cd = idCd;

                    IBusinessTransaction t = CN_FabricaTransaccionNegocios.Obtener(s, cdi.Cd_DescCorta);

                    IBusinessTransaction ibt = t;

                    /**/
                    CN_CatCalendario cnCatCalendario = new CN_CatCalendario();
                    var calendarioActual = cnCatCalendario.CalendarioActual(s, ibt);

                    int periodoActual = 0;
                    if (anyo == calendarioActual.Cal_Año && mes == calendarioActual.Cal_Mes)
                    {
                        periodoActual = 1;
                    }

                    var calendarioFechaFin = cnCatCalendario.PorAnyoMes(s, anyo, mes, ibt);

                    DateTime? fechaFin = calendarioFechaFin.Cal_FechaFin;
                    DateTime? fechaPeriodoInicial = calendarioFechaFin.Cal_FechaIni;
                    if (periodoActual == 1 && DateTime.Now < calendarioFechaFin.Cal_FechaFin)
                    {
                        fechaFin = DateTime.Now;
                    }

                    int mesInicial = mes;
                    int anyoInicial = anyo;

                    if (bTrimestral)
                    {
                        //aplicable solo para Febrero y Enero
                        if (mesInicial - 2 < 1)
                        {
                            anyoInicial = anyoInicial - 1;
                            mesInicial = 12 + (mesInicial - 2);
                        }
                    }

                    var calendarioFechaInicial = cnCatCalendario.PorAnyoMes(s, anyoInicial, mesInicial, ibt);
                    /**/

                    var subResultado = ProcesarEntradaParaCentroDeDistribucionPorRIK(s, sesionContextoDeDatos, calendarioFechaInicial.Cal_FechaIni.Value, fechaFin.Value, fechaPeriodoInicial.Value, cuotas, t).ToList();
                    subResultado = subResultado.ToList();
                    foreach (var sr in subResultado)
                    {
                        sr.Cd_Nombre = cdi.Cd_Nombre;
                    }

                    //Crear la entrada para los totales
                    EntradaCDReporteVentaCRM totales = new EntradaCDReporteVentaCRM()
                    {
                        Id_Cd = idCd,
                        Cd_Nombre = "Total",
                        EsZona = true,
                        CD_Zona = "",
                        Id_Rik = 0,
                        Rik_Nombre = "Total",
                        ProyectosIngresadosNumeroProyectos = subResultado.Sum(e => e.ProyectosIngresadosNumeroProyectos),
                        ProyectosIngresadosImporteProyecto = subResultado.Sum(e => e.ProyectosIngresadosImporteProyecto),
                        ProyectosPromocionMontoProyecto = subResultado.Sum(e => e.ProyectosPromocionMontoProyecto),
                        ProyectosPromocionCumplimiento = subResultado.Sum(e => e.ProyectosPromocionCumplimiento),
                        CierreMonto = subResultado.Sum(e => e.CierreMonto),
                        CierreCumplimiento = subResultado.Sum(e => e.CierreCumplimiento),
                        CanceladoNumProy = subResultado.Sum(e => e.CanceladoNumProy),
                        CanceladoImporteProy = subResultado.Sum(e => e.CanceladoImporteProy),
                        EntradasFrecuencia = subResultado.Sum(e => e.EntradasFrecuencia),

                        VI_ProyectosIngresadosNumeroProyectos = subResultado.Sum(e => e.VI_ProyectosIngresadosNumeroProyectos),
                        VI_ProyectosIngresadosImporteProyecto = subResultado.Sum(e => e.VI_ProyectosIngresadosImporteProyecto),
                        VI_ProyectosPromocionNumeroProyecto = subResultado.Sum(e => e.VI_ProyectosPromocionNumeroProyecto),
                        VI_ProyectosPromocionImporteProyecto = subResultado.Sum(e => e.VI_ProyectosPromocionImporteProyecto),
                        VI_CierreNumeroProyectos = subResultado.Sum(e => e.VI_CierreNumeroProyectos),
                        VI_CierreImporteProyectos = subResultado.Sum(e => e.VI_CierreImporteProyectos),
                        VI_CanceladoNumProy = subResultado.Sum(e => e.VI_CanceladoNumProy),
                        VI_CanceladoImporteProy = subResultado.Sum(e => e.VI_CanceladoImporteProy),

                        VE_ProyectosIngresadosNumeroProyectos = subResultado.Sum(e => e.VE_ProyectosIngresadosNumeroProyectos),
                        VE_ProyectosIngresadosImporteProyecto = subResultado.Sum(e => e.VE_ProyectosIngresadosImporteProyecto),
                        VE_ProyectosPromocionNumeroProyecto = subResultado.Sum(e => e.VE_ProyectosPromocionNumeroProyecto),
                        VE_ProyectosPromocionImporteProyecto = subResultado.Sum(e => e.VE_ProyectosPromocionImporteProyecto),
                        VE_CierreNumeroProyectos = subResultado.Sum(e => e.VE_CierreNumeroProyectos),
                        VE_CierreImporteProyectos = subResultado.Sum(e => e.VE_CierreImporteProyectos),
                        VE_CanceladoNumProy = subResultado.Sum(e => e.VE_CanceladoNumProy),
                        VE_CanceladoImporteProy = subResultado.Sum(e => e.VE_CanceladoImporteProy),
                    };
                    subResultado.Add(totales);
                    resultado.AddRange(subResultado);
                }
                catch (CapaNegocios.SIANNoEncontradoException sneEx)
                {
                    EntradaCDReporteVentaCRM entradaVacia = new EntradaCDReporteVentaCRM()
                    {
                        Id_Cd = cdi.Id_Cd,
                        Cd_Nombre = cdi.Cd_Nombre,
                        ProyectosIngresadosImporteProyecto = 0,
                        ProyectosIngresadosNumeroProyectos = 0,
                        ProyectosPromocionCumplimiento = 0,
                        ProyectosPromocionMontoProyecto = 0,
                        CierreCumplimiento = 0,
                        CierreMonto = 0,
                        CanceladoImporteProy = 0,
                        CanceladoNumProy = 0,
                        EntradasFrecuencia = 0,

                        VI_ProyectosIngresadosNumeroProyectos = 0,
                        VI_ProyectosIngresadosImporteProyecto = 0,
                        VI_ProyectosPromocionNumeroProyecto = 0,
                        VI_ProyectosPromocionImporteProyecto = 0,
                        VI_CierreNumeroProyectos = 0,
                        VI_CierreImporteProyectos = 0,
                        VI_CanceladoNumProy = 0,
                        VI_CanceladoImporteProy = 0,

                        VE_ProyectosIngresadosNumeroProyectos = 0,
                        VE_ProyectosIngresadosImporteProyecto = 0,
                        VE_ProyectosPromocionNumeroProyecto = 0,
                        VE_ProyectosPromocionImporteProyecto = 0,
                        VE_CierreNumeroProyectos = 0,
                        VE_CierreImporteProyectos = 0,
                        VE_CanceladoNumProy = 0,
                        VE_CanceladoImporteProy = 0
                    };
                    entradaVacia.Id_Cd = cdi.Id_Cd;
                    entradaVacia.Cd_Nombre = cdi.Cd_Nombre;
                    resultado.Add(entradaVacia);
                }
                return resultado;
            }
            throw new CentroDistribucionNoEncontrado(idCd);
        }

        public IEnumerable<EntradaRIKReporteDinamo> GenerarVistaProyectos(Sesion s, int anyo, int mes, int idCd, int idRik, bool bTrimestral, IBusinessTransaction ibtSianCentral)
        {
            //La transacción asume la entidad de la fuente de datos para el centro de distribución
            CapaDatos.SIANCentral.CD_CatCDI cdCatCDI = new CapaDatos.SIANCentral.CD_CatCDI();
            var cdis = cdCatCDI.Consultar(ibtSianCentral.DataContext);
            cdis = from cd in cdis
                   where cd.Id_Cd == idCd
                   select cd;

            if (cdis.Count() > 0)
            {
                var cdi = cdis.First();

                Sesion sesionContextoDeDatos = new Sesion(s);
                sesionContextoDeDatos.Id_Emp = s.Id_Emp;
                sesionContextoDeDatos.Id_Cd = idCd;
                sesionContextoDeDatos.Id_Rik = idRik;

                try
                {
                    //Puede arrojar CapaNegocios.SIANNoEncontradoException
                    IBusinessTransaction t = CN_FabricaTransaccionNegocios.Obtener(sesionContextoDeDatos, cdi.Cd_DescCorta);

                    IBusinessTransaction ibtCdi = t;

                    /**/
                    CN_CatCalendario cnCatCalendario = new CN_CatCalendario();
                    var calendarioActual = cnCatCalendario.CalendarioActual(s, ibtCdi);

                    int periodoActual = 0;
                    if (anyo == calendarioActual.Cal_Año && mes == calendarioActual.Cal_Mes)
                    {
                        periodoActual = 1;
                    }

                    var calendarioFechaFin = cnCatCalendario.PorAnyoMes(s, anyo, mes, ibtCdi);

                    DateTime? fechaFin = calendarioFechaFin.Cal_FechaFin;
                    DateTime? fechaPeriodoInicial = calendarioFechaFin.Cal_FechaIni;
                    if (periodoActual == 1 && DateTime.Now < calendarioFechaFin.Cal_FechaFin)
                    {
                        fechaFin = DateTime.Now;
                    }

                    int mesInicial = mes;
                    int anyoInicial = anyo;

                    if (bTrimestral)
                    {
                        //aplicable solo para Febrero y Enero
                        if (mesInicial - 2 < 1)
                        {
                            anyoInicial = anyoInicial - 1;
                            mesInicial = 12 + (mesInicial - 2);
                        }
                    }

                    var calendarioFechaInicial = cnCatCalendario.PorAnyoMes(s, anyoInicial, mesInicial, ibtCdi);
                    /**/

                    CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                    //Seleccionar los proyectos creados(¿o cerrados o en algún estado en específico?)
                    var proyectosDeRik = cnCrmOportunidad.ObtenerPorRik(s, ibtCdi).Where(
                        op => op.FechaCreacion > calendarioFechaInicial.Cal_FechaIni && op.FechaCreacion < calendarioFechaFin.Cal_FechaFin).Select(op =>
                        {
                            return new EntradaRIKReporteDinamo()
                            {
                                IdProyecto = op.Id_Op,
                                NombreCliente = op.CatCliente.Cte_NomComercial,
                                Area = op.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion,
                                Solucion = op.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion,
                                Aplicacion = op.CrmOportunidadesAplicacion.CatAplicacion.Apl_Descripcion,
                                Productos = string.Format("<ul>{0}</ul>", string.Join("", op.CrmOportunidadesProducto2.Select(p => string.Format("<li>{0}</li>", p.CatProducto.Prd_Descripcion)))),
                                VTeorico = op.CrmOportunidadesAplicacion.CrmOpAp_VPO,
                                Analisis = op.Analisis,
                                Presentacion = op.Presentacion,
                                Negociacion = op.Negociacion,
                                Cierre = op.Cierre,
                                MontoProyecto = op.MontoProyecto,
                                FechaModificacion = op.FechaModificacion,
                                Estatus = op.Estatus != null ? (op.Estatus.Value == 1 ? "Análisis" : (op.Estatus.Value == 2 ? "Presentación" : (op.Estatus.Value == 3 ? "Negociación" : (op.Estatus.Value == 4 ? "Cierre" : "Cancelado")))) : "",
                                Causa = "",
                                Comentarios = op.Comentarios
                            };
                        });

                    return proyectosDeRik;
                }
                catch (CapaNegocios.SIANNoEncontradoException sneEx)
                {
                    return new List<EntradaRIKReporteDinamo>();
                }
            }
            throw new CentroDistribucionNoEncontrado(idCd);
        }

        protected IEnumerable<EntradaCDReporteVentaCRM> ProcesarEntradaParaCentroDeDistribucion(Sesion s, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            var sourceEntradas = ObtenerControlEntrada(s, ibt);
            var proyectos = ProcesarFuenteDeTodos(s, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion != null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value, TipoVenta = p.Crm_TipoVenta } into grp
                            select new EntradaCDReporteVentaCRM()
                            {
                                Id_Cd = grp.Key.Id_Cd,
                                Id_Rik = grp.Key.Id_Rik,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                VI_ProyectosPromocionNumeroProyecto=grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)),
                                ProyectosPromocionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                VI_CierreNumeroProyectos=grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)),
                                CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoCierre.Value)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik),
                                TipoVenta=grp.Key.TipoVenta
                            };
            respuesta = from r in respuesta
                        group r by new { r.Id_Cd, r.TipoVenta } into grp
                        select new EntradaCDReporteVentaCRM()
                        {
                            Id_Cd = grp.Key.Id_Cd,
                            Id_Rik = 0,
                            VI_ProyectosIngresadosNumeroProyectos = grp.Where(e => e.TipoVenta == 1).Sum(e => e.ProyectosIngresadosNumeroProyectos),
                            VI_ProyectosIngresadosImporteProyecto = grp.Where(e => e.TipoVenta == 1).Sum(e => e.ProyectosIngresadosImporteProyecto),
                            VI_ProyectosPromocionNumeroProyecto = grp.Where(e => e.TipoVenta == 1).Sum(e => e.VI_ProyectosPromocionNumeroProyecto),
                            VI_ProyectosPromocionImporteProyecto = grp.Where(e => e.TipoVenta == 1).Sum(e => e.VI_ProyectosPromocionImporteProyecto),
                            VI_CierreNumeroProyectos = grp.Where(e => e.TipoVenta == 1).Sum(e => e.VI_CierreNumeroProyectos),
                            VI_CierreImporteProyectos = grp.Where(e => e.TipoVenta == 1).Sum(e => e.CierreMonto),
                            VI_CanceladoNumProy = grp.Where(e => e.TipoVenta == 1).Sum(e => e.CanceladoNumProy),
                            VI_CanceladoImporteProy = grp.Where(e => e.TipoVenta == 1).Sum(e => e.CanceladoImporteProy),

                            VE_ProyectosIngresadosNumeroProyectos = grp.Where(e => e.TipoVenta == 2).Sum(e => e.ProyectosIngresadosNumeroProyectos),
                            VE_ProyectosIngresadosImporteProyecto = grp.Where(e => e.TipoVenta == 2).Sum(e => e.ProyectosIngresadosImporteProyecto),
                            VE_ProyectosPromocionNumeroProyecto = grp.Where(e => e.TipoVenta == 2).Sum(e => e.VI_ProyectosPromocionNumeroProyecto),
                            VE_ProyectosPromocionImporteProyecto = grp.Where(e => e.TipoVenta == 2).Sum(e => e.VI_ProyectosPromocionImporteProyecto),
                            VE_CierreNumeroProyectos = grp.Where(e => e.TipoVenta == 2).Sum(e => e.VI_CierreNumeroProyectos),
                            VE_CierreImporteProyectos = grp.Where(e => e.TipoVenta == 2).Sum(e => e.CierreMonto),
                            VE_CanceladoNumProy = grp.Where(e => e.TipoVenta == 2).Sum(e => e.CanceladoNumProy),
                            VE_CanceladoImporteProy = grp.Where(e => e.TipoVenta == 2).Sum(e => e.CanceladoImporteProy)
                        };
            return respuesta;
        }

        protected IEnumerable<EntradaCDReporteVentaCRM> ProcesarEntradaParaCentroDeDistribucionPorRIK(Sesion sesionOperador, Sesion sesionDeContextoDeDatos, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            //Obtener el control de entrada para el centro de distribución
            var sourceEntradas = ObtenerControlEntrada(sesionDeContextoDeDatos, ibt);
            var proyectos = ProcesarFuenteDeTodos(sesionDeContextoDeDatos, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion != null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value, Rik_Nombre = p.CatTerritorio.InfoRIKComoUsuario.U_Nombre } into grp
                            select new EntradaCDReporteVentaCRM()
                            {
                                Id_Cd = grp.Key.Id_Cd,
                                Id_Rik = grp.Key.Id_Rik,
                                Rik_Nombre = grp.Key.Rik_Nombre,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                //VI_ProyectosPromocionNumeroProyecto = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)),
                                ProyectosPromocionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                //VI_CierreNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)),
                                CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik),

                                VI_ProyectosIngresadosNumeroProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                VI_ProyectosIngresadosImporteProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                VI_ProyectosPromocionNumeroProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)),
                                VI_ProyectosPromocionImporteProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                VI_CierreNumeroProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)),
                                VI_CierreImporteProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                VI_CanceladoNumProy = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                VI_CanceladoImporteProy = grp.Where(e => e.Proyecto.Crm_TipoVenta == 1).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),

                                VE_ProyectosIngresadosNumeroProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                VE_ProyectosIngresadosImporteProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                VE_ProyectosPromocionNumeroProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)),
                                VE_ProyectosPromocionImporteProyecto = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                VE_CierreNumeroProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)),
                                VE_CierreImporteProyectos = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                VE_CanceladoNumProy = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                VE_CanceladoImporteProy = grp.Where(e => e.Proyecto.Crm_TipoVenta == 2).Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                            };
            return respuesta;
        }

        /// <summary>
        /// Devuelve el conjunto de entradas de control de acceso de los usuarios
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable[CRMControlEntrada]</returns>
        protected IEnumerable<CRMControlEntrada> ObtenerControlEntrada(Sesion s, IBusinessTransaction ibt)
        {
            CD_CrmControlEntrada cdCrmControlEntrada = new CD_CrmControlEntrada();
            return cdCrmControlEntrada.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
        }

        protected IEnumerable<CrmOportunidade> ProcesarFuenteDeTodos(Sesion s, DateTime fechaInicial, DateTime fechaFinal, IBusinessTransaction ibt)
        {
            CD_CrmOportunidad cdCrmOportunidad = new CD_CrmOportunidad();
            var proyectos = cdCrmOportunidad.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
            var resultado = from p in proyectos
                            where /*(p.Id_Usu == s.Id_Rik || s.Id_Rik < 0)
                            &&*/ p.FechaModificacion > fechaInicial
                            && p.FechaModificacion < fechaFinal
                            && p.FechaCreacion != null
                            select p;
            return resultado;
        }
    }

    public class EntradaCDReporteVentaCRM
    {
        public EntradaCDReporteVentaCRM()
        {
            EsZona = false;
        }

        public int Id_Cd
        {
            get;
            set;
        }

        public string Cd_Nombre
        {
            get;
            set;
        }

        public string CD_Zona
        {
            get;
            set;
        }

        public int Id_Rik
        {
            get;
            set;
        }

        public string Rik_Nombre
        {
            get;
            set;
        }

        public decimal? ProyectosIngresadosNumeroProyectos
        {
            get;
            set;
        }

        public decimal? ProyectosIngresadosImporteProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosPromocionMontoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosPromocionCumplimiento
        {
            get;
            set;
        }

        public decimal? CierreMonto
        {
            get;
            set;
        }

        public decimal? CierreCumplimiento
        {
            get;
            set;
        }

        public int CanceladoNumProy
        {
            get;
            set;
        }

        public decimal? CanceladoImporteProy
        {
            get;
            set;
        }

        public float EntradasFrecuencia
        {
            get;
            set;
        }



        public decimal? VE_ProyectosIngresadosNumeroProyectos
        {
            get;
            set;
        }
        public decimal? VE_ProyectosIngresadosImporteProyecto
        {
            get;
            set;
        }
        public decimal? VE_ProyectosPromocionNumeroProyecto
        {
            get;
            set;
        }
        public decimal? VE_ProyectosPromocionImporteProyecto
        {
            get;
            set;
        }
        public decimal? VE_CierreNumeroProyectos
        {
            get;
            set;
        }
        public decimal? VE_CierreImporteProyectos
        {
            get;
            set;
        }
        public decimal? VE_CanceladoNumProy
        {
            get;
            set;
        }
        public decimal? VE_CanceladoImporteProy
        {
            get;
            set;
        }
        public decimal? VI_ProyectosIngresadosNumeroProyectos
        {
            get;
            set;
        }

        public decimal? VI_ProyectosIngresadosImporteProyecto
        {
            get;
            set;
        }
        public decimal? VI_ProyectosPromocionNumeroProyecto
        {
            get;
            set;
        }
        public decimal? VI_ProyectosPromocionImporteProyecto
        {
            get;
            set;
        }
        public decimal? VI_CierreNumeroProyectos
        {
            get;
            set;
        }
        public decimal? VI_CierreImporteProyectos
        {
            get;
            set;
        }
        public decimal? VI_CanceladoNumProy
        {
            get;
            set;
        }
        public decimal? VI_CanceladoImporteProy
        {
            get;
            set;
        }

        public string StyleCdNombre
        {
            get
            {
                if (EsZona)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    sb.Append("text-align: center;");
                    return sb.ToString();
                }
                return "";
            }
        }
        public string VE_StyleProyectosIngresadosNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleProyectosIngresadosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleProyectosPromocionNumeroProyecto
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleProyectosPromocionImporteProyecto
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleCierreNumeroProyectos
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleCierreImporteProyectos
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleCanceladoNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VE_StyleCanceladoImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleProyectosIngresadosNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleProyectosIngresadosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleProyectosPromocionNumeroProyecto
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleProyectosPromocionImporteProyecto
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleCierreNumeroProyectos
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleCierreImporteProyectos
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleCanceladoNumProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }
        public string VI_StyleCanceladoImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: center;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public int? TipoVenta
        {
            get;
            set;
        }

        internal int Indice
        {
            get;
            set;
        }

        public bool EsZona
        {
            get;
            set;
        }
    }
}
