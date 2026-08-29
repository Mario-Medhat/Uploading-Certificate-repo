using System.ComponentModel.DataAnnotations;

namespace Uploading_Certificate.Models
{
    public class Certificate
    {
       [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string? EmployeeID { get; set; }

        [MaxLength(200)]
        public string CertificateName { get; set; } = "Government Driving License";

        [MaxLength(50)]
        public string? CertificateNumber { get; set; }

        public DateOnly? IssueDate { get; set; }

        public DateOnly? RenewalDate { get; set; }

        public string Attachment { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        [MaxLength(20)]
        public string CreatedBy { get; set; } = Environment.UserName;

        public DateTime? Modified { get; set; } = null;

        [MaxLength(20)]
        public string? ModifiedBy { get; set; } = Environment.UserName;

        [MaxLength(20)]
        public string? CertificateType { get; set; }
    }
}
