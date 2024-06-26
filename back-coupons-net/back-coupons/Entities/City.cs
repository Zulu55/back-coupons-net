﻿using System.ComponentModel.DataAnnotations;

namespace back_coupons.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
        public int StateId { get; set; }
        public State? States { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
