using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DTL;

namespace Dominion_Client
{
    internal class Dominion_Client
    {
        static void Main(string[] args)
        {   
            //TEST

            int A = 0;
            string[] OID = new string[4];
            string myID="";
            int myScore = 0;
            int[] TotalScore = new int[4];
            string B;
            Random ran = new Random();
            myID += (char)ran.Next('a', 'z');   //가상 아이디 :: test용
            myScore = ran.Next(100);
            TransHandler t = new TransHandler("127.0.0.1", 5542, myID);
            Console.WriteLine(myID);
            t.Start_Matching();
            if (t.Wait_Full_Queue(A) == 0)  //client 4개 가상으로 생성 후 큐 확인
            {
                Console.WriteLine("ERROR!");
                return;
            }
            int res = t.Respond(1, OID);
            if (res == -1)
            {
                Console.WriteLine("방폭!");
                return;
            }
            else if (res == 0)
            {
                Console.WriteLine("ERROR!");
                return;
            }
            if(res == 1)
                Console.WriteLine("게임 시작!");
            while (true)
            {
                if (t.Game_Listener(false, myScore, TotalScore) == -1) break;
                Console.WriteLine("내 턴!");
                B = Console.ReadLine();
                if(B=="T")
                    t.Turn_end();
                if (B == "E")
                {
                    t.Game_End(myScore);
                    t.Recv_Total_Score(TotalScore);
                    break;
                }
            }
            Console.WriteLine("{0} {1} {2} {3}",TotalScore[0],TotalScore[1],TotalScore[2],TotalScore[3]);

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
        public int Wait_Full_Queue(int Current_user)
        {
            while (true)
            {
                Message recv = MessageUtil.Receive(Stream);
                switch (recv.Header.MSGTYPE)
                {
                    case CONSTANTS.ADD_PLAYER:
                        Current_user++;
                        break;
                    case CONSTANTS.SUB_PLAYER:
                        Current_user--;
                        break;
                    case CONSTANTS.SUCCESS_CANCLE_MATCHING:
                        return -1;
                    case CONSTANTS.FULL_QUEUE:
                        return 1;
                }
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
        public int Respond(int Res, string[] ID_LIST)
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
                    ID_LIST[0] = BitConverter.ToString((recv.Body as BodyGameStart).ID1);
                    ID_LIST[1] = BitConverter.ToString((recv.Body as BodyGameStart).ID2);
                    ID_LIST[2] = BitConverter.ToString((recv.Body as BodyGameStart).ID3);
                    ID_LIST[3] = BitConverter.ToString((recv.Body as BodyGameStart).ID4);

                    return 1;

                case CONSTANTS.GAME_CANCLE:

                    return -1;
            }

            return 0;
        }

        public int Game_Listener(bool Hyeaja, int S, int[] TS)  //내 차례가 아닐 때 수신대기하는 메서드  테스트용 수정필!
        {
            while (true)
            {
                Message Alway_Listen = MessageUtil.Receive(Stream);

                switch (Alway_Listen.Header.MSGTYPE)
                {
                    case CONSTANTS.TURN_SEND:
                        return 1;
                    case CONSTANTS.ALERT_ACTION:
                        {
                            switch ((Alway_Listen.Body as BodyAlertAction).ACTION)
                            {
                                case CONSTANTS.ATTACK:
                                    switch ((Alway_Listen.Body as BodyAlertAction).CARD)
                                    {
                                        case 00: //마녀 번호 입력해야됨
                                            //마녀 사용 이펙트 호출
                                            if (Hyeaja)
                                            {
                                                //방어 이펙트 호출
                                                //Log_Send(Defence_Log);  Defence_Log에 방어 성공 로그 입력
                                            }
                                            else
                                            {
                                                //저주 획득 이펙트 호출
                                                //저주 카드 개수 줄이는 메서드 호출

                                                //Get_Card(Curse_No); Curse_No에 저주 카드 번호 입력
                                                //Log_Send(Curse_Get_Log); Curse_Get_Log에 저주 카드 획득 로그 입력
                                            }
                                            break;
                                    }
                                    break;
                                case CONSTANTS.GET_CARD:
                                    int getcard_recv = (Alway_Listen.Body as BodyAlertAction).CARD;
                                    //해당 카드 번호 줄이는 메소드(매개변수 카드번호(<-getcard_recv))
                                    break;
                                case CONSTANTS.SCRAP_CARD:
                                    int scrap_recv = (Alway_Listen.Body as BodyAlertAction).CARD;
                                    //폐기장에 해당 카드 번호 추가하는 메소드 (매개변수 카드번호(<-scrap_recv))
                                    break;
                            }
                        }
                        break;
                    case CONSTANTS.LOG_SEND:
                        string log_recv = BitConverter.ToString((Alway_Listen.Body as BodyLogSend).LOG);
                        //로그 추가 메서드(매개변수 바이트 로그를 스트링으로 변환한 것 (<-log_recv))
                        break;
                    case CONSTANTS.SCORE_REQUEST:
                        Score_send(S);    //Score에 자기 점수 넣으면됨
                        Recv_Total_Score(TS);     //Total_Score에 모두의 점수 받을 int형 배열 입력
                        //출력 메서드 호출 (매개변수 all_score)
                        return -1;
                }
            }
        }

        public void Attack(int Card_Num)
        {
            Message Amsg = new Message();
            Amsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.ATTACK,
                CARD = Card_Num
            };
            Amsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.ALERT_ACTION,
                BODYLEN = Amsg.Body.GetSize()
            };
            MessageUtil.Send(Stream , Amsg);
        }
        public void Get_Card(int Card_Num)
        {
            Message GCmsg = new Message();
            GCmsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.GET_CARD,
                CARD = Card_Num
            };
            GCmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.ALERT_ACTION,
                BODYLEN = GCmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, GCmsg);
        }
        public void Scrap_Card(int Card_Num)
        {
            Message SCmsg = new Message();
            SCmsg.Body = new BodyAlertAction()
            {
                ACTION = CONSTANTS.SCRAP_CARD,
                CARD = Card_Num
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
                LOG = Encoding.UTF8.GetBytes(Log)
            };
            LSmsg.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.LOG_SEND,
                BODYLEN = LSmsg.Body.GetSize()
            };
            MessageUtil.Send(Stream, LSmsg);
        }
        public void Game_End(int S) //테스트용 수정 필
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
            Score_send(S);
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
