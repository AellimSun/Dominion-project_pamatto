using System;
using System.Windows.Forms;


//주석

namespace DominionApp
{
    public static class Global
    {
        public static string UserID;
        public static int HostNum;
        public static TransHandler transHandler;
        public static string[] ID_List = { "","","",""};
    }

    internal static class Program
    {
        //
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login()); //수정해야됨
        }
    }
}
