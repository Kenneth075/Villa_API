﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_KennyAPI.Model.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        //Applying validation to name.
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        public int Occupancy { get; set; }

        public int Sqft { get; set; }

    }
}
