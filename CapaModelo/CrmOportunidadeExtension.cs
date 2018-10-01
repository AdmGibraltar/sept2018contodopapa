using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace CapaModelo
{
    public partial class CrmOportunidade
        : IDBContextHolder
    {
        /// <summary>
        /// Representa los productos asociados por captura manual a un proyecto
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public IEnumerable<CrmOportunidadesProducto> CrmOportunidadesProducto
        {
            get;
            set;
        }

        /// <summary>
        /// Representa los productos asociados por captura manual a un proyecto
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public IEnumerable<CrmOportunidadesProducto> CrmOportunidadesProducto2
        {
            get
            {
                if (Context != null)
                {
                    if (_CrmOportunidadesProducto2 == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var productos = from p in ctx.CrmOportunidadesProductos
                                        where p.Id_Emp==Id_Emp
                                        && p.Id_Cd==Id_Cd
                                        && p.Id_Cte==Id_Cte
                                        && p.Id_Op==Id_Op
                                        && p.Id_Rik==Id_Usu
                                        select p;
                        _CrmOportunidadesProducto2 = productos;
                    }
                }
                return _CrmOportunidadesProducto2;
            }
        }

        /// <summary>
        /// Representa la valuación asociada al proyecto
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CapValProyecto Valuacion
        {
            get;
            set;
        }

        /// <summary>
        /// Territorio asociado
        /// </summary>
        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CatTerritorio CatTerritorio
        {
            get
            {
                if (_catTerritorio == null)
                {
                    if (Context != null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var territorios = from t in ctx.CatTerritorios
                                          where t.Id_Emp == Id_Emp && t.Id_Cd == Id_Cd && t.Id_Ter == Id_Ter
                                          select t;
                        if (territorios.Count() > 0)
                        {
                            _catTerritorio = territorios.First();
                        }
                    }
                }
                return _catTerritorio;
            }
            set
            {
                _catTerritorio = value;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CatCliente CatCliente
        {
            get
            {
                if (Context != null)
                {
                    if (_CatCliente == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var clientes = from c in ctx.CatClientes
                                       where c.Id_Emp==Id_Emp
                                       && c.Id_Cd==Id_Cd
                                       && c.Id_Cte==Id_Cte
                                       select c;
                        if (clientes.Count() > 0)
                        {
                            _CatCliente = clientes.Single();
                        }
                    }
                }
                return _CatCliente;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CrmOportunidadesAplicacion CrmOportunidadesAplicacion
        {
            get
            {
                if (Context != null)
                {
                    if (_CrmOportunidadesAplicacion == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var aplicaciones = from ap in ctx.CrmOportunidadesAplicacions
                                           where ap.Id_Op==Id_Op
                                           && Id_Emp==Id_Emp
                                           && Id_Cd==Id_Cd
                                           select ap;
                        if (aplicaciones.Count() > 0)
                        {
                            _CrmOportunidadesAplicacion = aplicaciones.First();
                        }
                    }
                }
                return _CrmOportunidadesAplicacion;
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

        private CatTerritorio _catTerritorio = null;
        private CatCliente _CatCliente = null;
        private CrmOportunidadesAplicacion _CrmOportunidadesAplicacion = null;
        private IEnumerable<CrmOportunidadesProducto> _CrmOportunidadesProducto2 = null;
    }
}
