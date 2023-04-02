using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Treinchat;
using Treinchat.Models;
using Treinchat.Arrivals;
using Treinchat.Journey;
using Treinchat.Tripss;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public Manager manager;
    public Models models;
    public Arrivals arrivals;
    public Journey journey;
    public Trips trips;

    public TextMeshProUGUI cur;
    public TextMeshProUGUI curL;
    public TextMeshProUGUI next;
    public TextMeshProUGUI nextL;
    public TextMeshProUGUI final;
    public TextMeshProUGUI finalL;
    public TextMeshProUGUI train;
    public TextMeshProUGUI trainNum;
    public TextMeshProUGUI trainType;

    public TextMeshProUGUI[] upcoming;

    [Header("Connecting")]
    public TextMeshProUGUI connect;
    public string notConnected;
    public string connected;

    [Header("Ritnummer")]
    public string notFound;
    public string searching;

    [Space]
    public bool requested;

    private void Start()
    {
        RequestUpcoming();
    }

    private async void Update()
    {
        cur.text = models.currentStation;
        curL.text = models.currentStationLong;
        next.text = models.nextStation;
        nextL.text = models.nextStationLong;
        final.text = models.finalStation;
        finalL.text = models.finalStationLong;
        train.text = models.trainType;
        trainType.text = journey.type;

        if (trips.identifiers.Count >= 2 && requested == false)
        {
            //for (int i = 0; i < upcoming.Length; i++)
            //{
            //    requested = true;
            //    Trips tripsss = new Trips();
            //    upcoming[i].text = $"{trips.identifiers[i]} {journey.TrainType(trips.identifiers[i])}";
            //}
            //requested = true;
            //upcoming[0].text = $"{trips.identifiers[0]} {journey.TrainType(trips.identifiers[0])}";
            //upcoming[1].text = $"{trips.identifiers[1]} {journey.TrainType(trips.identifiers[1])}";
            //upcoming[2].text = $"{trips.identifiers[2]} {journey.TrainType(trips.identifiers[2])}";
            await UpcomingTypes();

        }
        if (!manager.searchingNumber)
        {
            if (arrivals.trainNum == 0)
            {
                trainNum.text = notFound;
            }
            else
            {
                trainNum.text = arrivals.trainNum.ToString();
            }
        }
        else
        {
            trainNum.text = searching;
        }

        if (models.connecti)
        {
            connect.text = connected;
        }
        else
        {
            connect.text = notConnected;
        }

        if (models.currentStation == "n/a")
        {
            curL.text = "";
        }
    }

    public async Task UpcomingTypes()
    {
        requested = true;
        for (int i = 0; i < upcoming.Length; i++)
        {
            await journey.TrainType(trips.identifiers[i],i);
            upcoming[i].text = $"{trips.root.trips[i].legs[0].origin.plannedDateTime.ToString("HH:mm")} {trips.identifiers[i]} {journey.upcomingType[i]}";
        }

    }

    public async void RequestUpcoming()
    {
        await trips.GetTrips("UT", "PT");

        requested = false;
        await UpcomingTypes();

    }
}

