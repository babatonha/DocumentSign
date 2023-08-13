namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwRequisitionDocument")]
    public partial class vwRequisitionDocument
    {
        public long ID { get; set; }

        public long? RequisitionID { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(250)]
        public string FileType { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public byte[] FileImage { get; set; }
    }
}
