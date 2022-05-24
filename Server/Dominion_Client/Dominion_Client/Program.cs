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
    }
}
