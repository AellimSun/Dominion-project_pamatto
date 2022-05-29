using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp1
{
    class DB_ACCESS
    {
        private SqlConnection Con;
        public void SendDBLog(string GameLog)
        {
            string send_string = GameLog;
            try
            {
                Con = new SqlConnection();
                Con.ConnectionString = "Server=(local);database=GameTestDB;Integrated Security = true";
                Con.Open();

                DateTime now = DateTime.Now;
                string Room = Global.UserID + now.ToString();

                SqlCommand Com = new SqlCommand();
                Com.Connection = Con;
                Com.CommandText =
                    "insert into GameLogTest values(@ID, @NickName ,@GameLog, @time)";
                //"insert into GameLog values(@ID, @NickName ,@time, @GameLog)";
                //Com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                Com.Parameters.Add("@ID", SqlDbType.VarChar).Value = Global.UserID;
                Com.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = Global.UserID;
                //Com.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = player.PlayerName;
                Com.Parameters.Add("@GameLog", SqlDbType.NVarChar).Value = send_string;
                Com.Parameters.Add("@time", SqlDbType.DateTime).Value = now;
                Com.ExecuteNonQuery();
                //MessageBox.Show("정상적으로 데이터가 저장되었습니다.", "알림");
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB 저장에 실패했습니다.", "알림");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Con.Close();
            }
        }

        /*public void RecieveLog(ListBox listBox)
        {
            try
            {
                Con = new SqlConnection();
                Con.ConnectionString = "Server=(local);database=GameTestDB;Integrated Security = true";
                Con.Open();

                SqlCommand Com = new SqlCommand();
                Com.Connection = Con;
                Com.CommandText =
                    "select nickName, GameLog from GameLogTest";
                SqlDataReader reader;
                reader = Com.ExecuteReader();

                //어떤 값 받아올것인지 정해야 함. 맨 위? 마지막 로그 이후의 값?
                while (reader.Read())
                {
                    string showing_text = reader["nickName"].ToString() + " is ";
                    showing_text += reader["GameLog"].ToString();
                    listBox.Items.Add(showing_text);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB를 불러오는데 실패했습니다.", "알림");
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Con.Close();
            }
        }*/
    }
}