
using System;

namespace ChatApp
{
    internal class MessageContent
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }

        public MessageContent(){}
        public MessageContent(string email, string message) {
            Email = email;
            Message = message;
            Time = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}