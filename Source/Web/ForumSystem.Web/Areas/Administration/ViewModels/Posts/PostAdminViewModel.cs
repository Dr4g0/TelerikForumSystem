using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using ForumSystem.Data.Models;
using ForumSystem.Web.Infrastructure.Mapping;

namespace ForumSystem.Web.Areas.Administration.ViewModels.Posts
{
    public class PostAdminViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        //Id, Author name, Title, Content, CreatedOn, ModifiedOn and IsDeleted
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Post, PostAdminViewModel>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));
        }
    }
}