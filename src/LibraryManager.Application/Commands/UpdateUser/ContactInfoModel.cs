﻿using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class ContactInfoModel
    {
        public string Email { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }

        public ContactInfo ToValueObject() => new ContactInfo(Email, Website, PhoneNumber);
    }
}