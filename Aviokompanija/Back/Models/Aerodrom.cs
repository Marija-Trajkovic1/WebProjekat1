using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Aerodrom")]
    public class Aerodrom{
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string NazivAerodroma { get; set; }

        [Required]
        [MaxLength(20)]
        public string Lokacija { get; set; }


        [JsonIgnore]
        public virtual List<Destinacija> AerodromDestinacije { get; set; }




    }
}