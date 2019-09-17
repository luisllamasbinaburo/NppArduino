#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using NppArduino.Domain;
#endregion


namespace Kbg.NppPluginNET
{
    public partial class frmMain : Form
    {
        #region  "Fields"
        private readonly IScintillaGateway _editor;

        private readonly ArduinoSerialPort arduinoSerialPort = new ArduinoSerialPort();
        #endregion

        #region "Constructors"
        public frmMain()
        {
            InitializeComponent();
            InitControls();

            WireEvents();
            LoadPorts();
            LoadBoards();
        }

        private void InitControls()
        {
            btSend.Enabled = false;
            txSendText.Enabled = false;
        }

        public frmMain(IScintillaGateway editor)
        {
            _editor = editor;
            InitializeComponent();
            InitControls();

            WireEvents();
            LoadPorts();
            LoadBoards();
        }
        #endregion

        #region "Properties"
        public string[] Ports { get; set; }
        public Board[] Boards { get; set; }

        public int[] BaudRates { get; set; } = { 9600, 14400, 19200, 28800, 38400, 57600, 115200 };
        #endregion

        private void WireEvents()
        {
            btRefresh.Click += btRefresh_Click;
            btCompile.Click += btCompile_Click;
            btUpload.Click += btUpload_Click;
            btConnect.Click += btConnect_Click;
            btSend.Click += btSend_Click;
        }

        private void LoadPorts()
        {
            Ports = ArduinoCLI_API.GetPorts();
            cbComPorts.DataSource = Ports;
            cbBaudRates.DataSource = BaudRates;
        }

        private void LoadBoards()
        {
            Boards  = ArduinoCLI_API.GetInstalledBoards().OrderBy(b=> b.Name).ToArray();
            cbBoards.DataSource = Boards;
            cbBoards.DisplayMember = "Name";
            cbBoards.ValueMember = "Fqbn";
        }


        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadPorts();
            LoadBoards();
        }


        private void btCompile_Click(object sender, EventArgs e)
        {
            var path = GetTargetPath();
            var board = cbBoards.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(board)) return;
            if (System.IO.Path.GetExtension(path) != ".ino") return;

            txOutput.Text = $"--- Compiling {System.IO.Path.GetFileName(path)} ---";

            string rst = ArduinoCLI_API.CompileSketch(board, System.IO.Path.GetDirectoryName(path));
            txOutput.Text += '\n' + rst + '\n';
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            var path = GetTargetPath();
            var port = cbComPorts.SelectedValue.ToString();
            var board = cbBoards.SelectedValue.ToString();
            if (System.IO.Path.GetExtension(path) != ".ino") return;
            if (string.IsNullOrWhiteSpace(port)) return;
            if (string.IsNullOrWhiteSpace(board)) return;

            txOutput.Text = $"--- Uploading {System.IO.Path.GetFileName(path)} ---";
            string rst = ArduinoCLI_API.UploadSketch(port , board, System.IO.Path.GetDirectoryName(path));
            txOutput.Text += '\n' + rst + '\n';
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (arduinoSerialPort.IsOpen) serialPortDisconnect();
            else serialPortConnect();

        }

        private void serialPortDisconnect()
        {
            arduinoSerialPort.DataArrived -= ArduinoSerialPort_DataArrived;
            arduinoSerialPort.Close();
            btConnect.Text = "Connect";
            btSend.Enabled = false;
            txSendText.Enabled = false;
        }

        private void serialPortConnect()
        {
            int? baudRade = cbBaudRates.SelectedValue as int?;
            var port = cbComPorts.SelectedValue.ToString();
            if (baudRade == null) return;
            if (string.IsNullOrWhiteSpace(port)) return;

            arduinoSerialPort.Open(port, (int)baudRade);
            arduinoSerialPort.DataArrived += ArduinoSerialPort_DataArrived;
            btConnect.Text = "Disconnect";
            btSend.Enabled = true;
            txSendText.Enabled = true;
        }

        private void ArduinoSerialPort_DataArrived(object sender, EventArgs e)
        {
            txSerialPort.Invoke((Action)(() => { txSerialPort.Text += arduinoSerialPort.ReceivedData; }));
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            var text = txSendText.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            arduinoSerialPort.SendData(text);
        }

        private string GetTargetPath()
        {
            var path = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);

            return path.ToString();
        } 
    }
}