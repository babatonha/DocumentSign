using System.ComponentModel.DataAnnotations;

namespace DocumentSign_Models.ViewModels
{
    public partial class vwRequisitionTemplate
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(100)]
        public string FormName { get; set; }

        [StringLength(50)]
        public string FormCode { get; set; }

        public string TemplateName { get; set; }
    }
}
