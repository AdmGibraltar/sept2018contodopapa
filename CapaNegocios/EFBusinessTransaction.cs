using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    /// <summary>
    /// Implementación del contrato de transacciones de negocio usando un contexto basado en Entity Framework
    /// </summary>
    public class EFBusinessTransaction
        : BaseBusinessTransaction
    {
        /// <summary>
        /// Constructor que inicializa la transacción al sistema SIANWeb que creó la sesión
        /// </summary>
        /// <param name="s"></param>
        public EFBusinessTransaction(Sesion s)
        {
            _DataContext = CD_FabricaContexto.CrearDefault(s.Emp_Cnx_EF);
        }

        protected EFBusinessTransaction()
        {

        }

        public static EFBusinessTransaction ParaSIANCentral(Sesion s)
        {
            EFBusinessTransaction ef = new EFBusinessTransaction();
            ef._DataContext = CD_FabricaContexto.CrearParaSIANCentral(s.SIANCentralEF);
            return ef;
        }

        /// <summary>
        /// Constructor que inicializa la transacción dado el nombre del sistema SIANWeb al que se desea conectar.
        /// Arroja una excepción en caso de que el nombre clave del sistema web no exista
        /// </summary>
        /// <param name="s">Sesión del usuario en operación</param>
        /// <param name="nombreSIANWeb">Nombre clave del sistema SIANWeb al que se desea conectar</param>
        public EFBusinessTransaction(Sesion s, string nombreSIANWeb)
        {
            var cadenaCnx = from ccnx in s.ConexionesSIANWeb
                            where ccnx.Key==nombreSIANWeb
                            select ccnx;
            try
            {
                _DataContext = CD_FabricaContexto.CrearDefault(cadenaCnx.First().Value);
            }
            catch (InvalidOperationException invOpex)
            {
                if (string.Equals(invOpex.Message, "Sequence contains no elements"))
                {
                    throw new SIANNoEncontradoException(nombreSIANWeb);
                }
            }
        }

        public override ICD_Contexto DataContext
        {
            get
            {
                return base.DataContext;
            }
        }
    }

    public class SIANNoEncontradoException
        : Exception
    {
        public SIANNoEncontradoException(string nombreSIANWeb)
            : base(string.Format("La configuración del sistema SIAN {0} no ha sido encontrada", nombreSIANWeb))
        {

        }
    }
}
