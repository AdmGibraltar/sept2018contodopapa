using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using CapaModelo;
namespace CapaDatos
{
    public class CD_CapAcysExt
    {
        public CD_CapAcysExt(string cadenaDeConexion)
        {
            _cadenaDeConexion = cadenaDeConexion;
        }

        public void Insertar(Nullable<int> id_Emp, Nullable<int> id_Cd, Nullable<int> id_Acs, Nullable<int> id_AcsVersion, Nullable<int> id_TV, Nullable<int> id_TG, DbTransaction tran, DbConnection connection)
        {
            using (sianwebmty_gEntities outterCtx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                MetadataWorkspace mdw = ((IObjectContextAdapter)outterCtx).ObjectContext.MetadataWorkspace;
                EntityConnection ec = new EntityConnection(mdw, connection);
                using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(ec, false))
                {
                    ctx.Database.UseTransaction(tran);
                    //Esto debería estar en la capa de negocio
                    ctx.spCapAcysExt_Insertar(id_Emp, id_Cd, id_Acs, id_AcsVersion, id_TV, id_TG);
                }
            }
        }

        public void Insertar(Nullable<int> id_Emp, Nullable<int> id_Cd, Nullable<int> id_Acs, Nullable<int> id_AcsVersion, Nullable<int> id_TV, IEnumerable<Nullable<int>> id_TGs, DbTransaction tran, DbConnection connection)
        {
            using (sianwebmty_gEntities outterCtx = new sianwebmty_gEntities(_cadenaDeConexion))
            {
                MetadataWorkspace mdw = ((IObjectContextAdapter)outterCtx).ObjectContext.MetadataWorkspace;
                EntityConnection ec = new EntityConnection(mdw, connection);
                using (sianwebmty_gEntities ctx = new sianwebmty_gEntities(ec, false))
                {
                    ctx.Database.UseTransaction(tran);
                    foreach (var id_TG in id_TGs)
                    {
                        ctx.spCapAcysExt_Insertar(id_Emp, id_Cd, id_Acs, id_AcsVersion, id_TV, id_TG);
                    }
                }
            }
        }

        private string _cadenaDeConexion = null;
    }
}
