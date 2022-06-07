using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace DominionApp
{
    public class FontManager
    {
        private static FontManager instance = new FontManager();
        public PrivateFontCollection privateFont = new PrivateFontCollection();
        public static FontFamily[] myFont
        {
            get
            {
                return instance.privateFont.Families;
            }
        }

        public FontManager()
        {
            AddFontFromMemory();
        }

        private void AddFontFromMemory()
        {
            byte[] font = Properties.Resources.TypographerGotischB_Bold;
            IntPtr fontBuffer = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, fontBuffer, font.Length);
            privateFont.AddMemoryFont(fontBuffer, font.Length);

            Marshal.FreeHGlobal(fontBuffer);
        }
    }
}
