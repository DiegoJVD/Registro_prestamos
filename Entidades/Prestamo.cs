using System.ComponentModel.DataAnnotations;
using System;

namespace Registro_prestamos.Entidades
{
    

public class Prestamo{
        [Key]
        public int PrestamoId { get; set; }

        public DateTime Fecha { get; set; }

        public int PersonaId { get; set; }

        public string Concepto { get; set; }

        public decimal monto { get; set; }

        public decimal Balance { get; set; }

    }

}