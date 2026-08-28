using Microsoft.EntityFrameworkCore;
using Uploading_Certificate.Data;
using Uploading_Certificate.Models;

namespace Uploading_Certificate.Repositories
{
    public class SqlServerCertificateRepository
    {
        public UcDbContext ucDbContext;

        public SqlServerCertificateRepository(UcDbContext ucDbContext)
        {
            this.ucDbContext = ucDbContext;
        }

        public Certificate GetCertificate(int EmployeeID)
        {
            return ucDbContext.Certificates.FirstOrDefault(c => c.EmployeeID == EmployeeID);
        }

        public void InsertCertificate(Certificate certificate)
        {
            ucDbContext.Certificates.Add(certificate);
            ucDbContext.SaveChanges();
        }
        public void UpdateCertificate(Certificate certificate)
        {
            ucDbContext.Certificates.Update(certificate);
            ucDbContext.SaveChanges();
        }
    }
}
