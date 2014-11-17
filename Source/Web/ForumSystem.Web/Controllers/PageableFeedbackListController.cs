using ForumSystem.Data.Common.Repository;
using ForumSystem.Data.Models;
using ForumSystem.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using ForumSystem.Web.ViewModels.Feedbacks;

namespace ForumSystem.Web.Controllers
{
    [Authorize]
    public class PageableFeedbackListController : Controller
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        private readonly ISanitizer sanitizer;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer)
        {
            this.feedbacks = feedbacks;
            this.sanitizer = sanitizer;
        }

        // GET: PageableFeedbackList
        public ActionResult Display()
        {
            var feedbackModels = this.feedbacks
                .All()
                .Project()
                .To<FeedbackListViewModel>()
                .ToList();

            return View(feedbackModels);
        }
    }
}