namespace ChargingLocker.ClassLibrary
{
    public class ChargeControl
    {
        private IUsbCharger _charger = new UsbChargerSimulator();
        public ChargeControl()
        {

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
    }
}
