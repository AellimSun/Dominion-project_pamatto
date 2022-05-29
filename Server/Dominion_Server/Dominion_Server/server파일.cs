using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using DTL;

namespace Dominion_Server
{

    public class Client
    {
        public TcpClient client;
        public NetworkStream stream { get; set; }
        public string ID { get; set; }
        public bool Accept { get; set; }
        public bool respond { get; set; }

    }
    internal class Program
    {
        //private Object lck = new System.Object();
        static void Main(string[] args)
        {
            List<Client> clients = new List<Client>();
            const int Port = 5542; //수정
            int host_number = 1;
            IPEndPoint localAddress = new IPEndPoint(0, Port);
            TcpListener server = new TcpListener(localAddress);

            server.Start();
            Console.WriteLine("Dominion Server Start...");
            while (true)
            {
                //4명 모이기 전까지 계속 돈다
                AddClient(server, clients, host_number);
                if (clients.Count == 4)
                {
                    Create_Game(clients, host_number++);
                    clients = new List<Client>();
                }
            }

        }

        async static private void AddClient(TcpListener server, List<Client> clients, int host_number)
        {

            TcpClient newClient = server.AcceptTcpClient();
            Client tmp = new Client();
            tmp.client = newClient;
            tmp.stream = newClient.GetStream();
            tmp.Accept = false;
            tmp.respond = false;
            //newClient에 대한 정보를 tmp객체의 멤버변수에 정의
            while (true)
            {
                Message recv = MessageUtil.Receive(tmp.stream);
                if (recv.Header.MSGTYPE == CONSTANTS.START_MATCHING)
                {
                    BodyStartMatching bsm = new BodyStartMatching(recv.Body.GetBytes());
                    tmp.ID = Encoding.Default.GetString(bsm.ID);
                    break;
                }
            }
            //List에 있는 Client들에게 메시지 Send(큐에 인원이 들어왔다는 메시지) 
            if (clients.Count != 0)
            {
                Message ADDP = new Message();
                ADDP.Body = null;
                ADDP.Header = new Header()
                {
                    HASBODY = CONSTANTS.NO_BODY,
                    MSGTYPE = CONSTANTS.ADD_PLAYER,
                    BODYLEN = 0
                };
                foreach (Client c in clients)
                {
                    MessageUtil.Send(c.stream, ADDP);
                }
            }
            //lock ()
            //{
            clients.Add(tmp);
            //}
            Console.WriteLine("[Queue] ID = {0} HostNumber = {1} Queue = {2}/4", tmp.ID, host_number, clients.Count);
            //메시지 전송(현재 클라이언트에게 큐에 있는 인원 수를 알려주는 메시지)

            Message I_Q = new Message();
            byte[] num = new byte[4];
            num = BitConverter.GetBytes(clients.Count);

            I_Q.Body = new BodyInsertQueue(num);
            I_Q.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.INSERT_QUEUE,
                BODYLEN = 4
            };
            MessageUtil.Send(tmp.stream, I_Q);


