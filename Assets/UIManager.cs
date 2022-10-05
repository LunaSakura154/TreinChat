using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Treinchat.Models;

public class UIManager : MonoBehaviour
{
    public Models models;

    public TextMeshProUGUI cur;
    public TextMeshProUGUI next;
    public TextMeshProUGUI final;
    public TextMeshProUGUI finalL;
    public TextMeshProUGUI train;

    private void Update()
    {
        cur.text = models.currentStation;
        next.text = models.nextStation;
        final.text = models.finalStation;
        finalL.text = models.finalStationLong;
        train.text = models.trainType;
    }
}

