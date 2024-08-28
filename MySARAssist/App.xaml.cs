/* Unmerged change from project 'MySARAssist (net8.0-ios)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-android)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-windows10.0.19041.0)'
Added:
using MySARAssist.Models.Personnel;
*/
using MetroLog.Maui;
using MySARAssist.Converters;
using MySARAssist.Services;
using MySarAssistModels.Interfaces;
using MySarAssistModels.People;
using NewRelic.MAUI.Plugin;

namespace MySARAssist
{
    public partial class App : Application
    {
        private readonly PersonnelService _personnelService;

        public static Personnel? CurrentPerson { get; set; }


        public App()
        {
            InitializeComponent();
            this._personnelService = new PersonnelService();

            MainPage = new AppShell();

            CrossNewRelic.Current.HandleUncaughtException();
            CrossNewRelic.Current.TrackShellNavigatedEvents();

            // Set optional agent configuration
            // Options are: crashReportingEnabled, loggingEnabled, logLevel, collectorAddress, crashCollectorAddress
            // AgentStartConfiguration agentConfig = new AgentStartConfiguration(true, true, LogLevel.INFO, "mobile-collector.newrelic.com", "mobile-crash.newrelic.com");

            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                CrossNewRelic.Current.Start("AA32d4cf0465ae3269a836f3f40801a1bd04f4e60e-NRMA");
                // Start with optional agent configuration 
                // CrossNewRelic.Current.Start("<YOUR_ANDROID_TOKEN>", agentConfig);
            }
            else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            {
                CrossNewRelic.Current.Start("AA6a713ecb46cfabf55e052bfd4f6297c55258ea9e-NRMA");
                // Start with optional agent configuration 
                // CrossNewRelic.Current.Start("<YOUR_IOS_TOKEN>", agentConfig);
            }


            LogController.InitializeNavigation(
                page => MainPage!.Navigation.PushModalAsync(page),
                () => MainPage!.Navigation.PopModalAsync());


            LoadCurrentPerson();
            Task.Run(() => CreateInitialOrganizationsAsNeeded()).Wait();
            Task.Run(() => UpdateOrganizationsFromAPI()).Wait();


        }

        private async Task CreateInitialOrganizationsAsNeeded()
        {
            OrganizationService service = new OrganizationService();
            List<Organization> orgs = null;

            try
            {
                orgs = (await service.GetItems()).ToList();

            } catch
            {
                orgs = new List<Organization>();
            }
            if (!orgs.Any())
            {
                //get the static orgs and save them
                List<Organization> staticOrgs = OrganizationTools.GetStaticOrganizations(Guid.Empty);
                foreach(Organization org in staticOrgs)
                {
                    await service.UpsertItemAsync(org);
                }
            }


            


        }

        private async Task UpdateOrganizationsFromAPI()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                // Connection to internet is available
                RestService service = new RestService();
                OrganizationService orgService = new OrganizationService();
                List<ServiceReference1.Organization>? syncOrgs = await service.RefreshDataAsync();

                if(syncOrgs != null)
                {
                    foreach(ServiceReference1.Organization org in syncOrgs)
                    {
                        Organization newOrg = org.OrganizationFromWebserviceOrg();
                        if (newOrg != null) { await orgService.UpsertItemAsync(newOrg); }
                    }
                }

            }
        }

        private async void LoadCurrentPerson()
        {
            CurrentPerson = await _personnelService.GetCurrentPersonAsync();
            if(CurrentPerson == null)
            {
                ;
            }
        }
    }
}
