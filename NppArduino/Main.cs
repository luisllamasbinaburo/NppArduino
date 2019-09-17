using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Kbg.NppPluginNET;
using Kbg.NppPluginNET.PluginInfrastructure;


namespace NppArduino
{
    internal class Main
    {
        internal const string PluginName = "NppArduino";
        private static string _iniFilePath;
        private static bool _someSetting;
        private static frmMain _form;
        private static int _idMyDlg = -1;
        private static readonly Bitmap TbBmp = NppArduino.Properties.Resources.star;
        private static readonly Bitmap TbBmpTbTab = NppArduino.Properties.Resources.star_bmp;
        private static Icon _tbIcon;

        public static void OnNotification(ScNotification notification)
        {  
            // This method is invoked whenever something is happening in notepad++
            // use eg. as
            // if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
            // { ... }
            // or
            //
            // if (notification.Header.Code == (uint)SciMsg.SCNxxx)
            // { ... }
        }

        internal static void CommandMenuInit()
        {
            var sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            _iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(_iniFilePath)) Directory.CreateDirectory(_iniFilePath);
            _iniFilePath = Path.Combine(_iniFilePath, PluginName + ".ini");
            _someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, _iniFilePath) != 0);

            PluginBase.SetCommand(1, "NppArduino", MyDockableDialog); _idMyDlg = 1;
        }

        internal static void SetToolBarIcon()
        {
            var tbIcons = new toolbarIcons {hToolbarBmp = TbBmp.GetHbitmap()};
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[_idMyDlg]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("SomeSection", "SomeKey", _someSetting ? "1" : "0", _iniFilePath);
        }


        internal static void MyDockableDialog()
        {
            if (_form == null)
            {
                _form = new frmMain();

                using (var newBmp = new Bitmap(16, 16))
                {
                    Graphics g = Graphics.FromImage(newBmp);
                    var colorMap = new ColorMap[1];
                    colorMap[0] = new ColorMap {OldColor = Color.Fuchsia, NewColor = Color.FromKnownColor(KnownColor.ButtonFace)};
                    var attr = new ImageAttributes();
                    attr.SetRemapTable(colorMap);
                    g.DrawImage(TbBmpTbTab, new Rectangle(0, 0, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel, attr);
                    _tbIcon = Icon.FromHandle(newBmp.GetHicon());
                }

                var nppTbData = new NppTbData
                {
                    hClient = _form.Handle,
                    pszName = "Arduino Notepad++",
                    dlgID = _idMyDlg,
                    uMask = NppTbMsg.DWS_DF_CONT_RIGHT | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR,
                    hIconTab = (uint) _tbIcon.Handle,
                    pszModuleName = PluginName
                };
                IntPtr ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(nppTbData));
                Marshal.StructureToPtr(nppTbData, ptrNppTbData, false);

                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMREGASDCKDLG, 0, ptrNppTbData);
            }
            else
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMSHOW, 0, _form.Handle);
            }
        }
    }
}