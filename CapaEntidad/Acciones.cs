using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CapaEntidad
{
    public class Acciones 
    {
        private string _Etapa;
        public string Etapa
        {
            get { return _Etapa; }
            set { _Etapa = value; }
        }
        
        private double? _Dias;
        public double? Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }
        
        private string _Tipo_Respuesta;
        public string Tipo_Respuesta
        {
            get { return _Tipo_Respuesta; }
            set { _Tipo_Respuesta = value; }
        }
        
        private string _Pregunta;
        public string Pregunta
        {
            get { return _Pregunta; }
            set { _Pregunta = value; }
        }
        
        private ArrayList _Respuestas;
        public ArrayList Respuestas
        {
            get { return _Respuestas; }
            set { _Respuestas = value; }
        }
        
        private string _RespuestasStr;
        public string RespuestasStr
        {
            get { return _RespuestasStr; }
            set { _RespuestasStr = value; }
        }
        
        private string _Tipo_RespuestaStr;
        public string Tipo_RespuestaStr
        {
            get { return _Tipo_RespuestaStr; }
            set { _Tipo_RespuestaStr = value; }
        }
        
        private string _EtapaStr;
        public string EtapaStr
        {
            get { return _EtapaStr; }
            set { _EtapaStr = value; }
        }

        private string _GUID;
        public int Id_Conf;

        public string GUID
        {
            get { return _GUID; }
            set { _GUID = value; }
        }
    }
}
