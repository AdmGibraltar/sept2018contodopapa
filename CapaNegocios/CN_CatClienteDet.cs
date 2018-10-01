using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatClienteDet
    {
        private Sesion _s;

        public CN_CatClienteDet(Sesion s)
        {
            _s = s;
        }

        public Decimal? ObtenerAcumuladoDeRemisiones(int? idTer, int? idCte)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(_s.Emp_Cnx_EF);
            var resultado = cdCatClienteDet.ConsultarAcumuladoDeRemisiones(_s.Id_Emp, _s.Id_Cd, idTer, idCte);
            return resultado;
        }

        public bool ExistenRemisionesEnElPeriodo(int? idTer, int? idCte)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(_s.Emp_Cnx_EF);
            var result = cdCatClienteDet.ConsultarRemisionesEnPeriodo(_s.Id_Emp, _s.Id_Cd, idTer, idCte);
            if (result != null)
            {
                return result.Count > 0;
            }
            return false;
        }

        public CatClienteDet Obtener(int idCte, int idTer)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(_s.Emp_Cnx_EF);
            var catCteDets = cdCatClienteDet.Consultar(_s.Id_Emp, _s.Id_Cd, idCte, _s.Emp_Cnx_EF);
            var porTerritorios = (from r in catCteDets
                                  where r.Id_Ter == idTer
                                  select r).ToList();
            if (porTerritorios.Count > 0)
                return porTerritorios[0];
            return null;
        }

        public IEnumerable<CatClienteDet> ObtenerPorCliente(Sesion s, int idCte)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(s.Emp_Cnx);
            return cdCatClienteDet.Consultar(s.Id_Emp, s.Id_Cd, idCte, s.Emp_Cnx_EF);
        }

        public CatClienteDet CrearNuevo(Sesion s, int idCte, int idRik, int idTer, int idSeg, double vpo)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(s.Emp_Cnx);
            return cdCatClienteDet.InsertarBasico(s.Id_Emp, s.Id_Cd, idCte, idRik, idTer, idSeg, vpo, s.Emp_Cnx_EF);
        }

        public IEnumerable<CatClienteDet> CrearNuevos(Sesion s, CapaModelo.CatClienteDet[] dets)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(s.Emp_Cnx);
            int? idCte = null;
            if (dets.Length > 0)
            {
                idCte = dets[0].Id_Cte;
            }
            else
            {
                throw new Exception("No se especificaron entradas.");
            }
            int maximoId = cdCatClienteDet.ConsultarMaximoId(s.Id_Emp, s.Id_Cd, idCte.Value, s.Emp_Cnx_EF);
            foreach (var catClienteDet in dets)
            {
                if (catClienteDet.Id_Cte != idCte)
                {
                    throw new Exception("Las entradas no pertenecen al mismo cliente.");
                }
                catClienteDet.Id_Emp = s.Id_Emp;
                catClienteDet.Id_Cd = s.Id_Cd;
                catClienteDet.Id_CteDet = maximoId;
                catClienteDet.Cte_UnidadDim = null;
                catClienteDet.Cte_Dimension = null;
                catClienteDet.Cte_Pesos = 0;
                catClienteDet.Cte_CarMP = 0;
                catClienteDet.Cte_GasVarT = 0;
                catClienteDet.Cte_FletePaga = 0;
                catClienteDet.Cte_Activo = false;
                catClienteDet.Cte_PorcComision = 0;
                catClienteDet.Cte_ModFecha = null;
                catClienteDet.Meta = null;
                catClienteDet.Cte_Tradicional = null;
                catClienteDet.Cte_Garantia = null;
                catClienteDet.Cte_Activo = true;

                maximoId += 1;
            }
            return cdCatClienteDet.Insertar(dets, s.Emp_Cnx_EF);
        }

        public void Remover(Sesion s, int idCte, int idTer)
        {
            CD_CatClienteDet cdCatClienteDet = new CD_CatClienteDet(s.Emp_Cnx);
            //Validar que no existan otras entidades asociadas a esta instancia
            CN_CrmPromocion cnCrmPromocion = new CN_CrmPromocion();
            var asociacionesCteTer = cnCrmPromocion.ObtenerPorClienteTerritorio(s, idCte, idTer);
            if (asociacionesCteTer.Count() > 0)
            {
                throw new ClienteTerritorioActualmenteAsociadoAProyectosException();
            }
            cdCatClienteDet.Eliminar(s.Id_Emp, s.Id_Cd, idCte, idTer, s.Emp_Cnx_EF);
        }        
    }

    public class ClienteTerritorioActualmenteAsociadoAProyectosException
        : Exception
    {
        public ClienteTerritorioActualmenteAsociadoAProyectosException()
            : base("El territorio se encuentra asociado actualmente a proyectos")
        {
        }

        public ClienteTerritorioActualmenteAsociadoAProyectosException(Exception innerException)
            : base("El territorio se encuentra asociado actualmente a proyectos", innerException)
        {
        }

        public ClienteTerritorioActualmenteAsociadoAProyectosException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
