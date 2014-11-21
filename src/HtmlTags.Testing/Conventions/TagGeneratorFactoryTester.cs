using System.Linq;
using Moq;
using Should;
using HtmlTags.Conventions;
using NUnit.Framework;

namespace HtmlTags.Testing.Conventions
{
    [TestFixture]
    public class TagGeneratorFactoryTester //: InteractionContext<TagGeneratorFactory>
    {
        private ITagRequestActivator[] theActivators;
        private HtmlConventionLibrary theLibrary;
        private TagGeneratorFactory _tagGeneratorFactory;
        private ActiveProfile _activeProfile;

        [SetUp]
        public void SetUp()
        {
            var mockActivators = new []
                                 {
                                     new Mock<ITagRequestActivator>(),
                                     new Mock<ITagRequestActivator>(),
                                     new Mock<ITagRequestActivator>(),
                                     new Mock<ITagRequestActivator>(),
                                     new Mock<ITagRequestActivator>(),
                                 };

            mockActivators[0].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);
            mockActivators[1].Setup(x => x.Matches(typeof(FakeSubject))).Returns(false);
            mockActivators[2].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);
            mockActivators[3].Setup(x => x.Matches(typeof(FakeSubject))).Returns(false);
            mockActivators[4].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);


            theLibrary = new HtmlConventionLibrary();
            theActivators = mockActivators.Select(x => x.Object).ToArray();
//            Services.Inject(theLibrary);
            _activeProfile = new ActiveProfile();
            _tagGeneratorFactory = new TagGeneratorFactory(_activeProfile, theLibrary, null);
        }

        [Test]
        public void sets_the_active_profile_to_the_child_generators()
        {
            _activeProfile.Push("Blue");

            _tagGeneratorFactory.GeneratorFor<FakeSubject>().ActiveProfile.ShouldEqual("Blue");
            _tagGeneratorFactory.GeneratorFor<SecondSubject>().ActiveProfile.ShouldEqual("Blue");
        }
    }
}