using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace CapaModelo
{
    public partial class sianwebmty_gEntities
    {
        partial void Setup()
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += new System.Data.Entity.Core.Objects.ObjectMaterializedEventHandler(ObjectContext_ObjectMaterialized);
        }

        void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            IDBContextHolder idbContextHolder = e.Entity as IDBContextHolder;
            if (idbContextHolder != null)
            {
                idbContextHolder.Context = this;
            }
        }
    }
}
