namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int DepartmentId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
