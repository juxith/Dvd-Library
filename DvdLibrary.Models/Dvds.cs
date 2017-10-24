using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DvdLibrary.Models
{
    public class Dvds
    {
        [Key]
        public int DvdId { get; set; }
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public string Notes { get; set; }
    }
}
