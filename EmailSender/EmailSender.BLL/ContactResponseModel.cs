﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmailSender.BLL
{
    public class ContactResponseModel
    {
        [JsonProperty("Full Name")]
        public string FullName { get; set; }
        [JsonProperty("Company Name")]
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public Guid? Guid { get; set; }
        public List<string> EmailLists { get; set; }
    }
}
