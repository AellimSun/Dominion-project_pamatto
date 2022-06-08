using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace DominionApp
{
    public class json
    {
        public JObject getJson()
        {
            //    //json 파일 내용 읽어오기
            string content = Properties.Resources.card;

            try
            {
                //파싱(문자열 -> json)
                JObject jobj = JObject.Parse(content);

                return jobj;
            }
            catch (Exception e)
            {
                MessageBox.Show("json 파일을 읽는 중에 오류가 발생했습니다." + e.Message);
                return null;
            }
        }
    }
}
