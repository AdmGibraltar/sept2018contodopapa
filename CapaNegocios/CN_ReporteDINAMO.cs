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
    public class CN_ReporteDINAMO
    {
        public IEnumerable<EntradaCDReporteDinamo> GenerarVistaGeneral(Sesion s, int cdTipo, int anyo, int mes, bool bTrimestral, /*IBusinessTransaction ibt,*/ IBusinessTransaction ibtSianCentral)
        {
            var transaccionesNegocioSIANCentral = CN_FabricaTransaccionNegocios.ObtenerParaSIANCentral(s);
            //Se requiere la información de los representantes con sus proyectos.
            //Todos los proyectos de un solo repositorio pertenecen a un mismo centro de distribución
            
            CD_CatCuotasCRM cdCatCuotasCRM = new CD_CatCuotasCRM();
            var cuotas = cdCatCuotasCRM.ConsultarPorAnyoMes(anyo, mes, ibtSianCentral.DataContext);

            CapaDatos.SIANCentral.CD_CatCDI cdCatCDI = new CapaDatos.SIANCentral.CD_CatCDI();
            var cdis = cdCatCDI.Consultar(ibtSianCentral.DataContext).Where(cdi => cdi.Id_Cd != 710 && cdi.Cd_Tipo == cdTipo);

            List<EntradaCDReporteDinamo> resultado = new List<EntradaCDReporteDinamo>();

            foreach (var cdi in cdis)
            {
                try
                {
                    IBusinessTransaction t = CN_FabricaTransaccionNegocios.Obtener(s, cdi.Cd_DescCorta);

                    /**/
                    IBusinessTransaction ibt = t;
                    CN_CatCalendario cnCatCalendario = new CN_CatCalendario();

                    int periodoActual = 0;
                    int mesInicial = mes;
                    int anyoInicial = anyo;

                    try
                    {
                        // Validacion de contexto
                        if (ibt.DataContext  != null)
                        {
                            CatCalendario calendarioActual = cnCatCalendario.CalendarioActual(s, ibt);

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

                            DateTime? fechaPeriodoInicial = DateTime.Now;
                            DateTime? fechaFin = DateTime.Now;

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
                                fechaPeriodoInicial = DateTime.Now;
                                fechaFin = DateTime.Now;
                            }

                            if (bTrimestral)
                            {
                                //aplicable solo para Febrero y Enero
                                if (mesInicial - 2 < 1)
                                {
                                    anyoInicial = anyoInicial - 1;
                                    mesInicial = 12 + (mesInicial - 2);
                                }
                            }


                            CatCalendario calendarioFechaInicial = cnCatCalendario.PorAnyoMes(s, anyoInicial, mesInicial, ibt);
                            var subResultado = ProcesarEntradaParaCentroDeDistribucionConMontosCorregidos(s, calendarioFechaInicial.Cal_FechaIni.Value, fechaFin.Value, fechaPeriodoInicial.Value, cuotas, t);
                            var listaSubresultado = subResultado.ToList();

                            /**/
                        
                            if (listaSubresultado.Count == 0)
                            {
                                EntradaCDReporteDinamo entradaVacia = new EntradaCDReporteDinamo()
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
                                    CD_Zona = cdi.Cd_Region,
                                    Indice = 1
                                };
                                listaSubresultado.Add(entradaVacia);
                            }

                            //subResultado = subResultado.ToList();
                            foreach (var sr in listaSubresultado)
                            {
                                sr.Cd_Nombre = cdi.Cd_Nombre;
                                sr.CD_Zona = cdi.Cd_Region;
                                sr.Indice = 1;
                            }

                            resultado.AddRange(listaSubresultado);

                        } //(ibt != null)
                        else
                        {
                            //La configuración del SIAN no fué encontrado
                            EntradaCDReporteDinamo entradaVacia = new EntradaCDReporteDinamo()
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
                                Indice = 1,
                                CD_Zona = cdi.Cd_Region
                            };
                            resultado.Add(entradaVacia);
                        }

                    } catch {

                        //La configuración del SIAN no fué encontrado
                        EntradaCDReporteDinamo entradaVacia = new EntradaCDReporteDinamo()
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
                            Indice = 1,
                            CD_Zona = cdi.Cd_Region
                        };
                        resultado.Add(entradaVacia);

                    }


                } catch (CapaNegocios.SIANNoEncontradoException sneEx) {
                    //La configuración del SIAN no fué encontrado
                    EntradaCDReporteDinamo entradaVacia = new EntradaCDReporteDinamo()
                    {
                        Id_Cd = cdi.Id_Cd,
                        Cd_Nombre = cdi.Cd_Nombre,
                        ProyectosIngresadosImporteProyecto=0,
                        ProyectosIngresadosNumeroProyectos=0,
                        ProyectosPromocionCumplimiento=0,
                        ProyectosPromocionMontoProyecto=0,
                        CierreCumplimiento=0,
                        CierreMonto=0,
                        CanceladoImporteProy=0,
                        CanceladoNumProy=0,
                        EntradasFrecuencia=0,
                        Indice=1,
                        CD_Zona=cdi.Cd_Region
                    };
                    resultado.Add(entradaVacia);
                }
                
            }

            var agrupacionPorZona = from sr in resultado
                                    group sr by sr.CD_Zona into grp
                                    select new EntradaCDReporteDinamo()
                                    {
                                        Cd_Nombre = grp.Key,
                                        ProyectosIngresadosNumeroProyectos = grp.Sum(e => e.ProyectosIngresadosNumeroProyectos),
                                        ProyectosIngresadosImporteProyecto = grp.Sum(e => e.ProyectosIngresadosImporteProyecto),
                                        ProyectosPromocionMontoProyecto = grp.Sum(e => e.ProyectosPromocionMontoProyecto),
                                        ProyectosPromocionCumplimiento = grp.Sum(e => e.ProyectosPromocionCumplimiento),
                                        CanceladoNumProy = grp.Sum(e => e.CanceladoNumProy),
                                        CanceladoImporteProy = grp.Sum(e => e.CanceladoImporteProy),
                                        CierreMonto = grp.Sum(e => e.CierreMonto),
                                        CierreCumplimiento = grp.Sum(e => e.CierreCumplimiento),
                                        EntradasFrecuencia = grp.Sum(e => e.EntradasFrecuencia),
                                        CD_Zona=grp.Key,
                                        Indice=2,
                                        EsZona=true
                                    };
            resultado.AddRange(agrupacionPorZona);

            //resultado = (from r in resultado
            //             orderby new { r.CD_Zona, r.Indice } ascending
            //             select r).ToList();

            resultado = resultado.OrderBy(e => e.CD_Zona).ThenBy(e => e.Indice).ToList();
            return resultado;
        }

        public IEnumerable<EntradaCDReporteDinamo> GenerarVistaRIK(Sesion s, int anyo, int mes, int idCd, bool bTrimestral, IBusinessTransaction ibtSianCentral)
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
                List<EntradaCDReporteDinamo> resultado = new List<EntradaCDReporteDinamo>();
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

                    if (calendarioActual != null)
                    {
                        if (anyo == calendarioActual.Cal_Año && mes == calendarioActual.Cal_Mes)
                        {
                            periodoActual = 1;
                        }
                    }
                    else
                    {
                        periodoActual=1;
                    }
                    
                    var calendarioFechaFin = cnCatCalendario.PorAnyoMes(s, anyo, mes, ibt);

                    DateTime? fechaPeriodoInicial = DateTime.Now;
                    DateTime? fechaFin = DateTime.Now;

                    if (calendarioFechaFin != null)
                    {
                        fechaFin = calendarioFechaFin.Cal_FechaFin;
                        fechaPeriodoInicial = calendarioFechaFin.Cal_FechaIni;
                    }
                    else
                    {
                        fechaFin = DateTime.Now;
                        fechaPeriodoInicial = DateTime.Now;
                    }

                    if (calendarioFechaFin != null)
                    {
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
                    /**/

                    try 
                    {
                        var subResultado = ProcesarEntradaParaCentroDeDistribucionPorRIK/*ConMontosCorregidos*/(s, sesionContextoDeDatos, calendarioFechaInicial.Cal_FechaIni.Value, fechaFin.Value, fechaPeriodoInicial.Value, cuotas, t).ToList();
                        subResultado = subResultado.ToList();
                        foreach (var sr in subResultado)
                        {
                            sr.Cd_Nombre = cdi.Cd_Nombre;
                        }

                        //Crear la entrada para los totales
                        EntradaCDReporteDinamo totales = new EntradaCDReporteDinamo()
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
                            EntradasFrecuencia = subResultado.Sum(e => e.EntradasFrecuencia)
                        };
                        subResultado.Add(totales);
                        resultado.AddRange(subResultado);

                    } catch {
                        
                    }


                }
                catch (CapaNegocios.SIANNoEncontradoException sneEx)
                {
                    EntradaCDReporteDinamo entradaVacia = new EntradaCDReporteDinamo()
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
                        EntradasFrecuencia = 0
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

        /// <summary>
        /// Versión que calcula correctamente los montos de cada proyecto
        /// </summary>
        /// <param name="s"></param>
        /// <param name="anyo"></param>
        /// <param name="mes"></param>
        /// <param name="idCd"></param>
        /// <param name="idRik"></param>
        /// <param name="bTrimestral"></param>
        /// <param name="ibtSianCentral"></param>
        /// <returns></returns>
        public IEnumerable<EntradaRIKReporteDinamo> GenerarVistaProyectosMontoCorregido(Sesion s, int anyo, int mes, int idCd, int idRik, bool bTrimestral, IBusinessTransaction ibtSianCentral)
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
                            return new
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
                                //MontoProyecto = op.MontoProyecto,
                                FechaModificacion = op.FechaModificacion,
                                Estatus = op.Estatus != null ? (op.Estatus.Value == 1 ? "Análisis" : (op.Estatus.Value == 2 ? "Presentación" : (op.Estatus.Value == 3 ? "Negociación" : (op.Estatus.Value == 4 ? "Cierre" : "Cancelado")))) : "",
                                Causa = "",
                                Comentarios = op.Comentarios,
                                Proyecto=op
                            };
                        });

                    List<EntradaRIKReporteDinamo> resultado = new List<EntradaRIKReporteDinamo>();
                    foreach (var op in proyectosDeRik)
                    {
                        EntradaRIKReporteDinamo entrada = new EntradaRIKReporteDinamo()
                        {
                            IdProyecto = op.Proyecto.Id_Op,
                            NombreCliente = op.Proyecto.CatCliente.Cte_NomComercial,
                            Area = op.Proyecto.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.CatArea.Area_Descripcion,
                            Solucion = op.Proyecto.CrmOportunidadesAplicacion.CatAplicacion.CatSolucion.Sol_Descripcion,
                            Aplicacion = op.Proyecto.CrmOportunidadesAplicacion.CatAplicacion.Apl_Descripcion,
                            Productos = string.Format("<ul>{0}</ul>", string.Join("", op.Proyecto.CrmOportunidadesProducto2.Select(p => string.Format("<li>{0}</li>", p.CatProducto.Prd_Descripcion)))),
                            VTeorico = op.Proyecto.CrmOportunidadesAplicacion.CrmOpAp_VPO,
                            Analisis = op.Analisis,
                            Presentacion = op.Presentacion,
                            Negociacion = op.Negociacion,
                            Cierre = op.Cierre,
                            //MontoProyecto = op.MontoProyecto,
                            FechaModificacion = op.FechaModificacion,
                            Estatus = op.Estatus,
                            Causa = "",
                            Comentarios = op.Comentarios,
                        };

                        resultado.Add(entrada);

                        switch (op.Proyecto.Estatus)
                        {
                            case 1:
                                entrada.MontoProyecto = op.Proyecto.CrmOportunidadesAplicacion.CrmOpAp_VPO;
                                break;
                            case 2:
                                entrada.MontoProyecto = op.Proyecto.CrmOportunidadesProducto2.Sum(p => (p.COP_Cantidad == null ? 0.0M : p.COP_Cantidad.Value) * Convert.ToDecimal((p.ProductoActual2 == null ? 0.0D : (p.ProductoActual2.Prd_Pesos == null ? 0.0D : p.ProductoActual2.Prd_Pesos))));
                                break;
                            case 3:
                                entrada.MontoProyecto = op.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapValProyectoDets.Sum(p => p.Vap_Cantidad * Convert.ToDecimal(p.Vap_Precio));
                                break;
                            case 4:
                                entrada.MontoProyecto = (op.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys != null ? op.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)) : 0);
                                break;
                            case 5:
