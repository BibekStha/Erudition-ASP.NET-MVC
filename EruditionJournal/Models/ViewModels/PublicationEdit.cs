using System.Collections.Generic;

namespace EruditionJournal.Models.ViewModels
{
    public class PublicationEdit
    {
        public PublicationEdit()
        {

        }

        public virtual Publication Publication { get; set; }
        public IEnumerable<Publisher> publishers { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}