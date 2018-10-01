using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using CapaModelo;
using System.Data.SqlClient;
using System;

namespace CapaDatos
{
    public class CD_CatClienteDetGarantia
    {
        public CD_CatClienteDetGarantia(String cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

        public List<CatClienteDetGarantia> ObtenerTodos(int? Id_Emp, int? Id_Cd, int? Id_Cte, int? Id_CteDet)
        {
            List<CatClienteDetGarantia> ret = null;
            
            using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                ret = ctx.spCatClienteDetGarantia_ConsultarPorClienteTerritorio(Id_Emp, Id_Cd, Id_Cte, Id_CteDet).ToList();
            }
            return ret;
        }

        public List<CatClienteDetGarantia> ObtenerTodos_NOEF(int? Id_Emp, int? Id_Cd, int? Id_Cte, int? Id_CteDet)
        {
            List<CatClienteDetGarantia> lst = new List<CatClienteDetGarantia>();
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(_cadenaDeConexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Cte", "Id_CteDet" };
                object[] Valores = { Id_Emp, Id_Cd, Id_Cte, Id_CteDet };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteDetGarantia_ConsultarPorClienteTerritorio", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    CatClienteDetGarantia obj = new CatClienteDetGarantia();

                    obj.Id_Emp = dr.GetOrdinal("Id_Emp");
                    obj.Id_Cd = dr.GetOrdinal("Id_Cd");
                    obj.Id_Cte = dr.GetOrdinal("Id_Cte");
                    obj.Id_CteDet = dr.GetOrdinal("Id_CteDet");
                    obj.Id_TG = dr.GetOrdinal("Id_TG");
                    obj.Id_CteDetGtia = dr.GetOrdinal("Id_CteDetGtia");
                    lst.Add(obj);
                }
                //CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                lst = null;
            }
            return lst;
        }
        
        public void Insertar(int? Id_Emp, int? Id_Cd, int? Id_Cte, int? Id_CteDet, int? Id_TG, DbTransaction tran, DbConnection connection)
        {
            using (sianwebmty_gEntities outterCtx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                MetadataWorkspace mdw=((IObjectContextAdapter)outterCtx).ObjectContext.MetadataWorkspace;
                EntityConnection ec = new EntityConnection(mdw, connection);
                using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(ec, false))
                {
                    ctx.Database.UseTransaction(tran);
                    //Esto debería estar en la capa de negocio
                    ctx.spCatClienteDetGarantia_Insertar(Id_Emp, Id_Cd, Id_Cte, Id_CteDet, Id_TG);
                }
            }
            
        }

        /**
         *Elimina todas las garantías asociadas a un territorio 
         **/
        public void Eliminar(int? Id_Emp, int? Id_Cd, int? Id_Cte, int? Id_CteDet, sianwebmty_gEntities ctx)
        {
            ctx.spCatClienteDetGarantia_Eliminar(Id_Emp, Id_Cd, Id_Cte, Id_CteDet);
        }

        /**
         *Elimina todas las garantías asociadas a un territorio 
         **/
        public void Eliminar(int? Id_Emp, int? Id_Cd, int? Id_Cte, int? Id_CteDet, DbTransaction tran, DbConnection connection)
        {
            using (sianwebmty_gEntities outterCtx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                MetadataWorkspace mdw = ((IObjectContextAdapter)outterCtx).ObjectContext.MetadataWorkspace;
                EntityConnection ec = new EntityConnection(mdw, connection);
                using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(ec, false))
                {
                    ctx.Database.UseTransaction(tran);
                    ctx.spCatClienteDetGarantia_Eliminar(Id_Emp, Id_Cd, Id_Cte, Id_CteDet);
                }
            }
        }

        private String _cadenaDeConexion;
    }
}
