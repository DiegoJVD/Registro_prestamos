using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_prestamos.Entidades
{
    

public class Personas{
        [Key]
        public int PersonaId { get; set; }

        public string Nombres { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey("PersonaId")]
        public virtual List<Prestamos> Prestamos { get; set; }

    }

}