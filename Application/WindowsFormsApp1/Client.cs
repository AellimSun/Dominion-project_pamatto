using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DTL;

namespace WindowsFormsApp1
{
    internal class Dominion_Client
    {
        public void test(string[] args)
        {
            int A = 0;
            string[] OID = new string[4];
            string myID = "";
            Random ran = new Random();
            myID += (char)ran.Next('a', 'z');
            TransHandler t = new TransHandler("127.0.0.1", 5542, myID);
            Console.WriteLine(myID);
            t.Start_Matching();
            //if (t.Wait_Full_Queue(A) == 1)
            //{
            //    Console.WriteLine("다찾음");
            //    t.Respond(1, OID);
            //}

        }
    }

    public class TransHandler
    {
        private IPEndPoint ServerAddress;
        private IPEndPoint ClientAddress;
        private TcpClient Client;
        private NetworkStream Stream;
        private string ID;
        public TransHandler(string Server_IP, int serverPort, string ID)
        {
            ClientAddress = new IPEndPoint(0, 0);
            ServerAddress = new IPEndPoint(IPAddress.Parse(Server_IP), serverPort);
            Client = new TcpClient(ClientAddress);
            this.ID = ID;
        }
        public void disconn()
        {
            Stream.Close();
            Client.Close();
        }
        public int Start_Matching()
        {
            //연결
            Client.Connect(ServerAddress);
            Stream = Client.GetStream();

            //매칭 큐 입력 메시지 전송
            Message STM = new Message();
            STM.Body = new BodyStartMatching()
            {
                ID = Encoding.ASCII.GetBytes(ID)
            };
            STM.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.START_MATCHING,
                BODYLEN = STM.Body.GetSize()
            };

            MessageUtil.Send(Stream, STM);

            Message recv = MessageUtil.Receive(Stream);
            if (recv.Header.MSGTYPE == CONSTANTS.INSERT_QUEUE)
            {
                return (int)(recv.Body as BodyInsertQueue).UserCount;
            }
            else
            {
                return 0;
            }
        }
        public int Wait_Full_Queue(Form3 f)
        {
            while (true)
            {
                Message recv = MessageUtil.Receive(Stream);
                if (recv != null)
                {
                    switch (recv.Header.MSGTYPE)
                    {
                        case CONSTANTS.ADD_PLAYER:
                            //f.setNumberTextBox().Text = Current_user.ToString();
                            f.ADD_P();
                            break;
                        case CONSTANTS.SUB_PLAYER:
                            //f.setNumberTextBox().Text = Current_user.ToString();
                            f.SUB_P();
                            break;
                        case CONSTANTS.SUCCESS_CANCLE_MATCHING:
                            return -1;
                        case CONSTANTS.FULL_QUEUE:
                            return 1;
                        default:
                            break;
                    }
                }
                else return 0;
            }
        }
        async public void Cancle_Matching()
        {
            await Task.Run(async () =>
            {
                //큐 매칭 취소
                Message CNM = new Message();
                CNM.Body = new BodyCancleMatching()
                {
                    ID = Encoding.ASCII.GetBytes(ID)
                };
                CNM.Header = new Header()
                {
                    HASBODY = CONSTANTS.HAS_BODY,
                    MSGTYPE = CONSTANTS.CANCLE_MATCHING,
                    BODYLEN = CNM.Body.GetSize()
                };

                MessageUtil.Send(Stream, CNM);
            });
        }
        public int Respond(int Res, string[] ID_LIST, int Host_Number)
        {
            Message Res_Msg = new Message();
            Res_Msg.Body = null;
            switch (Res)
            {
                case 1:
                    Res_Msg.Header = new Header()
                    {
                        HASBODY = CONSTANTS.NO_BODY,
                        MSGTYPE = CONSTANTS.ACCEPT,
                        BODYLEN = 0
                    };
                    break;
                case -1:
                    Res_Msg.Header = new Header()
                    {
                        HASBODY = CONSTANTS.NO_BODY,
                        MSGTYPE = CONSTANTS.DECLINE,
                        BODYLEN = 0
                    };
                    break;
            }
            MessageUtil.Send(Stream, Res_Msg);

            Message recv = MessageUtil.Receive(Stream);
            switch (recv.Header.MSGTYPE)
            {
                case CONSTANTS.GAME_START:
                    ID_LIST[0] = Encoding.Default.GetString((recv.Body as BodyGameStart).ID1);
                    ID_LIST[1] = Encoding.Default.GetString((recv.Body as BodyGameStart).ID2);
                    ID_LIST[2] = Encoding.Default.GetString((recv.Body as BodyGameStart).ID3);
                    ID_LIST[3] = Encoding.Default.GetString((recv.Body as BodyGameStart).ID4);
                    Host_Number = (recv.Body as BodyGameStart).HostNumber;

                    return 1;

                case CONSTANTS.GAME_CANCLE:

                    return -1;
            }

            return 0;
        }

