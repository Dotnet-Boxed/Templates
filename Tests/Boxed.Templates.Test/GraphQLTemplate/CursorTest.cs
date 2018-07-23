namespace Boxed.Templates.Test.GraphQLTemplate
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::GraphQLTemplate;
    using Xunit;

    public class CursorTest
    {
        [Theory]
        [InlineData("YXJyYXljb25uZWN0aW9uOjA=", 0)]
        [InlineData("YXJyYXljb25uZWN0aW9uOjU=", 5)]
        [InlineData("YXJyYXljb25uZWN0aW9uOi01", -5)]
        public void FromCursor_IntValue_ReturnsPrefixedBase64Cursor(string cursor, int expectedValue)
        {
            var value = Cursor.FromCursor<int>(cursor);

            Assert.Equal(expectedValue, value);
        }

        [Theory]
        [InlineData("YXJyYXljb25uZWN0aW9uOkY=", "F")]
        [InlineData("YXJyYXljb25uZWN0aW9uOkZvbw==", "Foo")]
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

        [Theory]
        [InlineData("arrayconnectio")]
        [InlineData("arrayconnectio:")]
        [InlineData("arrayconnection")]
        [InlineData("arrayconnection:")]
        public void FromCursor_CursorWithInvalidPrefixLength_ReturnsNull(string cursorValue)
        {
            var cursor = Convert.ToBase64String(Encoding.UTF8.GetBytes(cursorValue));

            var value = Cursor.FromCursor<string>(cursor);

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

            Assert.Equal("YXJyYXljb25uZWN0aW9uOjE=", first);
            Assert.Equal("YXJyYXljb25uZWN0aW9uOjE=", last);
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

            Assert.Equal("YXJyYXljb25uZWN0aW9uOjE=", first);
            Assert.Equal("YXJyYXljb25uZWN0aW9uOjI=", last);
        }

        [Fact]
        public void ToCursor_Null_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Cursor.ToCursor<string>(null));

        [Theory]
        [InlineData(0, "YXJyYXljb25uZWN0aW9uOjA=")]
        [InlineData(5, "YXJyYXljb25uZWN0aW9uOjU=")]
        [InlineData(-5, "YXJyYXljb25uZWN0aW9uOi01")]
        public void ToCursor_IntValue_ReturnsPrefixedBase64Cursor(int value, string expectedCursor)
        {
            var cursor = Cursor.ToCursor(value);

            Assert.Equal(expectedCursor, cursor);
        }

        [Theory]
        [InlineData("", "YXJyYXljb25uZWN0aW9uOg==")]
        [InlineData("Foo", "YXJyYXljb25uZWN0aW9uOkZvbw==")]
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
