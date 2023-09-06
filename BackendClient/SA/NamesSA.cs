using Entities.Configuration;
using Entities.NamesSa;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SA
{
    public class NamesSA : INamesSa
    {
        #region properties
        private readonly ApiConfig _config;
        private readonly HttpClient client;
        #endregion

        #region ctor
        public NamesSA(IOptions<ApiConfig> config)
        {
			_config = config.Value;
            client = new HttpClient();
        }
        #endregion

        #region public
        public async Task<Response>PostNameAsync(PostNameRequest nameReq)
        {
            Response res = new();
           
            try
            {
                CheckNamesApiUrl();
                HttpResponseMessage clientResponse = await client.PostAsJsonAsync(_config.Names, nameReq);
                 ReadClientResponse(clientResponse, res);
            }
            catch (Exception x)
            {
                HandleException(x, res);
            }
            return res;
        }

        public async Task<NamesSaResponse> GetNames()
		{
            NamesSaResponse res = new();
            try
			{
                CheckNamesApiUrl();
        
                HttpResponseMessage clientResponse = await client.GetAsync(_config.Names);

                if (clientResponse.IsSuccessStatusCode)
				{
                    res = await clientResponse.Content.ReadAsAsync<NamesSaResponse>();
                }
				return res ?? new NamesSaResponse() { StatusCode = (int)clientResponse.StatusCode, StatusDescription = clientResponse.StatusCode.ToString() };
            }
			catch (Exception x)
			{
                HandleException(x, res);            
            }
            return res;
        }

        public async Task<Response> PutNameAsync(SAPutNameRequest nameReq)
        {
            Response res = new();
            try
            {
                CheckNamesApiUrl();
         
                HttpResponseMessage clientResponse = await client.PutAsJsonAsync(_config.Names, nameReq);
                ReadClientResponse(clientResponse, res);    
            }
            catch (Exception x)
            {
                HandleException(x, res);
            }
            return res;
        }

        public async Task<Response> PatchNameAsync(int id, JsonPatchDocument patch)
        {
            Response res = new();
            try
            {
                CheckNamesApiUrl();

                HttpResponseMessage clientResponse = await client.PatchAsJsonAsync($"{_config.Names}/{id}", patch);
                ReadClientResponse(clientResponse, res);
            }
            catch (Exception x)
            {
                HandleException(x, res);
            }
            return res;
        }

        public async Task<Response> DeleteNameAsync(int id)
        {
            Response res = new();
            try
            {
                CheckNamesApiUrl();

                HttpResponseMessage clientResponse = await client.DeleteAsync($"{_config.Names}/{id}");
                ReadClientResponse(clientResponse, res);
            }
            catch (Exception x)
            {
                HandleException(x, res);
            }
            return res;
        }

        #endregion

        #region private
        async void ReadClientResponse(HttpResponseMessage clientResponse, Response res)
        {
            //clientResponse.EnsureSuccessStatusCode(); //throws an exception if returned status code is not ok 200
            var content = clientResponse.Content;
            string contentAsString = await content.ReadAsStringAsync();

            if (clientResponse.IsSuccessStatusCode)
            {
                Response r = JsonConvert.DeserializeObject<Response>(contentAsString);
                res.StatusCode = r.StatusCode;
                res.StatusDescription = r?.StatusDescription;
            }
            else
            {
                res.StatusCode = StatusCodes.Status409Conflict;
                res.StatusDescription = contentAsString;
            }
        }

        void HandleException(Exception x, Response res) {
            res.StatusCode = 500;
            res.StatusDescription = x.Message;
        }

         void CheckNamesApiUrl()
        {
            if (_config.Names == null) throw new Exception($"Names Api endpoint is null.");
        }
        #endregion
    }
}