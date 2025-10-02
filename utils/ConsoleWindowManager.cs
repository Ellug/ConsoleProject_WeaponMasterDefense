using System;
using System.Runtime.InteropServices;

namespace WeaponMasterDefense
{
    public static class ConsoleWindowManager
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x00010000;
        private const int WS_SIZEBOX = 0x00040000;

        public static void LockConsoleSize(int width, int height)
        {
            // 콘솔 버퍼 및 창 크기 일치시켜서 스크롤바 제거
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width, height);

            // Win32 API로 리사이즈 및 최대화 버튼 제거
            IntPtr handle = GetConsoleWindow();
            int style = GetWindowLong(handle, GWL_STYLE);
            style &= ~WS_MAXIMIZEBOX;
            style &= ~WS_SIZEBOX;
            SetWindowLong(handle, GWL_STYLE, style);
        }
    }
}
