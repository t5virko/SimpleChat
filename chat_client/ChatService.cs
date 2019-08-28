using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chat_client
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChatService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int userId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = userId,
                UserName = name,
                operationContext = OperationContext.Current

            };
            userId++;
            SendMessage(" " + user.UserName + " connect to chat!", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(" " + user.UserName + " disconnected!", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.UserName + " "; 
                }
                answer += message;

                item.operationContext.GetCallbackChannel<IChatServiceCallback>().MessageCallback(answer);
            }

        }
    }
}
