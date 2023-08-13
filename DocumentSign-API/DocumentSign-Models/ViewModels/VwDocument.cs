namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VwDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DocumentId { get; set; }

        public byte[] LocationOriginal { get; set; }

        public DateTime? DateUploaded { get; set; }

        public int? UploadedBy { get; set; }

        [StringLength(500)]
        public string DocumentName { get; set; }
        public string UploaderFullName { get; set; }
    }
}
