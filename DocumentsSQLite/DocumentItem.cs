using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimaryKeyAttribute = SQLite.PrimaryKeyAttribute;

namespace DocumentsSQLite
{
    public class DocumentItem
    {
        [Key]
        [PrimaryKey, AutoIncrement]
        [Required] public int DocumentItems_ID { get; set; }
        [ForeignKey("Document")]
        [Required] public int Document_ID { get; set; }
        [Required] public Document Document { get; set; }
        [Required] public int Ordinal { get; set; }
        [Required] public string Product { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public float Price { get; set; }
        [Required] public int TextRate { get; set; }
    }
}
