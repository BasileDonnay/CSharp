using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ProjectBase.Services;

public partial class DeviceOrientationServices
{
    SerialPort mySerialPort;

    public partial void ConfigureScanner()
    { 
    
    this.mySerialPort = new SerialPort();
        String ComPort = "COM3";


        if(ComPort != "")
        {


            mySerialPort.PortName= ComPort;
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.DataBits= 8;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.ReadTimeout = 1000;
            mySerialPort.WriteTimeout = 1000;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);

            try
            {
                mySerialPort.Open();
            }

            catch(Exception ex)
            {

                Shell.Current.DisplayAlert("Error!", ex.Message, "OK");

            }

        }
    
    
    }


    public void DataHandler(object sender, SerialDataReceivedEventArgs e)
    {

        SerialPort sp = (SerialPort)sender;
        string data = "";

        data = sp.ReadTo("\r");

        Globals.SerialBuffer.Enqueue(data);

    }

   

  
}
