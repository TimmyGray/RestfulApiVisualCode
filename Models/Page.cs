using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestfulApiVisualCode.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Subheader { get; set; }      
        public string Info { get; set; }

    }
}
