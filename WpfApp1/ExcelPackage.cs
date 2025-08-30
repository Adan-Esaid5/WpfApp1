
using System.ComponentModel;
using System.IO;
using System.Windows;
//using OfficeOpenXml;


namespace WpfApp1
{
    public class ExcelPackage : Window,IDisposable
    {

        internal object Workbook;
        private bool disposedValue;

        // Uncomment the following line if you are using EPPlus version 5 or later
        public static LicenseContext LicenseC { get; set; }

        /// <summary>
  

        public ExcelPackage(FileInfo fileInfo)
        {
             this.LicenseC = LicenseContext.NonCommercial;
             FileInfo = fileInfo;
        }
        

        public FileInfo FileInfo { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ExcelPackage()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}