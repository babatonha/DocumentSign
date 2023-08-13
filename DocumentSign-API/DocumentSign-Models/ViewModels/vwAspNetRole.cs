namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vwAspNetRole
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }
    }
}
