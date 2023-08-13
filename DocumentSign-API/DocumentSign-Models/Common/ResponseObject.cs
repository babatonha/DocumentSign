using DocumentSign_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSign_Models.Common
{
    public class ResponseObject
    {
        public ResponseStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public long EntityID { get; set; }
    }
}
