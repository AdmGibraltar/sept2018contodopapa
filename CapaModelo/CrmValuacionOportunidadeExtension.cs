using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CrmValuacionOportunidade : IDBContextHolder
    {
        public CapValProyecto CapValProyectoSerializable
        {
            get;
            set;
        }

        /// <summary>
        /// Referencia al contexto de conexión a la fuente de datos
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public System.Data.Entity.DbContext Context
        {
            get;
            set;
        }

        /// <summary>
        /// Valuación global asociada a la valuación
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CapValuacionGlobalCliente CapValuacionGlobalCliente
        {
            get
            {
                if (_capValuacionGlobalCliente == null)
                {
                    sianwebmty_gEntities ctx=Context as sianwebmty_gEntities;
                    var valGlobales = from cvgc in ctx.CapValuacionGlobalClientes
                                      where cvgc.Id_Emp==this.Id_Emp && cvgc.Id_Cd==this.Id_Cd && cvgc.Id_Cte==this.Id_Cte && cvgc.Id_Vap==this.Id_Val
                                      select cvgc;
                    if (valGlobales.Count() > 0)
                    {
                        _capValuacionGlobalCliente = valGlobales.First();
                    }
                }
                return _capValuacionGlobalCliente;
            }
        }

        private CapValuacionGlobalCliente _capValuacionGlobalCliente=null;
    }
}
