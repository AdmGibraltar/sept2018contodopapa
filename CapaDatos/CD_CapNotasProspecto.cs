using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapNotasProspecto
    {
        public CapNotasProspecto Insertar(int idEmp, int idCd, int idRik, int idCte, int idNota, string cadenaConexionEF)
        {
            CapNotasProspecto result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                result = new CapNotasProspecto() { 
                    Id_Emp=idEmp, 
                    Id_Cd=idCd, 
                    Id_Rik=idRik, 
                    Id_Cliente=idCte, 
                    Id_Nota=idNota 
                };

                ctx.CapNotasProspectoes.Add(result);
                ctx.SaveChanges();
                result.CatNotaSerializable = result.CatNota;
            }
            return result;
        }

        public void Eliminar(int idEmp, int idCd, int idRik, int idCte, int idNota, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CapNotasProspectoes
                             where n.Id_Emp == idEmp && n.Id_Cd == idCd && n.Id_Rik == idRik && n.Id_Cliente == idCte && n.Id_Nota == idNota
                             select n).ToList();
                if (notas.Count > 0)
                {
                    ctx.CapNotasProspectoes.Remove(notas[0]);
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Regresa el conjunto de notas asociadas al prospecto con identificador de cliente idCte.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idRik"></param>
        /// <param name="idCte">Identificador del cliente asignado al prospecto</param>
        /// <param name="cadenaConexionEF">Cadena de conexión con formato para uso en EntityFramework</param>
        /// <returns>Conjunto de notas asociados al prospecto idCte</returns>
        public IEnumerable<CapNotasProspecto> ConsultarPorProspecto(int idEmp, int idCd, int idRik, int idCte, string cadenaConexionEF)
        {
            IEnumerable<CapNotasProspecto> result = null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CapNotasProspectoes
                             where n.Id_Emp == idEmp && n.Id_Cd == idCd && n.Id_Rik == idRik && n.Id_Cliente == idCte
                             select n).ToList().Select(cnp => { cnp.CatNotaSerializable = cnp.CatNota; return cnp; }).ToList();
                result = notas;
            }
            return result;
        }
    }
}
