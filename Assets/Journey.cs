using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using Treinchat.Tripss;

namespace Treinchat.Journey
{
    public class ActualStock
    {
        public string trainType { get; set; }
        public int numberOfSeats { get; set; }
        public int numberOfParts { get; set; }
        public List<TrainPart> trainParts { get; set; }
        public bool hasSignificantChange { get; set; }
    }

    public class Arrival
    {
        public Product product { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public DateTime plannedTime { get; set; }
        public DateTime actualTime { get; set; }
        public int delayInSeconds { get; set; }
        public string plannedTrack { get; set; }
        public string actualTrack { get; set; }
        public bool cancelled { get; set; }
        public string crowdForecast { get; set; }
        public List<string> stockIdentifiers { get; set; }
        public double? punctuality { get; set; }
    }

    public class Departure
    {
        public Product product { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public DateTime plannedTime { get; set; }
        public DateTime actualTime { get; set; }
        public int delayInSeconds { get; set; }
        public string plannedTrack { get; set; }
        public string actualTrack { get; set; }
        public bool cancelled { get; set; }
        public string crowdForecast { get; set; }
        public List<string> stockIdentifiers { get; set; }
    }

    public class Destination
    {
        public string name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
        public string uicCode { get; set; }
    }

    public class Image
    {
        public string uri { get; set; }
    }

    public class Origin
    {
        public string name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
        public string uicCode { get; set; }
    }

    public class Payload
    {
        public List<object> notes { get; set; }
        public List<string> productNumbers { get; set; }
        public List<Stop> stops { get; set; }
        public bool allowCrowdReporting { get; set; }
        public string source { get; set; }
    }

    public class PlannedStock
    {
        public string trainType { get; set; }
        public int numberOfSeats { get; set; }
        public int numberOfParts { get; set; }
        public List<TrainPart> trainParts { get; set; }
        public bool hasSignificantChange { get; set; }
    }

    public class Product
    {
        public string number { get; set; }
        public string categoryCode { get; set; }
        public string shortCategoryName { get; set; }
        public string longCategoryName { get; set; }
        public string operatorCode { get; set; }
        public string operatorName { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public Payload payload { get; set; }
    }

    public class Stop
    {
        public string id { get; set; }
        public Stop stop { get; set; }
        public List<string> previousStopId { get; set; }
        public List<string> nextStopId { get; set; }
        public string destination { get; set; }
        public string status { get; set; }
        public List<Arrival> arrivals { get; set; }
        public List<Departure> departures { get; set; }
        public ActualStock actualStock { get; set; }
        public PlannedStock plannedStock { get; set; }
        public List<object> platformFeatures { get; set; }
        public List<object> coachCrowdForecast { get; set; }
    }

    public class Stop2
    {
        public string name { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string countryCode { get; set; }
        public string uicCode { get; set; }
    }

    public class TrainPart
    {
        public string stockIdentifier { get; set; }
        public List<string> facilities { get; set; }
        public Image image { get; set; }
    }



    public class Journey : MonoBehaviour
    {
        public Root root;

        public Root rootU;

        public Trips trips;

        public string type;

        public async Task JourneyRequest(int journeyNum)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            #region
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "deb7cfed900c43cca32d0efe53d35d5e");
            #endregion

            // Request parameters
            queryString["train"] = journeyNum.ToString();
            //queryString["id"] = "{string}";
            //queryString["dateTime"] = "{string}";
            //queryString["departureUicCode"] = "{string}";
            //queryString["transferUicCode"] = "{string}";
            //queryString["arrivalUicCode"] = "{string}";
            var url = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/journey?" + queryString;

            var response = await client.GetAsync(url);

            Debug.Log(response);

            var data = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

            root = data;

            Debug.Log(root);

            SetType();

            await trips.GetTrips("UT", "PT");
        }

        public void SetType()
        {
            type = root.payload.stops[0].actualStock.trainType;
        }

        public async Task JourneyRequestUp(int journeyNum)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            #region
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "deb7cfed900c43cca32d0efe53d35d5e");
            #endregion

            // Request parameters
            queryString["train"] = journeyNum.ToString();
            //queryString["id"] = "{string}";
            //queryString["dateTime"] = "{string}";
            //queryString["departureUicCode"] = "{string}";
            //queryString["transferUicCode"] = "{string}";
            //queryString["arrivalUicCode"] = "{string}";
            var url = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/journey?" + queryString;

            var response = await client.GetAsync(url);

            Debug.Log(response);

            var data = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

            rootU = data;

            //SetType();

            //await trips.GetTrips("UT", "PT");
        }

        public string[] upcomingType;

        public async Task TrainType(int journey, int length)
        {
            //for (int i = 0; i < length; i++)
            //{
            //}
                await JourneyRequestUp(journey);
                upcomingType[length] = rootU.payload.stops[0].actualStock.trainType;
        }
    }
}
