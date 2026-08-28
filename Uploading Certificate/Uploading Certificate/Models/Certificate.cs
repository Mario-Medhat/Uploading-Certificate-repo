using System.ComponentModel.DataAnnotations;

namespace Uploading_Certificate.Models
{
    public class Certificate
    {
        [Key]
        public int GKey { get; set; }
        public int EmployeeID { get; set; }
        public string CertificateName { get; set; } = "Government Driving License";
        public string CertificateNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public string CertificateType { get; set; }
        public string ModifiedBy { get; set; } = Environment.UserName;
        public string CreatedBy { get; set; } = Environment.UserName;
        public string Created { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
