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
    public class CN_CapAcys
    {
        public CN_CapAcys()
        {
        }

        public CN_CapAcys(IBusinessTransaction ibt)
        {
            _ibt = ibt;
        }

        public void ConsultarAcys_Rpt_Cumplimiento(int Id_Cd, int Id_Ter, int Id_Rep, int Anio_Ini, int Mes_Ini, int Anio_Fin, int Mes_Fin, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultadeAcys_Rpt_Cumplimiento(Id_Cd, Id_Ter, Id_Rep, Anio_Ini, Mes_Ini, Anio_Fin, Mes_Fin, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcys_Rpt_Resumen(Acys acys, string Conexion, ref DataTable Dt)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarAcys_Rpt_Resumen(acys, Conexion, ref Dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarAcys_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarAcys_Lista(acys, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAcysVersiones_Lista(Acys acys, string Conexion, ref List<Acys> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarAcysVersiones_Lista(acys, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable seleccionados, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Insertar(acys, list, Conexion, seleccionados, ref verificador, asesorias, servicios, serviciosMantenimiento, listaGarantia, ValoresCalendario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable seleccionados, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario, IBusinessTransaction ibt)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Insertar(acys, list, Conexion, seleccionados, ref verificador, asesorias, servicios, serviciosMantenimiento, listaGarantia, ValoresCalendario, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(Acys acys, List<AcysPrd> list, string Conexion, DataTable seleccionados, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, int? id_TV, List<int?> modalidadesGarantias, Sesion s)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Insertar(acys, list, Conexion, seleccionados, ref verificador, asesorias, servicios, serviciosMantenimiento, listaGarantia, id_TV, modalidadesGarantias, s.Emp_Cnx_EF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, List<AcysDatosGarantia> listaGarantia, String ValoresCalendario)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Modificar(acys, list, Conexion, dt, ref verificador, asesorias, servicios, serviciosMantenimiento, listaGarantia, ValoresCalendario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar_Log(List<Producto> List_ServiciosMantenimiento_Or, List<Producto> List_ServicioTec_Or, List<Asesoria> List_Asesoria_Or, DataTable dtAcuerdos_Or, Acys acys_Or, Acys acys, List<AcysPrd> list, string Conexion, DataTable dt, ref int verificador, List<Asesoria> asesorias, List<Producto> servicios, List<Producto> serviciosMantenimiento, string _Usuario, string _Pantalla)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Modificar_Log(List_ServiciosMantenimiento_Or, List_ServicioTec_Or, List_Asesoria_Or, dtAcuerdos_Or, acys_Or, acys, list, Conexion, dt, ref verificador, asesorias, servicios, serviciosMantenimiento, _Usuario, _Pantalla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Cancelar(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Cancelar(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void actualizarEstatus(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.actualizarEstatus(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void AutorizarAcys(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.AutorizarAcys(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEnvio(ref Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEnvio(ref acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaUltimaVersion(ref Acys acys, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaUltimaVersion(ref acys, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CapAcy ConsultarUltimaPorClienteYFecha(int idEmp, int idCd, int idCte, int idTer, DateTime fecha, Sesion sesion)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();
            var res = cdCapAcys.ConsultarUltimaPorClienteYFecha(idEmp, idCd, idCte, idTer, fecha, sesion.Emp_Cnx_EF);
            return res;
        }
        public void CedVis_Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.CedVis_Consultar(ref acys, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultar(ref Acys acys, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Consultar(ref acys, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarDet(Acys acys, ref  DataTable dtAcuerdos, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarDet(acys, ref dtAcuerdos, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Acys acys, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Imprimir(acys, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarReemplazos(Acys acys, int Id_Prd, ref DataTable list2, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultarReemplazos(acys, Id_Prd, ref list2, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEquivalencia(int Id_Prd, int Id_Prd_Original, string Id_Acys, int Id_AcysVersion, int Id_Emp, int Id_Cd, string Conexion)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ModificarEquivalencia(Id_Prd, Id_Prd_Original, Id_Acys, Id_AcysVersion, Id_Emp, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAsesorias(Acys acys, string Conexion, ref List<Asesoria> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaAsesorias(acys, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstBi(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEstBi(acys, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEstBiMantenimiento(Acys acys, string Conexion, ref List<Producto> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ConsultaEstBiMantenimiento(acys, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteVentas_Consulta(RepVentasParams pParams, string pConexion, List<RepVentas> pList)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.ReporteVentas_Consulta(pParams, pConexion, pList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AcysXCliente_Consulta(Acys pAcys, string pConexion, ref List<Acys> pList)
        {
            try
            {
                CD_CapAcys clsCD = new CD_CapAcys();
                clsCD.AcysXCliente_Consulta(pAcys, pConexion, ref pList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AcysDatosGarantia> DatosGarantia_Consulta(string pConexion, AcysDatosGarantia datosGarantia)
        {

            try
            {
                CD_CapAcys clsCD = new CD_CapAcys();
                return clsCD.DatosGarantia_Consulta(pConexion, datosGarantia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consulta_Log(string Id_Acs, string Conexion, string Pantalla, ref List<Logs> List)
        {
            try
            {
                CD_CapAcys claseCapaDatos = new CD_CapAcys();
                claseCapaDatos.Consulta_Log(Id_Acs, Conexion, Pantalla, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<AcysDatosGarantia> DatosGarantia_Consulta_Remision(string pConexion, int remision, int idEmp, int idCd, int idCte, int idTer)
        {

            try
            {
                CD_CapAcys clsCD = new CD_CapAcys();
                return clsCD.DatosGarantia_Consulta_Remision(pConexion, remision, idEmp, idCd, idCte, idTer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteProductoEnGarantia(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik, Sesion sesion)
        {
            CD_CapAcysDet cdCad = new CD_CapAcysDet(sesion.Emp_Cnx);
            var cad = cdCad.Consultar_PorProducto(idEmp, idCd, idPrd, idTer, idCte, idRik);
            if (cad != null)
            {
                return cad.Id_TG != null;
            }
            return false;
        }

        public bool ExisteProductoEnGarantia(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik, Sesion sesion, int idTg)
        {
            CD_CapAcysDet cdCad = new CD_CapAcysDet(sesion.Emp_Cnx_EF);
            var cad = cdCad.ConsultarPorProducto(idEmp, idCd, idPrd, idTer, idCte, idRik, idTg);
            if (cad != null)
            {
                return cad.Id_TG != null;
            }
            return false;
        }

        public CapaModelo.CapAcysDet ObtenerDetallePorProducto(int idEmp, int idCd, int idPrd, int idTer, int idCte, int idRik, Sesion sesion)
        {
            CD_CapAcysDet cdCad = new CD_CapAcysDet(sesion.Emp_Cnx_EF);
            var cad = cdCad.ConsultarPorProducto(idEmp, idCd, idPrd, idTer, idCte, idRik);
            return cad;
        }

        /// <summary>
        /// Devuelve la instancia de la entidad [CapAcys], dado el identificador del acys idAcys.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idAcys">Identificador del acys de interés.</param>
        /// <returns>CapAcy. Instancia de la entidad [CapAcys]; null en caso contrario.</returns>
        public CapAcy Obtener(Sesion s, int idAcys)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();
            return cdCapAcys.ConsultarPorId(s.Id_Emp, s.Id_Cd, idAcys, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Asocia una valuación a un acys.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idAcys">Identificador del acys de interés</param>
        /// <param name="idVal">Identificador de la valuación a asociar al acys idAcys</param>
        public void AsociarValuacion(Sesion s, int idAcys, int idVal)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();
            cdCapAcys.ActualizarAtributoIdVal(s.Id_Emp, s.Id_Cd, idAcys, idVal, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Asocia una valuación a un acys, con la característica adicional de esperar una transacción de negocios
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idAcys">Identificador del acys de interés</param>
        /// <param name="idVal">Identificador de la valuación a asociar al acys idAcys</param>
        /// <param name="ibt">Transacción de negocios</param>
        public void AsociarValuacion(Sesion s, int idAcys, int idVal, IBusinessTransaction ibt)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();
            cdCapAcys.ActualizarAtributoIdVal(s.Id_Emp, s.Id_Cd, idAcys, idVal, ibt.DataContext);
        }

        /// <summary>
        /// Determina si un cliente tiene un ACYS asociado para un territorio determinado
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTerritorio">Identificador del territorio</param>
        /// <returns>bool: true en caso de que el cliente cuente con un ACYS; falso en caso contrario</returns>
        public bool ClieneTieneACYS(Sesion s, int idCte, int idTerritorio)
        {
            try
            {
                CD_CapAcys cdCapAcys = new CD_CapAcys();
                var acysDeCliente = cdCapAcys.ConsultarPorClienteYTerritorio(s.Id_Emp, s.Id_Cd, idCte, idTerritorio, _ibt.DataContext);
                return (acysDeCliente.Count() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ClieneTieneACYS_RFH(Sesion s, int idCte, int idTerritorio, IBusinessTransaction ibt)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();

            var acysDeCliente = cdCapAcys.ConsultarPorClienteYTerritorio(s.Id_Emp, s.Id_Cd, idCte, idTerritorio, ibt.DataContext);
            return (acysDeCliente.Count() > 0);
        }

        /// <summary>
        /// Obtiene el ACYS de un cliente. Se infiere que la última versión del ACYS es la de interés.
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idTer">Identificador del territorio</param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        /// <returns>CapAcy; null en caso de no encontrar una coincidencia</returns>
        public CapAcy ObtenerParaCliente(Sesion s, int idCte, int idTer, IBusinessTransaction ibt)
        {
            CD_CapAcys cdCapAcys = new CD_CapAcys();
            var acyss=cdCapAcys.ConsultarPorClienteYTerritorio(s.Id_Emp, s.Id_Cd, idCte, idTer, ibt.DataContext);
            if (acyss.Count() > 0)
            {
                return acyss.First();
            }
            return null;
        }

        private IBusinessTransaction _ibt = null;
    }
}
