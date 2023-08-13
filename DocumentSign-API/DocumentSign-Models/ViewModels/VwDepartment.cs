namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VwDepartment")]
    public partial class VwDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? CompanyId { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }
    }
}
