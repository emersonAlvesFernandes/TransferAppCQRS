﻿using System;
using TransferAppCQRS.Domain.Core.Models;

namespace TransferAppCQRS.Domain.Models
{
    public class Customer : Entity
    {
        public Customer(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected Customer() { }

        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public DateTime BirthDate { get; private set; }
        
        public Account Account { get; set; }
    }
}
