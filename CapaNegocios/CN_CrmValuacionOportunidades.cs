using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CrmValuacionOportunidades
    {

        public eValuacionProyecto ObtenerPorValuacion_Detallado(int Id_Op, int Id_Val, ref int verificador, Sesion sesion,
            double CuentasPorCobrar_,
            double Inventario_,
            double InversionActivosFijos_,
            double FinanciamientoProveedores_,
            double TasaIncrementoCostoCapital_,
            double VigenciaACYS_,
            double FleteCD_,
            double GastosServirCliente_,
            double ISRyPTU_,
            double Cetes_,
            double ManoObraProyectos_
        )
        {
            CD_CrmValuacionOportunidades CD = new CD_CrmValuacionOportunidades();
            return CD.ObtenerPorValuacion_Detallado(sesion.Id_Emp, sesion.Id_Cd, Id_Op, Id_Val, ref verificador, sesion.Emp_Cnx,
            CuentasPorCobrar_,
            Inventario_,
            InversionActivosFijos_,
            FinanciamientoProveedores_,
            TasaIncrementoCostoCapital_,
            VigenciaACYS_,
            FleteCD_,
            GastosServirCliente_,
            ISRyPTU_,
            Cetes_,
            ManoObraProyectos_);
        }

        /// <summary>
        /// Obtiene las asociaciones de valuación con proyectos.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idVal">Identificador de la valuación registrada para el cliente idCte</param>
        /// <returns>CrmValuacionOportunidade[]</returns>
        public IEnumerable<CrmValuacionOportunidade> ObtenerPorValuacion(Sesion sesion, int idCte, int idVal)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorValuacion(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idVal, sesion.Emp_Cnx_EF);
        }

        public List<eCrmValuacionOportunidades> ObtenerPorValuacion_(Sesion sesion, int idCte, int idVal)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorValuacion_(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idVal, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// Obtiene las asociaciones de valuación con proyectos.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="ibt">Contexto de conexión a la fuente de datos</param>
        /// <returns>CrmValuacionOportunidade[]</returns>
        public IEnumerable<CrmValuacionOportunidade> ObtenerPorValuacion(Sesion sesion, int idCte, int idVal, IBusinessTransaction ibt)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorValuacion(sesion.Id_Emp, sesion.Id_Cd, idCte, sesion.Id_Rik, idVal, ibt.DataContext);
        }

        public CrmValuacionOportunidade ObtenerPorProyecto(Sesion sesion, int idCte, int idOp)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorProyecto(sesion.Id_Emp, sesion.Id_Cd, idCte, idOp, sesion.Emp_Cnx_EF);
        }

        /// <summary>
        /// Regresa las asociación de un proyecto y una valuación. Versión transaccional.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente idCte</param>
        /// <param name="idOp">Identificador del proyecto asociado al cliente idCte</param>
        /// <param name="ibt">Transacción de negocios</param>
        /// <returns>CrmValuacionOportunidade</returns>
        public CrmValuacionOportunidade ObtenerPorProyecto(Sesion sesion, int idCte, int idOp, IBusinessTransaction ibt)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorProyecto(sesion.Id_Emp, sesion.Id_Cd, idCte, idOp, ibt.DataContext);
        }

        /// <summary>
        /// Regresa las asociación de un proyecto y una valuación. Versión transaccional.
        /// </summary>
        /// <param name="sesion">Sesión del usuario en operación</param>
        /// <param name="idCte">Identificador del cliente idCte</param>
        /// <param name="idOp">Identificador del proyecto asociado al cliente idCte</param>
        /// <param name="ibt">Transacción de negocios</param>
        /// <returns>CrmValuacionOportunidade</returns>
        public CrmValuacionOportunidade ObtenerPorProyectoSinValuacionGlobal(Sesion sesion, int idCte, int idOp, IBusinessTransaction ibt)
        {
            CD_CrmValuacionOportunidades cdCrmValuacionOportunidades = new CD_CrmValuacionOportunidades();
            return cdCrmValuacionOportunidades.ConsultarPorProyecto(sesion.Id_Emp, sesion.Id_Cd, idCte, idOp, ibt.DataContext);
        }
    }
}
