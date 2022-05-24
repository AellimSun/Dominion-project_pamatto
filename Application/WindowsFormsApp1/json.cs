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
        public JObject getJson()
        {
            //json 파일 위치 설정
            string path = Directory.GetCurrentDirectory() + "\\card.json";
            //json 파일 내용 읽어오기
            string content = File.ReadAllText(path);

            try
            {
                //파싱(문자열 -> json)
                JObject jobj = JObject.Parse(content);

                return jobj;
            }
            catch(Exception e)
            {
                MessageBox.Show("json 파일을 읽는 중에 오류가 발생했습니다.");
                return null;
            }
        }
    }
}
