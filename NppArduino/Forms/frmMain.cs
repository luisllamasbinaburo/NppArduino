#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using NppArduino.Domain;
using NppArduino.Utils;
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
        }

   
        public frmMain(IScintillaGateway editor)
        {
            _editor = editor;
            InitializeComponent();

            InitControls();
            WireEvents();
        }

        #endregion

        #region "Properties"
        public List<string> Ports { get; set; } = new List<string>();
        public List<Board> Boards { get; set; } = new List<Board>();

        public List<BoardOptionValue> Cpus { get; set; } = new List<BoardOptionValue>();

        public BoardDetail BoardDetail { get; set; }

        public int[] BaudRates { get; set; } = { 9600, 14400, 19200, 28800, 38400, 57600, 115200 };
        #endregion

        private void InitControls()
        {
            cbComPorts.DataSource = new BindingSource { DataSource = Ports };
            cbBaudRates.DataSource = new BindingSource { DataSource = BaudRates };
            cbBoards.DataSource = new BindingSource { DataSource = Boards };

            cbBoards.DisplayMember = "Name";
            cbBoards.ValueMember = "Fqbn";

            btSend.Enabled = false;
            txSendText.Enabled = false;

            gvProperties.AutoGenerateColumns = false;
            ((DataGridViewComboBoxColumn)gvProperties.Columns["Value"]).DisplayMember = "Value_Label";
            ((DataGridViewComboBoxColumn)gvProperties.Columns["Value"]).ValueMember = "Value";
        }

        private void WireEvents()
        {
            btRefresh.Click += btRefresh_Click;
            btCompile.Click += btCompile_Click;
            btUpload.Click += btUpload_Click;
            btConnect.Click += btConnect_Click;
            btSend.Click += btSend_Click;

            cbBoards.SelectedValueChanged += cbBoards_SelectedValueChanged;
        }

        public void Init()
        {
            LoadData();
        }
        private void LoadData()
        {
            cbBoards.Enabled = false;
            cbComPorts.Enabled = false;
            LoadPorts();
            LoadBoards();
            cbBoards.Enabled = true;
            cbComPorts.Enabled = true;
        }

        private void LoadPorts()
        {
            Ports.Clear();
            Ports.AddRange(ArduinoCLI_API.GetPorts());
            ((BindingSource) (cbComPorts.DataSource)).ResetBindings(false);

        }

        private void LoadBoards()
        {
            Boards.Clear();
            Boards.AddRange(ArduinoCLI_API.GetInstalledBoards().OrderBy(b=> b.Fqbn));
            ((BindingSource)(cbBoards.DataSource)).ResetBindings(false);

        }

        private void LoadBoardDetails(Board board)
        {
            if (board == null) return;

            BoardDetail = ArduinoCLI_API.GetBoardDetails(board.Fqbn);
            gvProperties.DataSource = BoardDetail.Options;

            foreach (DataGridViewRow row in gvProperties.Rows)
            {
                var option = row.DataBoundItem as BoardOption;
                var cell = row.Cells["Value"] as DataGridViewComboBoxCell;
                cell.DataSource = option.Values;
                cell.Value = option.Values.First(v => v.Selected).Value;
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void btCompile_Click(object sender, EventArgs e)
        {
            var path = GetTargetPath();
            var board = cbBoards.SelectedValue?.ToString();
            if (string.IsNullOrWhiteSpace(board)) return;
            if (Path.GetExtension(path) != ".ino") return;

            txOutput.Text = $"--- Compiling {Path.GetFileName(path)} ---";
            serialPortDisconnect();
            var options = GetCompileOptions();
            string rst = ArduinoCLI_API.CompileSketch(board, Path.GetDirectoryName(path), options);
            txOutput.Text += '\n' + rst + '\n';
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            var path = GetTargetPath();
            var port = cbComPorts.SelectedValue?.ToString();
            var board = cbBoards.SelectedValue?.ToString();
            if (Path.GetExtension(path) != ".ino") return;
            if (string.IsNullOrWhiteSpace(port)) return;
            if (string.IsNullOrWhiteSpace(board)) return;

            txOutput.Text = $"--- Uploading {Path.GetFileName(path)} ---";
            serialPortDisconnect();
            var options = GetCompileOptions();
            string rst = ArduinoCLI_API.UploadSketch(port, board, Path.GetDirectoryName(path), options);
            txOutput.Text += '\n' + rst + '\n';
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (arduinoSerialPort.IsOpen) serialPortDisconnect();
            else serialPortConnect();
        }

        private void cbBoards_SelectedValueChanged(object sender, EventArgs e)
        {
            var currentBoard = cbBoards.SelectedItem as Board;
            if (currentBoard == null) return;

        
            LoadBoardDetails(currentBoard);
        }

        private CompileOption[] GetCompileOptions()
        {
            var compilerOptions = gvProperties.Rows.Cast<DataGridViewRow>()
                .Select(row => new CompileOption()
                    { Option = ((BoardOption)row.DataBoundItem).Option,
                    Value = row.Cells["Value"].Value.ToString()}
                ).ToArray();

            if (compilerOptions.Length == 0) return null;
            return compilerOptions;
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