namespace DocumentSign_Models.TableModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [StringLength(600)]
        public string FullName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public byte[] Signature { get; set; }
        public bool IsUploadedPictureSignature { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        public DateTime? LastLoginDate { get; set; }
        public string SignatureFileType { get; set; }
    }
}
