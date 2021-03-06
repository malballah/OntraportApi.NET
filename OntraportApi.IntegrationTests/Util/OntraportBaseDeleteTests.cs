﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using HanumanInstitute.OntraportApi.Converters;
using HanumanInstitute.OntraportApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace HanumanInstitute.OntraportApi.IntegrationTests
{
    public abstract class OntraportBaseDeleteTests<T, U> : OntraportBaseWriteTests<T, U>
        where T : OntraportBaseDelete<U>
        where U : ApiObject
    {
        public OntraportBaseDeleteTests(ITestOutputHelper output, int validId, string validKeyValue) : 
            base(output, validId, validKeyValue)
        {
        }

        [Fact]
        public async Task DeleteAsync_IdJustCreated_ThrowsNoException()
        {
            var api = SetupApi();
            var obj = await api.CreateAsync();

            await api.DeleteAsync(obj.Id.Value);

            // Should throw Object Not Found.
            await Assert.ThrowsAsync<HttpRequestException>(() => api.SelectAsync(obj.Id.Value));
        }

        [Fact]
        public async Task DeleteMultipleAsync_IdJustCreated_NoException()
        {
            var api = SetupApi();
            var obj = await api.CreateAsync();

            await api.DeleteAsync(new ApiSearchOptions(obj.Id.Value));

            // Should throw Object Not Found.
            await Assert.ThrowsAsync<HttpRequestException>(() => api.SelectAsync(obj.Id.Value));
        }
    }
}
