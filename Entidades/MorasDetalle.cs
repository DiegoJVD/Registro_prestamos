using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_prestamos.Entidades
{
    


    public class MorasDetalle
    {
        [Key]
        public int MoraDetalleId { get; set; }
        public int MoraId { get; set; }
        public int PrestamoId { get; set; }
        public decimal Valor { get; set; }

        public MorasDetalle(int moraId, int prestamoId, decimal valor)
        {
            MoraId = moraId;
            PrestamoId = prestamoId;
            Valor = valor;
        }
    }
}