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
                            EmployeeID = row.Cell(1).GetString().Trim(),
                            CertificateName = row.Cell(2).GetString(),
                            CertificateNumber = row.Cell(3).GetString().Trim(),
                            IssueDate = DateOnly.FromDateTime(row.Cell(4).GetDateTime()),
                            RenewalDate = DateOnly.FromDateTime(row.Cell(5).GetDateTime()),
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
                        int updatedcounter = 0;
                        int insertedcounter = 0;

                        foreach (var certificateExcel in certificatesToUpload)
                        {
                            // Get the certificate from database to get the correct ID
                            var certificateDB = certificateRepository.GetCertificate(certificateExcel.EmployeeID!);

                            // check if it's existing
                            if (certificateDB != null)
                            {
                                // Update existing certificate
                                certificateRepository.UpdateCertificate(certificateDB);
                                updatedcounter++;

                            }
                            else
                            {
                                // Insert new certificate
                                certificateRepository.InsertCertificate(certificateExcel);
                                insertedcounter++;
                            }

                        }
                        string message = $"Certificates uploaded successfully!\nInserted: {insertedcounter} \nUpdated: {updatedcounter}";
                        MessageBox.Show(message, "Upload Certificates", MessageBoxButton.OK, MessageBoxImage.Information);
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
