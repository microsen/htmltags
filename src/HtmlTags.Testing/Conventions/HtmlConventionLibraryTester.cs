using System;
using HtmlTags.Conventions;
using Moq;
using NUnit.Framework;
using Should;

namespace HtmlTags.Testing.Conventions
{
    [TestFixture]
    public class HtmlConventionLibraryTester
    {
        [Test]
        public void for_returns_the_same_result()
        {
            var library = new HtmlConventionLibrary();
            var lib1 = library.For<FakeSubject>();
            var lib2 = library.For<FakeSubject>();

            lib1.ShouldBeSameAs(lib2);


            library.For<SecondSubject>().ShouldBeSameAs(library.For<SecondSubject>());
        }

        [Test]
        public void importing_test()
        {
            var b1 = new Mock<ITagBuilderPolicy<FakeSubject>>().Object;
            var b2 = new Mock<ITagBuilderPolicy<FakeSubject>>().Object;
            var b3 = new Mock<ITagBuilderPolicy<FakeSubject>>().Object;
            var b4 = new Mock<ITagBuilderPolicy<SecondSubject>>().Object;
            var b5 = new Mock<ITagBuilderPolicy<SecondSubject>>().Object;
            var b6 = new Mock<ITagBuilderPolicy<SecondSubject>>().Object;


            var lib1 = new HtmlConventionLibrary();
            lib1.For<FakeSubject>().Add(b1);
            lib1.For<FakeSubject>().Add(b2);

            var lib2 = new HtmlConventionLibrary();
            lib2.For<FakeSubject>().Add(b3);
            lib2.For<SecondSubject>().Add(b4);
            lib2.For<SecondSubject>().Add(b5);
            lib2.For<SecondSubject>().Add(b6);

            lib1.Import(lib2);

            lib1.For<FakeSubject>().Default.Defaults.Policies.ShouldHaveTheSameElementsAs(b1, b2, b3);
            lib1.For<SecondSubject>().Default.Defaults.Policies.ShouldHaveTheSameElementsAs(b4, b5, b6);
        }

        [Test]
        public void importing_services()
        {
            var builder1 = new ServiceBuilder();
            var builder2 = new ServiceBuilder();
        
            builder1.Add(() => "blue");
            builder2.Add(() => "green");

            builder2.Add(() => 1);

            builder2.FillInto(builder1);

            builder1.Build<string>().ShouldEqual("blue"); // not replaced
            builder1.Build<int>().ShouldEqual(1); // was filled in
        }

        [Test]
        public void services_are_imported()
        {
            var library1 = new HtmlConventionLibrary();
            library1.RegisterService<IFoo, LittleFoo>();

            var library2 = new HtmlConventionLibrary();
            library2.RegisterService<IFoo, BigFoo>("different");

            library1.Import(library2);

            library1.Get<IFoo>().ShouldBeType<LittleFoo>();
            library1.Get<IFoo>("different").ShouldBeType<BigFoo>();
        }

        [Test]
        public void services_are_not_overwritten_while_importing()
        {
            var library1 = new HtmlConventionLibrary();
            library1.RegisterService<IFoo, LittleFoo>();

            var library2 = new HtmlConventionLibrary();
            library2.RegisterService<IFoo, BigFoo>();

            library1.Import(library2);

            library1.Get<IFoo>().ShouldBeType<LittleFoo>();
        }


        [Test]
        public void get_with_no_prior_definition_will_happily_blow_up()
        {
            typeof(ArgumentOutOfRangeException).ShouldBeThrownBy(() => {
                new HtmlConventionLibrary().Get<IFoo>();
            });//.Message.ShouldContain("No service implementation is registered for type " + typeof(IFoo).FullName);
        }

        [Test]
        public void simple_registration_of_service_by_type()
        {
            var library = new HtmlConventionLibrary();
            library.RegisterService<IFoo, Foo>();

            library.Get<IFoo>().ShouldBeType<Foo>();
        }

        [Test]
        public void registration_of_service_by_func()
        {
            var library = new HtmlConventionLibrary();
            library.RegisterService<IFoo>(() => new ColoredFoo{Color = "Red"});

            library.Get<IFoo>().ShouldBeType<ColoredFoo>()
                .Color.ShouldEqual("Red");
        }

        [Test]
        public void if_not_explicitly_specified_assume_the_profile_is_default()
        {
            var library = new HtmlConventionLibrary();
            library.RegisterService<IFoo, Foo>(TagConstants.Default);

            library.Get<IFoo>().ShouldBeType<Foo>();
        }

        [Test]
        public void register_and_build_by_profile()
        {
            var library = new HtmlConventionLibrary();
            library.RegisterService<IFoo, Foo>(TagConstants.Default);

            library.RegisterService<IFoo, DifferentFoo>("Profile1");

            library.Get<IFoo>("Profile1").ShouldBeType<DifferentFoo>();
        }

        [Test]
        public void fetching_by_profile_should_fall_back_to_default_if_not_specific_implementation_is_used()
        {
            var library = new HtmlConventionLibrary();
            library.RegisterService<IFoo, Foo>(TagConstants.Default);

            library.Get<IFoo>("Profile1").ShouldBeType<Foo>();
        }
    }

    public interface IFoo{}
    public class Foo : IFoo{}
    public class DifferentFoo : IFoo{}
    public class ColoredFoo : IFoo
    {
        public string Color { get; set; }
    }

    public class BigFoo : IFoo { }
    public class LittleFoo : IFoo { }

    public class SecondSubject : TagRequest
    {
        public override object ToToken()
        {
            throw new NotImplementedException();
        }
    }
}