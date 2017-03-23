namespace Boilerplate.FeatureSelection.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;
    using Moq;
    using Xunit;

    public class ProjectServiceTest
    {
        [Theory(DisplayName = "ProjectService.EditComment")]
        [InlineData(EditCommentMode.LeaveCodeUnchanged, ".txt", "#", null)]
        [InlineData(EditCommentMode.DeleteCode, ".txt", "#", null)]
        [InlineData(EditCommentMode.UncommentCode, ".txt", "#", null)]
        [InlineData(EditCommentMode.LeaveCodeUnchanged, ".cs", "//", null)]
        [InlineData(EditCommentMode.DeleteCode, ".cs", "//", null)]
        [InlineData(EditCommentMode.UncommentCode, ".cs", "//", null)]
        [InlineData(EditCommentMode.LeaveCodeUnchanged, ".cshtml", "@*", "*@")]
        [InlineData(EditCommentMode.DeleteCode, ".cshtml", "@*", "*@")]
        [InlineData(EditCommentMode.UncommentCode, ".cshtml", "@*", "*@")]
        [InlineData(EditCommentMode.LeaveCodeUnchanged, ".xml", "<!--", "-->")]
        [InlineData(EditCommentMode.DeleteCode, ".xml", "<!--", "-->")]
        [InlineData(EditCommentMode.UncommentCode, ".xml", "<!--", "-->")]
        public async Task EditCommentTest(
            EditCommentMode mode,
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
            var inputLines = GetInputLines(commentName, commentStart, commentEnd);
            var outputLines = GetOutputLines(mode, commentStart, commentEnd);
            fileSystemServiceMock
                .Setup(x => x.FileReadAllLines(It.Is<string>(y => string.Equals(y, filePath))))
                .Returns(Task.FromResult(inputLines));
            fileSystemServiceMock
                .Setup(x => x.FileWriteAllLines(
                    It.Is<string>(y => string.Equals(y, filePath)),
                    It.Is<IEnumerable<string>>(y => Enumerable.SequenceEqual(y, outputLines))));
            IProjectService projectService = new ProjectService(fileSystemServiceMock.Object, @"C:\Project\Project.csproj");

            // Act
            await projectService.EditCommentInFile(commentName, mode, "File" + fileExtension);

            // Assert
            fileSystemServiceMock.VerifyAll();
        }

        private static IEnumerable<string> GetOutputLines(EditCommentMode mode, string commentStart, string commentEnd = null)
        {
            if (commentStart != null)
            {
                commentStart = commentStart + " ";
            }

            if (commentEnd != null)
            {
                commentEnd = " " + commentEnd;
            }

            if (mode == EditCommentMode.LeaveCodeUnchanged)
            {
                return new string[]
                {
                    $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                    $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                    $"    {commentStart}Blah Blah Blah{commentEnd}    "
                };
            }
            else if (mode == EditCommentMode.DeleteCode)
            {
                return new string[]
                {
                    $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                    $"    {commentStart}Blah Blah Blah{commentEnd}    "
                };
            }
            else // if (deleteCommentMode == DeleteCommentMode.StartEndCommentAndUncommentCode)
            {
                string endSpacer = new string(' ', commentEnd == null ? 0 : 1);
                return new string[]
                {
                    $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                    $"    Blah Blah Blah    " + endSpacer,
                    $"    {commentStart}Blah Blah Blah{commentEnd}    "
                };
            }
        }

        private static string[] GetInputLines(string commentName, string commentStart, string commentEnd)
        {
            if (commentStart != null)
            {
                commentStart = commentStart + " ";
            }

            if (commentEnd != null)
            {
                commentEnd = " " + commentEnd;
            }

            return new string[]
            {
                $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                $"    {commentStart}$Start-{commentName}${commentEnd}    ",
                $"    {commentStart}Blah Blah Blah{commentEnd}    ",
                $"    {commentStart}$End-{commentName}${commentEnd}    ",
                $"    {commentStart}Blah Blah Blah{commentEnd}    "
            };
        }
    }
}