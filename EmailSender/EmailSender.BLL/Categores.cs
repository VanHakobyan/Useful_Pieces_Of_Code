using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.BLL
{
    public class Categores
    {
        public string CategoreName { get; set; }
        public List<Profile> Profiles { get; set; }

    }

    public class Profile
    {
        public string Nickname { get; set; }
        public string Abonnés { get; set; }
        public string ProfilePageUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string eMail { get; set; }
        public string ContactPageURL { get; set; }
        public string InstagramURL { get; set; }
        public string FacebookURL { get; set; }
    }
}
