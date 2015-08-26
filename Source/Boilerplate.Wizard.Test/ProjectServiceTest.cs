namespace Boilerplate.Wizard
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;
    using Moq;
    using Xunit;

    public class ProjectServiceTest
    {
        [Theory(DisplayName = "ProjectService.DeleteComment")]
        [InlineData(DeleteCommentMode.StartEndComment, ".txt", "#", null)]
        [InlineData(DeleteCommentMode.StartEndCommentAndCode, ".txt", "#", null)]
        [InlineData(DeleteCommentMode.StartEndCommentAndUncommentCode, ".txt", "#", null)]
        [InlineData(DeleteCommentMode.StartEndComment, ".cs", "//", null)]
        [InlineData(DeleteCommentMode.StartEndCommentAndCode, ".cs", "//", null)]
        [InlineData(DeleteCommentMode.StartEndCommentAndUncommentCode, ".cs", "//", null)]
        [InlineData(DeleteCommentMode.StartEndComment, ".cshtml", "@*", "*@")]
        [InlineData(DeleteCommentMode.StartEndCommentAndCode, ".cshtml", "@*", "*@")]
        [InlineData(DeleteCommentMode.StartEndCommentAndUncommentCode, ".cshtml", "@*", "*@")]
        [InlineData(DeleteCommentMode.StartEndComment, ".xml", "<!--", "-->")]
        [InlineData(DeleteCommentMode.StartEndCommentAndCode, ".xml", "<!--", "-->")]
        [InlineData(DeleteCommentMode.StartEndCommentAndUncommentCode, ".xml", "<!--", "-->")]
        public async Task DeleteCommentTest(
            DeleteCommentMode mode,
            string fileExtension,
            string commentStart,
            string commentEnd = null)
        {
            // Arrange
            string commentName = "CommentName";
            string filePath = @"C:\Project\File" + fileExtension;
            Mock<IFileSystemService> fileSystemServiceMock = new Mock<IFileSystemService>(MockBehavior.Strict);
            fileSystemServiceMock
                .Setup(x => x.FileExists(It.Is<string>(y => string.Equals(y, filePath))))
                .Returns(true);
            fileSystemServiceMock
                .Setup(x => x.FileReadAllLines(It.Is<string>(y => string.Equals(y, filePath))))
                .Returns(Task.FromResult(GetInputLines(commentName, commentStart, commentEnd)));
            fileSystemServiceMock
                .Setup(x => x.FileWriteAllLines(
                    It.Is<string>(y => string.Equals(y, filePath)),
                    It.Is<IEnumerable<string>>(y => Enumerable.SequenceEqual(y, GetOutputLines(mode, commentStart, commentEnd)))))
                .Returns(Task.FromResult<object>(null));
            IProjectService projectService = new ProjectService(fileSystemServiceMock.Object, @"C:\Project\Project.xproj");

            // Act
            await projectService.DeleteComment(commentName, mode, "File" + fileExtension);

            // Assert
            fileSystemServiceMock.VerifyAll();
        }

        private static IEnumerable<string> GetOutputLines(DeleteCommentMode deleteCommentMode, string commentStart, string commentEnd = null)
        {
            if (commentEnd != null)
            {
                commentEnd = " " + commentEnd;
            }

            if (deleteCommentMode == DeleteCommentMode.StartEndComment)
            {
                return new string[]
                {
                    $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                    $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                    $"   {commentStart} Blah Blah Blah{commentEnd}    "
                };
            }
            else if (deleteCommentMode == DeleteCommentMode.StartEndCommentAndCode)
            {
                return new string[]
                {
                    $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                    $"   {commentStart} Blah Blah Blah{commentEnd}    "
                };
            }
            else // if (deleteCommentMode == DeleteCommentMode.StartEndCommentAndUncommentCode)
            {
                string startSpacer = new string(' ', commentStart.Length);
                string endSpacer = new string(' ', commentEnd == null ? 0 : 1);
                return new string[]
                {
                    $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                    startSpacer + $"    Blah Blah Blah    " + endSpacer,
                    $"   {commentStart} Blah Blah Blah{commentEnd}    "
                };
            }
        }

        private static string[] GetInputLines(string commentName, string commentStart, string commentEnd)
        {
            if (commentEnd != null)
            {
                commentEnd = " " + commentEnd;
            }

            return new string[]
            {
                $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                $"   {commentStart} $Start-{commentName}${commentEnd}    ",
                $"   {commentStart} Blah Blah Blah{commentEnd}    ",
                $"   {commentStart} $End-{commentName}${commentEnd}    ",
                $"   {commentStart} Blah Blah Blah{commentEnd}    "
            };
        }
    }
}