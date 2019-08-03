using System;
using System.ComponentModel.DataAnnotations;
using Generic.Entity.Interfaces;

namespace Generic.Entity.Enums
{
    public enum PublishStatus
    {
        [Display(Name = "Draft")]
        Draft,
        [Display(Name = "Pending Review")]
        PendingReview,
        [Display(Name = "Published")]
        Published
    }

    public interface IModifiablePublicationEntity : IModifiableEntity
    {
        PublishStatus Status { get; set; }
        DateTime? PublishDate { get; set; }
        DateTime? ExpireDate { get; set; }
    }
    public interface IPublicationEntity : IModifiablePublicationEntity, IEntity
    { }
    public interface IPublicationEntity<T> : IPublicationEntity, IEntity<T>
    { }
    public interface IModifiablePageEntity : IModifiablePublicationEntity
    {
        string Title { get; set; }
        string Slug { get; set; }
        string Description { get; set; }
        string Abstract { get; set; }
        string Content { get; set; }
    }
    public interface IPageEntity : IModifiablePageEntity, IPublicationEntity
    { }
    public interface IPageEntity<T> : IPageEntity, IPublicationEntity<T>
    { }

}