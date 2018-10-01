using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CatBiblioteca
    {
        public CatBiblioteca ConsultarPorUsuario(int idEmp, int idCd, int idU, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from cb in ctx.CapBibliotecaUsuarios
                           where cb.Id_Emp==idEmp && cb.Id_Cd==idCd && cb.Id_U==idU
                           select cb.CatBiblioteca;
            if (entradas.Count() > 0)
            {
                return entradas.First();
            }
            return null;
        }

        public IQueryable<CapBibliotecaUsuario> ConsultarBibliotecasPorUsuario(int idEmp, int idCd, int idU, ICD_Contexto icdCtx)
        {
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var entradas = from cb in ctx.CapBibliotecaUsuarios
                           where cb.Id_Emp == idEmp && cb.Id_Cd == idCd && cb.Id_U == idU
                           select cb;
            return entradas;
        }
    }
}
