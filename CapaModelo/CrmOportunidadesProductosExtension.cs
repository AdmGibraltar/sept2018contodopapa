using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CapaModelo
{
    public partial class CrmOportunidadesProducto: IDBContextHolder
    {
        public String Descripcion
        {
            get;
            set;
        }

        public String Nombre
        {
            get;
            set;
        }

        public String Ruta
        {
            get;
            set;
        }

        public CatProducto ProductoSerializable
        {
            get;
            set;
        }

        /// <summary>
        /// Representa la entidad con información del precio actual del producto asociado.
        /// </summary>
        public ProductoPrecio ProductoActual
        {
            get;
            set;
        }

        [IgnoreDataMember]
        [ScriptIgnore]
        [JsonIgnore]
        public ProductoPrecio ProductoActual2
        {
            get
            {
                if (Context != null)
                {
                    if (_ProductoActual2 == null)
                    {
                        sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                        var prds = from p in ctx.ProductoPrecios
                                   where
                                   p.Id_Prd==Id_Prd
                                   && p.Id_Emp==Id_Emp
                                   && p.Id_Cd==Id_Cd
                                   && p.Id_Pre==1
                                   && p.Prd_Actual
                                   && p.Prd_FechaInicio<DateTime.Now
                                   && p.Prd_FechaFin>DateTime.Now
                                   select p;
                        if (prds.Count() > 0)
                        {
                            _ProductoActual2=prds.First();
                        }
                    }
                }
                return _ProductoActual2;
            }
        }

        public string DilucionCompuesta
        {
            get
            {
                if (COP_EsQuimico != null)
                {
                    if (COP_EsQuimico.Value)
                    {
                        if(COP_DilucionAntecedente!= null && COP_DilucionConsecuente!=null)
                            return string.Format("{0}:{1}", COP_DilucionAntecedente.Value.ToString(), COP_DilucionConsecuente.Value.ToString());
                    }
                }
                return string.Format("{0}:{1}", 1, 1);
            }
        }

        /// <summary>
        /// Cálculo del costo en uso cuando aplica para productos disolventes.
        /// </summary>
        public decimal CostoEnUso
        {
            get
            {
                if (COP_EsQuimico != null)
                {
                    if (COP_EsQuimico.Value)
                    {
                        if (ProductoActual != null)
                        {
                            if (ProductoActual.Prd_Pesos != null)
                            {
                                decimal precio = Convert.ToDecimal(ProductoActual.Prd_Pesos.Value);
                                if (ProductoSerializable.Prd_UniEmp != null)
                                {
                                    decimal unidadesPresentacion = Convert.ToDecimal(ProductoSerializable.Prd_UniEmp);
                                    if (COP_ConsumoMensual != null && COP_DilucionConsecuente != null)
                                    {
                                        decimal consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * COP_ConsumoMensual.Value) * (COP_DilucionConsecuente.Value + 1));
                                        if (consumoMensualEnLitrosDiluidos != 0.0M)
                                            return (COP_ConsumoMensual.Value * precio) / consumoMensualEnLitrosDiluidos;
                                    }
                                }
                            }
                        }
                    }
                }
                
                return 0.0M;
            }
        }

        public CapValProyectoDet CapValProyectoDet
        {
            get
            {
                if (_CapValProyectoDet == null)
                {
                    if (Context != null)
                    {
                        try
                        {
                            sianwebmty_gEntities ctx = Context as sianwebmty_gEntities;
                            var valuaciones = from valOp in ctx.CrmValuacionOportunidades
                                              where valOp.Id_Emp == Id_Emp && valOp.Id_Cd == Id_Cd && valOp.Id_Cte == Id_Cte && valOp.Id_Op == Id_Op && valOp.Id_Rik == Id_Rik
                                              select valOp;
                            if (valuaciones.Count() > 0)
                            {
                                var valuacion = valuaciones.First();
                                var productos = from pd in ctx.CapValProyectoDets
                                                where pd.Id_Emp == valuacion.CapValProyecto.Id_Emp && pd.Id_Cd == valuacion.CapValProyecto.Id_Cd &&
                                                pd.Id_Vap == valuacion.CapValProyecto.Id_Vap && pd.Id_Prd == Id_Prd
                                                select pd;
                                if (productos.Count() > 0)
                                {
                                    _CapValProyectoDet = productos.First();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                return _CapValProyectoDet;
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

        private CapValProyectoDet _CapValProyectoDet = null;
        private ProductoPrecio _ProductoActual2 = null;
    }
}
