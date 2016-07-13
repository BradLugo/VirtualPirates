using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;

namespace UnityTest
{
    [IntegrationTest.DynamicTestAttribute("ExampleIntegrationTests")]
    // [IntegrationTest.Ignore]
    [IntegrationTest.ExpectExceptions(false, typeof(ArgumentException))]
    [IntegrationTest.SucceedWithAssertions]
    [IntegrationTest.TimeoutAttribute(1)]
    [IntegrationTest.ExcludePlatformAttribute(RuntimePlatform.Android, RuntimePlatform.LinuxPlayer)]
    public class TestMonsterMovement : MonoBehaviour
    {
        public void Start()
        {
            IntegrationTest.Pass(gameObject);
        }
    }

}

