namespace ForumSystem.Web.ViewModels.Feedbacks
{
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class FeedbackViewModel : IMapFrom<Feedback>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        [UIHint("MultiLineText")]
        [AllowHtml]
        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}