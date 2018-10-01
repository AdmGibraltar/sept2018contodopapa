using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class crmCausasCancelacion
        : IDBContextHolder
    {
        public IEnumerable<crmCausasCancelacion> SubCausas
        {
            get
            {
                if (m_SubCausas == null)
                {
                    if (Context != null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        if (ctx != null)
                        {
                            m_SubCausas = from causa in ctx.crmCausasCancelacions
                                          where causa.CausaPadre==Id_Causa
                                          select causa;
                        }
                    }
                }
                return m_SubCausas;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]        
        public System.Data.Entity.DbContext Context
        {
            get;
            set;
        }

        protected IEnumerable<crmCausasCancelacion> m_SubCausas = null;
    }
}
