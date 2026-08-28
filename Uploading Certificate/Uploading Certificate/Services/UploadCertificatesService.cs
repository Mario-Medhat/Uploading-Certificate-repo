using ClosedXML.Excel;
using Microsoft.Win32;
using System.Windows;
using Uploading_Certificate.Models;
using Uploading_Certificate.Repositories;

namespace Uploading_Certificate.Services
{
    public class UploadCertificatesService
    {
        public List<Certificate> certificatesToUpload = new List<Certificate>();
        public SqlServerCertificateRepository certificateRepository;
        public UploadCertificatesService(SqlServerCertificateRepository certificateRepository)
        {
            this.certificateRepository = certificateRepository;
        }

        public void GetCertificates()
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Select The Certificates Excel File",
                    Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls",
                    Multiselect = false
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    certificatesToUpload.Clear(); // Clear the list before adding new certificates

                    string filePath = openFileDialog.FileName;

                    // Read the Excel file and populate the certificatesToUpload list
                    using var workbook = new XLWorkbook(filePath);
                    var worksheet = workbook.Worksheet(1);

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        certificatesToUpload.Add(new Certificate
                        {
                            EmployeeID = int.Parse(row.Cell(1).GetString().Trim()),
                            CertificateName = row.Cell(2).GetString(),
                            CertificateNumber = row.Cell(3).GetString().Trim(),
                            IssueDate = row.Cell(4).GetDateTime(),
                            RenewalDate = row.Cell(5).GetDateTime(),
                            CertificateType = row.Cell(6).GetString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading the Excel file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UploadCertificate()
        {
            try
            {
                if (certificatesToUpload.Count > 0)
                {
                    var result = MessageBox.Show($"{certificatesToUpload.Count} certificates ready to upload.", "Upload Certificates", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        foreach (var cert in certificatesToUpload)
                        {
                            bool doesExisting = certificateRepository.GetCertificate(cert.EmployeeID) != null;
                            if (doesExisting)
                            {
                                // Update existing certificate
                                certificateRepository.UpdateCertificate(cert);

                            }
                            else
                            {
                                // Insert new certificate
                                certificateRepository.InsertCertificate(cert);
                            }

                        }
                            MessageBox.Show($"{certificatesToUpload.Count} Certificates uploaded successfully!", "Upload Certificates", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No certificates to upload. Please select an Excel file first.", "Upload Certificates", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading certificates: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
