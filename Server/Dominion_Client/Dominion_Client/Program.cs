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
    internal class Program
    {
        static void Main(string[] args)
        {
            int A = 0;
            string[] OID = new string[4];
            string myID="";
            Random ran = new Random();
            myID += (char)ran.Next('a', 'z');
            TransHandler t = new TransHandler("127.0.0.1", 5542,myID);
            Console.WriteLine(myID);
            t.Start_Matching();
            if (t.Wait_Full_Queue(A) == 1)
            {
                Console.WriteLine("다찾음");
                t.Respond(1, OID);
            }
            
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
                BODYLEN = (uint)STM.Body.GetSize()
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
                   BODYLEN = (uint)CNM.Body.GetSize()
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

        public void Game_Listener(bool Hyeaja)  //내 차례가 아닐 때 수신대기하는 메서드
        {
            while (true)
            {
                Message Alway_Listen = MessageUtil.Receive(Stream);
                switch (Alway_Listen.Header.MSGTYPE)
                {
                    case CONSTANTS.TURN_SEND:
                        return;
                    case CONSTANTS.ALERT_ACTION:
                        {
                            switch((Alway_Listen.Body as BodyAlertAction).ACTION)
                            {
                                case CONSTANTS.ATTACK:
                                    switch((Alway_Listen.Body as BodyAlertAction).CARD)
                                    {
                                        case 00: //마녀 번호 입력해야됨
                                            //마녀 사용 이펙트 호출
                                            if (Hyeaja)
                                            {
                                                //방어 이펙트 호출
                                                Message Send_Log = new Message();
                                                Send_Log.Body = new BodyLogSend()
                                                {
                                                    //해자카드에 대한 로그 정의를 확실히 한 후 추가
                                                    //LOG = 
                                                };
                                                Send_Log.Header = new Header()
                                                {
                                                    HASBODY = CONSTANTS.HAS_BODY,
                                                    MSGTYPE = CONSTANTS.LOG_SEND,
                                                    BODYLEN = (uint)Send_Log.Body.GetSize()
                                                };
                                                MessageUtil.Send(Stream, Send_Log);
                                            }
                                            else
                                            {
                                                //저주 획득 이펙트 호출
                                                //저주 카드 개수 줄이는 메서드 호출

                                                Message GetCard = new Message();
                                                GetCard.Body = new BodyAlertAction()
                                                {
                                                    ACTION = CONSTANTS.GET_CARD,
                                                    CARD = 01       //저주 카드 번호 입력 필요함
                                                };
                                                GetCard.Header = new Header()
                                                {
                                                    HASBODY = CONSTANTS.HAS_BODY,
                                                    MSGTYPE = CONSTANTS.ALERT_ACTION,
                                                    BODYLEN = (uint)GetCard.Body.GetSize()
                                                };

                                                Message Send_Log = new Message();
                                                Send_Log.Body = new BodyLogSend()
                                                {
                                                    //저주카드에 대한 로그 정의를 확실히 한 후 추가
                                                    //LOG = 
                                                };
                                                Send_Log.Header = new Header()
                                                {
                                                    HASBODY = CONSTANTS.HAS_BODY,
                                                    MSGTYPE = CONSTANTS.LOG_SEND,
                                                    BODYLEN = (uint)Send_Log.Body.GetSize()
                                                };

                                                MessageUtil.Send(Stream, GetCard);
                                                MessageUtil.Send(Stream, Send_Log);
                                            }
                                            break;
                                    }
                                    break;
                                case CONSTANTS.GET_CARD:
                                    uint getcard_recv = (Alway_Listen.Body as BodyAlertAction).CARD;
                                    //해당 카드 번호 줄이는 메소드(매개변수 카드번호(<-getcard_recv))
                                    break;
                                case CONSTANTS.SCRAP_CARD:
                                    uint scrap_recv = (Alway_Listen.Body as BodyAlertAction).CARD;
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
                        Message my_score = new Message();
                        my_score.Body = new BodyScoreSend()
                        {
                            //Score = 스코어 계산하는 메서드 호출
                        };
                        my_score.Header = new Header()
                        {
                            HASBODY = CONSTANTS.HAS_BODY,
                            MSGTYPE = CONSTANTS.SCORE_SEND,
                            BODYLEN = (uint)my_score.Body.GetSize()
                        };
                        MessageUtil.Send(Stream, my_score);

                        Message All_Score = MessageUtil.Receive(Stream);
                        int[] all_score = new int[4];
                        all_score[0] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE1;
                        all_score[1] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE2;
                        all_score[2] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE3;
                        all_score[3] = (int)(All_Score.Body as BodyTotalScoreSend).SCORE4;

                        //출력 메서드 호출 (매개변수 all_score)
                        break;
                }   
            }
        }

        public void 
    }
}
