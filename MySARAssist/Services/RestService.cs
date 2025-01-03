
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySARAssist.Services
{
    public class RestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Organization>? Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<List<Organization>?> RefreshDataAsync()
        {
            Items = new List<Organization>();

            ServiceReference1.GetAllOrganizationsRequest request = new ServiceReference1.GetAllOrganizationsRequest();
            CAUpdatesWebserviceSoapClient client = new CAUpdatesWebserviceSoapClient(CAUpdatesWebserviceSoapClient.EndpointConfiguration.ICAUpdatesWebserviceSoap);
            GetAllOrganizationsResponse response = await client.GetAllOrganizationsAsync(request);
            if (response.GetAllOrganizationsResult != null)
            {
                foreach (Organization org in response.GetAllOrganizationsResult.Result)
                {
                    Items.Add(org);
                }
            }


            /*
             Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
             try
             {
                 HttpResponseMessage response = await _client.GetAsync(uri); 
                 if (response.IsSuccessStatusCode)
                 {
                     string content = await response.Content.ReadAsStringAsync();
                     Items = JsonSerializer.Deserialize<List<Organization>>(content, _serializerOptions);
                 }
             }
             catch (Exception ex)
             {
                 Debug.WriteLine(@"\tERROR {0}", ex.Message);
             }
            */
            return Items;
        }


        public async Task<List<NewsItem>?> RefreshNewsDataAsync()
        {

            ServiceReference1.GetAllNewsItemsRequest request = new ServiceReference1.GetAllNewsItemsRequest();
            CAUpdatesWebserviceSoapClient client = new CAUpdatesWebserviceSoapClient(CAUpdatesWebserviceSoapClient.EndpointConfiguration.ICAUpdatesWebserviceSoap);
            GetAllNewsItemsResponse response = await client.GetAllNewsItemsAsync(request);
            if (response.GetAllNewsItemsResult != null)
            {
                int n = response.GetAllNewsItemsResult.Length;
            }


            /*
             Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
             try
             {
                 HttpResponseMessage response = await _client.GetAsync(uri); 
                 if (response.IsSuccessStatusCode)
                 {
                     string content = await response.Content.ReadAsStringAsync();
                     Items = JsonSerializer.Deserialize<List<Organization>>(content, _serializerOptions);
                 }
             }
             catch (Exception ex)
             {
                 Debug.WriteLine(@"\tERROR {0}", ex.Message);
             }
            */
            return new List<NewsItem>();
        }

    }
}
