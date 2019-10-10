using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationProcessor.UserData
{
    public class UserAuthData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public Guid UserId { get; set; }
        public string PasswordHash { get; set; }
        public byte NumberOfFailLoginAttempts { get; set; } = 0;
        
        [MaxLength(15)]
        public string LastUsedIp { get; set; }
        public int IpChangeCounter { get; set; }
        public DateTime IpChangeDate { get; set; } = DateTime.Now;
        
        [MaxLength(50)]
        public string Device { get; set; }
        public CountryCodes CountryCode { get; set; }
    }
}