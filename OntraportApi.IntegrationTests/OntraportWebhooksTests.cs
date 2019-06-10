﻿using System;
using System.Threading.Tasks;
using EmergenceGuardian.OntraportApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace EmergenceGuardian.OntraportApi.IntegrationTests
{
    public class OntraportWebhookTests : OntraportBaseReadTests<OntraportWebhooks, ApiWebhook>
    {
        public OntraportWebhookTests(ITestOutputHelper output) :
            base(output, 1)
        {
        }

        [Fact]
        public async Task Subscribe_ValidData_ReturnsIdAndUnsubscribe()
        {
            var api = SetupApi();
            string url = "https://test98676342.32";
            string eventName = "object_create(0)";

            var result = await api.SubscribeAsync(url, eventName, null);

            Assert.NotNull(result);
            await api.UnsubscribeAsync(result.Id.Value);
        }
    }
}
