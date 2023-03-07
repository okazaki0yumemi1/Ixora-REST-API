﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ixora_REST_API.Models
{
    public class Client : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), ]
        public int Id { get; private set; }
        public string ClientName { get; set; } //ФИО
        public string PhoneNumber { get; set; } //Phone number
        public Client()
        {

        }
    }
}
