using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class AdendaDet
    {
        int _longitud;

        public int Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
        string _campo;

        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        string _nodo;

        public string Nodo
        {
            get { return _nodo; }
            set { _nodo = value; }
        }

        string _valor;

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        int _tipo;

        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        int _id;

        public int Id_AdeDet
        {
            get { return _id; }
            set { _id = value; }
        }

        int? _id_Prd;

        public int? Id_Prd
        {
            get { return _id_Prd; }
            set { _id_Prd = value; }
        }

        int? _id_Ter;
        private bool _Requerido;

        public bool Requerido
        {
            get { return _Requerido; }
            set { _Requerido = value; }
        }

        public int? Id_Ter
        {
            get { return _id_Ter; }
            set { _id_Ter = value; }
        }
    }
}
