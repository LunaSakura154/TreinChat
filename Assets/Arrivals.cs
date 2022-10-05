using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Treinchat.Arrivals
{
    public class Arrival
    {
        public string origin { get; set; }
        public string name { get; set; }
        public DateTime plannedDateTime { get; set; }
        public int plannedTimeZoneOffset { get; set; }
        public DateTime actualDateTime { get; set; }
        public int actualTimeZoneOffset { get; set; }
        public string plannedTrack { get; set; }
        public Product product { get; set; }
        public string trainCategory { get; set; }
        public bool cancelled { get; set; }
        public List<Message> messages { get; set; }
        public string arrivalStatus { get; set; }
    }

    public class Disruptions
    {
        public string uri { get; set; }
    }

    public class Links
    {
        public Disruptions disruptions { get; set; }
    }

    public class Message
    {
        public string message { get; set; }
        public string style { get; set; }
    }

    public class Meta
    {
        public int numberOfDisruptions { get; set; }
    }

    public class Payload
    {
        public string source { get; set; }
        public List<Arrival> arrivals { get; set; }
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
        public Links links { get; set; }
        public Meta meta { get; set; }
    }


    public class Arrivals : MonoBehaviour
    {
        public Treinchat.Models.Models models;
        public Root root;
        public string arrivalStation;
        public DateTime arrivalTime;
        public async void CheckArrivalAsync(string code)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "deb7cfed900c43cca32d0efe53d35d5e");

            // Request parameters
            //queryString["lang"] = "{string}";
            queryString["station"] = code;
            //queryString["uicCode"] = "{string}";
            //queryString["dateTime"] = "{string}";
            //queryString["maxJourneys"] = "{integer}";
            var uri = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?" + queryString;

            var response = await client.GetAsync(uri);

            Debug.Log(uri);
            Debug.Log(response);

            arrivalStation = code;

            var data = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
            root = data;

            Debug.Log(root.payload.arrivals[0].origin);

            SetArrival();
        }

        public int trainNum;

        public void SetArrival()
        {
            for (int i = 0; i < root.payload.arrivals.Count; i++)
            {
                var planTime = root.payload.arrivals[i].plannedDateTime;
                string arrivTime = models.root.trip.stops.Last().arrivalDateTime;

                var parsed = DateTime.Parse(arrivTime);
                Debug.Log(parsed);
                //Debug.Log(planTime);
                //if (root.payload.arrivals[i].plannedDateTime == models.root.trip.stops.Last().arrivalDateTime.)
                //{

                //}

                if (planTime == parsed)
                {
                    trainNum = int.Parse(root.payload.arrivals[i].product.number);
                }

                //trainNum = int.Parse(root.payload.arrivals[0].product.number);
            }
        }
    }


}
