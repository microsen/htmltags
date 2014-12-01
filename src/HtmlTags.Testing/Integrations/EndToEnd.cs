using System;
using HtmlTags.Conventions;
using HtmlTags.Testing.Conventions;
using NUnit.Framework;

namespace HtmlTags.Testing.Integrations
{
    [TestFixture]
    public class EndToEnd
    {
        private TagLibrary<FakeSubject> _tagLibrary;
        private TagGeneratorFactory _generatorFactory;

        [SetUp]
        public void SetUp()
        {
            var conventionLibrary = new HtmlConventionLibrary();
            conventionLibrary.For<FakeSubject>().If(x => x.Name == "berf").Build(x => new DivTag().Text("BERF"));
            conventionLibrary.For<FakeSubject>().If(x => x.Name == "berf")
                .Modify(x => x.CurrentTag.AddClass("div-class"));
            conventionLibrary.For<FakeSubject>().Always.Modify(x => x.CurrentTag.Attr("name", x.Name));

            _tagLibrary = conventionLibrary.For<FakeSubject>();
//            _tagLibrary.If(x => x.Name == "berf").Build(x => new DivTag().Text("BERF"));
//            _tagLibrary.If(x => x.Name == "berf")
//                .Modify(x => x.CurrentTag.AddClass("div-class"));
//            _tagLibrary.Always.Modify(x => x.CurrentTag.Attr("name", x.Name));

            var requestBuilder = new TagRequestBuilder(new ITagRequestActivator[0]);
            
            _generatorFactory = new TagGeneratorFactory(new ActiveProfile(), conventionLibrary, requestBuilder);
        }

        [Test]
        public void Lib()
        {
            var fakeSubject = new FakeSubject {Name = "berf"};
            var actual = _tagLibrary.PlanFor(fakeSubject).Build(fakeSubject);

            var expected = "<div name=\"berf\" class=\"div-class\">BERF</div>";
            Assert.AreEqual(expected, actual.ToString());

            actual = _generatorFactory.GeneratorFor<FakeSubject>().Build(fakeSubject);
            Assert.AreEqual(expected, actual.ToString());

        }
    }

   

    
}