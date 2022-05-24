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

        public const uint ENTER_SERVER = 0x01;              //x
        public const uint SUCCESS_SERVER = 0x02;            //x
        public const uint START_MATCHING = 0x03;            //ID(string)
        public const uint INSERT_QUEUE = 0x04;              //user_count(uint)
        public const uint CANCLE_MATCHING = 0x05;           //ID(string)
        public const uint SUCCESS_CANCLE_MATCHING = 0x06;   //x
        public const uint ADD_PLAYER = 0x07;                //x
        public const uint SUB_PLAYER = 0x08;                //x
        public const uint FULL_QUEUE = 0x09;                //x
        public const uint ACCEPT = 0x0A;                    //x
        public const uint DECLINE = 0x0B;                   //x
        public const uint GAME_START = 0x0C;                //ID1(string), ID2(string), ID3(string), ID4(string)
        public const uint GAME_CANCLE = 0x0D;               //x
        public const uint ALERT_ACTION = 0x0E;              //TYPE(uint), CARD(uint)
        public const uint LOG_SEND = 0x0F;                  //LOG(string)
        public const uint TURN_END = 0x10;                  //x
        public const uint TURN_SEND = 0x11;                 //x
        public const uint GAME_FIN = 0x12;                  //x
        public const uint SCORE_REQUEST = 0x13;             //x
        public const uint SCORE_SEND = 0x14;                //SCORE(uint)
        public const uint TOTAL_SCORE_SEND = 0x15;          //SCORE1(uint), SCORE2(uint), SCORE3(uint), SCORE4(uint)

        public const uint GET_CARD = 0x01;
        public const uint SCRAP_CARD = 0x02;
        public const uint ATTACK = 0x03;

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
            if(Header.HASBODY == CONSTANTS.HAS_BODY)
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
