using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace MyMessage
{
    //[Serializable]
    public class Merchant : IMessage
    {
        public string AppId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public object GetBody()
        {
            return this;
        }

        public MessageProperties Properties { get; }
        public Type MessageType { get; }
    }
}
