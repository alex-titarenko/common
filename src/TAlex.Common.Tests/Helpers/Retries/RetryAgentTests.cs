using NUnit.Framework;
using System;
using System.Collections.Generic;
using TAlex.Common.Helpers.Retries;
using TAlex.Common.Helpers.Retries.Intervals;

namespace TAlex.Common.Tests.Helpers.Retries
{
    [TestFixture]
    public class RetryAgentTests
    {
        protected RetryPolicy Policy; 

        [SetUp]
        public void SetUp()
        {
            Policy = new RetryPolicy
            {
                RetriesCount = 10,
                InitialRetryInterval = TimeSpan.FromMilliseconds(20),
                MaxRetryInterval = TimeSpan.FromMilliseconds(1000),
                IntervalFunction = new ConstantBackoffIntervalFunction()
            };
        }


        [Test]
        public void Retry_NullCode_ThrowArgumenNullException()
        {
            //action
            TestDelegate action = () => RetryAgent.Retry(null);

            //assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void Retry_NullRetryableExceptions_ThrowArgumenNullException()
        {
            //action
            TestDelegate action = () => RetryAgent.Retry(() => { }, null, null);

            //assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void Retry_DoNothing_NoRetries()
        {
            //arrange
            var callsCount = 0;
            Action code = () => { callsCount++; };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code); };

            //assert
            Assert.DoesNotThrow(action);
            Assert.AreEqual(1, callsCount);
        }

        [Test]
        public void Retry_FailOnFirstAttempt_RetryOneTime()
        {
            //arrange
            var callsCount = 0;
            Action code = () =>
            {
                callsCount++;
                if (callsCount == 1)
                {
                    throw new InvalidOperationException();
                }
            };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code, Policy); };

            //assert
            Assert.DoesNotThrow(action);
            Assert.AreEqual(2, callsCount);
        }

        [Test]
        public void Retry_FailOnFirstAttempt_InvokeRetryHandler()
        {
            //arrange
            var callsCount = 0;
            Action code = () =>
            {
                callsCount++;
                if (callsCount == 1)
                {
                    throw new InvalidOperationException();
                }
            };
            var retries = 0;
            Policy.RetryHandler = (exc, i, runTime, p) => { retries++; };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code, Policy); };

            //assert
            Assert.DoesNotThrow(action);
            Assert.AreEqual(1, retries);
        }

        [Test]
        public void Retry_FailAllTheTime_ThrowRetriesExceededException()
        {
            //arrange
            var callsCount = 0;
            Action code = () =>
            {
                callsCount++;
                throw new InvalidOperationException();
            };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code, Policy); };

            //assert
            Assert.Throws<RetriesExceededException>(action);
            Assert.AreEqual(Policy.RetriesCount, callsCount);
        }

        [Test]
        public void Retry_FailAllTheTime_InvokeRetriesExceededHandler()
        {
            //arrange
            Action code = () =>
            {
                throw new InvalidOperationException();
            };
            var retriesExceeded = false;
            Policy.RetriesExceededHandler = (exc, runTime, policy) => { retriesExceeded = true; };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code, Policy); };

            //assert
            Assert.Throws<RetriesExceededException>(action);
            Assert.IsTrue(retriesExceeded);
        }

        [Test]
        public void Retry_NonRetryableException_FailFast()
        {
            //arrange
            var callsCount = 0;
            Action code = () =>
            {
                callsCount++;
                throw new InvalidOperationException();
            };

            //action
            TestDelegate action = () => { RetryAgent.Retry(code, new List<Type> { typeof(ArgumentException) }); };

            //assert
            Assert.Throws<InvalidOperationException>(action);
            Assert.AreEqual(1, callsCount);
        }
    }
}
