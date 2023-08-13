namespace DocumentSign_Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vwAspNetUser
    {
        [Key]
        [Column(Order = 0)]
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }
    }
}
