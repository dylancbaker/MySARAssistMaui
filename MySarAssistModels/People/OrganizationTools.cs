namespace MySarAssistModels.People
{
    public static class OrganizationTools
    {
        public static Organization? GetOrganization(Guid orgID)
        {
            List<Organization> orgs = GetOrganizations(Guid.Empty, false);
            if (orgs.Any(o => o.OrganizationID == orgID)) { return orgs.First(o => o.OrganizationID == orgID); }
            if (GetParentOrganizations().Any(o => o.OrganizationID == orgID)) { return GetParentOrganizations().First(O => O.OrganizationID == orgID); }
            else { return null; }
        }

        public static List<Organization> GetParentOrganizations()
        {
            List<Organization> organizations = new List<Organization>();
            Guid ABSAR = new Guid("14CC75FE-75D3-44EE-B622-0C0727160675");
            Guid BCSARA = new Guid("CC3A9DC9-01A3-4D39-B806-2128B51120BC");
            Guid SARVAC = new Guid("2c7b4d1b-151e-4315-b4c9-f7c810568b2f");
            Guid Euro = new Guid("62d294f8-73cc-4957-b43a-a03d886d3bbe");
            Guid Portugal = new Guid("0948300a-bca0-4338-a5fc-f08402120498");
            Guid NCSAR = new Guid("806b2639-403b-473f-bba8-3b247ad3175a");
            Guid NBSAR = new Guid("fa0eea08-7d7f-4f71-af1b-1ae8b38e2566");
            Guid OtherOrgs = new Guid("D2BB4ADB-13FE-4941-AB4A-1A5020C3DC8C");
            Guid LE = new Guid("2a164b90-4364-4de3-b9ef-457555df5855");
            Guid GA = new Guid("30307d12-ff30-48bc-bb0e-1d06899f228a");

            organizations.Add(new Organization(ABSAR, Guid.Empty, "AB Search and Rescue", "saralberta-logo.png"));
            organizations.Add(new Organization(BCSARA, Guid.Empty, "BC Search and Rescue", "BCSARA -Logo-960.png"));
            organizations.Add(new Organization(SARVAC, Guid.Empty, "SAR Volunteer Assoc. of Canada", "crest@2x.png"));
            organizations.Add(new Organization(Euro, Guid.Empty, "European Association Civil Protection Volunteer Teams", "European Civil Protection.png"));
            organizations.Add(new Organization(Portugal, Guid.Empty, "Portuguese Teams", "protecao_civil_portugal_1_1024_2500.png"));
            organizations.Add(new Organization(NCSAR, Guid.Empty, "North Carolina Search and Rescue", "ncsar.png"));
            organizations.Add(new Organization(NBSAR, Guid.Empty, "New Brunswick Search and Rescue", "NBGSARA.png"));
            organizations.Add(new Organization(GA, Guid.Empty, "State of Georgia", "SAR Assistant_lq.png"));
            organizations.Add(new Organization(LE, Guid.Empty, "Law Enforcement", "SAR Assistant_lq.png"));

            organizations = organizations.OrderBy(o => o.OrganizationName).ToList();


            organizations.Add(new Organization(OtherOrgs, Guid.Empty, "Other Organizations", "BCSARA -Logo-960.png"));

            return organizations;
        }

        public static List<Organization> GetOrganizations(Guid ParentID, bool addBlankRecord = false, bool includeOtherOrgs = true)
        {
            //to be used when the org list is needed and internet is not avilable;
            List<Organization> organizations = new List<Organization>();
            Guid BCSARA = new Guid("CC3A9DC9-01A3-4D39-B806-2128B51120BC");
            Guid ABSAR = new Guid("14CC75FE-75D3-44EE-B622-0C0727160675");
            Guid OtherOrgs = new Guid("D2BB4ADB-13FE-4941-AB4A-1A5020C3DC8C");
            Guid SARVAC = new Guid("2c7b4d1b-151e-4315-b4c9-f7c810568b2f");
            Guid Euro = new Guid("62d294f8-73cc-4957-b43a-a03d886d3bbe");
            Guid Portugal = new Guid("0948300a-bca0-4338-a5fc-f08402120498");
            Guid NCSAR = new Guid("806b2639-403b-473f-bba8-3b247ad3175a");
            Guid NBSAR = new Guid("fa0eea08-7d7f-4f71-af1b-1ae8b38e2566");
            Guid LE = new Guid("2a164b90-4364-4de3-b9ef-457555df5855");
            Guid GA = new Guid("30307d12-ff30-48bc-bb0e-1d06899f228a");


            //BCSARA Teams
            organizations.Add(new Organization(new Guid("46698CE0-B146-40E2-B834-0089DECE3896"), BCSARA, "Mackenzie SAR", "MackenzieSAR.png"));
            organizations.Add(new Organization(new Guid("9202836C-DC5C-4C62-8190-022A03769098"), BCSARA, "Revelstoke SAR", "RevelstokeSAR.png"));
            organizations.Add(new Organization(new Guid("BE921E46-2521-4B8C-9438-0645CCB19DE8"), BCSARA, "Arrowsmith SAR", "ArrowsmithSAR.png"));
            organizations.Add(new Organization(new Guid("2F07BE02-EC9F-435D-B8E2-0B31799879BC"), BCSARA, "Quesnel SAR", "QuesnelSAR.png"));
            organizations.Add(new Organization(new Guid("C111498E-74F0-46B3-8442-0B38D435C854"), BCSARA, "Juan de Fuca SAR", "JdF.png"));
            organizations.Add(new Organization(new Guid("16FAD27A-76E6-471E-A3FA-146CCB740DBC"), BCSARA, "Kent Harrison SAR", "KentHarrisonSAR.png"));
            organizations.Add(new Organization(new Guid("4A2BA222-3766-4F38-86E4-1724FDE94AF3"), BCSARA, "Logan Lake SAR", "Logan-Lake.png"));
            organizations.Add(new Organization(new Guid("5C8A48A2-62F6-44E8-93CC-18ECAAE7E2ED"), BCSARA, "Arrow Lakes SAR"));
            organizations.Add(new Organization(new Guid("DBCE7252-92D1-481F-ADD0-1923F1F7FD39"), BCSARA, "Castlegar SAR", "CastlegarSAR.png"));
            organizations.Add(new Organization(new Guid("F2874DE5-0066-45A5-A3F8-1A38A64D67EC"), BCSARA, "Cowichan SAR", "CowichanSAR.png"));
            organizations.Add(new Organization(new Guid("499A9B5C-E712-4968-8B9F-1DEB9D402915"), BCSARA, "Campbell River SAR", "CRSAR.png"));
            organizations.Add(new Organization(new Guid("0EBB892E-F241-466A-B1E1-1E05F01EF0C7"), BCSARA, "North Shore SAR", "NorthShoreRescue.png"));
            organizations.Add(new Organization(new Guid("52078C4B-4A25-4103-A7EA-224B68AB5C55"), BCSARA, "West Chilcotin SAR", "WestChilcotinSAR.png"));
            organizations.Add(new Organization(new Guid("BFDAB6B8-BEE2-4157-A144-23965C166BD8"), BCSARA, "Stewart SAR", "StewartSAR.png"));
            organizations.Add(new Organization(new Guid("E05BB4DE-8786-4289-B39B-2574C41E2777"), BCSARA, "Houston SAR", "HoustonSAR.png"));
            organizations.Add(new Organization(new Guid("76A1101B-464D-4D12-ABBC-266599F2F5DD"), BCSARA, "Robson Valley SAR", "RobsonValleySAR.png"));
            organizations.Add(new Organization(new Guid("D44C628D-E9AD-43CE-9671-2965C64FF0E5"), BCSARA, "Fernie SAR", "FernieSAR.png"));
            organizations.Add(new Organization(new Guid("F964C5E1-2337-4761-AB36-2965F57E7DC4"), BCSARA, "Atlin SAR", "AtlinSAR.png"));
            organizations.Add(new Organization(new Guid("62231DA2-DCB8-413E-8C60-2A0B2E8DC214"), BCSARA, "Parkland SAR"));
            organizations.Add(new Organization(new Guid("37F43ECF-9CD5-4EB5-9699-2C0DBB7974F3"), BCSARA, "Ladysmith SAR", "LadysmithSARlogo.jpg"));
            organizations.Add(new Organization(new Guid("5B2C5A11-07DC-4104-99A5-364C204D4B32"), BCSARA, "Nicola Valley SAR", "NicolaValleySAR.png"));
            organizations.Add(new Organization(new Guid("937148E6-72FD-456B-9DD9-38E975492D5F"), BCSARA, "Nanaimo SAR", "NanaimoSAR.png"));
            organizations.Add(new Organization(new Guid("124CA510-0BD2-4F53-82BE-3A5F4960BFCC"), BCSARA, "Penticton SAR", "PenSAR.jpg"));
            organizations.Add(new Organization(new Guid("0B0DC8D4-9A62-4987-8EFA-43FC2A31D598"), BCSARA, "Whistler SAR", "WhistlerSAR.png"));
            organizations.Add(new Organization(new Guid("87756CEF-01AC-493A-89BE-464CADAE7EE1"), BCSARA, "Kimberley SAR", "KimberleySAR.png"));
            organizations.Add(new Organization(new Guid("74E5E639-A831-427F-8494-4DCD0A55BF37"), BCSARA, "Creston Valley SAR", "CrestonValleySAR.png"));
            organizations.Add(new Organization(new Guid("6A39C786-4C74-4B1E-91A6-4E1FCFE7584D"), BCSARA, "Kitimat SAR", "KitimatSAR.png"));
            organizations.Add(new Organization(new Guid("7AF5AD55-2655-4218-B693-56F48353E190"), BCSARA, "South Columbia SAR", "SouthColumbiaSAR.png"));
            organizations.Add(new Organization(new Guid("11A311E6-6768-46AA-9F3C-571AA95E3726"), BCSARA, "Nakusp SAR"));
            organizations.Add(new Organization(new Guid("65D6C93C-63F9-476F-8A63-5C8A968F4E11"), BCSARA, "PEMO SAR", "PeninsulaSAR.png"));
            organizations.Add(new Organization(new Guid("D83B66D5-C697-448F-9E4F-5DA7828A66A5"), BCSARA, "Metchosin SAR", "MetchosinSAR.png"));
            organizations.Add(new Organization(new Guid("9893E045-D590-44E4-8AF4-5DBB1F1661A4"), BCSARA, "Prince Rupert SAR", "PrinceRupertSAR.png"));
            organizations.Add(new Organization(new Guid("B3AE6204-9E95-452A-9C10-5EB4178E0ABE"), BCSARA, "Pemberton SAR", "PembertonSAR.png"));
            organizations.Add(new Organization(new Guid("2207EB6C-E292-47E8-AE3D-68855B846109"), BCSARA, "Powell River SAR", "PowellRiverSAR.png"));
            organizations.Add(new Organization(new Guid("A1C1A217-4AE3-462C-A7D4-68FFAA8C0210"), BCSARA, "Rossland SAR", "RosslandSAR.png"));
            organizations.Add(new Organization(new Guid("98851139-E736-4974-A672-6972F3A8965F"), BCSARA, "South Peace SAR", "SouthPeaceSAR.png"));
            organizations.Add(new Organization(new Guid("AB67B6D4-F795-43AD-9506-69880F7BEAFD"), BCSARA, "Alberni Valley Rescue Squad", "AlberniValleySAR.png"));
            organizations.Add(new Organization(new Guid("8940A059-6A5A-4004-9D1E-758C4D576C2E"), BCSARA, "Bulkley Valley SAR", "BulkleyValleySAR.png"));
            organizations.Add(new Organization(new Guid("169C529D-792A-4615-ABE9-78203A6A70A4"), BCSARA, "Grand Forks SAR", "GrandForksSAR.png"));
            organizations.Add(new Organization(new Guid("88B20B0B-04BC-4596-A2F5-7CCCB317A4A0"), BCSARA, "Ridge Meadows SAR", "RidgeMeadowsSAR.png"));
            organizations.Add(new Organization(new Guid("FEC27732-35F1-43D6-9597-7E0D2FEEDD65"), BCSARA, "Mission SAR", "MissionSAR.png"));
            organizations.Add(new Organization(new Guid("CDA9B3A3-FC65-4803-8685-813BCE298DEF"), BCSARA, "Bella Coola Valley SAR", "BellaCoolaSAR.png"));
            organizations.Add(new Organization(new Guid("2EFA81F4-3E9B-4E45-BE94-8166AAA9298C"), BCSARA, "Keremeos SAR", "KeremeosSAR.png"));
            organizations.Add(new Organization(new Guid("FF7F03D5-E414-443F-8315-82BB378ABBD8"), BCSARA, "Comox Valley SAR", "CVGSAR2020Logo.png"));
            organizations.Add(new Organization(new Guid("4643747E-FB2A-4230-B4B8-8506F9951DFF"), BCSARA, "Chetwynd SAR", "ChetwyndSAR.png"));
            organizations.Add(new Organization(new Guid("B865EDF6-F0A7-4241-8CD4-8A5942C7B18A"), BCSARA, "Tumbler Ridge SAR", "TumblerRidgeSAR.png"));
            organizations.Add(new Organization(new Guid("589F720D-A304-49CF-B40C-8E944D7C9D2F"), BCSARA, "Fort St. James SAR", "FortStJamesJSAR.png"));
            organizations.Add(new Organization(new Guid("893B590C-A159-4A89-9CA4-A3DF24D6ABFE"), BCSARA, "Sunshine Coast SAR", "SunshineCoastSAR.png"));
            organizations.Add(new Organization(new Guid("B04FE1E7-979F-40CC-892C-A74EEA295ECB"), BCSARA, "Kaslo SAR", "KasloSAR.png"));
            organizations.Add(new Organization(new Guid("8B8D2A86-91D6-4606-B2D7-A9EDF3C8EE35"), BCSARA, "Burns Lake SAR", "BurnsLakeSAR.png"));
            organizations.Add(new Organization(new Guid("8FB71ABC-4C5C-43A8-8E68-AA75DD2262EB"), BCSARA, "Vernon SAR", "Vernon.png"));
            organizations.Add(new Organization(new Guid("CE558295-B9E2-41A3-88F1-AE029EC1AE0D"), BCSARA, "Kamloops SAR", "KamloopsSAR.png"));
            organizations.Add(new Organization(new Guid("DF7FBC23-17F2-41AB-8DEB-AF00D27C5B7F"), BCSARA, "Oliver Osoyoos SAR", "OOSAR.png"));
            organizations.Add(new Organization(new Guid("64B9D581-05FD-4D9B-ABFF-B24BFAFCEA2C"), BCSARA, "Lions Bay SAR", "LionsBaySAR.png"));
            organizations.Add(new Organization(new Guid("3CD654FA-8B48-4C75-BB75-B7834639990C"), BCSARA, "South Cariboo SAR", "SouthCaribooSAR.png"));
            organizations.Add(new Organization(new Guid("F7CD798B-52AD-423E-B66F-BD01D3B70D5D"), BCSARA, "Squamish SAR", "SquamishSAR.png"));
            organizations.Add(new Organization(new Guid("D0AEC6BB-EE25-437B-8A8C-BEAA284FF685"), BCSARA, "Fort Nelson SAR"));
            organizations.Add(new Organization(new Guid("890789F2-9DB1-4785-BF00-BF051676B11E"), BCSARA, "Shuswap SAR", "ShuswapSAR.png"));
            organizations.Add(new Organization(new Guid("2679596A-3D96-4E74-913E-BF273235FC6F"), BCSARA, "Nechako Valley SAR", "NechakoValleySAR.png"));
            organizations.Add(new Organization(new Guid("EA48BC49-A566-4248-A4B5-C1A6DBBEF00B"), BCSARA, "Central Fraser Valley SAR", "CFVSAR.png"));
            organizations.Add(new Organization(new Guid("12FDF8F5-4127-456E-9F6D-C6912161BE6F"), BCSARA, "Columbia Valley SAR", "ColumbiaValleySAR.png"));
            organizations.Add(new Organization(new Guid("20A1A9F3-CB11-43F8-BEFE-C8933A566764"), BCSARA, "Prince George SAR", "PGSAR-002.png"));
            organizations.Add(new Organization(new Guid("C704D0F8-AF89-4143-BD3E-CB4EAC7A7AA7"), BCSARA, "Central Okanagan SAR", "COOSAR.png"));
            organizations.Add(new Organization(new Guid("EF91565B-77F8-428B-9D15-D0EAA4043A0E"), BCSARA, "Hope SAR", "HopeSAR.png"));
            organizations.Add(new Organization(new Guid("97717FBA-977C-49CD-B105-D55FA705AA14"), BCSARA, "North Peace SAR", "NorthPeaceSAR.png"));
            organizations.Add(new Organization(new Guid("83081A8A-6C30-45A8-B9B1-D625090148D6"), BCSARA, "Elkford SAR", "ElkfordSAR.png"));
            organizations.Add(new Organization(new Guid("6C4ED3C8-18D1-4F25-BEB1-D8C3B5272267"), BCSARA, "Coquitlam SAR", "CoquitlamSAR.png"));
            organizations.Add(new Organization(new Guid("6FD79D61-ED33-46E1-A1AA-E3BC1F24A143"), BCSARA, "Chilliwack SAR", "ChilliwackSAR.png"));
            //organizations.Add(new Organization(new Guid("B8E1D50D-C5A4-4ECD-9B5A-E4DB0AEFA57C"), "Saanich SAR"));
            organizations.Add(new Organization(new Guid("C0A7C62C-8C76-43F5-9357-E65394CCA2CB"), BCSARA, "South Fraser SAR", "SFSAR_Logo_TranparentBG_1500px-1.png"));
            organizations.Add(new Organization(new Guid("452E4432-F01C-4FDB-8DB4-E7399FC09A97"), BCSARA, "Sparwood SAR", "SparwoodSAR.png"));
            organizations.Add(new Organization(new Guid("0E63E227-27A8-4B11-8043-EC2C589A4CBA"), BCSARA, "Golden SAR", "GoldenSAR.png"));
            organizations.Add(new Organization(new Guid("3642094A-274C-44DA-B379-ED42E9265FF8"), BCSARA, "Central Cariboo SAR", "CentralCaribooSAR.png"));
            organizations.Add(new Organization(new Guid("D5A57651-6C40-4A8E-A442-F0D7294FE0ED"), BCSARA, "Terrace SAR", "TerraceSAR.png"));
            organizations.Add(new Organization(new Guid("8CA3E11B-5A87-4C72-A11C-F14225AC7AAF"), BCSARA, "Princeton SAR", "1 -PGSAR.png"));
            organizations.Add(new Organization(new Guid("77F87A03-46E8-4C70-A05B-F32DAE58276B"), BCSARA, "Westcoast Inland SAR", "WestCoastSAR.png"));
            organizations.Add(new Organization(new Guid("8E824695-1EDD-49FE-BADF-F42F8A34A95F"), BCSARA, "Salt Spring Island SAR", "SSILogo1.jpg"));
            organizations.Add(new Organization(new Guid("5153C373-4B73-45F6-99B6-F4DA00D28B92"), BCSARA, "Wells Gray SAR", "WellsGreySAR.png"));
            organizations.Add(new Organization(new Guid("B2CD40D5-6ABF-4FF8-A89E-F5FE6B995C89"), BCSARA, "Nelson SAR", "NelsonSAR.png"));
            organizations.Add(new Organization(new Guid("71FFF997-108B-4DDF-914E-F81069F8EA26"), BCSARA, "Barriere SAR", "Barrier.png"));
            organizations.Add(new Organization(new Guid("A3190007-E0EA-49F8-95F8-F8FF8396A38B"), BCSARA, "Cranbrook SAR", "CranbrookSAR.png"));
            organizations.Add(new Organization(new Guid("F1B9CA16-CB19-4DD2-961F-FE3EB6CC6477"), BCSARA, "Archipelago SAR", "ArchSAR.png"));


            //Alberta SAR
            organizations.Add(new Organization(new Guid("A52F1CD3-CE79-4AA7-BEA4-EA5BCB4DBC3C"), ABSAR, "Alberta/British Columbia Cave Rescue"));
            organizations.Add(new Organization(new Guid("F18ACA88-3F15-4F94-A067-E0F7195331DE"), ABSAR, "Badlands Search and Rescue"));
            organizations.Add(new Organization(new Guid("FB594701-8FB9-49B1-AD06-9F0CE462A0B0"), ABSAR, "Bonnyville SAR"));
            organizations.Add(new Organization(new Guid("5AEA95A1-398A-4A9F-86DC-DAFFD1550E1C"), ABSAR, "Brazeau Regional Search and Rescue"));
            organizations.Add(new Organization(new Guid("2107B4FA-A818-4951-9E76-19E43FA78E10"), ABSAR, "Calgary Search and Rescue Association"));
            organizations.Add(new Organization(new Guid("EE92AA60-D987-483F-8CCB-A49A27EA251F"), ABSAR, "Calling Lake Search and Rescue"));
            organizations.Add(new Organization(new Guid("D00E558F-AABA-4C27-A791-88C55C3200AD"), ABSAR, "Canadian Search Dog Association"));
            organizations.Add(new Organization(new Guid("78CC6C91-48F2-4A8A-93B1-5B4C751FDAEE"), ABSAR, "Central Zone Search and Rescue"));
            organizations.Add(new Organization(new Guid("BE2C305E-A83F-411A-82FA-7CCC07E8C945"), ABSAR, "Cochrane Search and Rescue "));
            organizations.Add(new Organization(new Guid("1146DE80-75A9-431C-93FA-A50F2BE3D882"), ABSAR, "Cold Lake Search and Rescue"));
            organizations.Add(new Organization(new Guid("1CD64BE4-3BDB-471F-B433-2A3F5B65A426"), ABSAR, "Edmonton Regional SAR"));
            organizations.Add(new Organization(new Guid("52DE1A5C-5421-4BF3-B5A5-13F22825BDA0"), ABSAR, "Foothills Search & Rescue"));
            organizations.Add(new Organization(new Guid("C1307123-D412-4906-940D-5E8E7A65C7A2"), ABSAR, "Fort McMurray SAR"));
            organizations.Add(new Organization(new Guid("98632A6A-7984-46E9-9C20-21FD38F908CF"), ABSAR, "Grande Cache SAR"));
            organizations.Add(new Organization(new Guid("0920E8BD-F0C1-4314-B294-50689BB83ADF"), ABSAR, "Greenview Search and Rescue Association"));
            organizations.Add(new Organization(new Guid("55F762C4-594E-4716-A3A9-A7FA7C6F16D2"), ABSAR, "Hinton Search & Rescue"));
            organizations.Add(new Organization(new Guid("56DF7757-DD4E-4B03-B972-C0922ACA3272"), ABSAR, "Klondike Trail SAR"));
            organizations.Add(new Organization(new Guid("92D9089B-8B57-4BA1-A833-DA3389F97D39"), ABSAR, "LASARA"));
            organizations.Add(new Organization(new Guid("6F951735-6400-45AB-8D5B-470BEAF92397"), ABSAR, "Lesser Slave Lake Search and Rescue"));
            organizations.Add(new Organization(new Guid("02C80D21-6A55-4107-8180-AE263C77A2BA"), ABSAR, "Little Divide Search and Rescue"));
            organizations.Add(new Organization(new Guid("5BA2D51B-C5F5-4D06-BB3F-A809257F5FD7"), ABSAR, "Mountain View Search & Rescue"));
            organizations.Add(new Organization(new Guid("38F21B78-00BD-43D5-8422-9526CE6F929F"), ABSAR, "Parkland Search and Rescue"));
            organizations.Add(new Organization(new Guid("B25D8BB0-9CFE-41A9-88B9-E7DA8BBBA318"), ABSAR, "Peace Region Search and Rescue"));
            organizations.Add(new Organization(new Guid("21F36B99-445D-43F3-B7DA-843FC1B4A55C"), ABSAR, "Pincher Creek Search & Rescue"));
            organizations.Add(new Organization(new Guid("F333977A-3BF0-4780-80F5-69D070488AC0"), ABSAR, "Red Deer County SAR"));
            organizations.Add(new Organization(new Guid("83978072-06CB-45E2-8C22-61D6E93917DE"), ABSAR, "Rocky Mountain House Search & Rescue"));
            organizations.Add(new Organization(new Guid("EBAC4741-CBC7-47C3-A132-E396FC1174C6"), ABSAR, "SARDAA"));
            organizations.Add(new Organization(new Guid("E50F1F97-2E42-4CA9-8C26-704E0C23282A"), ABSAR, "South Eastern Alberta Search and Rescue"));
            organizations.Add(new Organization(new Guid("4F020A6A-5CF4-4479-A6AF-A6319DB3800F"), ABSAR, "St Paul SAR"));
            organizations.Add(new Organization(new Guid("7A5DCB28-0244-4DFF-98C1-5C6DAA46D7E8"), ABSAR, "Sundre Volunteer Search and Rescue"));
            organizations.Add(new Organization(new Guid("5347C261-0187-4044-8B58-ED494B1FF805"), ABSAR, "Technical Search and Rescue"));
            organizations.Add(new Organization(new Guid("19703F2D-4479-4D64-81E4-FA04B6144D87"), ABSAR, "Wetaskiwin Search and Rescue"));
            organizations.Add(new Organization(new Guid("E0E78144-CD98-4623-AB20-6020CBB68772"), ABSAR, "Whitecourt SAR"));

            //Europe
            organizations.Add(new Organization(new Guid("8a0165b4-71f2-4dc8-ac6f-09b9ea6678c3"), Euro, "SAR Team – Associação de Voluntários de Proteção Civil, EVOLSAR"));
            organizations.Add(new Organization(new Guid("f01f53e3-fed4-4931-9c31-ebc1533a3f31"), Euro, "EFRU – Emergency Fire & Rescue Unit, EVOLSAR"));
            organizations.Add(new Organization(new Guid("9fda8fc9-6de5-4228-8129-b17145735535"), Euro, "SARAID – Search and Rescue Assistance in Disasters, EVOLSAR"));
            organizations.Add(new Organization(new Guid("07a93b03-9eba-48cb-8062-80a5e25be7a6"), Euro, "PUI – Pompiers de l’Urgence Internationale, EVOLSAR"));
            organizations.Add(new Organization(new Guid("7e5fedef-91e5-48a7-ae54-425a679c4c40"), Euro, "UCRS Madrid – Unidad Canina de Rescagte y Salvamento de Madrid, EVOLSAR"));
            organizations.Add(new Organization(new Guid("efab4677-59d7-4e97-ad60-1a0a06e79fe2"), Euro, "CCPVC – Cyprus Civil Protection Volunteer Corps, EVOLSAR"));
            organizations.Add(new Organization(new Guid("c5246a2f-3b44-4dfb-b8fa-f5b939ab368e"), Euro, "Serve ON, EVOLSAR"));
            organizations.Add(new Organization(new Guid("c15b622f-6d61-4060-9e87-160c0e50a8fb"), Euro, "SRT – Serbian Rescue Team, EVOLSAR"));
            organizations.Add(new Organization(new Guid("1a8d91de-c4bb-428b-bdf7-a340066d1e29"), Euro, "OPVE – Central Buda Volunteer Civil Protection Association, EVOLSAR"));
            organizations.Add(new Organization(new Guid("d2e8440a-c01a-4972-a7e5-02a99d56a079"), Euro, "EPOMEA – Elite Team Special Mission of Greece, EVOLSAR"));
            organizations.Add(new Organization(new Guid("d6511edf-e552-4afc-a608-94d72e3815d5"), Euro, "Edelweiss, EVOLSAR"));
            organizations.Add(new Organization(new Guid("63f28253-d774-43c7-8164-90111895173c"), Euro, "AHBVP – Bombeiros Voluntários de Peniche, EVOLSAR"));
            organizations.Add(new Organization(new Guid("70bf8a7e-2d7a-4fd7-bf6a-eca938c2a29e"), Euro, "Rescue GR, EVOLSAR"));
            organizations.Add(new Organization(new Guid("9c31d966-89d8-4ed8-b23c-7b38e0b745e8"), Euro, "Angeli Della Sila, EVOLSAR"));
            organizations.Add(new Organization(new Guid("f0273de9-f7e5-4a95-9ca8-f20a75e5777f"), Euro, "Protezione Civile Antelao, EVOLSAR"));
            organizations.Add(new Organization(new Guid("8ebe34f2-09c2-45f5-abd7-2bed92409a4b"), Euro, "GIANNINO CARIA, EVOLSAR"));

            organizations.Add(new Organization(new Guid("4beb346f-abad-40f2-8f16-8a558e2a169c"), Portugal, "APMC – Associação Portuguesa de Mantrailing e Canicross"));
            organizations.Add(new Organization(new Guid("de306c0e-f4d4-4b8c-a1f7-00c7b91687bb"), Portugal, "K9H CIOPS – Corpo de Intervenção em Operações Proteção e Socorro"));
            organizations.Add(new Organization(new Guid("f00487f3-6369-461b-b2b2-5a010084b291"), Portugal, "IRA – Intervenção e Resgate Animal"));
            organizations.Add(new Organization(new Guid("375479b3-d36d-4983-be4d-0921dffa5c84"), Portugal, "UPIR – Unidade Portuguesa Intervenção e Resgate"));
            organizations.Add(new Organization(new Guid("7d11a174-bba3-46d6-8e4e-b34c73a0500d"), Portugal, "ARC – Associação de Resgate Cinotécnico"));
            organizations.Add(new Organization(new Guid("f344c49c-945e-4cea-9518-8f3e2b0915ca"), Portugal, "APBS – Associação Portuguesa de Busca e Salvamento"));
            organizations.Add(new Organization(new Guid("0c65dc19-3cc1-47b0-afac-9b48fa59ed3a"), Portugal, "GNR – Guarda Nacional Republicana"));
            organizations.Add(new Organization(new Guid("b6ee6f2b-7c85-4c7d-b7b0-72fb3ace0389"), Portugal, "UEPS – Unidade Emergência Proteção e Socorro"));
            organizations.Add(new Organization(new Guid("3df714ce-ccc5-426f-af57-fd987e1daafb"), Portugal, "UEPS PIPS – Pelotão de Intervenção Proteção e Socorro"));
            organizations.Add(new Organization(new Guid("77aa7eb5-fb34-4527-b274-fb105edb1854"), Portugal, "UEPS Montanha – Busca e Resgate em Montanha"));
            organizations.Add(new Organization(new Guid("43ca4c7d-2922-4aed-95be-b4a0b3472100"), Portugal, "GIC GNR – Grupo Intervenção Cinotécnico"));
            organizations.Add(new Organization(new Guid("736cce7f-abf7-43da-919f-caa272db7a99"), Portugal, "PSP – Policia Segurança Pública"));
            organizations.Add(new Organization(new Guid("0723314b-9eca-4c36-82a2-4040ac8bc53b"), Portugal, "PJ – Policia Judiciária"));
            organizations.Add(new Organization(new Guid("698200df-5294-4592-b367-58084d1c408f"), Portugal, "FOE – Força de Operações Especiais"));
            organizations.Add(new Organization(new Guid("6c1a79ea-60f7-4c19-895d-65d966cb63fd"), Portugal, "CVP – Cruz Vermelha Portuguesa"));
            organizations.Add(new Organization(new Guid("294c6071-b790-45f0-bb48-46c0be473022"), Portugal, "ANEPC – Autoridade Nacional Emergência Proteção Civil"));
            organizations.Add(new Organization(new Guid("008d5f52-f165-4228-bac9-ce7575585e7f"), Portugal, "FEPC – Força Especial de Proteção Civil"));
            organizations.Add(new Organization(new Guid("e5e24919-a592-481b-b8a8-0183aae32912"), Portugal, "Bombeiros Municipais de Loulé"));
            organizations.Add(new Organization(new Guid("4ac46014-643a-4fb8-90ad-d518646b07e5"), Portugal, "Bombeiros Voluntários de Portimão"));
            organizations.Add(new Organization(new Guid("22eaa153-cf3e-49f8-ae5e-0b484edf6b3a"), Portugal, "Bombeiros Voluntários de Albufeira"));
            organizations.Add(new Organization(new Guid("0c0de226-2a95-4fb1-a85a-840df88b4c78"), Portugal, "Bombeiros Voluntários de Lagos"));
            organizations.Add(new Organization(new Guid("03eee73b-bb46-4a81-a11b-8dd7dc72e461"), Portugal, "Bombeiros Voluntários de Vila do Bispo"));
            organizations.Add(new Organization(new Guid("2c0b8f40-8a08-4e25-b5fd-6a6e96b3bd50"), Portugal, "Bombeiros Voluntários de Aljezur"));
            organizations.Add(new Organization(new Guid("48ea93ef-a533-4290-9b10-e38a5693e1d8"), Portugal, "GSE – Grupo de Salvamentos Especiais"));
            organizations.Add(new Organization(new Guid("bae439fa-da7f-4774-bcb0-2d5739a0239e"), Portugal, "Bombeiros Mistos de Amora"));
            organizations.Add(new Organization(new Guid("9a567b9b-4f5f-490f-b3da-9bbb1e8c6bfc"), Portugal, "Bombeiros Voluntários de Cacilhas"));
            organizations.Add(new Organization(new Guid("cb5fb786-5b89-4eed-b276-8d7d453d6ead"), Portugal, "Bombeiros Voluntários de Mora"));
            organizations.Add(new Organization(new Guid("fa7775e9-8bdd-4a30-a875-39024a90396d"), Portugal, "Bombeiros Voluntários de Minde"));
            organizations.Add(new Organization(new Guid("a2555425-b67b-458c-8e6d-2ed7419cef8a"), Portugal, "Bombeiros Voluntários de Aveiro Velhos"));
            organizations.Add(new Organization(new Guid("4a0614fb-798f-4081-ab32-28ff7297987a"), Portugal, "Bombeiros Voluntários de Oliveira do Bairro"));
            organizations.Add(new Organization(new Guid("1020c293-5b90-4759-bb46-4aa93bb38c30"), Portugal, "Bombeiros Voluntários de Póvoa de Lanhoso"));
            organizations.Add(new Organization(new Guid("eedaa4a4-6f41-4cec-b0ab-4c91aa3c2912"), Portugal, "Bombeiros Voluntários de Baião"));
            organizations.Add(new Organization(new Guid("0c5876c6-5844-4555-8476-4086836bfd1c"), Portugal, "Bombeiros Voluntários de Santa Marinha do Zêzere"));
            organizations.Add(new Organization(new Guid("6601abe5-0d98-4bca-bb25-2f6a85effd6e"), Portugal, "Bombeiros Voluntários de Viseu"));
            organizations.Add(new Organization(new Guid("b7e94356-f43b-4adc-95aa-7d429a341879"), Portugal, "Bombeiros Sapadores do Porto"));
            organizations.Add(new Organization(new Guid("86482a7f-0ffb-4e60-ad90-77b875157400"), Portugal, "Bombeiros Sapadores de Lisboa"));
            organizations.Add(new Organization(new Guid("ce87c046-34cc-4812-9e51-58bc2332393c"), Portugal, "Bombeiros Sapadores do Cartaxo"));
            organizations.Add(new Organization(new Guid("a0d61ba8-1d0d-4dbd-a7fd-1a4ecb91a5ea"), Portugal, "Bombeiros do Concelho de Espinho"));





            //SARVAC

            organizations.Add(new Organization(new Guid("1C8C2043-4A7A-43D9-B035-21C5444816F8"), SARVAC, "SARVAC", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("5F48C5BD-6645-4920-A065-EDC67D7ABDE2"), SARVAC, "Humanitarian Workforce", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("cad7191c-df37-4ca6-bc6c-0ec19b9c957b"), SARVAC, "SAR Manitoba", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("af4df48e-9f82-45bb-b302-6b14b539e336"), SARVAC, "SAR New Brunswick", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("189e80f9-d552-4763-84f4-8bc3ac6c0af8"), SARVAC, "SAR Newfoundland and Labrador", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("7c4d74f3-d2aa-4569-93a1-845bdb179cb7"), SARVAC, "SAR Northwest Territories", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("EA620563-7DBF-4D20-88F9-BDB0A80C2C94"), SARVAC, "SAR Nova Scotia", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("96f19e34-5a42-445d-a935-40bc78162705"), SARVAC, "SAR Nunavut", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("871323c6-903d-4734-a2f8-a58613ab7996"), SARVAC, "SAR Ontario", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("907dbebe-270b-443e-8366-b5d368fa5335"), SARVAC, "SAR Prince Edward Island", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("6f0febc9-6740-4540-b42a-450f1a22a7ac"), SARVAC, "SAR Quebec", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("b6c70fac-07d9-4f8a-bb66-22c815001032"), SARVAC, "SAR Saskatchewan", "crest@2x.png"));
            organizations.Add(new Organization(new Guid("a72a71ce-4f34-4e46-80a2-b2e524f8af64"), SARVAC, "SAR Yukon", "crest@2x.png"));


            //North Carolina
            organizations.Add(new Organization(new Guid("65ec02d8-67a9-4ef9-83a4-8605e12fc88c"), NCSAR, "Cowee Search and Rescue", "ncsar.png"));
            organizations.Add(new Organization(new Guid("adf93714-a7f9-4cf1-b91e-ffb7030b5eda"), NCSAR, "NC Volunteer SAR", "ncsar.png"));

            //Georgia
            organizations.Add(new Organization(new Guid("0ed3afc3-212d-4220-bb46-28a073359635"), GA, "White County SAR"));
            organizations.Add(new Organization(new Guid("05d07f03-1660-4e69-94d5-51a91bb2af87"), GA, "Friends of SAR, inc."));
            organizations.Add(new Organization(new Guid("93729cf8-f53c-4efc-a2c1-654bd11e9288"), GA, "Stephens County SAR"));
            organizations.Add(new Organization(new Guid("80c5650d-f91c-49e9-8f0c-382c159f7906"), GA, "Rabun County SAR"));
            organizations.Add(new Organization(new Guid("77c041c9-fa15-498c-837c-820a35188b01"), GA, "Union County SAR"));
            organizations.Add(new Organization(new Guid("d803f83b-da53-492e-a27a-e5a7a1233839"), GA, "Habersham County"));
            organizations.Add(new Organization(new Guid("2183b6ea-0267-40ec-8572-7ab844cb58e9"), GA, "Lumpkin County"));
            organizations.Add(new Organization(new Guid("54cdeae4-940c-489a-920c-b70bd46015f4"), GA, "Franklin County"));



            //New Brunswick
            organizations.Add(new Organization(new Guid("5bf6f806-f11f-48df-b1e5-0fbdd99a5476"), NBSAR, "North West GSAR", "nw gsar.jpg"));
            organizations.Add(new Organization(new Guid("9dcf452c-22af-44c3-aa32-30e61d8f743e"), NBSAR, "Acadie-Chaleur GSAR", "ac gsar.jpg"));
            organizations.Add(new Organization(new Guid("14a38990-6e53-4982-b64c-846fe10b4420"), NBSAR, "Miramichi GSAR", "miramichi gsar.jpg"));
            organizations.Add(new Organization(new Guid("8ed9f2e6-cd21-49fd-898e-3b10abb5a0d0"), NBSAR, "Tri-County GSAR", "Tri County.png"));
            organizations.Add(new Organization(new Guid("3871123d-3678-4b7c-ace0-b6bf19275b0d"), NBSAR, "Greater Fundy GSAR", "GF GSAR.jpg"));
            organizations.Add(new Organization(new Guid("863fc64b-db75-4198-a79f-c80031bc5fd4"), NBSAR, "River Valley GSAR", "rvgsar.jpg"));
            organizations.Add(new Organization(new Guid("597778cb-627d-4175-be26-65adb30dee8c"), NBSAR, "Charlotte County GSAR", "charlote.png"));
            organizations.Add(new Organization(new Guid("a8ccf5f1-4b2d-48b5-bc04-d48d2f9744c8"), NBSAR, "York Sunbury SAR", "YSSR Logo.png"));
            organizations.Add(new Organization(new Guid("bcbe0646-6e04-4e58-97fb-e9ec3708e228"), NBSAR, "Carleton GSAR", "carleton.jpg"));

            //Law enforcement entities
            organizations.Add(new Organization(new Guid("6b7e460d-2490-4a8c-b828-5c1d7d707c83"), LE, "Ontario Prov. Police", "OPPERT.png"));
            organizations.Add(new Organization(new Guid("75467ef9-fee0-4911-b54f-62e1ba926095"), LE, "Police", "SAR Assistant_lq.png"));
            organizations.Add(new Organization(new Guid("85755fe3-d757-4bdc-87c2-156ccafbdc57"), LE, "Royal Canadian Mounted Police", "RCMPLogo.png"));
            organizations.Add(new Organization(new Guid("c0376477-71c3-42ba-bcba-4d674dd18572"), LE, "Sheriff", "SAR Assistant_lq.png"));


            organizations = organizations.OrderBy(o => o.OrganizationName).ToList();

            //manually put these at the end
            organizations.Add(new Organization(new Guid("489f9815-808d-4f55-a17f-214d352e7661"), BCSARA, "BC Search Dog Association", "BCSDA-2016-logo_512.png"));
            organizations.Add(new Organization(new Guid("008bdd33-28a1-46b6-818d-59225f2e97df"), BCSARA, "BC Tracking Association", "BCTALogoSeal@1x.png"));
            organizations.Add(new Organization(new Guid("88A344BD-BEED-4727-A353-3E75B028507D"), BCSARA, "Critical Incident Stress Management", "BCSARA-Logo-960.png"));



            if (ParentID != Guid.Empty)
            {
                organizations = organizations.Where(o => o.ParentOrganizationID == ParentID).ToList();
            }

            if (ParentID == OtherOrgs || includeOtherOrgs)
            {
                organizations.Add(new Organization(new Guid("d7d6350e-7666-4ef1-9205-f38edd1efc1f"), OtherOrgs, "Convergent Volunteer", "SAR Assistant_lq.png"));
                organizations.Add(new Organization(new Guid("b98654a4-2e95-4d20-b8c3-80a0cf565075"), OtherOrgs, "Community Volunteer", "SAR Assistant_lq.png"));
                organizations.Add(new Organization(new Guid("2c4ff368-fe8b-41d9-a34d-7c2691b73138"), OtherOrgs, "First Nations Member", "SAR Assistant_lq.png"));
                organizations.Add(new Organization(new Guid("8CBE0C6D-78B1-4600-96C0-21E3C16A444D"), OtherOrgs, "Great Hat Web Design", "GreatHatCircle.png"));
                organizations.Add(new Organization(new Guid("02035C34-CD9C-4B3D-9C22-5AF29068A0D9"), OtherOrgs, "Non-SAR", "SAR Assistant_lq.png"));
                organizations.Add(new Organization(new Guid("96BA69A4-436C-4DA1-85B1-992E84C36019"), OtherOrgs, "Unassigned", "SAR Assistant_lq.png"));
            }
            if (addBlankRecord)
            {
                Organization blankOrg = new Organization();
                blankOrg.OrganizationID = Guid.Empty;
                blankOrg.OrganizationName = "-All Groups-";
                organizations.Insert(0, blankOrg);
            }


            return organizations;
        }
    }
}
