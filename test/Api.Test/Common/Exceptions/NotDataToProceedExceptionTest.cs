using System.Net;
using Api.Common.Exceptions;
using NUnit.Framework;
using Shouldly;

namespace Api.Test.Common.Exceptions;

    [TestFixture]
    public class NotDataToProceedExceptionTest
    {
        private const string DEFAULT_MESSAGE = "Not found data to proceed";
        private const string CUSTOM_MESSAGE = "custom message";
        private const int EXPECTED_ERROR_CODE = (int)HttpStatusCode.NoContent;

        private CustomException? _defaultConstructorException;
        private CustomException? _customMessageConstructorException;
        
        [SetUp]
        public void Setup()
        {
            // Arrange & Act
            _defaultConstructorException = new NotDataToProceedException();
            _customMessageConstructorException = new NotDataToProceedException(CUSTOM_MESSAGE);
        }
        
        #region Default Constructor 
      [Test]
        public void DefaultConstructor_ShouldSetHResultCorrectly()
        {
            // Assert
            _defaultConstructorException?.HResult.ShouldBe(EXPECTED_ERROR_CODE);
        }

        [Test]
        public void DefaultConstructor_ShouldSetMessageCorrectly()
        {
            // Assert
            _defaultConstructorException?.Message.ShouldBe(DEFAULT_MESSAGE);
        }

        [Test]
        public void DefaultConstructor_ShouldSetErrorCodeCorrectly()
        {
            // Assert
            _defaultConstructorException?.ErrorCode.ShouldBe(EXPECTED_ERROR_CODE);
        }

        [Test]
        public void DefaultConstructor_ShouldSetHttpStatusCodeCorrectly()
        {
            // Assert
            _defaultConstructorException?.HResult.ShouldBe(EXPECTED_ERROR_CODE);
        }
        #endregion Default Constructor

        #region Custom Message Constructor
        [Test]
        public void CustomMessageConstructor_ShouldSetHResultCorrectly()
        {
            // Assert
            _customMessageConstructorException?.HResult.ShouldBe(EXPECTED_ERROR_CODE);
        }

        [Test]
        public void CustomMessageConstructor_ShouldSetMessageCorrectly()
        {
            // Assert
            _customMessageConstructorException?.Message.ShouldBe(CUSTOM_MESSAGE);
        }

        [Test]
        public void CustomMessageConstructor_ShouldSetErrorCodeCorrectly()
        {
            // Assert
            _customMessageConstructorException?.ErrorCode.ShouldBe(EXPECTED_ERROR_CODE);
        }

        [Test]
        public void CustomMessageConstructor_ShouldSetHttpStatusCodeCorrectly()
        {
            // Assert
            _customMessageConstructorException?.HResult.ShouldBe(EXPECTED_ERROR_CODE);
        }
        #endregion Custom Message Constructor
    
    }