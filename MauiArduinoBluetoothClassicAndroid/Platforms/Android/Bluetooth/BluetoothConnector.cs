using Android.Bluetooth;
using Java.Util;
using MauiArduinoBluetoothClassicAndroid.Bluetooth;
using MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth;

[assembly: Dependency(typeof(BluetoothConnector))]
namespace MauiArduinoBluetoothClassicAndroid.Platforms.Android.Bluetooth
{
    public class BluetoothConnector : IBluetoothConnector
    {
        /// <inheritdoc />
        public List<string> GetConnectedDevices()
        {
            _adapter = BluetoothAdapter.DefaultAdapter;
            if (_adapter == null)
                throw new Exception("No Bluetooth adapter found.");

            if (_adapter.IsEnabled)
            {
                if (_adapter.BondedDevices.Count > 0)
                {
                    return _adapter.BondedDevices.Select(d => d.Name).ToList();
                }
            }
            else
            {
                Console.Write("Bluetooth is not enabled on device");
            }
            return new List<string>();
        }

        /// <inheritdoc />
        public void Connect(string deviceName)
        {
            var device = _adapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);
            var _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString(SspUdid));

            _socket.Connect();
            var buffer = new byte[] { 1 };

            // Write data to the device to trigger LED
            _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The standard UDID for SSP
        /// </summary>
        private const string SspUdid = "00001101-0000-1000-8000-00805f9b34fb";
        private BluetoothAdapter _adapter;
    }
}