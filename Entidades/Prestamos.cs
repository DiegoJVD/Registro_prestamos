using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_prestamos.Entidades
{
    

public class Prestamos{
        [Key]
        public int PrestamoId { get; set; }

        public DateTime Fecha { get; set; }

        public int PersonaId { get; set; }

        public string Concepto { get; set; }

        public decimal monto { get; set; }

        public decimal Balance { get; set; }

        public decimal Mora {get; set; }

        public Prestamos(){
            Mora  = 0;
        }

        [ForeignKey("PrestamoId")]
        public virtual List<MorasDetalle> Detalle { get; set; }

    }

}