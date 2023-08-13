namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VwUserDepartment")]
    public partial class VwUserDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserDepartmentId { get; set; }

        public int? UserId { get; set; }

        public int? DepartmentId { get; set; }

        [StringLength(150)]
        public string DepartmentName { get; set; }

        [StringLength(511)]
        public string UserFullName { get; set; }
        public string CompanyName { get; set; }
    }
}
