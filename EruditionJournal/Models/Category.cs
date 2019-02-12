using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EruditionJournal.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CategoryName { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
    }
}