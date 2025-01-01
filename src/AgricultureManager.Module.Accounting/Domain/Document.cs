using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgricultureManager.Module.Accounting.Domain
{
    [Table("AccountingDocument")]
    public class Document
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountMouvementId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Documentname { get; set; } = string.Empty;
        [Required]
        public string Documentpath { get; set; } = string.Empty;
        [Column(TypeName = "longblob")]
        public byte[]? Content { get; set; }


        public AccountMouvement? AccountMouvement { get; set; }

    }
}
