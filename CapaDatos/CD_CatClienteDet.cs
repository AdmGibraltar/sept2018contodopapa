using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
namespace CapaDatos
{
    public class CD_CatClienteDet
    {
        public CD_CatClienteDet(string cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

        public Decimal? ConsultarAcumuladoDeRemisiones(int? idEmp, int? idCd, int? idTer, int? idCte)
        {
            Decimal? resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                var res = ctx.spConsultarAcumuladoDeRemisiones(idEmp, idCd, idTer, idCte).ToList();
                if (res.Count > 0)
                {
                    resultado = res[0];
                }
            }
            return resultado;
        }

        /// <summary>
        /// Este método regresa el conjunto de remisiones con estado "Abiertas" en el periodo actual.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idTer">Identificador del territorio</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>El conjunto de remisiones con estado abierto en el periodo actual.</returns>
        public List<CapRemision> ConsultarRemisionesEnPeriodo(int? idEmp, int? idCd, int? idTer, int? idCte)
        {
            List<CapRemision> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                resultado = ctx.spConsultarRemisionesEnPeriodo(idEmp, idCd, idTer, idCte).ToList();
            }
            return resultado;
        }

        /// <summary>
        /// Este método regresa el conjunto de remisiones con estado "Abiertas" en el periodo actual. Logra el mismo resultado que ConsultarRemisionesEnPeriodo.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa</param>
        /// <param name="idCd">Identificador del centro de distribución</param>
        /// <param name="idTer">Identificador del territorio</param>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>El conjunto de remisiones con estado abierto en el periodo actual.</returns>
        public List<CapRemision> ObtenerRemisionesAbiertasPeriodoActual(int? idEmp, int? idCd, int? idTer, int? idCte)
        {
            List<CapRemision> resultado = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                resultado = ctx.spCatClienteDet_ObtenerRemisionesAbiertasPeriodoActual(idEmp, idCd, idTer, idCte).ToList();
            }
            return resultado;
        }

        /// <summary>
        /// Inserta un conjunto de nuevos registros en CatTerritorios con la mínima información necesaria para satisfacer las restricciones de llaves.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa asociada el cliente idCte</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idCte">Identificador del cliente al que se le asocia los territorios</param>
        /// <param name="idRik">Identificador del representante asociado a los territorios</param>
        /// <param name="territorios">Conjunto de territorios a asociar al cliente idCte</param>
        /// <param name="cadenaDeConexionEF"></param>
        public void InsertarBasico(int idEmp, int idCd, int idCte, int idRik, int[] territorios, string cadenaDeConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var ters = (from t in ctx.CatTerritorios
                            where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik && territorios.Contains(t.Id_Ter)
                            select t).ToList();
                int catCteDet = 0;
                var existentes = Consultar(idEmp, idCd, idCte, ctx).ToList();
                catCteDet = existentes.Count;
                foreach (var t in ters)
                {
                    CatClienteDet catClienteDet = new CatClienteDet();
                    catClienteDet.Id_Emp = idEmp;
                    catClienteDet.Id_Cd = idCd;
                    catClienteDet.Id_Cte = idCte;
                    catClienteDet.Id_CteDet = catCteDet;
                    catClienteDet.Id_Ter = t.Id_Ter;
                    catClienteDet.Id_Seg = t.Id_Seg;
                    catClienteDet.Cte_UnidadDim = null;
                    catClienteDet.Cte_Dimension = null;
                    catClienteDet.Cte_Pesos = 0;
                    catClienteDet.Cte_Potencial = 0;
                    catClienteDet.Cte_CarMP = 0;
                    catClienteDet.Cte_GasVarT = 0;
                    catClienteDet.Cte_FletePaga = 0;
                    catClienteDet.Cte_Activo = false;
                    catClienteDet.Cte_PorcComision = 0;
                    catClienteDet.Cte_ModFecha = null;
                    catClienteDet.Meta = null;
                    catClienteDet.Cte_Tradicional = null;
                    catClienteDet.Cte_Garantia = null;
                    ctx.CatClienteDets.Add(catClienteDet);
                    catCteDet++;
                }

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Inserta un conjunto de nuevos registros en CatTerritorios con la mínima información necesaria para satisfacer las restricciones de llaves. Versión transaccional.
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa asociada el cliente idCte</param>
        /// <param name="idCd">Identificador del centro de distribución asociado a la empresa idEmp</param>
        /// <param name="idCte">Identificador del cliente al que se le asocia los territorios</param>
        /// <param name="idRik">Identificador del representante asociado a los territorios</param>
        /// <param name="territorios">Conjunto de territorios a asociar al cliente idCte</param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        public void InsertarBasico(int idEmp, int idCd, int idCte, int idRik, int[] territorios, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var ters = (from t in ctx.CatTerritorios
                        where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik && territorios.Contains(t.Id_Ter)
                        select t).ToList();
            int catCteDet = 0;
            var existentes = Consultar(idEmp, idCd, idCte, ctx).ToList();
            catCteDet = existentes.Count;
            foreach (var t in ters)
            {
                CatClienteDet catClienteDet = new CatClienteDet();
                catClienteDet.Id_Emp = idEmp;
                catClienteDet.Id_Cd = idCd;
                catClienteDet.Id_Cte = idCte;
                catClienteDet.Id_CteDet = catCteDet;
                catClienteDet.Id_Ter = t.Id_Ter;
                catClienteDet.Id_Seg = t.Id_Seg;
                catClienteDet.Cte_UnidadDim = null;
                catClienteDet.Cte_Dimension = null;
                catClienteDet.Cte_Pesos = 0;
                catClienteDet.Cte_Potencial = 0;
                catClienteDet.Cte_CarMP = 0;
                catClienteDet.Cte_GasVarT = 0;
                catClienteDet.Cte_FletePaga = 0;
                catClienteDet.Cte_Activo = true;
                catClienteDet.Cte_PorcComision = 0;
                catClienteDet.Cte_ModFecha = null;
                catClienteDet.Meta = null;
                catClienteDet.Cte_Tradicional = null;
                catClienteDet.Cte_Garantia = null;
                ctx.CatClienteDets.Add(catClienteDet);
                catCteDet++;
            }
        }

        public void InsertarBasico(int idEmp, int idCd, int idCte, int idRik, List<CrmProspecto.ProspectoTerritorioViewPOST> territorios, string cadenaDeConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var terIds = (from t in territorios
                              select t.Id_Ter).ToList();
                var objTers = (from t in ctx.CatTerritorios
                               join ter in terIds
                               on new { Id_Ter = t.Id_Ter } equals new { Id_Ter = ter }
                               where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik // && tersIds.Contains(t.Id_Ter)
                               select t).ToList();
                var ters = (from t in objTers
                            join ter in territorios
                            on new { Id_Ter = t.Id_Ter } equals new { Id_Ter = ter.Id_Ter }
                            where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik
                            select new { Territorio = t, ProspectoTerritorio = ter }).ToList();
                int catCteDet = 0;
                var existentes = Consultar(idEmp, idCd, idCte, ctx).ToList();
                catCteDet = existentes.Count;
                foreach (var t in ters)
                {
                    CatClienteDet catClienteDet = new CatClienteDet();
                    catClienteDet.Id_Emp = idEmp;
                    catClienteDet.Id_Cd = idCd;
                    catClienteDet.Id_Cte = idCte;
                    catClienteDet.Id_CteDet = catCteDet;
                    catClienteDet.Id_Ter = t.Territorio.Id_Ter;
                    catClienteDet.Id_Seg = t.Territorio.Id_Seg;
                    catClienteDet.Cte_UnidadDim = null;
                    catClienteDet.Cte_Dimension = null;
                    catClienteDet.Cte_Pesos = 0;
                    catClienteDet.Cte_Potencial = t.ProspectoTerritorio.VPO;
                    catClienteDet.Cte_CarMP = 0;
                    catClienteDet.Cte_GasVarT = 0;
                    catClienteDet.Cte_FletePaga = 0;
                    catClienteDet.Cte_Activo = false;
                    catClienteDet.Cte_PorcComision = 0;
                    catClienteDet.Cte_ModFecha = null;
                    catClienteDet.Meta = null;
                    catClienteDet.Cte_Tradicional = null;
                    catClienteDet.Cte_Garantia = null;
                    ctx.CatClienteDets.Add(catClienteDet);
                    catCteDet++;
                }

                ctx.SaveChanges();
            }
        }

        public void InsertarBasico(int idEmp, int idCd, int idCte, int idRik, List<CrmProspecto.ProspectoTerritorioViewPOST> territorios, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var terIds = (from t in territorios
                              select t.Id_Ter).ToList();
            var objTers = (from t in ctx.CatTerritorios
                            join ter in terIds
                            on new { Id_Ter = t.Id_Ter } equals new { Id_Ter = ter }
                            where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik // && tersIds.Contains(t.Id_Ter)
                            select t).ToList();
            var ters = (from t in objTers
                        join ter in territorios
                        on new { Id_Ter = t.Id_Ter } equals new { Id_Ter = ter.Id_Ter }
                        where t.Id_Emp == idEmp && t.Id_Cd == idCd && t.Id_Rik == idRik
                        select new { Territorio = t, ProspectoTerritorio = ter }).ToList();
            int catCteDet = 0;
            var existentes = Consultar(idEmp, idCd, idCte, ctx).ToList();
            catCteDet = existentes.Count;
            foreach (var t in ters)
            {
                CatClienteDet catClienteDet = new CatClienteDet();
                catClienteDet.Id_Emp = idEmp;
                catClienteDet.Id_Cd = idCd;
                catClienteDet.Id_Cte = idCte;
                catClienteDet.Id_CteDet = catCteDet;
                catClienteDet.Id_Ter = t.Territorio.Id_Ter;
                catClienteDet.Id_Seg = t.Territorio.Id_Seg;
                catClienteDet.Cte_UnidadDim = null;
                catClienteDet.Cte_Dimension = null;
                catClienteDet.Cte_Pesos = 0;
                catClienteDet.Cte_Potencial = t.ProspectoTerritorio.VPO;
                catClienteDet.Cte_CarMP = 0;
                catClienteDet.Cte_GasVarT = 0;
                catClienteDet.Cte_FletePaga = 0;
                catClienteDet.Cte_Activo = true;
                catClienteDet.Cte_PorcComision = 0;
                catClienteDet.Cte_ModFecha = null;
                catClienteDet.Meta = null;
                catClienteDet.Cte_Tradicional = null;
                catClienteDet.Cte_Garantia = null;
                ctx.CatClienteDets.Add(catClienteDet);
                catCteDet++;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro en CatTerritorios con la mínima información necesaria para satisfacer las restricciones de llaves.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idCte"></param>
        /// <param name="idRik"></param>
        /// <param name="idTer"></param>
        /// <param name="idSeg"></param>
        /// <param name="vpo"></param>
        /// <param name="cadenaDeConexionEF"></param>
        public CatClienteDet InsertarBasico(int idEmp, int idCd, int idCte, int idRik, int idTer, int idSeg, double vpo, string cadenaDeConexionEF)
        {
            CatClienteDet catClienteDet = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                int catCteDet = 0;
                try
                {
                    catCteDet = Consultar(idEmp, idCd, idCte, ctx).Max(ccd => ccd.Id_CteDet) + 1;
                }
                catch (InvalidOperationException ioe) //sequence contains no elements
                {
                    catCteDet = 0;
                }

                catClienteDet = new CatClienteDet();
                catClienteDet.Id_Emp = idEmp;
                catClienteDet.Id_Cd = idCd;
                catClienteDet.Id_Cte = idCte;
                catClienteDet.Id_CteDet = catCteDet;
                catClienteDet.Id_Ter = idTer;
                catClienteDet.Id_Seg = idSeg;
                catClienteDet.Cte_UnidadDim = null;
                catClienteDet.Cte_Dimension = null;
                catClienteDet.Cte_Pesos = 0;
                catClienteDet.Cte_Potencial = vpo;
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
                ctx.CatClienteDets.Add(catClienteDet);

                ctx.SaveChanges();

                ctx.Entry(catClienteDet).Reference(ccd => ccd.CatTerritorio).Load();
                catClienteDet.CatTerritorioSerializable = catClienteDet.CatTerritorio;
            }
            return catClienteDet;
        }

        /// <summary>
        /// Devuelve el siguiente índice disponible para la llave [Id_CteDet] de la entidad [CatClienteDet].
        /// </summary>
        /// <param name="idEmp">Identificador de la empresa a la que pertenece el cliente</param>
        /// <param name="idCd">Identificador del centro de distribución a la que pertenece el cliente</param>
        /// <param name="idCte">Identificador del cliente de interés</param>
        /// <param name="cadenaConexionEF">Cadena de conexión a la fuente de datos con formato de Entity Framework.</param>
        /// <returns>int. Siguiente índice disponible.</returns>
        public int ConsultarMaximoId(int idEmp, int idCd, int idCte, string cadenaConexionEF)
        {
            int catCteDet = 0;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                try
                {
                    catCteDet = Consultar(idEmp, idCd, idCte, ctx).Max(ccd => ccd.Id_CteDet) + 1;
                }
                catch (InvalidOperationException ioe)
                {
                    //sequence has no elements: its ok.
                    catCteDet = 0;
                }
                catch (Exception ex)
                {
                    //any other case its interpreted as a general error. Throw it.
                    throw ex;
                }

            }
            return catCteDet;
        }

        public IEnumerable<CatClienteDet> Insertar(CapaModelo.CatClienteDet[] dets, string cadenaDeConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                ctx.CatClienteDets.AddRange(dets);
                ctx.SaveChanges();

                foreach (var d in dets)
                {
                    ctx.Entry(d).Reference(ccd => ccd.CatTerritorio).Load();
                    d.CatTerritorioSerializable = d.CatTerritorio;
                }
            }
            return dets;
        }

        public IEnumerable<CatClienteDet> Consultar(int idEmp, int idCd, int idCte, string cadenaDeConexionEF)
        {
            IEnumerable<CatClienteDet> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var detalles = (from d in ctx.CatClienteDets
                                where d.Id_Emp == idEmp && d.Id_Cd == idCd && d.Id_Cte == idCte && d.Cte_Activo == true
                                select d).ToList().Select(ccd => { ccd.CatTerritorioSerializable = ccd.CatTerritorio; return ccd; }).ToList();
                result = detalles;
            }
            return result;
        }

        public void Eliminar(int idEmp, int idCd, int idCte, int idTer, string cadenaDeConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaDeConexionEF))
            {
                var ccds = (from ccd in ctx.CatClienteDets
                            where ccd.Id_Emp == idEmp && ccd.Id_Cd == idCd && ccd.Id_Cte == idCte && ccd.Id_Ter == idTer
                            select ccd).ToList();
                if (ccds.Count > 0)
                {
                    ctx.CatClienteDets.Remove(ccds[0]);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Versión interna de [Consultar], que aprovecha la existencia de un contexto de acceso a datos.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idCte"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        internal IEnumerable<CatClienteDet> Consultar(int idEmp, int idCd, int idCte, sianwebmty_gEntities ctx)
        {
            var detalles = (from d in ctx.CatClienteDets
                            where d.Id_Emp == idEmp && d.Id_Cd == idCd && d.Id_Cte == idCte
                            select d).ToList();
            return detalles;
        }

        private string _cadenaDeConexion;
    }
}
