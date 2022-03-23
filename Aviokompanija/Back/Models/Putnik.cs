using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Putnik")]
    public class Putnik
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression("\\d+")]
        [Range(100000000,999999999)]
        public int BrojPasosa { get; set; }

        [Required]
        [RegularExpression("\\w+")]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [RegularExpression("\\w+")]
        [MaxLength(20)]
        public string Prezime { get; set; }

        [Required]
        [Range(0,100)]
        [RegularExpression("\\d+")]
        public int TezinaPrtljagaUKg { get; set; }
        
        public virtual List<Sediste> Sedista { get; set; }

        

    }
}