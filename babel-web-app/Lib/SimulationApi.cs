using System;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace babel_web_app.Lib
{
    public class SimulationApi
    {
        private readonly IRestClient _client;

        public SimulationApi(IRestClient client, IOptions<SimulationEngineOptions> optionsAccessor) {
            _client = client;
            _client.BaseUrl = new Uri(optionsAccessor.Value.Url);
            _client.UseNewtonsoftJson();
        }

        public W ExecutePost<W>(string endpoint, object request) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);

            restRequest.AddJsonBody(request);
            var response = _client.Post<W>(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new SimulationApiException(response.Content);
            }

            return response.Data;
        }

        public W ExecuteGet<W>(string endpoint) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            var response = _client.Get<W>(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new SimulationApiException(response.Content);
            }

            return response.Data;
        }

        public void ExecuteDelete(string endpoint) {
            var restRequest = new RestRequest(endpoint, DataFormat.Json);
            var response = _client.Delete(restRequest);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new SimulationApiException(response.Content);
            }
        }
        
    }
}