            await Task.Run(() =>
            {
                Client me = clients[clients.Count - 1];
                //10초안에 판단해서 클라이언트가 서버에게 메세지를 넘겨줘야함 (클라이언트 측에서 구현)
                Message recv = MessageUtil.Receive(me.stream);
                switch (recv.Header.MSGTYPE)
                {
                    //큐 도중 매칭 취소
                    case CONSTANTS.CANCLE_MATCHING:
                        BodyCancleMatching bcm = new BodyCancleMatching(recv.Body.GetBytes());
                        for (int i = 0; i < clients.Count; i++)
                        {
                            //ID값을 이용하여 index 결정 -> 해당 List의 client & stream 종료 및 List배열에서 제거 
                            if (clients[i].ID == Encoding.Default.GetString(bcm.ID))
                            {
                                Message SCM = new Message();
                                SCM.Body = null;
                                SCM.Header = new Header()
                                {
                                    HASBODY = CONSTANTS.NO_BODY,
                                    MSGTYPE = CONSTANTS.SUCCESS_CANCLE_MATCHING,
                                    BODYLEN = 0
                                };
                                MessageUtil.Send(me.stream, SCM);
                                me.client.Close();
                                me.stream.Close();

                                //lock()
                                //{
                                clients.RemoveAt(i);
                                //}
                                Console.WriteLine("[CANCLE] ID = {0} HostNumber = {1}", me.ID, host_number);
                                break;
                            }
                        }

                        //매칭 취소 인원이 존재함을 나머지 Client에게 전송
                        {
                            Message SUBP = new Message();
                            SUBP.Body = null;
                            SUBP.Header = new Header()
                            {
                                HASBODY = CONSTANTS.NO_BODY,
                                MSGTYPE = CONSTANTS.SUB_PLAYER,
                                BODYLEN = 0
                            };
                            foreach (Client c in clients)
                            {
                                MessageUtil.Send(c.stream, SUBP);
                            }
                        }
                        break;
                    case CONSTANTS.ACCEPT:
                        me.respond = true;
                        me.Accept = true;
                        Console.WriteLine("[ACCEPT] ID = {0} HostNumber = {1}", me.ID, host_number);
                        break;
                    //큐 종료 후 매칭 취소 (메시지 전송은 Create_Game에서)
                    case CONSTANTS.DECLINE:
                        me.respond = true;
                        //me.Accept = false; 디폴트값
                        Console.WriteLine("[DECLINE] ID = {0} HostNumber = {1}", me.ID, host_number);
                        break;
                }
            });
        }

        async static private void Create_Game(List<Client> clients, int host_number)
        {
            int this_host_number = host_number;
            Client[] full_list = new Client[4];
            List<int> index_list = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                index_list.Add(i);
            }
            for (int i = 0; i < 4; i++)
            {
                Random rand = new Random();
                int next = rand.Next(4 - i);
                full_list[i] = clients[index_list[next]];
                index_list.RemoveAt(next);
            }

            Console.WriteLine("[FULL] HostNumber = {0}", this_host_number);

