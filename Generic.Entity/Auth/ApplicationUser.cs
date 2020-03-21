using System;
using System.ComponentModel.DataAnnotations;
using Generic.Entity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Generic.Entity.Auth
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        object IEntity.Id
        {
            get => this.Id;
            set => this.Id = (string)Convert.ChangeType(value, typeof(string));
        }

        public string Name { get; set; }

        private DateTime? _createdDate;

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}