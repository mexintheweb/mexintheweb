using System.ComponentModel.DataAnnotations;

namespace mexintheweb.Data.Models
{
    public class BaseTableModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
