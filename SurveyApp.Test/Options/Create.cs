using Microsoft.AspNetCore.Mvc;
using SurveyApp.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SurveyApp.Test.Options
{
    public class Create
    {
        [Fact]
        public async Task CreateOption()
        {
            using (var db = new PostgresContext(Utilities.DB.TestDbContextOptions()))
            {
                Option option = new Option();
                option.Name = "Unit Test Option";
                option.Description = "First Test for create metod";
                var createModel = new Pages.Options.CreateModel(db);
                createModel.Option = option;
                var result = await createModel.OnPostAsync();
                Assert.IsType<RedirectToPageResult>(result);
            }
        }

        [Fact]
        public async Task CreateNullOption()
        {
            using (var db = new PostgresContext(Utilities.DB.TestDbContextOptions()))
            {
                Option option = new Option();
                var createModel = new Pages.Options.CreateModel(db);
                createModel.Option = option;
                var result = await createModel.OnPostAsync();
                Assert.IsType<RedirectToPageResult>(result);
            }
        }
    }
}
