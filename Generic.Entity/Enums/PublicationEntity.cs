using System;
using System.ComponentModel.DataAnnotations;
using Generic.Entity.Abstracts;

namespace Generic.Entity.Enums
{
    public abstract class PublicationEntity<T> : Entity<T>, IPublicationEntity<T>
    {
        private PublishStatus? _status;
        public PublishStatus Status
        {
            get => _status ?? PublishStatus.Draft;
            set => _status = value;
        }
        private DateTime? _publishDate;

        [DataType(DataType.DateTime)]
        public DateTime? PublishDate
        {
            get
            {
                if (!_publishDate.HasValue && Status == PublishStatus.Published)
                {
                    _publishDate = DateTime.UtcNow;
                }
                return _publishDate;
            }
            set => _publishDate = value;
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