        public int Game_Listener(ref string Card_Name, ref string Log)
        {
            Message Alway_Listen = MessageUtil.Receive(Stream);
            switch (Alway_Listen.Header.MSGTYPE)
            {
                case CONSTANTS.TURN_SEND:
                    return 1;
                case CONSTANTS.ALERT_ACTION:
                    {
                        Card_Name = Encoding.Default.GetString((Alway_Listen.Body as BodyAlertAction).CARD);
                        switch ((Alway_Listen.Body as BodyAlertAction).ACTION)
                        {
                            case CONSTANTS.ATTACK:
                                return 2;
                            case CONSTANTS.GET_CARD:
                                return 3;
                            case CONSTANTS.SCRAP_CARD:
                                return 4;
                        }
                    }
                    return -1;
                case CONSTANTS.LOG_SEND:
                    Log = Encoding.Default.GetString((Alway_Listen.Body as BodyLogSend).LOG);
                    return 5;
                case CONSTANTS.SCORE_REQUEST:
                    return 6;

                default:
                    return -1;
            }

        }

        public void Attack(string Card_Name)
        {
            byte[] cbyte = new byte[21];
            byte[] tmp = Encoding.Default.GetBytes(Card_Name);
            Array.Copy(tmp, 0, cbyte, 0, 21);
            Message Amsg = new Message();
            Amsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.ATTACK,
                CARD = cbyte
            };
            Amsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.ALERT_ACTION,
                BODYLEN = Amsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, Amsg);
        }
        public void Get_Card(string Card_Name)
        {
            byte[] cbyte = new byte[21];
            byte[] tmp = Encoding.Default.GetBytes(Card_Name);
            Array.Copy(tmp, 0, cbyte, 0, 21);
            Message GCmsg = new Message();
            GCmsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.GET_CARD,
                CARD = cbyte
            };
            GCmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.ALERT_ACTION,
                BODYLEN = GCmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, GCmsg);
        }
        public void Scrap_Card(string Card_Name)
        {
            byte[] cbyte = new byte[21];
            byte[] tmp = Encoding.Default.GetBytes(Card_Name);
            Array.Copy(tmp, 0, cbyte, 0, 21);
            Message SCmsg = new Message();
            SCmsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.SCRAP_CARD,
                CARD = cbyte
            };
            SCmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.ALERT_ACTION,
                BODYLEN = SCmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, SCmsg);
        }
        public void Turn_end()
        {
            Message TEmsg = new Message();
            TEmsg.Body = null;
            TEmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.NO_BODY,
                MSGTYPE = CONSTANTS.TURN_END,
                BODYLEN = 0
            };
            MessageUtil.Send(Stream, TEmsg);
        }
        public void Log_Send(string Log)
        {
            Message LSmsg = new Message();
            LSmsg.Body = new BodyLogSend()
            {
                LOG = Encoding.Default.GetBytes(Log)
            };
            LSmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.LOG_SEND,
                BODYLEN = LSmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, LSmsg);
        }
        public string Log_Receive()
        {
            Message LRmsg = MessageUtil.Receive(Stream);
            if (LRmsg.Header.MSGTYPE == CONSTANTS.LOG_SEND)
            {
                string log = Encoding.Default.GetString((LRmsg.Body as BodyLogSend).LOG);
                return log;
            }
            return null;
        }
        public void Game_End(int Score)
        {
            Message GEmsg = new Message();
            GEmsg.Body = null;
            GEmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.NO_BODY,
                MSGTYPE = CONSTANTS.GAME_FIN,
                BODYLEN = 0
            };
            MessageUtil.Send(Stream, GEmsg);

            Message RSmsg = MessageUtil.Receive(Stream);
            if (RSmsg.Header.MSGTYPE != CONSTANTS.SCORE_REQUEST)
            {
                return;
            }
            Score_send(Score);
        }
        public void Score_send(int Score)
        {
            Message SSmsg = new Message();
            SSmsg.Body = new BodyScoreSend()
            {
                SCORE = Score
            };
            SSmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.SCORE_SEND,
                BODYLEN = SSmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, SSmsg);
        }
        public void Recv_Total_Score(int[] all_score)
        {
            Message All_Score = MessageUtil.Receive(Stream);
            all_score[0] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE1;
            all_score[1] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE2;
            all_score[2] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE3;
            all_score[3] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE4;
        }
    }
}
