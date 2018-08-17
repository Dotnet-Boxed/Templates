namespace Boxed.Templates.Test.GraphQLTemplate
{
    using System;
    using System.Collections.Generic;
    using global::GraphQLTemplate;
    using Xunit;

    public class CursorTest
    {
        [Theory]
        [InlineData("MA=", 0)]
        [InlineData("NQ==", 5)]
        [InlineData("LTU=", -5)]
        public void FromCursor_IntValue_ReturnsPrefixedBase64Cursor(string cursor, int expectedValue)
        {
            var value = Cursor.FromCursor<int>(cursor);

            Assert.Equal(expectedValue, value);
        }

        [Theory]
        [InlineData("Rg==", "F")]
        [InlineData("Rm9v", "Foo")]
        public void FromCursor_StringValue_ReturnsPrefixedBase64Cursor(string cursor, string expectedValue)
        {
            var value = Cursor.FromCursor<string>(cursor);

            Assert.Equal(expectedValue, value);
        }

        [Fact]
        public void FromCursor_NullStringValue_ReturnsNull()
        {
            var value = Cursor.FromCursor<string>(null);

            Assert.Null(value);
        }

        [Fact]
        public void FromCursor_EmptyStringValue_ReturnsNull()
        {
            var value = Cursor.FromCursor<string>(string.Empty);

            Assert.Null(value);
        }

        [Fact]
        public void FromCursor_InvalidBase64String_ReturnsNull()
        {
            var value = Cursor.FromCursor<string>("This is not base 64");

            Assert.Null(value);
        }

        [Fact]
        public void FromCursor_InvalidBase64StringValue_ReturnsNull()
        {
            var value = Cursor.FromCursor<string>("This is not base64");

            Assert.Null(value);
        }

        [Fact]
        public void GetFirstAndLastCursor_NullCollection_ReturnsNullFirstAndLast()
        {
            var (first, last) = Cursor.GetFirstAndLastCursor<Item, int>(null, x => x.Integer);

            Assert.Null(first);
            Assert.Null(last);
        }

        [Fact]
        public void GetFirstAndLastCursor_NullGetCursorDelegate_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Cursor.GetFirstAndLastCursor<Item, int>(null, null));

        [Fact]
        public void GetFirstAndLastCursor_EmptyCollection_ReturnsNullFirstAndLast()
        {
            var items = new List<Item>();

            var (first, last) = Cursor.GetFirstAndLastCursor(items, x => x.Integer);

            Assert.Null(first);
            Assert.Null(last);
        }

        [Fact]
        public void GetFirstAndLastCursor_OneItem_ReturnsSameCursor()
        {
            var items = new List<Item>()
            {
                new Item() { Integer = 1 },
            };

            var (first, last) = Cursor.GetFirstAndLastCursor(items, x => x.Integer);

            Assert.Equal("MQ==", first);
            Assert.Equal("MQ==", last);
        }

        [Fact]
        public void GetFirstAndLastCursor_TwoItems_ReturnsFirstAndLastCursor()
        {
            var items = new List<Item>()
            {
                new Item() { Integer = 1 },
                new Item() { Integer = 2 },
            };

            var (first, last) = Cursor.GetFirstAndLastCursor(items, x => x.Integer);

            Assert.Equal("MQ==", first);
            Assert.Equal("Mg==", last);
        }

        [Fact]
        public void ToCursor_Null_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Cursor.ToCursor<string>(null));

        [Theory]
        [InlineData(0, "MA==")]
        [InlineData(5, "NQ==")]
        [InlineData(-5, "LTU=")]
        public void ToCursor_IntValue_ReturnsPrefixedBase64Cursor(int value, string expectedCursor)
        {
            var cursor = Cursor.ToCursor(value);

            Assert.Equal(expectedCursor, cursor);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Foo", "Rm9v")]
        public void ToCursor_StringValue_ReturnsPrefixedBase64Cursor(string value, string expectedCursor)
        {
            var cursor = Cursor.ToCursor(value);

            Assert.Equal(expectedCursor, cursor);
        }

        private class Item
        {
            public int Integer { get; set; }
        }
    }
}
