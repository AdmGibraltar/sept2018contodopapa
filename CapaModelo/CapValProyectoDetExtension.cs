using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CapValProyectoDet 
        : IDBContextHolder
    {
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public System.Data.Entity.DbContext Context
        {
            get;
            set;
        }


        /// <summary>
        /// CatProducto asociado al detalle de la valuación
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CatProducto CatProducto
        {
            get
            {
                if (Context != null)
                {
                    if (_CatProducto == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var productos = from p in ctx.CatProductoes
                                        where p.Id_Emp==Id_Emp && p.Id_Prd==Id_Prd
                                        select p;
                        if (productos.Count() > 0)
                        {
                            _CatProducto = productos.First();
                        }
                    }
                }
                return _CatProducto;
            }
        }

        private CatProducto _CatProducto = null;
    }
}
