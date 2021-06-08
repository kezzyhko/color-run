using System.Collections;
using Core;
using FluentAssertions;
using UnityEngine.TestTools;

namespace Tests.Runtime
{
    public class SampleRuntimeTests
    {
        [UnityTest]
        public IEnumerator GivenSampleObject_WhenWaitingOneFrameAndCheckingForNull_ThenItIsNotNull()
        {
            // Arrange
            var obj = new SampleClass();

            // Act
            yield return null;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var isNull = obj == null;

            // Assert
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            isNull.Should().Be(false);
        }
    }
}