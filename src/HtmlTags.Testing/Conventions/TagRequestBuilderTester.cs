using System.Linq;
using Moq;
using Should;
using HtmlTags.Conventions;
using NUnit.Framework;

namespace HtmlTags.Testing.Conventions
{
    [TestFixture]
    public class TagRequestBuilderTester 
    {
        private ITagRequestActivator[] _theActivators;
        private FakeSubject _theSubject;
        private Mock<ITagRequestActivator>[] _mockActivators;

        [SetUp]
        public void SetUp()
        {
           _mockActivators = new Mock<ITagRequestActivator>[]
                             {
                                 new Mock<ITagRequestActivator>(), 
                                 new Mock<ITagRequestActivator>(), 
                                 new Mock<ITagRequestActivator>(), 
                                 new Mock<ITagRequestActivator>(), 
                                 new Mock<ITagRequestActivator>(), 
                             };
           _mockActivators[0].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);
           _mockActivators[1].Setup(x => x.Matches(typeof(FakeSubject))).Returns(false);
           _mockActivators[2].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);
           _mockActivators[3].Setup(x => x.Matches(typeof(FakeSubject))).Returns(false);
           _mockActivators[4].Setup(x => x.Matches(typeof(FakeSubject))).Returns(true);

            _theActivators = _mockActivators.Select(x => x.Object).ToArray();

            _theSubject = new FakeSubject()
                {
                    Name = "HerpDerp",
                    Level = 99
                };
        }

        [Test]
        public void should_only_call_activators_that_match()
        {
            var builder = new TagRequestBuilder(_theActivators);
            builder.Build(_theSubject);

             _mockActivators[0].Verify(x => x.Activate(_theSubject));
             _mockActivators[1].Verify(x => x.Activate(_theSubject), Times.Never);
             _mockActivators[2].Verify(x => x.Activate(_theSubject));
             _mockActivators[3].Verify(x => x.Activate(_theSubject), Times.Never);
             _mockActivators[4].Verify(x => x.Activate(_theSubject));
        }
    }
}