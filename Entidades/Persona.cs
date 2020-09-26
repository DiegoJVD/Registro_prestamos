using System.ComponentModel.DataAnnotations;
using System;

namespace Registro_prestamos.Entidades
{
    

public class Persona{
        [Key]
        public int PersonaId { get; set; }

        public string Nombres { get; set; }

        public decimal Balance { get; set; }

    }

}