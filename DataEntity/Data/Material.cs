using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Data
{
    // Pravý alt (alt gr) + f/g
    [AddINotifyPropertyChangedInterface]
    [Table("Material")]
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(60)]
        public string Nazev { get; set; } = string.Empty;

        public int MnozPoj { get; set; } = 0;

        public int MnozDoPal { get; set; } = 0;

        [Column(TypeName ="date")]
        public DateTime Datum { get; set; } = DateTime.Now;

        public string Komentar { get; set; } = string.Empty;

        public int MernaJednotkaId { get; set; }

        [ForeignKey(nameof(MernaJednotkaId))]
        public virtual required MernaJednotka MernaJednotka { get; set; } = null!;

    }
}
