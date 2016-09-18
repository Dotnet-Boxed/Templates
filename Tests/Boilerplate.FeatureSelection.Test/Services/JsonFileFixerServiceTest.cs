namespace Boilerplate.FeatureSelection.Test.Services
{
    using System;
    using Boilerplate.FeatureSelection.Services;
    using Xunit;

    public class JsonFileFixerServiceTest
    {
        [Fact]
        public void FixJsonCommas_DoubleQuoteFollowedByCloseBrace_CommaRemoved()
        {
            var json =
                "{" +
                " \"foo\": \"bar\"," +
                " \"foo\": \"bar\", " +
                "}";
            var expected =
                "{" +
                " \"foo\": \"bar\"," +
                " \"foo\": \"bar\" " +
                "}";

            var actual = new JsonFileFixerService().Fix(json);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FixJsonCommas_CloseBraceFollowedByCloseBrace_CommaRemoved()
        {
            var json =
                "{" +
                " {" +
                " \"foo\": \"bar\"" +
                " }, " +
                " {" +
                " \"foo\": \"bar\"" +
                " }, " +
                "}";
            var expected =
                "{" +
                " {" +
                " \"foo\": \"bar\"" +
                " }, " +
                " {" +
                " \"foo\": \"bar\"" +
                " } " +
                "}";

            var actual = new JsonFileFixerService().Fix(json);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FixJsonCommas_CloseBraceFollowedByCloseBraceWithNewLines_CommaRemoved()
        {
            var json =
                "{" + Environment.NewLine +
                " {" + Environment.NewLine +
                " \"foo\": \"bar\"" + Environment.NewLine +
                " }, " + Environment.NewLine +
                " {" + Environment.NewLine +
                " \"foo\": \"bar\"" + Environment.NewLine +
                " }, " + Environment.NewLine +
                "}";
            var expected =
                "{" + Environment.NewLine +
                " {" + Environment.NewLine +
                " \"foo\": \"bar\"" + Environment.NewLine +
                " }, " + Environment.NewLine +
                " {" + Environment.NewLine +
                " \"foo\": \"bar\"" + Environment.NewLine +
                " } " + Environment.NewLine +
                "}";

            var actual = new JsonFileFixerService().Fix(json);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FixJsonCommas_CloseBraceFollowedByCloseBraceTwice_BothCommasRemoved()
        {
            var json =
                "{" +
                " {" +
                " {" +
                " \"foo\": \"bar\"" +
                " }," +
                " }, " +
                "}";
            var expected =
                "{" +
                " {" +
                " {" +
                " \"foo\": \"bar\"" +
                " }" +
                " } " +
                "}";

            var actual = new JsonFileFixerService().Fix(json);

            Assert.Equal(expected, actual);
        }
    }
}