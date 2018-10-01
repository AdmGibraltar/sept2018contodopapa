using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatCalendario
    {

        public void ConsultarCombo(int Emp, int Cd, ref List<CrmLista> Lista, string Conexion)
        {
            try
            {
                CD_CatCalendario CL = new CD_CatCalendario();
                CL.ConsultaCombo(Emp,Cd, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ConsultaCalendario(ref Calendario calendario, int año, CapaEntidad.Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendario(ref calendario, año, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCalendarioUltimaFecha(ref Calendario calendario, int año, CapaEntidad.Sesion sesion)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendarioUltimaFecha(ref calendario, año, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarCalendario(ref List<Calendario> calendarios, string Conexion, ref int verificador, bool actualizar)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.GuardarCalendario(ref calendarios, Conexion, ref verificador, actualizar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCalendario(int Id_Cal, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCalendario cd = new CD_CatCalendario();
                cd.EliminarCalendario(Id_Cal, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCalendarioAño(int Cal_Año, Sesion session, ref int verificador)
        {
            try
            {
                CD_CatCalendario cd = new CD_CatCalendario();
                cd.EliminarCalendarioAño(Cal_Año, session, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void VerificaCalendario(ref Calendario calendario, int año,int Cal_Mes, Sesion sesion, ref List<Calendario> list)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.VerificaCalendario(ref calendario, año,Cal_Mes, sesion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaCalendarioActual(ref Calendario calendario, Sesion sesion)
        {
            try
            {
                CD_CatCalendario cal = new CD_CatCalendario();
                cal.ConsultaCalendarioActual(ref calendario, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el calendario actual.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CatCalendario</returns>
        public CatCalendario CalendarioActual(Sesion s, IBusinessTransaction ibt)
        {
            try
            {
                CD_CatCalendario cdCatCalendario = new CapaDatos.CD_CatCalendario();
                var calendarios = cdCatCalendario.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
                var calendariosActuales = from c in calendarios
                                          where c.Cal_Actual.Value
                                          select c;
                if (calendariosActuales.Count() > 0)
                {
                    return calendariosActuales.First();
                }

            } catch {
                return null;
            }
            return null;
        }

        /// <summary>
        /// Obtiene el calendario para el año y mes especificados
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="anyo">Año del calendario deseado</param>
        /// <param name="mes">Mes del calendario deseado</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CatCalendario</returns>
        public CatCalendario PorAnyoMes(Sesion s, int anyo, int mes, IBusinessTransaction ibt)
        {
            try
            {
                CD_CatCalendario cdCatCalendario = new CapaDatos.CD_CatCalendario();
                var calendarios = cdCatCalendario.Consultar(s.Id_Emp, s.Id_Cd, ibt.DataContext);
                var coincidencias = from c in calendarios
                                    where c.Cal_Año == anyo && c.Cal_Mes == mes
                                    select c;
                if (coincidencias.Count() > 0)
                {
                    return coincidencias.First();
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
    }
}
