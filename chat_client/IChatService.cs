using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chat_client
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract(CallbackContract =typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id);
    }

    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay =true)]

        void MessageCallback(string message);
    }
}
