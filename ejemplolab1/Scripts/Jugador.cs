using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ejemplolab1.Models
{
    public class Jugador
    {
        public int Jugadorid{get;set;}
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public double Salario { get; set; }
        public string Posicion { get; set; }
        public string Club { get; set; }
    }
}