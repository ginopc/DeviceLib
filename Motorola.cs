using System;

namespace SelfDC.Utils
{
    public class Motorola : IDevice
    {
        private string className = "Motorola";
        private bool _enabled;
        private Symbol.Barcode2.Design.Barcode2 bcReader;
        private string _version = "2014.11.25.r01";
        private string _barcode = null;

        public event EventHandler OnScan;

        #region IDevice Members

        public string Barcode
        {
            get
            {
                return this._barcode;
            }
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
                if (value)
                    this.Open();
                else
                    this.Close();
            }
        }

        public string Version
        {
            get
            {
                return this._version;
            }
        }

        public void Open()
        {
            try
            {
                this.bcReader.EnableScanner = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Motorola.Open(): errore di attivazione");
                Console.WriteLine(ex.StackTrace);
                return;
            }
            this._enabled = true;
        }

        public void Close()
        {
            try
            {
                this.bcReader.EnableScanner = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Motorola.Close(): errore di disattivazione dello scanner");
                Console.WriteLine(ex.StackTrace);
                return;
            }
            this._enabled = false;
        }

        #endregion

        public Motorola()
        {
            this.bcReader = new Symbol.Barcode2.Design.Barcode2();
            bcReader.OnScan += new Symbol.Barcode2.Design.Barcode2.OnScanEventHandler(OnBarcodeScan);
        }

        public string Name
        {
            get
            {
                return this.className;
            }
        }

        private void OnBarcodeScan(Symbol.Barcode2.ScanDataCollection dataScanned)
        {
            this._barcode = dataScanned.GetFirst.ToString();
            if (this.OnScan != null)
                OnScan(this, EventArgs.Empty);
        }
    }
}
