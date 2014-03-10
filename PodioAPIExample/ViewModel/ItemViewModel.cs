using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodioAPIExample.ViewModel
{
    public class ItemViewModel
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string Link { get; set; }

        public string Money { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}