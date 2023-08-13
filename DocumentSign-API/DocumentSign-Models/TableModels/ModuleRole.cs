namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ModuleRole
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}
