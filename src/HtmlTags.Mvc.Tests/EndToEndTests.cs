using System.Web.Mvc;
using HtmlTags.Conventions;
using HtmlTags.Mvc.Bootstrap;
using NUnit.Framework;

namespace HtmlTags.Mvc.Tests
{
    [TestFixture]
    public class EndToEndTests
    {
        [SetUp]
        public void SetUp()
        {
            var conventions = new ElementConventionLibrary();
            conventions.EditorsAlways().Modify(x => x.CurrentTag.AddClass("col-md-5"));

            HtmlTagsConfiguration.Configure(new HtmlConventionLibrary[]
                                            {
                                                new BootstrapConventions(),
                                                conventions,
                                            });
        }

        [Test]
        public void BoolRequest()
        {
            var request = new ElementRequest
                          {
                              Intent = TagRequestIntent.Edit,
                              Name = "FooBar",
                              Id = "gooBar",
                              DisplayName = "Foo Bar"
                          }.SetType<bool>();

            var result = request.Build();
            Assert.AreEqual("<div class=\"form-group col-md-5\"><label for=\"gooBar\">Foo Bar<input type=\"checkbox\" value=\"True\" id=\"gooBar\" name=\"FooBar\" class=\"form-control\" /></label></div>",
               result.ToString());
        }

        [Test]
        public void StringRequest()
        {
            var request = new ElementRequest
                          {
                              Intent = TagRequestIntent.Edit,
                              Name = "FooBarEmail",
                              Id = "fooBar",
                              DisplayName = "Foo Bar"
                          }.SetType<string>();

            var result = request.Build();


            Assert.AreEqual("<div class=\"form-group col-md-5\"><label for=\"fooBar\">Foo Bar<input type=\"email\" id=\"fooBar\" name=\"FooBarEmail\" class=\"form-control\" /></label></div>",
                result.ToString());
        }

        [Test]
        public void StringRequestWithModelErrors()
        {
            var modelState = new ModelState();
            modelState.Errors.Add("Whoops");

            var request = new ElementRequest
            {
                Intent = TagRequestIntent.Edit,
                Name = "FooBarEmail",
                Id = "fooBar",
                DisplayName = "Foo Bar",
                ModelState = modelState,
                Required = true
            }.SetType<string>();

            var result = request.Build();


            Assert.AreEqual("<div class=\"form-group has-feedback has-error col-md-5\"><label for=\"fooBar\">Foo Bar<input type=\"email\" id=\"fooBar\" name=\"FooBarEmail\" class=\"input-validation-error form-control\" /><span class=\"glyphicon glyphicon-remove form-control-feedback\"></span></label><span class=\"text-danger\">Whoops</span></div>",
                result.ToString());
        }
    }
}