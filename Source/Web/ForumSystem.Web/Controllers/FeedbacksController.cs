using ForumSystem.Data.Common.Repository;
using ForumSystem.Data.Models;
using ForumSystem.Web.Infrastructure;
using ForumSystem.Web.ViewModels.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;

namespace ForumSystem.Web.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        public FeedbacksController(IDeletableEntityRepository<Feedback> feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        // GET: Feedback
        public ActionResult Index(int id)
        {
            var feedbackModel = this.feedbacks
                .All()
                .Where(f => f.Id == id)
                .Project()
                .To<FeedbackViewModel>()
                .FirstOrDefault();

            return View(feedbackModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedbackViewModel feedbackModel)
        {
            if (ModelState.IsValid)
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    feedbackModel.AuthorId = this.User.Identity.GetUserId();
                }

                var feedback = new Feedback
                {
                    Title = feedbackModel.Title,
                    Content = feedbackModel.Content,
                    AuthorId = feedbackModel.AuthorId,
                };

                this.feedbacks.Add(feedback);
                this.feedbacks.SaveChanges();
                TempData["Success"] = "The feedback was leaved";
                return RedirectToAction("Index", new { id = feedback.Id });
            }

            return this.View(feedbackModel);
        }
    }
}