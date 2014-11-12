using System;
using System.Collections.Generic;
using System.Text;

namespace SelfDC.Utils
{
    public interface IDevice
    {
        /// <summary>
        /// Occurred when barcode are successful readed
        /// </summary>
        event EventHandler OnScan;

        /// <summary>
        /// Device status value
        /// </summary>
        bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Librery assembly version
        /// </summary>
        string Version
        {
            get;
        }

        /// <summary>
        /// Barcode reader
        /// </summary>
        string Barcode
        {
            get;
        }

        /// <summary>
        /// Enable scanner device
        /// </summary>
        void Open();

        /// <summary>
        /// Disable scanner device
        /// </summary>
        void Close();
    }
}
