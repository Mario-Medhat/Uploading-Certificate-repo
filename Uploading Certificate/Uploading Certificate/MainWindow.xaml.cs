using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Uploading_Certificate.Data;
using Uploading_Certificate.Models;
using Uploading_Certificate.Services;
using Uploading_Certificate.Repositories;

namespace CertificateUploader
{
    public partial class MainWindow : Window
    {
        private string _loadedFilePath;
        UploadCertificatesService uploadCertificatesService;
        public MainWindow()
        {
            InitializeComponent();

            UcDbContext ucDbContext = new UcDbContextFactory().CreateDbContext(null!);
            SqlServerCertificateRepository sqlServerCertificateRepository = new SqlServerCertificateRepository(ucDbContext);
            this.uploadCertificatesService = new UploadCertificatesService(sqlServerCertificateRepository);
            CertificatesGrid.ItemsSource = uploadCertificatesService.certificatesToUpload;
        }
        private void GetCertificatesButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            uploadCertificatesService.GetCertificates();

            if (uploadCertificatesService.certificatesToUpload.Count > 0)
            {
                StatusText.Text = $"{uploadCertificatesService.certificatesToUpload.Count} certificates loaded successfully.";
                UploadButton.IsEnabled = true;
                ClearButton.IsEnabled = true;
            }
            else
            {
                StatusText.Text = "No certificates found in the selected file.";
                UploadButton.IsEnabled = false;
                ClearButton.IsEnabled = false;
            }
            CertificatesGrid.Visibility = uploadCertificatesService.certificatesToUpload.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            CertificatesGrid.ItemsSource = uploadCertificatesService.certificatesToUpload;
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (uploadCertificatesService.certificatesToUpload.Count == 0)
            {
                MessageBox.Show("There is no data to upload.", "Nothing to Upload",
                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                uploadCertificatesService.UploadCertificate();
                StatusText.Text = "Upload completed successfully.";
                uploadCertificatesService.certificatesToUpload.Clear();
                UploadButton.IsEnabled = false;
                ClearButton.IsEnabled = false;
            }
            catch
            {
                StatusText.Text = "Upload failed.";
            }
            CertificatesGrid.Visibility = uploadCertificatesService.certificatesToUpload.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            CertificatesGrid.ItemsSource = uploadCertificatesService.certificatesToUpload;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            uploadCertificatesService.certificatesToUpload.Clear();
            CertificatesGrid.ItemsSource = uploadCertificatesService.certificatesToUpload;
            CertificatesGrid.Visibility = Visibility.Collapsed;
            UploadButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
        }
    }
}
