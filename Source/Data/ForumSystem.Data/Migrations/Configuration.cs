namespace ForumSystem.Data.Migrations
{
    using ForumSystem.Common;
    using ForumSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.SeedUsers(context);
            this.SeedPostsWithTags(context);
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new ApplicationUser
                {
                    Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16), this.random.RandomString(6, 16)),
                    UserName = this.random.RandomString(6, 16)
                };

                this.userManager.Create(user, "111111");
            }
        }

        private void SeedPostsWithTags(ApplicationDbContext context)
        {
            if (context.Posts.Any())
            {
                return;
            }

            var users = context.Users.Take(10).ToList();
            for (int i = 0; i < 10; i++)
            {
                var post = new Post
                {
                    Title = this.random.RandomString(5, 100),
                    Author = users[this.random.RandomNumber(0, users.Count - 1)],
                    Content = this.CreateContent()
                };

                for (int j = 0; j < 3; j++)
                {
                    var tag = new Tag
                    {
                        Name = GetTag(post.Content, j),
                    };

                    post.Tags.Add(tag);
                }

                context.Posts.Add(post);
                context.SaveChanges();
            }
        }

        private string GetTag(string post, int numberOfWord)
        {
            var allWords = post.Split(new[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
            return allWords[numberOfWord];
        }

        private string CreateContent()
        {
            var content = new StringBuilder();
            var wordsInContent = this.random.RandomNumber(20, 100);
            for (int i = 0; i < wordsInContent; i++)
            {
                var currentWord = this.random.RandomString(3, 10);
                content.Append(currentWord);
                var wordsInSentence = this.random.RandomNumber(0, 10);
                if (wordsInSentence == 10)
                {
                    content.Append(". ");
                }
                else
                {
                    content.Append(" ");
                }
            }

            return content.ToString();
        }
    }
}
