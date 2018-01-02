// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PMStockQuote
{
    static class NativeMethods
    {
        #region Constants

        const int EM_SETRECT = 0xB3;

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle r)
                : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }
        }

        #endregion

        #region Imports

        [DllImport(@"User32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        static extern int SendMessageRefRect(IntPtr hWnd, uint msg, int wParam, ref RECT rect);

        #endregion

        #region Methods

        public static void SetTextBoxPadding(TextBox textBox, Padding padding)
        {
            // Textbox padding
            // https://stackoverflow.com/questions/4903083/textbox-padding

            var rect = new RECT(new Rectangle(padding.Left, padding.Top, textBox.ClientSize.Width - padding.Left - padding.Right, textBox.ClientSize.Height - padding.Top - padding.Bottom));

            SendMessageRefRect(textBox.Handle, EM_SETRECT, 0, ref rect);
        }

        #endregion
    }
}