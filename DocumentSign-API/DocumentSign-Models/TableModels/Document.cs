namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public long DocumentId { get; set; }

        public byte[] LocationOriginal { get; set; }

        public DateTime? DateUploaded { get; set; }

        public int? UploadedBy { get; set; }
        public string DocumentName { get; set; }
    }
}
