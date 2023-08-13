namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vwAccountsApproval
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public long? RequisitionID { get; set; }

        [StringLength(255)]
        public string RejectionReason { get; set; }

        [StringLength(50)]
        public string ApprovedAccountsID { get; set; }

        [StringLength(50)]
        public string ApprovedManagerID { get; set; }

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

        [StringLength(50)]
        public string Status { get; set; }

        public string CompanyName { get; set; }
        public string DepartmentManagerID { get; set; }
        public string DepartmentManager { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(511)]
        public string ApprovalManagerName { get; set; }

        [StringLength(150)]
        public string DepartmentName { get; set; }

        [StringLength(510)]
        public string PayeeName { get; set; }

        public long RowNum { get; set; }
        public string RequisitionTemplateCode { get; set; }
    }
}
