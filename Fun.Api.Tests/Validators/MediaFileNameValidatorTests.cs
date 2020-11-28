using Fun.Api.Services;
using Fun.Api.Validators;
using Moq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Fun.Api.Tests.Validators
{
    public class MediaFileNameValidatorTests
    {
        private readonly Mock<IDirectoryService> _directoryService;

        private readonly MediaFileNameValidator _mediaFileNameValidator;

        public MediaFileNameValidatorTests()
        {
            _directoryService = new Mock<IDirectoryService>();

            _mediaFileNameValidator = new MediaFileNameValidator(_directoryService.Object);
        }

        [Fact]
        public void Validatate_returns_true_when_directory_contains_file()
        {
            // Arrange
            const string fileName = "exists";
            var files = new List<string> { "a", "b", "c", fileName };

            _directoryService.Setup(d => d.GetMediaFileNames()).Returns(files);

            // Act
            var isValid = _mediaFileNameValidator.Validate(fileName);

            // Assert
            isValid.Should().Be(true);
        }

    }
}
