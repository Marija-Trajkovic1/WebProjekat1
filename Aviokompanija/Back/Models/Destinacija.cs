using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    [Table("Destinacija")]
    public class Destinacija{
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string NazivDestinacije { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tip { get; set; }
        
        public virtual List<Let> DestinacijaLetovi { get; set; }

        public virtual Aerodrom DestinacijaAerodrom { get; set; }

    }
}