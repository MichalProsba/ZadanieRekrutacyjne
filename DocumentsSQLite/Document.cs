using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsSQLite
{
    public class Document
    {
        [Key]
        [Required] public int Document_ID { get; set; }
        [Required] public string Type{ get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string City { get; set; }
        public List<DocumentItem> DocumentItems { get; set; }
    }
}
