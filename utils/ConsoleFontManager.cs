using System;
using System.Runtime.InteropServices;

namespace WeaponMasterDefense
{
    public static class ConsoleFontManager
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        private const int STD_OUTPUT_HANDLE = -11;

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        // 폰트 크기 설정
        public static void SetFontSize(short width, short height, string font = "Consolas")
        {
            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);

            CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.FaceName = font;
            info.dwFontSize = new COORD { X = width, Y = height };

            SetCurrentConsoleFontEx(hnd, false, ref info);
        }
    }
}
