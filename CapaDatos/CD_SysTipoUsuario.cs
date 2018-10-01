using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using System.Data.Objects.SqlClient;

namespace CapaDatos
{
    public class CD_SysTipoUsuario
    {
        public SysTipoUsuario ConsultarPorId(int idEmp, int idTipoUsuario, ICD_Contexto icdCtx)
        {
            SysTipoUsuario resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var tipoUsuarios = (from tu in ctx.SysTipoUsuarios
                                where tu.Id_Emp == idEmp && tu.Id_Tu == idTipoUsuario
                                select tu).ToList();
            if (tipoUsuarios.Count > 0)
            {
                resultado = tipoUsuarios[0];
            }
            return resultado;
        }

        public SysTipoUsuario ConsultarPorDescripcion(int idEmp, string descripcion, ICD_Contexto icdCtx)
        {
            SysTipoUsuario resultado = null;
            sianwebmty_gEntities ctx = ((ICD_Contexto<sianwebmty_gEntities>)icdCtx).Contexto;
            var tipos = from t in ctx.SysTipoUsuarios
                        where t.Id_Emp == idEmp && t.Tu_Descripcion.Contains(descripcion)
                        select t;
            if (tipos.Count() > 0)
            {
                resultado = tipos.First();
            }
            return resultado;
        }
    }
}
