using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CatRik
        : IDBContextHolder
    {
        public CatUsuario CatUsuario
        {
            get
            {
                if (Context != null)
                {
                    if (_catUsuario == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var usuario = from u in ctx.CatUsuarios
                                      where u.Id_Rik==Id_Rik && u.Id_Emp==Id_Emp && u.Id_Cd==Id_Cd
                                      select u;
                        if (usuario.Count() > 0)
                        {
                            _catUsuario = usuario.First();
                        }
                    }
                }
                return _catUsuario;
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

        private CatUsuario _catUsuario = null;
    }
}
