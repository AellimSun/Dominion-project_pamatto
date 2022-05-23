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
        public string NICK { get; set; }
        public bool Accept { get; set; }

        public Client DeepCopy()
        {
            Client newClient = new Client();
            newClient.client = this.client;
            newClient.stream = this.stream;
            newClient.ID = this.ID;
            newClient.NICK = this.NICK;
            newClient.Accept = this.Accept;
            return newClient;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients= new List<Client>();
            const int Port = 5542; //수정
            IPEndPoint localAddress = new IPEndPoint(0, Port);
            TcpListener server = new TcpListener(localAddress);

            server.Start();
            while (true)
            {
                //4명 모이기 전까지 계속 돈다
                AddClient(server, clients);

                if (clients.Count == 4)
                {
                    Create_Game(clients);
                    clients = new List<Client>();
                }
            }

        }

        async static private void AddClient(TcpListener server, List<Client> clients)
        {

            TcpClient newClient = server.AcceptTcpClient();
            Client tmp = new Client();
            tmp.client = newClient;
            tmp.stream = newClient.GetStream();
            tmp.Accept = false;
            //newClient에 대한 정보를 tmp객체의 멤버변수에 정의
            while (true)
            {
                Message recv = MessageUtil.Receive(tmp.stream);
                if (recv.Header.MSGTYPE == CONSTANTS.START_MATCHING)
                {
                    BodyStartMatching bsm = new BodyStartMatching(recv.Body.GetBytes());
                    tmp.ID = Encoding.Default.GetString(bsm.ID);
                    tmp.NICK = Encoding.Default.GetString(bsm.NICK);
                    break;
                }
            }
            //List에 있는 Client들에게 메시지 Send(큐에 인원이 들어왔다는 메시지) 
            //if (clients.Count != 0)
            //{
            //    Message ADDP = new Message();
            //    ADDP.Body = null;
            //    ADDP.Header = new Header()
            //    {
            //        HASBODY = CONSTANTS.NO_BODY,
            //        MSGTYPE = CONSTANTS.ADD_PLAYER,
            //        BODYLEN = 0
            //    };
            //    foreach (Client c in clients)
            //    {
            //        MessageUtil.Send(c.stream, ADDP);
            //    }
            //}
            clients.Add(tmp);
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
            foreach (Client c in clients)
            {
                MessageUtil.Send(c.stream, I_Q);
            }

            await Task.Run(async () =>
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
                                clients.RemoveAt(i);
                                break;
                            }
                        }
                        
                        //매칭 취소 인원이 존재함을 나머지 Client에게 전송
                        //{
                        //    Message SUBP = new Message();
                        //    SUBP.Body = null;
                        //    SUBP.Header = new Header()
                        //    {
                        //        HASBODY = CONSTANTS.NO_BODY,
                        //        MSGTYPE = CONSTANTS.SUB_PLAYER,
                        //        BODYLEN = 0
                        //    };
                        //    foreach (Client c in clients)
                        //    {
                        //        MessageUtil.Send(c.stream, SUBP);
                        //    }
                        //}
                        break;
                    case CONSTANTS.ACCEPT:
                        me.Accept = true;
                        break;
                    //큐 종료 후 매칭 취소 (메시지 전송은 Create_Game에서)
                    case CONSTANTS.DECLINE:
                        //me.Accept = false; 디폴트값
                        break;
                }
            });
        }

        async static private void Create_Game(List<Client> clients)
        {
            Client[] full_list = new Client[4];
            List<int> index_list = new List<int>();
            for(int i=0; i<4; i++)
            {
                index_list.Add(i);
            }
            for (int i = 0; i < 4; i++)
            {
                Random rand = new Random();
                int next = rand.Next(0, 4 - i);
                full_list[i] = clients[index_list[next]];
                index_list.RemoveAt(next);
            }
            await Task.Run(async () =>
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
                await Task.Delay(16000);
                bool Ready = true;
                foreach (Client c in full_list)
                {
                    Ready = Ready && c.Accept;
                }

                if (Ready)
                {
                    byte[] bytes = new byte[84];
                    for (int i=0; i<4; i++)
                    {
                        byte[] tmp = Encoding.ASCII.GetBytes(clients[i].NICK);
                        Array.Copy(tmp, 0, bytes, 21 * i, 21);
                    }
                    Message P_G = new Message();
                    P_G.Body = new BodyGameStart(bytes);
                    P_G.Header = new Header()
                    {
                        HASBODY = CONSTANTS.HAS_BODY,
                        MSGTYPE = CONSTANTS.GAME_START,
                        BODYLEN = 84
                    };
                    foreach (Client c in full_list)
                    {
                        MessageUtil.Send(c.stream, P_G);
                    }
                    Play_Game(full_list);
                }
                else
                {
                    foreach(Client c in full_list)
                    {
                        // 메시지 전송 필요(게임 취소 인원에 의해 방 폭파 메시지 전송)
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
        static public void Play_Game(Client[] full_list)
        {

        }
    }
}
