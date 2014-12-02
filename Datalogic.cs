using System;
using datalogic.datacapture;

namespace SelfDC.Utils
{
    public class Datalogic : IDevice
    {
        private string className = "DataLogic";
        private bool _enabled;
        private string _version = "2014.11.25.r01";
        private datalogic.datacapture.Laser bcReader;
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
                bcReader.ScannerEnabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datalogic.Open(): errore di attivazione dello scanner");
                Console.WriteLine(ex.StackTrace);
                return;
            }
            this._enabled = true;
        }

        public void Close()
        {
            try
            {
                bcReader.ScannerEnabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datalogic.Close(): errore di disattivazione dello scanner");
                Console.WriteLine(ex.StackTrace);
                return;
            }
            this._enabled = false;
        }
        #endregion

        public string Name
        {
            get
            {
                return this.className;
            }
        }

        /* Costruttore */
        public Datalogic()
        {
            this.bcReader = new Laser();
            this.bcReader.GoodReadEvent += new ScannerEngine.LaserEventHandler(OnBarcodeScan);
        }

        /* membro delegato che specializza l'evento di lettura per i datalogic */
        private void OnBarcodeScan(datalogic.datacapture.ScannerEngine sender)
        {
            this._barcode = sender.BarcodeDataAsText.ToString();
            /*
            Console.WriteLine("Lista degli register all'evento OnScan:");
            foreach (Delegate register in this.OnScan.GetInvocationList())
            {
                Console.WriteLine("delegato {0}: {1}", register.Method.ToString(), register.Target.ToString());
            }
             */ 
            if (this.OnScan != null)
                OnScan(this, EventArgs.Empty);
        }
    }
}