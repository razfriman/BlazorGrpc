using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorGrpc.Api
{
    public static class GrpcServiceExtensions
    {
        public static void RegisterGrpcServiceClient<TClient>(this IServiceCollection services, string serviceUrl)
            where TClient : Grpc.Core.ClientBase => services.AddGrpcClient<TClient>(options =>
                options.Address = new Uri(serviceUrl))
            .ConfigurePrimaryHttpMessageHandler(() => InsecureHttpClientHandler);

        private static HttpClientHandler InsecureHttpClientHandler { get; } = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => true
        };
    }
}
