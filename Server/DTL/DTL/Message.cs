using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class CONSTANTS
    {
        public const byte HAS_BODY = 0x01;
        public const byte NO_BODY = 0x00;

        public const int ENTER_SERVER = 0x01;              //x
        public const int SUCCESS_SERVER = 0x02;            //x
        public const int START_MATCHING = 0x03;            //ID(string)
        public const int INSERT_QUEUE = 0x04;              //user_count(int)
        public const int CANCLE_MATCHING = 0x05;           //ID(string)
        public const int SUCCESS_CANCLE_MATCHING = 0x06;   //x
        public const int ADD_PLAYER = 0x07;                //x
        public const int SUB_PLAYER = 0x08;                //x
        public const int FULL_QUEUE = 0x09;                //x
        public const int ACCEPT = 0x0A;                    //x
        public const int DECLINE = 0x0B;                   //x
        public const int GAME_START = 0x0C;                //ID1(string), ID2(string), ID3(string), ID4(string), Host_Number(int)
        public const int GAME_CANCLE = 0x0D;               //x
        public const int ALERT_ACTION = 0x0E;              //TYPE(int), CARD(int)
        public const int LOG_SEND = 0x0F;                  //LOG(string)
        public const int TURN_END = 0x10;                  //x
        public const int TURN_SEND = 0x11;                 //x
        public const int GAME_FIN = 0x12;                  //x
        public const int SCORE_REQUEST = 0x13;             //x
        public const int SCORE_SEND = 0x14;                //SCORE(int)
        public const int TOTAL_SCORE_SEND = 0x15;          //SCORE1(int), SCORE2(int), SCORE3(int), SCORE4(int)
        public const int EXCAPE_LISTEN_METHOD = 0x16;      //x

        public const int GET_CARD = 0x01;
        public const int SCRAP_CARD = 0x02;
        public const int ATTACK = 0x03;

    }

    public interface ISerializable
    {
        byte[] GetBytes();
        int GetSize();
    }
    public class Message : ISerializable
    {
        public Header Header { get; set; }
        public ISerializable Body { get; set; }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            Header.GetBytes().CopyTo(bytes, 0);
            if (Header.HASBODY == CONSTANTS.HAS_BODY)
                Body.GetBytes().CopyTo(bytes, Header.GetSize());

            return bytes;
        }

        public int GetSize()
        {
            if (Header.HASBODY == CONSTANTS.HAS_BODY)
                return Header.GetSize() + Body.GetSize();
            else
                return Header.GetSize();
        }
    }
}
