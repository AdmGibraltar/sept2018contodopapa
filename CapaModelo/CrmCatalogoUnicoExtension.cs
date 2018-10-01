using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CrmCatalogoUnico
        : IDBContextHolder
    {
        public String DescripcionProducto
        {
            get;
            set;
        }

        public CatProducto CatProductoSerializable
        {
            get
            {
                return CatProducto;
            }
        }

        //[IgnoreDataMember]
        //[ScriptIgnore]
        //[JsonIgnore]
        //public CatAplicacion CatAplicacion
        //{
        //    get
        //    {
        //        if (Context != null)
        //        {
        //            if (_CatAplicacion == null)
        //            {
        //                sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
        //                var aplicaciones = from a in ctx.CatAplicacions
        //                                   where a.Id_Emp==Id_Emp
        //                                   && a.Id_Apl==Id_Apl
        //                                   select a;
        //                if (aplicaciones.Count() > 0)
        //                {
        //                    _CatAplicacion = aplicaciones.First();
        //                }
        //            }
        //        }
        //        return _CatAplicacion;
        //    }
        //}

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public System.Data.Entity.DbContext Context
        {
            get;
            set;
        }

        //private CatAplicacion _CatAplicacion = null;
    }
}
