using System.Collections;
using ForumSystem.Data.Common.Repository;
using ForumSystem.Data.Models;
using ForumSystem.Web.Areas.Administration.ViewModels.Posts;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace ForumSystem.Web.Areas.Administration.Controllers
{
    public class PostsController : KendoGridAdministrationController
    {
        public PostsController(IDeletableEntityRepository<Post> posts) : base(posts)
        {
        }

        protected override IEnumerable GetData()
        {
            return this.posts
                .All()
                .Project()
                .To<PostAdminViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.posts.GetById(id) as T;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts_Read([DataSourceRequest] DataSourceRequest request)
        {
            var posts = this.GetData();
            return Json(posts.ToDataSourceResult(request));
        }
    }
}