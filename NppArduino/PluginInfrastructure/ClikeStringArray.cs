// NPP plugin platform for .Net v0.94.00 by Kasper B. Graversen etc.
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Kbg.NppPluginNET.PluginInfrastructure
{
    public class ClikeStringArray : IDisposable
    {
        readonly List<IntPtr> _nativeItems;
        bool _disposed = false;

        public ClikeStringArray(int num, int stringCapacity)
        {
            NativePointer = Marshal.AllocHGlobal((num + 1) * IntPtr.Size);
            _nativeItems = new List<IntPtr>();
            for (var i = 0; i < num; i++)
            {
                IntPtr item = Marshal.AllocHGlobal(stringCapacity);
                Marshal.WriteIntPtr((IntPtr)((int)NativePointer + (i * IntPtr.Size)), item);
                _nativeItems.Add(item);
            }
            Marshal.WriteIntPtr((IntPtr)((int)NativePointer + (num * IntPtr.Size)), IntPtr.Zero);
        }
        public ClikeStringArray(List<string> lstStrings)
        {
            NativePointer = Marshal.AllocHGlobal((lstStrings.Count + 1) * IntPtr.Size);
            _nativeItems = new List<IntPtr>();
            for (var i = 0; i < lstStrings.Count; i++)
            {
                IntPtr item = Marshal.StringToHGlobalUni(lstStrings[i]);
                Marshal.WriteIntPtr((IntPtr)((int)NativePointer + (i * IntPtr.Size)), item);
                _nativeItems.Add(item);
            }
            Marshal.WriteIntPtr((IntPtr)((int)NativePointer + (lstStrings.Count * IntPtr.Size)), IntPtr.Zero);
        }

        public IntPtr NativePointer { get; }

        public List<string> ManagedStringsAnsi => _getManagedItems(false);
        public List<string> ManagedStringsUnicode => _getManagedItems(true);

        List<string> _getManagedItems(bool unicode)
        {
            var _managedItems = new List<string>();
            for (var i = 0; i < _nativeItems.Count; i++)
            {
                if (unicode) _managedItems.Add(Marshal.PtrToStringUni(_nativeItems[i]));
                else _managedItems.Add(Marshal.PtrToStringAnsi(_nativeItems[i]));
            }
            return _managedItems;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                for (var i = 0; i < _nativeItems.Count; i++)
                    if (_nativeItems[i] != IntPtr.Zero) Marshal.FreeHGlobal(_nativeItems[i]);
                if (NativePointer != IntPtr.Zero) Marshal.FreeHGlobal(NativePointer);
                _disposed = true;
            }
        }
        ~ClikeStringArray()
        {
            Dispose();
        }
    }
}