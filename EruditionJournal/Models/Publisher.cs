using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EruditionJournal.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string PublisherFName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string PublisherLName { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string PublisherDisplayName { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
    }
}