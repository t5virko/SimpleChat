using System.ServiceModel;

namespace chat_client
{
    class ServerUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
