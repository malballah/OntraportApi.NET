﻿using System;
using System.Net.Http;
using HanumanInstitute.OntraportApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace HanumanInstitute.OntraportApi.IntegrationTests
{
    public class ConfigHelper
    {
        public OntraportHttpClient GetHttpClient()
        {
            // var factory = Mock.Of<IHttpClientFactory>(x => x.CreateClient(It.IsAny<string>()) == new HttpClient());
            return new OntraportHttpClient(new HttpClient(), GetConfig(), null);
        }

        public IOptions<OntraportConfig> GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<ConfigHelper>();
            var configuration = builder.Build();

            var appId = configuration["OntraportAppId"];
            var apiKey = configuration["OntraportApiKey"];

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException(
@"Ontraport API credentials must be set in your User Secret Manager. Open the command-line tool and navigate to the OntraportApi.IntegrationTests project directory.

Use the following commands to set your keys. (Ask Ontraport for a SandBox account and get your keys in the admin section):
dotnet user-secrets set OntraportAppId ""your-app-id-here""
dotnet user-secrets set OntraportApiKey ""your-api-key-here""");
            }

            var config = new OntraportConfig()
            {
                AppId = appId,
                ApiKey = apiKey
            };
            return Mock.Of<IOptions<OntraportConfig>>(x => x.Value == config);
        }
    }
}
