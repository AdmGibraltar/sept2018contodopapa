using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CapAcy
        : IDBContextHolder
    {
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public IEnumerable<CapAcysDet> CapAcysDets
        {
            get
            {
                if (Context != null)
                {
                    if (_CapAcysDets == null)
                    {
                        var ctx = Context as sianwebmty_gEntities;
                        _CapAcysDets = from cad in ctx.CapAcysDets
                                       where 
                                       cad.Id_Emp==Id_Emp
                                       && cad.Id_Cd==Id_Cd
                                       && cad.Id_Acs==Id_Acs
                                       && cad.Id_AcsVersion==Id_AcsVersion
                                       && cad.Id_Ter==Id_Ter
                                       select cad;
                    }
                }
                return _CapAcysDets;
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

        private IEnumerable<CapAcysDet> _CapAcysDets = null;
    }
}
