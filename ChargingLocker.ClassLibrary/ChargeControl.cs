using System;

namespace ChargingLocker.ClassLibrary
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;
        private IDisplay _display;
        private int runs = 0;
        public ChargeControl(IDisplay display, IUsbCharger charger)
        {
            _display = display;
            _charger = charger;
            _charger.CurrentValueEvent += ReadEvent;
        }

        public bool isConnected()
        {
            return _charger.Connected;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }

        public void ReadEvent(object sender, CurrentEventArgs e)
        {
            if (runs == 0)
            {
                if (e.Current == 0.0 && _charger.Connected == false)
                {
                    //Der er ingen forbindelse til en telefon, eller ladning er ikke startet.
                    //Displayet viser ikke noget om ladning
                    //Console.WriteLine("Current = 0: Current = {0}", e.Current);
                    _display.DisplayConnectPhone();
                    runs = 1;
                }
                else if (e.Current > 500)
                {
                    //Der er noget galt, fx en kortslutning.
                    //USBladningen skal straks stoppes. Displayet viser en fejlmeddelelse.
                    Console.WriteLine("Current > 500: Current = {0}", e.Current);
                    
                    
                    _display.DisplayFailedConnection();
                    _charger.StopCharge();
                    runs = 1;
                }
            }
            if (e.Current > 5 && e.Current <= 500)
            {
                //Opladningen foregår normalt. Displayet viser, at ladning foregår.
                double p = e.Current / 500;
                p = p * 100;
                p = 100 - p;
                _display.DisplayPhoneCharging(p);
                //Console.WriteLine("Current > 0 && Current <= 500: Current = {0}", e.Current);
                runs = 1;
            }
            if (e.Current > 0.0 && e.Current <= 5 && runs == 1)
            {
                //Opladningen er tilendebragt, og USB ladningen kan stoppes.
                //Displayet viser, at telefonen er fuldt opladet.
                //Console.WriteLine("Current > 0 && <= 5: Current = {0}",e.Current);
                _display.DisplayPhoneFullyCharged();
                _charger.StopCharge();
                runs = 2;
            }
        }
    }
}
