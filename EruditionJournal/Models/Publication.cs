using System;
using System.ComponentModel.DataAnnotations;

namespace EruditionJournal.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string PublicationTitle { get; set; }

        [Required]
        [StringLength(750)]
        [Display(Name = "Abstract")]
        public string PublicationAbstract { get; set; }

        [Required]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; set; }

        //Manuscript(Submitted Paper) check
        // 0 => Not submitted
        // 1 => Submitted [All entries are required to have 1]
        [Required]
        public int HasManuscript { get; set; }

        // Each publication has one publisher
        public virtual Publisher Publisher { get; set; }

        // Each publication can be categorized into one category
        public virtual Category Category { get; set; }
    }
}