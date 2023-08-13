namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuleUserRole")]
    public partial class ModuleUserRole
    {
        [Key]
        public string UserId { get; set; }

        public int RoleId { get; set; }
    }
}
