using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public class json
    {
        public void getJson(string cardName, string key)
        {
            string path = Directory.GetCurrentDirectory() + "\\card.json";

            string content = File.ReadAllText(path);

            try
            {
                JObject jobj = JObject.Parse(content);
                MessageBox.Show("마녀 가격 : " + jobj[cardName][key]);
            }
            catch(Exception e)
            {
                MessageBox.Show("json 파일을 읽는 중에 오류가 발생했습니다.");
            }
        }
    }
}
