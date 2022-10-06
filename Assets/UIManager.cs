using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Treinchat.Models;
using Treinchat.Arrivals;

public class UIManager : MonoBehaviour
{
    public Models models;
    public Arrivals arrivals;

    public TextMeshProUGUI cur;
    public TextMeshProUGUI curL;
    public TextMeshProUGUI next;
    public TextMeshProUGUI nextL;
    public TextMeshProUGUI final;
    public TextMeshProUGUI finalL;
    public TextMeshProUGUI train;
    public TextMeshProUGUI trainNum;

    [Header("Connecting")]
    public TextMeshProUGUI connect;
    public string notConnected;
    public string connected;

    private void Update()
    {
        cur.text = models.currentStation;
        curL.text = models.currentStationLong;
        next.text = models.nextStation;
        nextL.text = models.nextStationLong;
        final.text = models.finalStation;
        finalL.text = models.finalStationLong;
        train.text = models.trainType;

        if (arrivals.trainNum == 0)
        {
            trainNum.text = "Ritnummer nog niet gevonden. Een moment AUB";
        }
        else
        {
            trainNum.text = arrivals.trainNum.ToString();
        }

        if (models.connecti)
        {
            connect.text = connected;
        }
        else
        {
            connect.text = notConnected;
        }
    }
}