#warning "Se debe de definir un campo extra en la tabla CrmOportunidades para conservar el último estado de un proyecto antes de que este fuera cancelado, con el fin de determinar"
                                entrada.MontoProyecto = op.Proyecto.MontoProyecto;
                                break;
                        }
                        
                    }

                    return resultado;
                }
                catch (CapaNegocios.SIANNoEncontradoException sneEx)
                {
                    return new List<EntradaRIKReporteDinamo>();
                }
            }
            throw new CentroDistribucionNoEncontrado(idCd);
        }

        protected IEnumerable<EntradaCDReporteDinamo> ProcesarEntradaParaCentroDeDistribucion(Sesion s, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            var sourceEntradas = ObtenerControlEntrada(s, ibt);
            var proyectos = ProcesarFuenteDeTodos(s, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion!=null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } into grp
                            select new EntradaCDReporteDinamo()
                            {
                                Id_Cd=grp.Key.Id_Cd,
                                Id_Rik=grp.Key.Id_Rik,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                ProyectosPromocionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CuotaCumplimientoMontoProyecto=grp.Max(a=>a.Cuota.Cuo_MontoProy.Value),
                                ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CuotaCumplimientoMontoCierre=grp.Max(a=>a.Cuota.Cuo_MontoCierre.Value),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoCierre.Value)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik)
                            };
            respuesta = from r in respuesta
                        group r by new { r.Id_Cd} into grp
                        select new EntradaCDReporteDinamo()
                        {
                            Id_Cd = grp.Key.Id_Cd,
                            Id_Rik = 0,
                            ProyectosIngresadosNumeroProyectos = grp.Sum(e=>e.ProyectosIngresadosNumeroProyectos),
                            ProyectosIngresadosImporteProyecto = grp.Sum(e => e.ProyectosIngresadosImporteProyecto),
                            ProyectosPromocionMontoProyecto = grp.Sum(e=>e.ProyectosPromocionMontoProyecto),
                            CuotaCumplimientoMontoProyecto=grp.Sum(e=>e.CuotaCumplimientoMontoProyecto),
                            ProyectosPromocionCumplimiento = grp.Sum(e=>e.ProyectosPromocionCumplimiento),
                            CierreMonto = grp.Sum(e=>e.CierreMonto),
                            CuotaCumplimientoMontoCierre=grp.Sum(e=>e.CuotaCumplimientoMontoCierre),
                            CierreCumplimiento = grp.Sum(e=>e.CierreCumplimiento),
                            CanceladoNumProy = grp.Sum(e => e.CanceladoNumProy),
                            CanceladoImporteProy = grp.Sum(e=>e.CanceladoImporteProy),
                            EntradasFrecuencia = grp.Sum(e=>e.EntradasFrecuencia)
                        };
            return respuesta;
        }

        protected IEnumerable<EntradaCDReporteDinamo> ProcesarEntradaParaCentroDeDistribucionConMontosCorregidos(Sesion s, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            var sourceEntradas = ObtenerControlEntrada(s, ibt);
            var proyectos = ProcesarFuenteDeTodos(s, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion != null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } into grp
                            select new EntradaCDReporteDinamo()
                            {
                                Id_Cd = grp.Key.Id_Cd,
                                Id_Rik = grp.Key.Id_Rik,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),

                                ProyectosAnalisisMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesAplicacion.CrmOpAp_VPO),
                                ProyectosPresentacionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 2)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesProducto2.Sum(p => (p.COP_Cantidad == null ? 0.0M : p.COP_Cantidad.Value) * Convert.ToDecimal((p.ProductoActual2 == null ? 0.0D : (p.ProductoActual2.Prd_Pesos == null ? 0.0D : p.ProductoActual2.Prd_Pesos))))),
                                ProyectosNegociacionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapValProyectoDets.Sum(p=>p.Vap_Cantidad* Convert.ToDecimal(p.Vap_Precio))),
                                ProyectosCierreMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => (crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys!=null ? crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)): 0)),

                                //ProyectosPromocionMontoProyecto = ProyectosAnalisisMontoProyecto + ProyectosPresentacionMontoProyecto + ProyectosNegociacionMontoProyecto //grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CuotaCumplimientoMontoProyecto = grp.Max(a => a.Cuota.Cuo_MontoProy.Value),
                                ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                //CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CuotaCumplimientoMontoCierre = grp.Max(a => a.Cuota.Cuo_MontoCierre.Value),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => (crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys!=null ? crmOp.Proyecto.Valuacion.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)) / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoCierre.Value): 0)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik)
                            };
            respuesta = from r in respuesta
                        group r by new { r.Id_Cd } into grp
                        select new EntradaCDReporteDinamo()
                        {
                            Id_Cd = grp.Key.Id_Cd,
                            Id_Rik = 0,
                            ProyectosIngresadosNumeroProyectos = grp.Sum(e => e.ProyectosIngresadosNumeroProyectos),
                            ProyectosIngresadosImporteProyecto = grp.Sum(e => e.ProyectosIngresadosImporteProyecto),
                            ProyectosPromocionMontoProyecto = grp.Sum(e => e.ProyectosAnalisisMontoProyecto + e.ProyectosPresentacionMontoProyecto + e.ProyectosNegociacionMontoProyecto),
                            CuotaCumplimientoMontoProyecto = grp.Sum(e => e.CuotaCumplimientoMontoProyecto),
                            ProyectosPromocionCumplimiento = grp.Sum(e => e.ProyectosPromocionCumplimiento),
                            CierreMonto = grp.Sum(e => e.ProyectosCierreMontoProyecto),
                            CuotaCumplimientoMontoCierre = grp.Sum(e => e.CuotaCumplimientoMontoCierre),
                            CierreCumplimiento = grp.Sum(e => e.CierreCumplimiento),
                            CanceladoNumProy = grp.Sum(e => e.CanceladoNumProy),
                            CanceladoImporteProy = grp.Sum(e => e.CanceladoImporteProy),
                            EntradasFrecuencia = grp.Sum(e => e.EntradasFrecuencia)
                        };
            return respuesta;
        }

        protected IEnumerable<EntradaCDReporteDinamo> ProcesarEntradaParaCentroDeDistribucionPorRIK(Sesion sesionOperador, Sesion sesionDeContextoDeDatos, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            //Obtener el control de entrada para el centro de distribución
            var sourceEntradas = ObtenerControlEntrada(sesionDeContextoDeDatos, ibt);
            var proyectos = ProcesarFuenteDeTodos(sesionDeContextoDeDatos, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion != null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value, Rik_Nombre=p.CatTerritorio.InfoRIKComoUsuario.U_Nombre } into grp
                            select new EntradaCDReporteDinamo()
                            {
                                Id_Cd = grp.Key.Id_Cd,
                                Id_Rik = grp.Key.Id_Rik,
                                Rik_Nombre=grp.Key.Rik_Nombre,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                ProyectosPromocionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => crmOp.Proyecto.MontoProyecto / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik)
                            };
            return respuesta;
        }

        /// <summary>
        /// El propósito de esta versión es el definir el cálculo correcto de los montos por proyecto dado la fase en el que se encuentren
        /// </summary>
        /// <param name="sesionOperador"></param>
        /// <param name="sesionDeContextoDeDatos"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="fechaInicialPeriodoActual"></param>
        /// <param name="cuotas"></param>
        /// <param name="ibt"></param>
        /// <returns></returns>
        protected IEnumerable<EntradaCDReporteDinamo> ProcesarEntradaParaCentroDeDistribucionPorRIKConMontosCorregidos(Sesion sesionOperador, Sesion sesionDeContextoDeDatos, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaInicialPeriodoActual, IEnumerable<CatCuotasCrm> cuotas, IBusinessTransaction ibt)
        {
            //Obtener el control de entrada para el centro de distribución
            var sourceEntradas = ObtenerControlEntrada(sesionDeContextoDeDatos, ibt);
            var proyectos = ProcesarFuenteDeTodos(sesionDeContextoDeDatos, fechaInicial, fechaFinal, ibt);

            var respuesta = from p in proyectos
                            join cuota in cuotas
                            on new { Id_Cd = p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value } equals new { Id_Cd = cuota.Id_Cd, Id_Rik = cuota.Id_rik }
                            where p.FechaCreacion != null && p.FechaCreacion.Value.Month == cuota.Cuo_Mes && p.FechaCreacion.Value.Year == cuota.Cuo_Anio
                            group new { Proyecto = p, Cuota = cuota } by new { p.Id_Cd, Id_Rik = p.CatTerritorio.Id_Rik.Value, Rik_Nombre = p.CatTerritorio.InfoRIKComoUsuario.U_Nombre } into grp
                            select new EntradaCDReporteDinamo()
                            {
                                Id_Cd = grp.Key.Id_Cd,
                                Id_Rik = grp.Key.Id_Rik,
                                Rik_Nombre = grp.Key.Rik_Nombre,
                                ProyectosIngresadosNumeroProyectos = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal),
                                ProyectosIngresadosImporteProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicialPeriodoActual && crmOp.Proyecto.FechaCreacion < fechaFinal).Sum(crmOp => crmOp.Proyecto.VentaMensual),
                                
                                //El siguiente bloque calcula de manera correcta los montos de los proyectos dada la fase en donde se encuentra el proyecto
                                ProyectosAnalisisMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesAplicacion.CrmOpAp_VPO),
                                ProyectosPresentacionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 2)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesProducto2.Sum(p => (p.COP_Cantidad == null ? 0.0M : p.COP_Cantidad.Value) * Convert.ToDecimal((p.ProductoActual2 == null ? 0.0D : (p.ProductoActual2.Prd_Pesos == null ? 0.0D : p.ProductoActual2.Prd_Pesos))))),
                                ProyectosNegociacionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapValProyectoDets.Sum(p => p.Vap_Cantidad * Convert.ToDecimal(p.Vap_Precio))),
                                ProyectosCierreMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => (crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys != null ? crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)) : 0)),

                                //Al igual que el correcto cálculo de los montos depende de la fase en el que se encuetra el proyecto, así tambien lo es para el cumplimiento (debido a la definición del cálculo del cumplimiento)
                                ProyectosAnalisisCumplimientoProyecto=grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesAplicacion.CrmOpAp_VPO/ Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                ProyectosPresentacionCumplimientoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 2)).Sum(crmOp => crmOp.Proyecto.CrmOportunidadesProducto2.Sum(p => (p.COP_Cantidad == null ? 0.0M : p.COP_Cantidad.Value) * Convert.ToDecimal((p.ProductoActual2 == null ? 0.0D : (p.ProductoActual2.Prd_Pesos == null ? 0.0D : p.ProductoActual2.Prd_Pesos)))) / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                ProyectosNegociacionCumplimientoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapValProyectoDets.Sum(p => p.Vap_Cantidad * Convert.ToDecimal(p.Vap_Precio)) / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),

                                //ProyectosPromocionMontoProyecto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                //ProyectosPromocionCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 1 || crmOp.Proyecto.Estatus == 2 || crmOp.Proyecto.Estatus == 3)).Sum(crmOp => crmOp.Proyecto.MontoProyecto.Value / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoProy.Value)),
                                CierreMonto = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => (crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys != null ? crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)) : 0)),
                                CierreCumplimiento = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 4)).Sum(crmOp => (crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys != null ? crmOp.Proyecto.CrmValuacionOportunidades.First().CapValProyecto.CapAcys.CapAcysDets.Sum(cad => cad.Acs_Cantidad * Convert.ToDecimal(cad.Acs_Precio.Value)) / Convert.ToDecimal(crmOp.Cuota.Cuo_MontoCierre.Value) : 0)),
                                CanceladoNumProy = grp.Count(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)),
                                CanceladoImporteProy = grp.Where(crmOp => crmOp.Proyecto.FechaCreacion > fechaInicial && crmOp.Proyecto.FechaCreacion < fechaFinal && (crmOp.Proyecto.Estatus == 5)).Sum(crmOp => crmOp.Proyecto.MontoProyecto),
                                EntradasFrecuencia = (float)(fechaFinal - fechaInicial).TotalDays / (float)sourceEntradas.Count(crmCE => crmCE.Id_Cd == grp.Key.Id_Cd && crmCE.Id_Usu == grp.Key.Id_Rik)
                            };
            foreach (var r in respuesta)
            {
                //El cálculo del cierre y su cumplimiento han sido calculados en la expresión de selección, por lo que no existe la necesidad de calcularlos aquí
                r.ProyectosPromocionMontoProyecto = r.ProyectosAnalisisMontoProyecto + r.ProyectosPresentacionMontoProyecto + r.ProyectosNegociacionMontoProyecto;
                r.ProyectosPromocionCumplimiento = r.ProyectosAnalisisCumplimientoProyecto + r.ProyectosPresentacionCumplimientoProyecto + r.ProyectosNegociacionCumplimientoProyecto;
            }
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
                            && p.FechaCreacion!=null
                            select p;
            return resultado;
        }
    }

    public class EntradaCDReporteDinamo
    {
        public EntradaCDReporteDinamo()
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

        public string StyleProyectosIngresadosNumProy
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

        public string StyleProyectosIngresadosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }

                return sb.ToString();
            }
        }

        public string StyleProyectosPromocionMontoProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleProyectosPromocionCumplimientoProy
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

                if (ProyectosPromocionCumplimiento.Value<=.5M)
                {
                    sb.Append("background-color: red;");
                }
                else if (ProyectosPromocionCumplimiento.Value >= .51M && ProyectosPromocionCumplimiento.Value <= .7M)
                {
                    sb.Append("background-color: yellow;");
                }
                else
                {
                    sb.Append("background-color: green;");
                }
                
                return sb.ToString();
            }
        }

        public string StyleCierreMontoCierre
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleCierreCumplimiento
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

                if (CierreCumplimiento <= .5M)
                {
                    sb.Append("background-color: red;");
                }
                else if (CierreCumplimiento >= .51M && CierreCumplimiento <= .7M)
                {
                    sb.Append("background-color: yellow;");
                }
                else
                {
                    sb.Append("background-color: green;");
                }

                return sb.ToString();
            }
        }

        public string StyleCanceladosNumProy
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

        public string StyleCanceladosImporteProy
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("text-align: right;");
                if (EsZona)
                {
                    sb.Append("font-weight: bold;");
                    sb.Append("background-color: #C0C0C0;");
                    return sb.ToString();
                }
                return sb.ToString();
            }
        }

        public string StyleEntradasFrecuencia
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

        public double? CuotaCumplimientoMontoProyecto
        {
            get;
            set;
        }

        public double? CuotaCumplimientoMontoCierre
        {
            get;
            set;
        }

        public string StyleProyectosPromocionPlantilla
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

        public string StyleCierrePlantilla
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

        public decimal? ProyectosAnalisisMontoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosPresentacionMontoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosNegociacionMontoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosCierreMontoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosAnalisisCumplimientoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosPresentacionCumplimientoProyecto
        {
            get;
            set;
        }

        public decimal? ProyectosNegociacionCumplimientoProyecto
        {
            get;
            set;
        }


    }

    public class IntervaloDePeriodoIncongruenteException
        : Exception
    {
        public IntervaloDePeriodoIncongruenteException()
            : base("El intervalo del periodo es incongruente")
        {
        }
    }

    public class PeriodoNoEspecificadoException
        : Exception
    {
        public PeriodoNoEspecificadoException()
            : base("No se ha especificado el periodo para la generación del reporte")
        {
        }
    }

    public class EntradaRIKReporteDinamo
    {
        public EntradaRIKReporteDinamo()
        {
        }

        public int IdProyecto
        {
            get;
            set;
        }

        public string NombreCliente
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public string Solucion
        {
            get;
            set;
        }

        public string Aplicacion
        {
            get;
            set;
        }

        public string Productos
        {
            get;
            set;
        }

        public decimal? VTeorico
        {
            get;
            set;
        }

        public DateTime? Analisis
        {
            get;
            set;
        }

        public DateTime? Presentacion
        {
            get;
            set;
        }

        public DateTime? Negociacion
        {
            get;
            set;
        }

        public DateTime? Cierre
        {
            get;
            set;
        }

        public DateTime? Cancelacion
        {
            get;
            set;
        }

        public decimal? MontoProyecto
        {
            get;
            set;
        }

        public DateTime? FechaModificacion
        {
            get;
            set;
        }

        public string Estatus
        {
            get;
            set;
        }

        public string Causa
        {
            get;
            set;
        }

        public string Comentarios
        {
            get;
            set;
        }
    }
}
