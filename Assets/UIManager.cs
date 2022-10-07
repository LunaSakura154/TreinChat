using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Treinchat;
using Treinchat.Models;
using Treinchat.Arrivals;

public class UIManager : MonoBehaviour
{
    public Manager manager;
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

    [Header("Ritnummer")]
    public string notFound;
    public string searching;

    private void Update()
    {
        cur.text = models.currentStation;
        curL.text = models.currentStationLong;
        next.text = models.nextStation;
        nextL.text = models.nextStationLong;
        final.text = models.finalStation;
        finalL.text = models.finalStationLong;
        train.text = models.trainType;
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
    }
}

