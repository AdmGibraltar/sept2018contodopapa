using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CapValProyecto
        : IDBContextHolder
    {
        public CatCliente CatClienteSerializable
        {
            get;
            set;
        }

        /// <summary>
        /// Carga la descripción representativa del área de aplicación de la valuación.
        /// NOTA TECNICA: se necesita que el contexto de conexión a la fuente de datos se encuentre activa y con vida en un contexto de carga floja (lazy load).
        /// </summary>
        public void CargarAreaAplicacion()
        {
            var crmOp = this.CrmValuacionOportunidades.Single();
            var opApl = crmOp.CrmOportunidade.CrmOportunidadesAplicacions.Single();
            _AreaAplicacion = string.Format("{0}/{1}/{2}", opApl.CatAplicacion.CatSolucion.CatArea.Area_Descripcion, opApl.CatAplicacion.CatSolucion.Sol_Descripcion, opApl.CatAplicacion.Apl_Descripcion);
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public string AreaAplicacion
        {
            get
            {
                if (_AreaAplicacion == null)
                {
                    var crmOp = this.CrmValuacionOportunidades.Single();
                    var opApl = crmOp.CrmOportunidade.CrmOportunidadesAplicacions.Single();
                    _AreaAplicacion = string.Format("{0}/{1}/{2}", opApl.CatAplicacion.CatSolucion.CatArea.Area_Descripcion, opApl.CatAplicacion.CatSolucion.Sol_Descripcion, opApl.CatAplicacion.Apl_Descripcion);
                }
                return _AreaAplicacion;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public IEnumerable<CapValProyectoDet> CapValProyectoDets
        {
            get
            {
                if (Context != null)
                {
                    if (_CapValProyectoDets == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        _CapValProyectoDets = from cvpd in ctx.CapValProyectoDets
                                              where cvpd.Id_Emp==Id_Emp && cvpd.Id_Cd==Id_Cd && cvpd.Id_Vap==Id_Vap
                                              select cvpd;
                    }
                }
                return _CapValProyectoDets;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CapAcy CapAcys
        {
            get
            {
                if (Context != null)
                {
                    if (_CapAcys == null)
                    {
                        var ctx = Context as sianwebmty_gEntities;
                        var acyss = from a in ctx.CapAcys
                                    where a.Id_Emp==Id_Emp && a.Id_Cd==Id_Cd && a.Id_Val==Id_Vap && a.Id_Rik==Id_Rik && a.Id_Cte==Id_Cte
                                    select a;
                        if (acyss.Count() > 0)
                        {
                            _CapAcys = acyss.First();
                        }
                    }
                }
                return _CapAcys;
            }
        }

        /// <summary>
        /// Indica si una valuación es positiva: true en caso de que así sea; false en caso contrario.
        /// </summary>
        public bool EsPositiva
        {
            get
            {
                if (Vap_ValorPresenteNeto == null || Vap_UtilidadRemanente == null)
                {
                    return false;
                }
                return Vap_ValorPresenteNeto > 0 && Vap_UtilidadRemanente > 0;
            }
        }

        protected string _AreaAplicacion = null;
        private IEnumerable<CapValProyectoDet> _CapValProyectoDets = null;
        private CapAcy _CapAcys = null;

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public System.Data.Entity.DbContext Context
        {
            get;
            set;
        }
    }
}
