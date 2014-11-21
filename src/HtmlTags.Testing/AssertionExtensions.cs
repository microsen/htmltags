using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HtmlTags.Testing
{
    public static class AssertionExtensions
    {
        public static void ShouldEndWith(this string actual, string expected)
        {
            Assert.True(actual.EndsWith(expected));
        }

        public static void ShouldHaveCount<T>(this IEnumerable<T> enumerable, int expected)
        {
            Assert.AreEqual(expected, enumerable.Count());
        }

        public static void ShouldBeThrownBy<T>(this T exception, TestDelegate testDelegate) where T : Exception
        {
            Assert.Throws<T>(testDelegate);
        }

        public static void ShouldBeThrownBy(this Type exceptionType, TestDelegate testDelegate)
        {
            Assert.Throws(exceptionType, testDelegate);
        }

        public static void ShouldHaveTheSameElementsAs<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Assert.AreEqual(expected.Count(),actual.Count());
            foreach (var elem in actual)
            {
                Assert.True(expected.Contains(elem));
            }
        }
    }
}