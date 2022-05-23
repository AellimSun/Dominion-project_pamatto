using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL
{
    public class BodyStartMatching : ISerializable
    {
        public byte[] ID;
        public byte[] NICK;

        public BodyStartMatching() { }
        public BodyStartMatching(byte[] bytes)
        {
            ID = new byte[11];
            NICK = new byte[21];
            Array.Copy(bytes, 0, ID, 0, 11);
            Array.Copy(bytes, 11, NICK, 0, 21);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            ID.CopyTo(bytes, 0);
            NICK.CopyTo(bytes, 11);
            return bytes;
        }
        public int GetSize()
        {
            return 32;
        }
    }
    public class BodyInsertQueue : ISerializable
    {
        public uint UserCount;
        public BodyInsertQueue() { }
        public BodyInsertQueue(byte[] bytes)
        {
            UserCount = BitConverter.ToUInt32(bytes, 0);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = BitConverter.GetBytes(UserCount);
            return bytes;
        }
        public int GetSize()
        {
            return 4;
        }
    }

    public class BodyCancleMatching : ISerializable
    {
        public byte[] ID;
        public BodyCancleMatching() { }
        public BodyCancleMatching(byte[] bytes)
        {
            ID = new byte[11];
            Array.Copy(bytes, 0, ID, 0, 11);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            ID.CopyTo(bytes, 0);
            return bytes;
        }
        public int GetSize()
        {
            return 11;
        }
    }

    public class BodyGameStart : ISerializable
    {
        public byte[] NICK1;
        public byte[] NICK2;
        public byte[] NICK3;
        public byte[] NICK4;
        public BodyGameStart() { }
        public BodyGameStart(byte[] bytes)
        {
            NICK1 = new byte[21];
            NICK2 = new byte[21];
            NICK3 = new byte[21];
            NICK4 = new byte[21];
            for (int i = 0; i < 4; i++)
            {
                Array.Copy(bytes, 21 * i, NICK1, 0, 21);
            }
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            NICK1.CopyTo(bytes, 0);
            NICK2.CopyTo(bytes, 21);
            NICK3.CopyTo(bytes, 42);
            NICK4.CopyTo(bytes, 63);
            return bytes;
        }
        public int GetSize()
        {
            return 84;
        }
    }

    public class BodyAlertAction : ISerializable
    {
        public uint ACTION;
        public uint CARD;
        public BodyAlertAction() { }
        public BodyAlertAction(byte[] bytes)
        {
            ACTION = BitConverter.ToUInt32(bytes, 0);
            CARD = BitConverter.ToUInt32(bytes, 4);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(ACTION);
            Array.Copy(temp, 0, bytes, 0, 4);
            temp = BitConverter.GetBytes(CARD);
            Array.Copy(temp, 0, bytes, 4, 4);
            return bytes;
        }
        public int GetSize()
        {
            return 8;
        }
    }

    public class BodyLogSend : ISerializable
    {
        public byte[] LOG;
        public BodyLogSend() { }
        public BodyLogSend(byte[] bytes)
        {
            LOG = new byte[51];
            Array.Copy(bytes, 0, LOG, 0, 51);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            LOG.CopyTo(bytes, 0);
            return bytes;
        }
        public int GetSize()
        {
            return 51;
        }
    }

    public class BodyScoreSend : ISerializable
    {
        public uint SCORE;
        public BodyScoreSend() { }
        public BodyScoreSend(byte[] bytes)
        {
            SCORE = BitConverter.ToUInt32(bytes, 0);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(SCORE);
            Array.Copy(temp, 0, bytes, 0, 4);
            return bytes;
        }
        public int GetSize()
        {
            return 4;
        }
    }

    public class BodyTotalScoreSend : ISerializable
    {
        public uint SCORE1;
        public uint SCORE2;
        public uint SCORE3;
        public uint SCORE4;
        public BodyTotalScoreSend() { }
        public BodyTotalScoreSend(byte[] bytes)
        {
            SCORE1 = BitConverter.ToUInt32(bytes, 0);
            SCORE2 = BitConverter.ToUInt32(bytes, 4);
            SCORE3 = BitConverter.ToUInt32(bytes, 8);
            SCORE4 = BitConverter.ToUInt32(bytes, 12);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(SCORE1);
            Array.Copy(temp, 0, bytes, 0, 4);
            temp = BitConverter.GetBytes(SCORE2);
            Array.Copy(temp, 0, bytes, 4, 4);
            temp = BitConverter.GetBytes(SCORE3);
            Array.Copy(temp, 0, bytes, 8, 4);
            temp = BitConverter.GetBytes(SCORE4);
            Array.Copy(temp, 0, bytes, 12, 4);
            return bytes;
        }
        public int GetSize()
        {
            return 16;
        }
    }
}
