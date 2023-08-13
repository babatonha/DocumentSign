namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public int CompanyId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public int? SortBy { get; set; }
    }
}
