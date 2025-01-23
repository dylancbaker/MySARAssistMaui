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

        public async Task<List<Organization>?> TestRefreshDataAsync()
        {
            List<Organization> parentOrgs = new List<Organization>();
            List<Organization> childOrgs = new List<Organization>();

            try
            {
                ServiceReference1.GetParentOrganizationsAsJSONRequest request = new ServiceReference1.GetParentOrganizationsAsJSONRequest();
                CAUpdatesWebserviceSoapClient client = new CAUpdatesWebserviceSoapClient(CAUpdatesWebserviceSoapClient.EndpointConfiguration.ICAUpdatesWebserviceSoap);
                GetParentOrganizationsAsJSONResponse response = await client.GetParentOrganizationsAsJSONAsync(request);
                if (response != null)
                {
                    string str = response.GetParentOrganizationsAsJSONResult.ToString();
                   
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            foreach (Organization org in Items)
            {
                childOrgs.AddRange(await GetChildOrganizationsAsync(org.OrganizationID));
            }

            Items = new List<Organization>();
            Items.AddRange(parentOrgs);
            Items.AddRange(childOrgs);
            return Items;
        }


        public async Task<List<Organization>?> RefreshDataAsync()
        {
            List<Organization> parentOrgs = new List<Organization>();
            List<Organization> childOrgs = new List<Organization>();

            try
            {
                ServiceReference1.GetParentOrganizationsAsyncRequest request = new ServiceReference1.GetParentOrganizationsAsyncRequest();
                CAUpdatesWebserviceSoapClient client = new CAUpdatesWebserviceSoapClient(CAUpdatesWebserviceSoapClient.EndpointConfiguration.ICAUpdatesWebserviceSoap);
                GetParentOrganizationsAsyncResponse response = await client.GetParentOrganizationsAsyncAsync(request).ConfigureAwait(false);
                if (response.GetParentOrganizationsAsyncResult != null)
                {
                    foreach (Organization org in response.GetParentOrganizationsAsyncResult.Result)
                    {
                        parentOrgs.Add(org);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            foreach (Organization org in Items)
            {
                childOrgs.AddRange(await GetChildOrganizationsAsync(org.OrganizationID));
            }

            Items = new List<Organization>();
            Items.AddRange(parentOrgs);
            Items.AddRange(childOrgs);
            return Items;
        }

        private async Task<List<Organization>> GetChildOrganizationsAsync(Guid Parent)
        {
            Items = new List<Organization>();

            ServiceReference1.GetChildOrganizationsAsyncRequest request = new ServiceReference1.GetChildOrganizationsAsyncRequest(Parent);
            CAUpdatesWebserviceSoapClient client = new CAUpdatesWebserviceSoapClient(CAUpdatesWebserviceSoapClient.EndpointConfiguration.ICAUpdatesWebserviceSoap);
            GetChildOrganizationsAsyncResponse response = await client.GetChildOrganizationsAsyncAsync(request).ConfigureAwait(false);
            if (response.GetChildOrganizationsAsyncResult != null)
            {
                foreach (Organization org in response.GetChildOrganizationsAsyncResult.Result)
                {
                    Items.Add(org);
                }
            }

            return Items;
        }

    }
}
