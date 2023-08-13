namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserDepartment")]
    public partial class UserDepartment
    {
        public long UserDepartmentId { get; set; }
        public int UserId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
