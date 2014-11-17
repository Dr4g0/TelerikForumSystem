using AutoMapper;
using ForumSystem.Data.Models;
using ForumSystem.Web.Infrastructure.Mapping;
using System;
using System.Linq;

namespace ForumSystem.Web.ViewModels.Feedbacks
{
    public class FeedbackListViewModel : IMapFrom<Feedback>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Feedback, FeedbackListViewModel>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));
        }
    }
}