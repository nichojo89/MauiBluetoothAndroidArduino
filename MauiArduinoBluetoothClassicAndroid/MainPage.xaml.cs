using MauiArduinoBluetoothClassicAndroid.Bluetooth;

namespace MauiArduinoBluetoothClassicAndroid;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
        InitializeComponent();

        const string ArduinoBluetoothTransceiverName = "HC-05";

        var connector = DependencyService.Get<IBluetoothConnector>();
        //Gets a list of all connected Bluetooth devices
        var ConnectedDevices = connector.GetConnectedDevices();

        //Connects to the Arduino
        var arduino = ConnectedDevices.FirstOrDefault(d => d == ArduinoBluetoothTransceiverName);
        connector.Connect(arduino);
    }
}