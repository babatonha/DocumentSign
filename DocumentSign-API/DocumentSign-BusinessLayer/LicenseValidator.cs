using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSign_BusinessLayer
{
    public class LicenseValidator
    {
        public ResponseObject ValidateLicense()
        {
            ResponseObject response = new ResponseObject();
            response.Status = ResponseStatus.Success;
            response.StatusDescription = "License has expired, renew your license to restore full system functionality.";
            return response;
        }
    }
}
