using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Game_Screen : Form
    {
        //void Application_Idle(object sender, EventArgs e)
        //{
        //    appli
        //}
        public void A(string A)
        {
            MessageBox.Show(A);
        }
        async public void LogTest()
        {
            await Task.Run(() =>
            {
                string CardName = "";
                string Log = "";
                int type_check = 0;
                //MessageBox.Show("쓰레드 시작");
                while (true)
                {
                    type_check = Global.transHandler.Game_Listener(ref CardName, ref Log);

                    if (type_check != 1)
                    {
                        if (type_check == 5)
                        {
                            //MessageBox.Show(Log);
                            setLogBox(Log);
                        }
                        //Log_Handle(Log);
                        else
                            return;
                    }

                    else if (type_check == 1)
                    {
                        if (!list_log.Text.Equals("내 턴!"))
                        {
                            setLogBox("내 턴!");
                            break;
                        }
                        if (button1.Enabled == false)
                        {
                            Global.transHandler.Turn_end();
                            type_check = 0;
                        }
                    }
                }
            });
                       
        }
    }
}
