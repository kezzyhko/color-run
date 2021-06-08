using Core;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Editor
{
    public class SampleEditorTests
    {
        [Test]
        public void GivenSampleObject_WhenCheckingForNull_ThenItIsNotNull()
        {
            // Arrange
            var obj = new SampleClass();

            // Act
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var isNull = obj == null;

            // Assert
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            isNull.Should().Be(false);
        }
    }
}