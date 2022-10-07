using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;


namespace Treinchat.Models
{

    public class BeanFactory
    {
    }

    public class Disturbances
    {
        public List<object> ev { get; set; }
        public List<object> vtb { get; set; }
    }

    public class FinalDestination
    {
        public string code { get; set; }
        public string type { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Languages languages { get; set; }
        public string defaultLanguageCode { get; set; }
    }

    public class IntelligentTrain
    {
        public string positionUrl { get; set; }
        public string tripUrl { get; set; }
        public string stateUrl { get; set; }
        public string disturbanceUrl { get; set; }
        public int disturbanceVersion { get; set; }
        public string transferUrl { get; set; }
        public string displayTravelInformationUrl { get; set; }
        public string comfortUrl { get; set; }
        public bool enableSplitCombine { get; set; }
        public string splitCombineUrl { get; set; }
        public string travelInformationTripUrl { get; set; }
    }

    public class JsonConfiguration
    {
        public string _doc_materialNumber { get; set; }
        public string materialNumber { get; set; }
        public string _doc_intelligentTrainUrl { get; set; }
        public string intelligentTrainUrl { get; set; }
        public string _doc_intelligentTrain { get; set; }
        public IntelligentTrain intelligentTrain { get; set; }
        public string _doc_logTrainDataErrors { get; set; }
        public bool logTrainDataErrors { get; set; }
        public string _doc_requestInternetConnectionURL { get; set; }
        public string requestInternetConnectionURL { get; set; }
        public string _doc_storagefolderVersion { get; set; }
        public string storagefolderVersion { get; set; }
        public string _doc_presentationBusURL { get; set; }
        public string presentationBusURL { get; set; }
        public string _doc_bbaPresentationBusURL { get; set; }
        public string bbaPresentationBusURL { get; set; }
        public int externalDisplayWebserviceVersion { get; set; }
        public string ketenLogFile { get; set; }
        public string dynamicConfigLocation { get; set; }
        public string webserviceRequestTimeout { get; set; }
        public string webserviceRetryIntervalSeconds { get; set; }
        public string nthLogLimitPositionComfort { get; set; }
        public string hasDelayedMasterdata { get; set; }
        public string enableTickerTape { get; set; }
        public string defaultScreenSize { get; set; }
        public string activateAudioOnTrainType { get; set; }
        public string stationsAudio { get; set; }
        public string stationsNew { get; set; }
        public string stations { get; set; }
        public string internationalTrip { get; set; }
        public string traintype { get; set; }
        public string geojson { get; set; }
        public string ppvInterruptPhase { get; set; }
        public string ppvInterruptImpact { get; set; }
        public string ppvVTBInterrupt { get; set; }
    }

    public class Languages
    {
        public Nl nl { get; set; }
    }

    public class LoggingService
    {
    }

    public class Masterdatautil
    {
        public string edgeconfigPath { get; set; }
        public string resourcePath { get; set; }
        public BeanFactory beanFactory { get; set; }
        public LoggingService loggingService { get; set; }
        public JsonConfiguration jsonConfiguration { get; set; }
    }

    public class Nl
    {
        public string shortName { get; set; }
        public string middleName { get; set; }
        public string longName { get; set; }
    }

    public class Root
    {
        public string currentStation { get; set; }
        public string nextStation { get; set; }
        public string splitCombineContent { get; set; }
        public bool displayTravelInformation { get; set; }
        public FinalDestination finalDestination { get; set; }
        public int phase { get; set; }
        public Trip trip { get; set; }
        public List<Transfer> transfer { get; set; }
        public Disturbances disturbances { get; set; }
    }

    public class Station
    {
        public string code { get; set; }
        public string type { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Languages languages { get; set; }
        public string defaultLanguageCode { get; set; }
    }

