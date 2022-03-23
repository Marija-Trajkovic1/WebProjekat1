using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Sediste")]
    public class Sediste
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression("\\d+")]
        [Range(1,100)]
        public int RedniBrojSedista { get; set; }

        [Required]
        public bool RezervisanoSediste { get; set; }

        [Required]
        [RegularExpression("\\w+")]
        [Range(1,100)]
        public string TipSedista {get; set;}

        public virtual Putnik SedistePutnik { get; set; }
        public virtual Let SedisteLet { get; set; }


    }
}