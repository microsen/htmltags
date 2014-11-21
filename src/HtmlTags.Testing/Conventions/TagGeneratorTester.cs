using Moq;
using Moq.Language.Flow;
using Should;
using HtmlTags.Conventions;
using NUnit.Framework;

namespace HtmlTags.Testing.Conventions
{
    [TestFixture]
    public class TagGeneratorTester // : InteractionContext<TagGenerator<FakeSubject>>
    {
        private FakeSubject theSubject;
        private HtmlTag theTag;
        private TagGenerator<FakeSubject> _tagGenerator;
        private Mock<ITagRequestBuilder> _tagRequestBuilder;
        private ActiveProfile _activeProfile;
        private Mock<ITagLibrary<FakeSubject>> _tagLibrary;

        [SetUp]
        public void SetUp()
        {
            _activeProfile = new ActiveProfile();
            _tagRequestBuilder = new Mock<ITagRequestBuilder>();
            _tagLibrary = new Mock<ITagLibrary<FakeSubject>>();
            _tagGenerator = new TagGenerator<FakeSubject>(_tagLibrary.Object, _tagRequestBuilder.Object, _activeProfile);
            theSubject = new FakeSubject{
                Name = "Jeremy",
                Level = 10
            };

            theTag = new HtmlTag("div");
        }

        private void expect(FakeSubject subject, string category = null, string profile = null)
        {
            var thePlan = new Mock<ITagPlan<FakeSubject>>();

            _tagLibrary.Setup(x => x.PlanFor(subject, profile, category))
                .Returns(thePlan.Object);


            thePlan.Setup(x => x.Build(theSubject)).Returns(theTag);

          
            _tagRequestBuilder.Setup(x => x.Build(It.IsAny<FakeSubject>()));
        }

        [Test]
        public void the_default_profile_is_Default()
        {
            _tagGenerator.ActiveProfile.ShouldEqual(TagConstants.Default);
        }

        [Test]
        public void ensure_tag_requests_are_always_built()
        {
            expect(theSubject, TagConstants.Default, TagConstants.Default);

            _tagGenerator.Build(theSubject);

           _tagRequestBuilder.Verify(x => x.Build(theSubject));
            
        }

        [Test]
        public void call_build_in_default_mode()
        {
            expect(theSubject, category:TagConstants.Default, profile:TagConstants.Default);

            _tagGenerator.Build(theSubject).ShouldBeSameAs(theTag);
        }

        [Test]
        public void call_build_with_the_profile_set()
        {
            _activeProfile.Push("A");

            expect(theSubject, category: TagConstants.Default, profile: "A");

            _tagGenerator.Build(theSubject).ShouldBeSameAs(theTag);
        }

        [Test]
        public void call_build_with_category()
        {
            expect(theSubject, category:"A", profile:TagConstants.Default);

            _tagGenerator.Build(theSubject, "A", null).ShouldBeSameAs(theTag);
        }

        [Test]
        public void call_build_with_both_category_and_non_default_profile()
        {
            _activeProfile.Push("B");

            expect(theSubject, category:"A", profile:"B");

            _tagGenerator.Build(theSubject, "A", null).ShouldBeSameAs(theTag);
        }

        [Test]
        public void call_build_with_both_category_and_non_default_profile_by_passing_in_the_default()
        {
            expect(theSubject, category: "A", profile: "B");

            _tagGenerator.Build(theSubject, "A", "B").ShouldBeSameAs(theTag);
        }
    }
}