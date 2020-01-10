using System;
using Generic.Entity.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Generic.Entity.Abstracts
{
    public abstract class Entity<T> : IFlaggedEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public abstract T Id { get; set; }

        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (T)Convert.ChangeType(value, typeof(T)); }
        }
        public string Name { get; set; }

        private DateTime? _createdDate;

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get { return _createdDate ?? DateTime.UtcNow; }
            set { _createdDate = value; }
        }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        private DateTime? _deletedDate;

        [DataType(DataType.DateTime)]
        public DateTime? DeletedDate
        {
            get
            {
                return _deletedDate ?? DateTime.UtcNow;
            }
            set { _deletedDate = value; }
        }
        public string DeletedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public T ShallowCopy()
        {
            return (T)this.MemberwiseClone();
        }

        public T DeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
