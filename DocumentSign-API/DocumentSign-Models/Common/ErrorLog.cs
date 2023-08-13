using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentSign_Models
{
    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Module { get; set; }

        [StringLength(150)]
        public string Action { get; set; }

        public string Data { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public string InnerInnerException { get; set; }

        public DateTime? Date { get; set; }
    }
}
