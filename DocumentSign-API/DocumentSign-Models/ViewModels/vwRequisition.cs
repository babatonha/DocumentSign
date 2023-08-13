namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwRequisition")]
    public partial class vwRequisition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public DateTime? RequisitionDate { get; set; }

        public int? DepartmentID { get; set; }

        public int? PaymentToID { get; set; }

        [StringLength(255)]
        public string PayeeID { get; set; }

        [StringLength(255)]
        public string InvoiceNumbers { get; set; }

        [StringLength(255)]
        public string OrderNumber { get; set; }

        [StringLength(255)]
        public string OrderComplete { get; set; }

        [StringLength(255)]
        public string Forecast { get; set; }

        public double? Amount { get; set; }
        public double? Budget { get; set; }

        public string PaymentDescription { get; set; }

        [StringLength(255)]
        public string POPEmail { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; }

        [StringLength(255)]
        public string RequestedBy { get; set; }

        [StringLength(255)]
        public string CheckedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(255)]
        public string ModifiledBy { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(150)]
        public string DepartmentName { get; set; }

        [StringLength(510)]
        public string PayeeName { get; set; }
        public long RowNum { get; set; }
        public string RequisitionTemplateCode { get; set; }
    }
}
