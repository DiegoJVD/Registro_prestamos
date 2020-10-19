using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_prestamos.Entidades
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("MoraId")]
        public virtual List<MorasDetalle> Detalle { get; set; }

        public Moras()
        {
            Fecha = DateTime.Now;
            Detalle = new List<MorasDetalle>();
        }
    }


}