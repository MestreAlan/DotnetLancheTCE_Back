using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }

        [Required]
        [StringLength(20)]
        public string? Andar { get; set; }

        [StringLength(20)]
        public string? Sala { get; set; }

        [StringLength(20)]
        public string? Departamento { get; set; }

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
