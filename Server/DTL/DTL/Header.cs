using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class Header:ISerializable
    {
        public byte HASBODY { get; set; }
        public int MSGTYPE { get; set; }
        public int BODYLEN { get; set; }

        public Header() { }
        public Header(byte[] bytes)
        {
            HASBODY = bytes[0];
            MSGTYPE = BitConverter.ToInt32(bytes, 1);
            BODYLEN = BitConverter.ToInt32(bytes, 5);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[9];

            bytes[0] = HASBODY;
            byte[] temp = BitConverter.GetBytes(MSGTYPE);
            Array.Copy(temp, 0, bytes, 1, temp.Length);
            temp = BitConverter.GetBytes(BODYLEN);
            Array.Copy(temp, 0, bytes, 5, temp.Length);

            return bytes;
        }
        public int GetSize()
        {
            return 9;
        }
    }
}
