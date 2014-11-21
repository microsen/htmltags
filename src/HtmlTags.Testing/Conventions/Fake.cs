using NUnit.Framework;
using Should;

namespace HtmlTags.Testing.Conventions
{
    [TestFixture]
    public class Fake
    {
        [Test]
        public void good()
        {
            true.ShouldBeTrue();
        }

    }
}