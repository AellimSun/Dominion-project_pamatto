using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DTL
{
    public class MessageUtil
    {
        public static void Send(Stream writer, Message msg)
        {
            writer.Write(msg.GetBytes(), 0, msg.GetSize());
        }
        public static Message Receive(Stream reader)
        {
            int totalRecv = 0;
            int sizeToRead = 9;
            byte[] hBuffer = new byte[sizeToRead];

            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0)
                    return null;
                buffer.CopyTo(hBuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            Header header = new Header(hBuffer);
            byte[] bBuffer = null;

            if (header.HASBODY == CONSTANTS.HAS_BODY)
            {
                totalRecv = 0;
                bBuffer = new byte[header.BODYLEN];
                sizeToRead = (int)header.BODYLEN;

                while (sizeToRead > 0)
                {
                    byte[] buffer = new byte[sizeToRead];
                    int recv = reader.Read(buffer, 0, sizeToRead);
                    if (recv == 0)
                        return null;

                    buffer.CopyTo(bBuffer, totalRecv);
                    totalRecv += recv;
                    sizeToRead -= recv;
                }
            }
            ISerializable body = null;
            switch (header.MSGTYPE)
            {
                case CONSTANTS.START_MATCHING:
                    body = new BodyStartMatching(bBuffer);
                    break;
                case CONSTANTS.INSERT_QUEUE:
                    body = new BodyInsertQueue(bBuffer);
                    break;
                case CONSTANTS.CANCLE_MATCHING:             //확인필요
                    body = new BodyCancleMatching(bBuffer);
                    break;
                case CONSTANTS.GAME_START:
                    body = new BodyGameStart(bBuffer);
                    break;
                case CONSTANTS.ALERT_ACTION:
                    body = new BodyAlertAction(bBuffer);
                    break;
                case CONSTANTS.LOG_SEND:
                    body = new BodyLogSend(bBuffer);
                    break;
                case CONSTANTS.SCORE_SEND:
                    body = new BodyScoreSend(bBuffer);
                    break;
                case CONSTANTS.TOTAL_SCORE_SEND:
                    body = new BodyTotalScoreSend(bBuffer);
                    break;
            }
            return new Message() { Header = header, Body = body };
        }
    }
}