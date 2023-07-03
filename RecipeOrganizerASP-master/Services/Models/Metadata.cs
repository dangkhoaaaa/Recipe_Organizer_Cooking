using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Metadata
    {
        //public object feedbackId;

        public int MetadataId { get; set; }
        public string? UserId { get; set; }
        public int? RecipeId { get; set; }
        public int? MediaId { get; set; }
        public int? FeedbackId { get; set; }
        public int? NotificationId { get; set; }

        public virtual Feedback? Feedback { get; set; }
        public virtual Media? Media { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual AppUser? User { get; set; }
        public virtual Notification? Notification { get; set; }
    }
}