    public class Stop
    {
        public Station station { get; set; }
        public string stationCode { get; set; }
        public string stationType { get; set; }
        public string platform { get; set; }
        public bool platFormChanged { get; set; }
        public string fromDateTime { get; set; }
        public int arrivalDelay { get; set; }
        public int fromDelay { get; set; }
        public string dataDateTime { get; set; }
        public BeanFactory beanFactory { get; set; }
        public string arrivalDateTime { get; set; }
    }

    public class TrainTypeDao
    {
        public string edgeconfigPath { get; set; }
        public string resourcePath { get; set; }
        public BeanFactory beanFactory { get; set; }
        public LoggingService loggingService { get; set; }
        public JsonConfiguration jsonConfiguration { get; set; }
        public Masterdatautil masterdatautil { get; set; }
    }

    public class TraintypeService
    {
        public TrainTypeDao trainTypeDao { get; set; }
    }

    public class Transfer
    {
        public string trainNumber { get; set; }
        public string trainType { get; set; }
        public string trainTypeFull { get; set; }
        public string fromDateTime { get; set; }
        public string fromPlatform { get; set; }
        public bool platFormChanged { get; set; }
        public List<FinalDestination> finalDestination { get; set; }
        public int fromDelay { get; set; }
        public string dataDateTime { get; set; }
        public TraintypeService traintypeService { get; set; }
    }

    public class Trip
    {
        public bool isInternationTrip { get; set; }
        public string trainTypeFull { get; set; }
        public List<Stop> stops { get; set; }
        public List<object> tripLanguages { get; set; }
    }
    public class Models : MonoBehaviour
    {
        public Manager manager;

        public Root root;

        public Treinchat.Arrivals.Arrivals arrival;

        public string currentStation;
        public string currentStationLong;
        public string nextStation;
        public string nextStationLong;
        public string finalStation;
        public string finalStationLong;
        public string trainType;

        public bool connecti = false;

        private void Start()
        {
            GetInformation();
        }

        public void Refresh()
        {
            GetInformation();
        }
        public async Task GetInformation()
        {
            HttpClient httpClient = new HttpClient();
            ////Real
            var result = await httpClient.GetAsync("http://portal.nstrein.ns.nl/nstrein:main/travelInfo");
            var data = JsonConvert.DeserializeObject<Root>(await result.Content.ReadAsStringAsync());
            ////Testing
            //var result = File.ReadAllText(@"C:\Users\super\OneDrive\Documenten\Unity\TreinChat\Assets\Test.txt");
            //var data = JsonConvert.DeserializeObject<Root>(result);
            root = data;

            if (root != null)
            {
                connecti = true;
            }
            Debug.Log("Information Requested");
            await SetInformation();
        }

        public async Task SetInformation()
        {
            currentStation = root.currentStation;
            nextStation = root.nextStation;
            finalStation = root.finalDestination.code;
            finalStationLong = root.finalDestination.languages.nl.longName;
            trainType = root.trip.trainTypeFull;
            CheckName(nextStation);
            CheckNameCur(currentStation);
            await arrival.CheckArrivalAsync(finalStation);
        }


        public void CheckName(string code)
        {
            for (int i = 0; i < root.trip.stops.Count; i++)
            {
                if (root.trip.stops[i].station.code == code)
                {
                    nextStationLong = root.trip.stops[i].station.languages.nl.longName;
                    Debug.Log($"{root.trip.stops[i].station.languages.nl.longName} was found");
                }
                else
                {
                    Debug.Log($"{root.trip.stops[i].station.languages.nl.longName} was not found");
                }
            }
        }
        public void CheckNameCur(string code)
        {
            for (int i = 0; i < root.trip.stops.Count; i++)
            {
                if (root.trip.stops[i].station.code == code)
                {
                    currentStationLong = root.trip.stops[i].station.languages.nl.longName;
                    Debug.Log($"{root.trip.stops[i].station.languages.nl.longName} was found");
                }
                else
                {
                    Debug.Log($"{root.trip.stops[i].station.languages.nl.longName} was not found");
                }
            }
        }


    }
}



