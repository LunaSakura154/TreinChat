using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;

namespace Treinchat
{
    public class MassRequest : MonoBehaviour
    {
        public TextMeshProUGUI[] upcoming;

        public Journey.Journey journey;

        public Tripss.Trips trips;
        [Tooltip("Ritnummers gebruiken honderdtallen ex: Zwolle - Utrecht Centraal = 5600")]
        public int traject;

        private async void Start()
        {
            await UpcomingTypes();
        }
        public async Task UpcomingTypes()
        {
            for (int i = 0; i < upcoming.Length; i++)
            {
                int j = traject + i;
                //Debug.Log(j);
                //StartCoroutine(Requester(upcoming[i], j));
                try
                {
                    await journey.JourneyRequest(j);
                    upcoming[i].text = $"{journey.root.payload.stops[0].departures[0].plannedTime.ToString("HH:mm")} {j} {journey.root.payload.stops[0].actualStock.trainType}";
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("oopsie");
                    upcoming[i].text = $"{DateTime.Now.ToString("HH:mm")} {j} fail";
                }

            }

        }

        public IEnumerator Requester(TextMeshProUGUI text, int j)
        {
            bool loaded = false;
            text.text = $"{DateTime.Now.ToString("HH:mm")} {j} null";

            while (!loaded)
            {
                journey.JourneyRequest(j);
                if (journey.root.payload.stops[0] != null)
                {
                    loaded = true;
                }
                yield return null;
            }

            text.text = $"{journey.root.payload.stops[0].departures[0].plannedTime.ToString("HH:mm")} {j} {journey.root.payload.stops[0].actualStock.trainType}";
            
            
                //Debug.Log("oopsie");
            
            yield return null;
        }

    }
}