            await Task.Run(() =>
            {
                Message F_Q = new Message();
                F_Q.Body = null;
                F_Q.Header = new Header()
                {
                    HASBODY = CONSTANTS.NO_BODY,
                    MSGTYPE = CONSTANTS.FULL_QUEUE,
                    BODYLEN = 0
                };
                //AddClient 메서드에서 넘긴 클라이언트에게 큐 인원이 다 찼다는 메시지를 전송
                foreach (Client c in full_list)
                {
                    MessageUtil.Send(c.stream, F_Q);
                }
                while (true)
                {
                    bool All_Respond = true;
                    foreach (Client c in full_list)
                    {
                        All_Respond = All_Respond && c.respond;
                    }
                    if (All_Respond) break;
                }
                bool Ready = true;
                foreach (Client c in full_list)
                {
                    Ready = Ready && c.Accept;
                }
                if (Ready)
                {
                    Message P_G = new Message();
                    P_G.Body = new BodyGameStart()
                    {
                        ID1 = Encoding.ASCII.GetBytes(full_list[0].ID),
                        ID2 = Encoding.ASCII.GetBytes(full_list[1].ID),
                        ID3 = Encoding.ASCII.GetBytes(full_list[2].ID),
                        ID4 = Encoding.ASCII.GetBytes(full_list[3].ID),
                        HostNumber = this_host_number
                    };
                    P_G.Header = new Header()
                    {
                        HASBODY = CONSTANTS.HAS_BODY,
                        MSGTYPE = CONSTANTS.GAME_START,
                        BODYLEN = P_G.Body.GetSize()
                    };

                    foreach (Client c in full_list)
                    {
                        MessageUtil.Send(c.stream, P_G);
                    }
                    Console.WriteLine("[START] ID1 = {0} ID2 = {1} ID3 = {2} ID4 = {3} HostNumber = {4}", full_list[0].ID, full_list[1].ID, full_list[2].ID, full_list[3].ID, this_host_number);
                    Play_Game(full_list);
                }
                else
                {
                    Console.WriteLine("[BREAK] HostNumber = {0}", this_host_number);
                    foreach (Client c in full_list)
                    {
                        Message Boob_Game = new Message();
                        Boob_Game.Body = null;
                        Boob_Game.Header = new Header()
                        {
                            HASBODY = CONSTANTS.NO_BODY,
                            MSGTYPE = CONSTANTS.GAME_CANCLE,
                            BODYLEN = 0
                        };

                        MessageUtil.Send(c.stream, Boob_Game);

                        c.client.Close();
                        c.stream.Close();
                    }
                }
            });
        }

        static bool Matching_Character(string CardName, string Name)
        {
            bool strMatch = true;

            for (int i = 0; i < Name.Length; i++)
            {
                strMatch = strMatch && (CardName[i] == Name[i]);
            }

            return strMatch;
        }

        static public void Play_Game(Client[] full_list)
        {
            /*
                1. Game 스타트
            턴시작 -> 메시지 받기, 주기 -> 턴 종료
            유저1---------
                1 <- 턴 시작 메세지 전송 list[0] , 나머지 유저들의 정보 저장 리스트 필요?!
                2
                3  
                4             
             */
            int next_player = 0;
            bool turn_end = false;

            while (true)
            {
                //TURN_SEND
                Message Start_Turn = new Message();
                Start_Turn.Body = null;
                Start_Turn.Header = new Header()
                {
                    HASBODY = CONSTANTS.NO_BODY,
                    MSGTYPE = CONSTANTS.TURN_SEND,
                    BODYLEN = 0
                };

                //첫턴을 넘겨준다
                MessageUtil.Send(full_list[next_player].stream, Start_Turn);

                //2. 주고받기
                while (true)
                {
                    Message R_Msg = MessageUtil.Receive(full_list[next_player].stream);

                    switch (R_Msg.Header.MSGTYPE)
                    {
                        case CONSTANTS.LOG_SEND:
                            {
                                foreach (Client c in full_list)
                                {
                                    MessageUtil.Send(c.stream, R_Msg);
                                }
                                Console.WriteLine(Encoding.Default.GetString((R_Msg.Body as BodyLogSend).LOG));
                            }
                            break;

                        case CONSTANTS.ALERT_ACTION:
                            {
                                Console.WriteLine(Encoding.Default.GetString((R_Msg.Body as BodyAlertAction).CARD));

                                switch ((R_Msg.Body as BodyAlertAction).ACTION)
                                {
                                    case CONSTANTS.GET_CARD:
                                    case CONSTANTS.SCRAP_CARD:
                                        {
                                            for (int i = 0; i < 4; i++)
                                            {
                                                if (next_player != i)
                                                {
                                                    MessageUtil.Send(full_list[i].stream, R_Msg);
                                                }
                                            }
                                        }
                                        break;
                                    case CONSTANTS.ATTACK:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (next_player != i)
                                            {
                                                MessageUtil.Send(full_list[i].stream, R_Msg);
                                            }
                                        }

                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (next_player == i)
                                            {
                                                continue;
                                            }

                                            string CardName = Encoding.Default.GetString((R_Msg.Body as BodyAlertAction).CARD);
                                            Console.WriteLine(CardName);
                                            Message recv_res = MessageUtil.Receive(full_list[i].stream);
                                            Console.WriteLine("1");     //테스트용
                                            Console.WriteLine(CardName);

                                            if (Matching_Character(CardName, "witch"))
                                            {
                                                Console.WriteLine("2"); //테스트용

                                                string Log = "";
                                                if (recv_res.Header.MSGTYPE == CONSTANTS.LOG_SEND)
                                                {
                                                    Console.WriteLine("3"); //테스트용
                                                    //해자가 없다면 저주 카드 획득 로그 전달

                                                    Log = Encoding.Default.GetString((recv_res.Body as BodyLogSend).LOG);
                                                    Console.WriteLine(Log + "으아아악!");   //테스트용
                                                    recv_res = MessageUtil.Receive(full_list[i].stream);
                                                    if (recv_res.Header.MSGTYPE == CONSTANTS.LOG_SEND)
                                                    {
                                                        foreach (Client c in full_list)
                                                        {
                                                            MessageUtil.Send(c.stream, recv_res);
                                                        }
                                                    }
                                                }
                                                Console.WriteLine(Log); //테스트용


                                                //보낸 로그의 내용이 저주를 먹었다는 내용이라면 
                                                if (Log.Contains("curse"))
                                                {
                                                    Console.WriteLine("4"); //테스트용

                                                    //클라이언트 측에서 저주 먹음 alert를 보냄
                                                    Message recv_alert = MessageUtil.Receive(full_list[i].stream);
                                                    Console.WriteLine(Encoding.Default.GetString((recv_alert.Body as BodyAlertAction).CARD) + "<- 이거 먹었데 ㅋㅋ");

                                                    if (recv_alert.Header.MSGTYPE == CONSTANTS.ALERT_ACTION)
                                                    {
                                                        Console.WriteLine("5"); //테스트용
                                                        for (int j = 0; j < 4; j++)
                                                        {
                                                            //다른 플레이어에게 전달
                                                            if (i != j)
                                                            {
                                                                MessageUtil.Send(full_list[j].stream, recv_res);
                                                                Console.WriteLine(j.ToString()
                                                                    + "유저에게 Get_Card : "
                                                                    + (recv_res.Body as BodyAlertAction).CARD);//테스트용

                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //공격자 클라이언트에게 Listen_Method를 쓰레드를 종료하라는 메세지 전송
                                        Message exc_msg = new Message();
                                        exc_msg.Body = null;
                                        exc_msg.Header = new Header()
                                        {
                                            HASBODY = CONSTANTS.NO_BODY,
                                            MSGTYPE = CONSTANTS.EXCAPE_LISTEN_METHOD,
                                            BODYLEN = 0
                                        };
                                        MessageUtil.Send(full_list[next_player].stream, exc_msg);

                                        break;
                                }
                            }
                            break;

                        case CONSTANTS.TURN_END:
                            {
                                next_player++;
                                if (next_player == 4)
                                    next_player = 0;

                                turn_end = true;
                            }
                            break;

                        case CONSTANTS.GAME_FIN:
                            {
                                game_Fin(full_list);
                            }
                            return;
                    }

                    if (turn_end)
                    {
                        turn_end = false;
                        break;
                    }

                }

            }

        }

        static public void game_Fin(Client[] full_list)
        {
            //시장 카드3개 솔드아웃 or 속주 솔드아웃
            // => 점수계산

            //점수를 저장 할 공간
            int[] U_Score = new int[4];


            //1. 점수주세요->client
            Message Score_Req = new Message();
            Score_Req.Body = null;
            Score_Req.Header = new Header()
            {
                HASBODY = CONSTANTS.NO_BODY,
                MSGTYPE = CONSTANTS.SCORE_REQUEST,
                BODYLEN = 0
            };

            foreach (Client c in full_list)
            {
                MessageUtil.Send(c.stream, Score_Req);

            }

            //2. client 요청응답 받기
            for (int i = 0; i < 4; i++)
            {
                Message Score_Rec = MessageUtil.Receive(full_list[i].stream);

                if (Score_Rec.Header.MSGTYPE == CONSTANTS.SCORE_SEND)
                {
                    U_Score[i] = (Score_Rec.Body as BodyScoreSend).SCORE;
                }

            }

            Message Score_Final = new Message();
            Score_Final.Body = new BodyTotalScoreSend()
            {
                SCORE1 = U_Score[0],
                SCORE2 = U_Score[1],
                SCORE3 = U_Score[2],
                SCORE4 = U_Score[3]
            };

            Score_Final.Header = new Header()
            {
                HASBODY = CONSTANTS.HAS_BODY,
                MSGTYPE = CONSTANTS.TOTAL_SCORE_SEND,
                BODYLEN = Score_Final.Body.GetSize()
            };

            //3. 점수 다시보내기 및 끊기
            foreach (Client c in full_list)
            {
                MessageUtil.Send(c.stream, Score_Final);

                c.stream.Close();
                c.client.Close();

            }
        }
    }
}
