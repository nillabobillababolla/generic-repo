using System;
using System.ComponentModel.DataAnnotations;
using Generic.Entity.Abstracts;

namespace Generic.Entity.Enums
{
    public abstract class PublicationEntity<T> : Entity<T>, IPublicationEntity<T>
    {
        private PublishStatus? status;
        public PublishStatus Status
        {
            get { return status ?? PublishStatus.Draft; }
            set { status = value; }
        }
        private DateTime? publishDate;

        [DataType(DataType.DateTime)]
        public DateTime? PublishDate
        {
            get
            {
                if (!publishDate.HasValue && Status == PublishStatus.Published)
                {
                    publishDate = DateTime.UtcNow;
                }
                return publishDate;
            }
            set { publishDate = value; }
        }

        [DataType(DataType.DateTime)]
        public DateTime? ExpireDate { get; set; }

    }

    public abstract class PageEntity<T> : PublicationEntity<T>, IPageEntity<T>
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [StringLength(80)]
        [Required]
        public string Slug { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(155)]
        public string Description { get; set; }

        [DataType(DataType.Html)]
        public string Abstract { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

    }
}