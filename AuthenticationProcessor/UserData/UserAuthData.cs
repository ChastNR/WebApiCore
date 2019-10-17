using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationProcessor.UserData
{
    public class UserAuthData
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        //public Guid UserId { get; set; }
        public int NumberOfFailLoginAttempts { get; set; } = 0;

        [MaxLength(15)] public string LastUsedIp { get; set; }

        public int IpChangeCounter { get; set; } = 0;
        public DateTime IpChangeDate { get; set; } = DateTime.Now;

        [MaxLength(255)] public string UserAgent { get; set; }
        public CountryCodes CountryCode { get; set; }
    }
}