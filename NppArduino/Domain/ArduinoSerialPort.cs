using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using NppArduino.Utils;


namespace NppArduino.Domain
{
    public class ArduinoSerialPort
    {
        private const char NEW_LINE_CHAR = '\n';

        private readonly SerialPort _arduinoPort = new SerialPort();

        public string ReceivedData { get; private set; } = "";
        public string LastLine { get; private set; } = "";
        private string _lastLineBuffer { get; set; } = "";

        public bool IsReady { get; private set; }

        public bool IsOpen;

        public event EventHandler DataArrived;
        public event EventHandler LineArrived;

        public void Open(string port, int baud)
        {
            IsOpen = true;
            _arduinoPort.PortName = port;
            _arduinoPort.BaudRate = baud;
            _arduinoPort.DtrEnable = true;
            _arduinoPort.ReadTimeout = 1;
            _arduinoPort.WriteTimeout = 1;
            _arduinoPort.Open();
            _arduinoPort.DiscardInBuffer();
            _arduinoPort.DiscardOutBuffer();
            ClearData();
            _arduinoPort.DataReceived += DataReceived;
        }


        public void Close()
        {
            if (!IsOpen) return;
            try
            {
                //_arduinoPort.Flush();
                _arduinoPort.DataReceived -= DataReceived;
                _arduinoPort.Close();
                IsOpen = false;
            }
            catch (Exception)
            {
                //do nothing
            }

        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            ReceivedData = _arduinoPort.ReadExisting();
            _lastLineBuffer += ReceivedData;

            while (_lastLineBuffer.Contains(NEW_LINE_CHAR))
            {
                LastLine = _lastLineBuffer.GetBefore(NEW_LINE_CHAR);
                _lastLineBuffer = _lastLineBuffer.GetAfter(NEW_LINE_CHAR);
                LineArrived?.Invoke(this, new EventArgs());
            }

            DataArrived?.Invoke(this, new EventArgs());
        }

        public void ClearData()
        {
            LastLine = "";
            _lastLineBuffer = "";
        }

        public void SendData(string data)
        {
            _arduinoPort.Write(data);
        }
    }
}
