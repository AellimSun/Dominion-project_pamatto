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
    internal class SendServer
    {
        public void sendServer_StartSignal(string ID)
        {
            TransHandler t = new TransHandler("127.0.0.1", 5542);
            Console.WriteLine(t.Start_Matching(ID, ID));
            Thread.Sleep(2000);
            Console.WriteLine(t.Cancle_Matching("AAA"));
            while (true) { };
        }
    }

    public class TransHandler
    {
        private IPEndPoint ServerAddress;
        private IPEndPoint ClientAddress;
        private TcpClient Client;
        private NetworkStream Stream;
        public TransHandler(string Server_IP, int serverPort)
        {
            ClientAddress = new IPEndPoint(0, 0);
            ServerAddress = new IPEndPoint(IPAddress.Parse(Server_IP), serverPort);
            Client = new TcpClient(ClientAddress);
        }
        public int Start_Matching(string ID, string NICK)
        {
            //연결
            Client.Connect(ServerAddress);
            Stream = Client.GetStream();

            //매칭 큐 입력 메시지 전송
            Message STM = new Message();
            STM.Body = new BodyStartMatching()
            {
                ID = Encoding.ASCII.GetBytes(ID),
                NICK = Encoding.ASCII.GetBytes(NICK)
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
                //return (int)(recv.Body as BodyInsertQueue).UserCount;
                return 0;
            }
            else
            {
                return -1;
            }
        }
        //async public int Wait_Queue(int Current_user)
        //{
        //    await Task.Run(async () =>
        //    {

        //    });
        //}
        public int Cancle_Matching(string ID)
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
            Message recv = MessageUtil.Receive(Stream);
            if (recv.Header.MSGTYPE == CONSTANTS.SUCCESS_CANCLE_MATCHING)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
