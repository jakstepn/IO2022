using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(){}

        public Message(Guid id, string subject, string content)
        {
            Id = id;
            Subject = subject;
            Content = content;
        }
    }
}
