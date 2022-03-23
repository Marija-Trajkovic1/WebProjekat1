using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Let")]
    public class Let{
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime VremePoletanja { get; set; }

        [Required]
        public DateTime VremeSletanja { get; set; }

        [Required]
        [RegularExpression("\\d+")]
        [Range(1,250)]
        public int UkupanBrojSedista { get; set; }

        [Required]
        public int BrojZauzetih { get; set; }
        public virtual List<Sediste> LetoviSedista{ get; set; }
        public virtual Destinacija LetoviDestinacije { get; set; }

    }
}