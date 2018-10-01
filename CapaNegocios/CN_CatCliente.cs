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
    public class CN_CatCliente
    {
        public void ConsultarClienteSigCentroDist(ref int verificador, int Id_Emp, int Id_Cd_Ver, string Conexion)
        {
            try
            {
                new CD_CatCliente().ConsultarClienteSigCentroDist(ref verificador, Id_Emp, Id_Cd_Ver, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClientes(clientes, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarClientes(Clientes clientes, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ModificarClientes(clientes, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CatClienteCondPago(int Id_Emp, int Id_Cd_Ver, int Id_Cte, ref double DiasRotacion, string Conexion)
        {
            try
            {
                new CD_CatCliente().CatClienteCondPago(Id_Emp, Id_Cd_Ver, Id_Cte, ref DiasRotacion, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que toma una transacción de la capa de negocio
        /// </summary>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd_Ver"></param>
        /// <param name="Id_Cte"></param>
        /// <param name="DiasRotacion"></param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void CatClienteCondPago(int Id_Emp, int Id_Cd_Ver, int Id_Cte, ref double DiasRotacion, IBusinessTransaction ibt)
        {
            try
            {
                new CD_CatCliente().CatClienteCondPago(Id_Emp, Id_Cd_Ver, Id_Cte, ref DiasRotacion, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientes(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarCliente(cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteOtraBD(ref Clientes cte, string serie, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteOtraBD(cte, serie, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFolioFactEle(Sesion sesion, int Tipo, ref string Folio)
        {
            try
            {
                CD_CatCliente cd_c = new CD_CatCliente();
                cd_c.ConsultaFolioFactEle(sesion, Tipo, ref Folio);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaClienteDet(ClienteDet clientedet, string Conexion, ref DataTable dt)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteDet(clientedet, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion, ref DataTable dt)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteDirEntrega(clienteDirEntrega, Conexion, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion, ref List<Comun> lista)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteDirEntrega(clienteDirEntrega, Conexion, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteDirEntrega(ClienteDirEntrega clienteDirEntrega, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteDirEntrega(clienteDirEntrega, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ConsultaExisteClienteDet(int Id_Ter, int Id_Seg, Sesion sesion, ref int validador)
        //{
        //    try
        //    {
        //        CD_CatCliente claseCapaDatos = new CD_CatCliente();
        //        claseCapaDatos.ConsultaExisteClienteDet(Id_Ter, Id_Seg, sesion, ref validador);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public void InsertarClientesDirEntrega(Clientes clientes, DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClienteDirEntrega(clientes, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarClientesDet(Clientes clientes, DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClienteDet(clientes, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarClientesDet(Clientes clientes, DataTable dt, string Conexion, DataTable catClienteDet, DataTable catClienteDetGarantia, string efConexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.InsertarClienteDet(clientes, dt, Conexion, catClienteDet, catClienteDetGarantia, efConexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="id_cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTerritoriosDelCliente(int id_cliente, Sesion sesion, ref List<Territorios> territorios)
        {
            try
            {
                new CD_CatCliente().ConsultaTerritoriosDelCliente(id_cliente, sesion, ref territorios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="id_cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelCliente(int id_cliente, Sesion sesion, ref List<Territorios> territorios)
        {
            try
            {
                new CD_CatCliente().ConsultaTodosTerritoriosDelCliente(id_cliente, sesion, ref territorios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Recibe un id de cliente, sesion(id_emp,id_cd_ver) y devulve una lista de los territorios a los que pertenece 
        /// </summary>
        /// <param name="id_cliente"></param>
        /// <param name="sesion"></param>
        /// <param name="territorios"></param>
        public void ConsultaTodosTerritoriosDelClienteBI(int id_cliente, Sesion sesion, ref List<Territorios> territorios)
        {
            try
            {
                new CD_CatCliente().ConsultaTodosTerritoriosDelClienteBI(id_cliente, sesion, ref territorios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ReporteRentabilidad_ConsultarEstadistica(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string Anio, String Mes, ref List<EstadisticaRentabilidad> List, string Conexion)
        {
            try
            {
                new CD_CatCliente().ReporteRentabilidad_ConsultarEstadistica(Id_Emp, Id_Cd_Ver, Id_Cte, Id_Ter, Anio, Mes, ref List, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteRentabilidad_ConsultarTotales(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string periodo, string ventas, ref DataTable dt, string Conexion)
        {
            try
            {
                new CD_CatCliente().ReporteRentabilidad_ConsultarTotales(Id_Emp, Id_Cd_Ver, Id_Cte, Id_Ter, periodo, ventas, ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lista(Clientes cte, string Conexion, ref List<Clientes> List)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.Lista(cte, ref List, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTerritorio(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaClienteTerritorio(ref cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPermisosUEN(ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaPermisosUEN(ref dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTipoCDC(int Id_Cd_ver, ref int Tipo_CDC, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaTipoCDC(Id_Cd_ver, ref Tipo_CDC, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaClienteTieneCuentaNacional(ref Clientes cte, ref int TieneCuentaNacional, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultaClienteTieneCuentaNacional(ref cte, ref TieneCuentaNacional, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EstructuraSegmento(ref DataSet dsEstructuraSegmento, Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.EstructuraSegmento(ref dsEstructuraSegmento, cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaPotencial(Clientes cte, double NuevoVPObservado, string NuevoVPObservadoApp, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ActualizaPotencial(cte, NuevoVPObservado, NuevoVPObservadoApp, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaDimension(Clientes cte, int Dimension, double? VPTeorico, DateTime Fecha, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ActualizaDimension(cte, Dimension, VPTeorico, Fecha, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaContactos(Clientes cte, ref DataSet dsContactosClientes, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaContactos(cte, ref dsContactosClientes, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarContacto(Contacto cont, ref int verificador, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.EliminarContacto(cont, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, int Id_cte, string Tipo, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, ref List<AdendaDet> listCabR, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Id_cte, Tipo, ref listCab, ref listDet, ref listCabR, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarAdenda(int Id_Emp, int Id_Cd_Ver, string Ade_Descripcion, ref List<AdendaDet> listCab, ref List<AdendaDet> listDet, ref List<AdendaDet> listCabR, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultarAdenda(Id_Emp, Id_Cd_Ver, Ade_Descripcion, ref listCab, ref listDet, ref listCabR, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientes(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaClientes(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClientesTerAsesor(Clientes cte, string pConexion, object pFiltroId, object pFiltroDesc, ref List<Comun> pList)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaClientesTerAsesor(cte, pConexion, pFiltroId, pFiltroDesc, ref pList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPrecios(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaPrecios(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteFormaPago(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultarClienteFormaPago(ref cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCteFormaPago(Clientes cte, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.InsertarCteFormaPago(cte, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEstadistica(Clientes cte, string Conexion, ref List<Comun> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaEstadistica(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaIndicadores(Clientes cte, string Conexion, ref List<Producto> List, object FiltroId, object FiltroDesc)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaIndicadores(cte, Conexion, ref List, FiltroId, FiltroDesc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTransf(ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente claseCapaDatos = new CD_CatCliente();
                claseCapaDatos.ConsultarClienteTransf(cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaClienteTipo(Clientes cte, string Conexion, ref List<Comun> List)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaTipoCliente(cte, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCuentaNacional(int? idCuenta, string Conexion, ref List<Comun> List)
        {
            try
            {
                CD_CatCliente cd_catcliente = new CD_CatCliente();
                cd_catcliente.ConsultaCuentaNacional(idCuenta, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaClienteCorrreos(int Id_Cd, int Id_Fac, ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.ConsultaClienteCorrreos(Id_Cd, Id_Fac, ref cte, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaClienteCorrreosOtraBD(int Id_Emp, int Id_Cd, int Id_Fac, string serie, ref Clientes cte, string Conexion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.ConsultaClienteCorrreosOtraBD(Id_Emp, Id_Cd, Id_Fac, serie, ref cte, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ClienteConsultaNombre(int Id_Cte, ref string Cte_NomComercial, Sesion sesion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.ClienteConsultaNombre(Id_Cte, ref Cte_NomComercial, sesion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ConsultaModalidadOP(string Conexion, ref List<TipoVenta> modOperList)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.ConsultaModalidadOP(Conexion, ref modOperList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClienteDetGarantia> ConsultarClienteTerr_EsGarantia(ClienteDet clientedet, string Conexion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                return cd_cte.ConsultarClienteTerr_EsGarantia(clientedet, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CatCliente> Obtener(Sesion s, string terminoDeBusqueda)
        {
            IEnumerable<CatCliente> resultado = null;
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            resultado = cdCatCliente.Consultar(s.Id_Emp, s.Id_Cd, terminoDeBusqueda, s.Emp_Cnx_EF);
            return resultado;
        }

        /// <summary>
        /// Devuelve el conjunto de clientes condicionado por un término de búsqueda de texto
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="terminoDeBusqueda">Término de búsqueda en forma de cadena de texto</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>IEnumerable<CatCliente></returns>
        public IEnumerable<CatCliente> Obtener(Sesion s, string terminoDeBusqueda, IBusinessTransaction ibt)
        {
            IEnumerable<CatCliente> resultado = null;
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            resultado = cdCatCliente.Consultar(s.Id_Emp, s.Id_Cd, terminoDeBusqueda, ibt.DataContext);
            return resultado;
        }

        public IEnumerable<CatCliente> ObtenerPorRFC(Sesion s, string rfc)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            return cdCatCliente.ConsultarPorRFC(s.Id_Emp, s.Id_Cd, rfc, s.Emp_Cnx_EF);
        }

        public IEnumerable<CatCliente> Obtener_PorRFC(Sesion s, string rfc)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            return cdCatCliente.Consultar_PorRFC(s.Id_Emp, s.Id_Cd, rfc, s.Emp_Cnx);
        }

        public IEnumerable<CatCliente> ObtenerPorNombreComercial(Sesion s, string nombre)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            return cdCatCliente.ConsultarPorNombre(s.Id_Emp, s.Id_Cd, nombre, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Activa el cliente especificado
        /// </summary>
        /// <param name="s">Sesión de inicio</param>
        /// <param name="idCte">Identificador de cliente a activar</param>
        public void Activar(Sesion s, int idCte)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            cdCatCliente.ActualizarCampo_Cte_Activo(s.Id_Emp, s.Id_Cd, idCte, true, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Activa el cliente especificado
        /// </summary>
        /// <param name="s">Sesión de inicio</param>
        /// <param name="idCte">Identificador de cliente a activar</param>
        /// <param name="ibt">Transacción de capa de negocios</param>
        public void Activar(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            cdCatCliente.ActualizarCampo_Cte_Activo(s.Id_Emp, s.Id_Cd, idCte, true, ibt.DataContext);
        }

        /// <summary>
        /// Obtiene la instancia de datos de CatCliente mediante su identificador idCte
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>CatCliente</returns>
        public CatCliente Obtener(Sesion s, int idCte)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            return cdCatCliente.ConsultarPorId(s.Id_Emp, s.Id_Cd, idCte, s.Emp_Cnx_EF);
        }

        /// <summary>
        /// Obtiene la instancia de datos de CatCliente mediante su identificador idCte
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Transacción de capa de negocio</param>
        /// <returns>CatCliente</returns>
        public CatCliente Obtener(Sesion s, int idCte, IBusinessTransaction ibt)
        {
            CD_CatCliente cdCatCliente = new CD_CatCliente();
            return cdCatCliente.ConsultarPorId(s.Id_Emp, s.Id_Cd, idCte, ibt.DataContext);
        }


        // 6 Sep 2018 
        public List<eClienteBuscar> ListarBusqueda(int Id_Emp, int Id_Cd, int Id_Uen, int Id_Seg, int Id_Terr, int Id_Rik,
            string TextoBuscar, string Conexion)
        {
            List<eClienteBuscar> lst = new List<eClienteBuscar>();

            CD_CatCliente CC = new CD_CatCliente();
            lst = CC.ListarBusqueda(Id_Emp, Id_Cd, Id_Uen, Id_Seg, Id_Terr, Id_Rik, TextoBuscar, Conexion);

            return lst;
        }

        // Regresa el Cliente por Id
        // 8 Sep 2018 
        public Clientes Consultar_PorId_Cte(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion)
        {

            Clientes Cte = new Clientes();

            CD_CatCliente CC = new CD_CatCliente();
            Cte = CC.Consultar_PorId_Cte(Id_Emp, Id_Cd, Id_Cte, Conexion);

            return Cte;
        }
        //

        //RBM
        //Inicio
        public void UpdateClientesDet(Clientes clientes, DataTable dt, string Conexion, DataTable catClienteDet, DataTable catClienteDetGarantia, string efConexion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.UpdateClientesDet(clientes, dt, Conexion, catClienteDet, catClienteDetGarantia, efConexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateClientesDet(Clientes clientes, DataTable dt, string Conexion)
        {
            try
            {
                CD_CatCliente cd_cte = new CD_CatCliente();
                cd_cte.UpdateClientesDet(clientes, dt, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Fin

    }
}