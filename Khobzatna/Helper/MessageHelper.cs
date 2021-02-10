using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khobzatna.Helper
{
    public class MessageHelper
    {
        public MessageType MessageType { get; set; }
        public string MessageLink { get; set; }
        public string MessageLinkText { get; set; }
        public string Message { get; set; }
    }
    public enum MessageType
    {
        success,
        info,
        warning,
        danger
    }
    
}