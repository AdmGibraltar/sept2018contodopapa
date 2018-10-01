using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CatTerritorio
        : IDBContextHolder
    {
        public CatUEN CatUENSerializable
        {
            get;
            set;
        }

        public CatSegmento CatSegmentoSerializable
        {
            get;
            set;
        }

        /// <summary>
        /// Regresa la descripción del territorio con el formato: {Id} - {Nombre}, o establece su valor
        /// </summary>
        public string Nomenclatura
        {
            get
            {
                if (_nomenclatura == null)
                {
                    _nomenclatura = string.Format("{0} - {1}", Id_Ter, Ter_Nombre);
                }
                return _nomenclatura;
            }
            set
            {
                _nomenclatura = value;
            }
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public CatUsuario InfoRIKComoUsuario
        {
            get
            {
                if (Context != null)
                {
                    if (_InfoRIKComoUsuario == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var usuarios = from u in ctx.CatUsuarios
                                       where u.Id_Emp == this.Id_Emp
                                       && u.Id_Cd == this.Id_Cd
                                       && u.Id_Rik == this.Id_Rik
                                       select u;
                        if (usuarios.Count() > 0)
                        {
                            _InfoRIKComoUsuario = usuarios.First();
                        }
                    }
                }
                return _InfoRIKComoUsuario;
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

        private string _nomenclatura = null;
        private CatUsuario _InfoRIKComoUsuario = null;
    }
}
