﻿namespace Tools.Messages.EmailSender
{
    public class EmailOptions
    {
        public string PrimaryDomain { get; set; }
        
        public int PrimaryPort { get; set; }
        
        public string UsernameEmail { get; set; }
        
        public string UsernamePassword { get; set; }
        
        public string FromEmail { get; set; }
        
        public string DisplayName { get; set; }
    }
} 