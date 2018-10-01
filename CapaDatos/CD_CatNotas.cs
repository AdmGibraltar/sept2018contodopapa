using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatNotas
    {
        public CatNota Insertar(string texto, string cadenaConexionEF)
        {
            CatNota result=null;
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                result = new CatNota(){ Texto=texto };
                ctx.CatNotas.Add(result);
                ctx.SaveChanges();
            }
            return result;
        }

        public void Eliminar(int idNota, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CatNotas
                             where n.Id_Nota == idNota
                             select n).ToList();
                if (notas.Count > 0)
                {
                    ctx.CatNotas.Remove(notas[0]);
                    ctx.SaveChanges();
                }
            }
        }

        public void Actualizar(int idNota, string texto, string cadenaConexionEF)
        {
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(cadenaConexionEF))
            {
                var notas = (from n in ctx.CatNotas
                             where n.Id_Nota == idNota
                             select n).ToList();
                if (notas.Count > 0)
                {
                    notas[0].Texto = texto;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
