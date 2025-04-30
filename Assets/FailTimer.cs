using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;
public class FailTimer : MonoBehaviour
{
    private Timer timerScript;
    private int light1classifier = 1;
    private int light2classifier = 2;
    private int light3classifier = 3;
    private int light4classifier = 4;
    public bool faildetected = false;
    private Dictionary<int, float> checkTimeLeft = new Dictionary<int, float>();
    private Dictionary<int, bool> checkFailTracking = new Dictionary<int, bool>();

    void Start()
    {
        checkTimeLeft.Add(light1classifier, 12f);
        checkFailTracking.Add(light1classifier, false);
        checkTimeLeft.Add(light2classifier, 12f);
        checkFailTracking.Add(light2classifier, false);
        checkTimeLeft.Add(light3classifier, 12f);
        checkFailTracking.Add(light3classifier, false);
        checkTimeLeft.Add(light4classifier, 12f);
        checkFailTracking.Add(light4classifier, false);
    }

    void Update()
    {
        BinaryLightScript1 light1 = FindFirstObjectByType<BinaryLightScript1>();
        BinaryLightScript2 light2 = FindFirstObjectByType<BinaryLightScript2>();
        BinaryLightScript3 light3 = FindFirstObjectByType<BinaryLightScript3>();
        BinaryLightScript4 light4 = FindFirstObjectByType<BinaryLightScript4>();
        timerScript = FindFirstObjectByType<Timer>();
        if (faildetected == false && timerScript.TimeLeft >= 0)
        {
            if (light1.colorRed == true && checkFailTracking[light1classifier] == false)
            {
                checkFailTracking[light1classifier] = true;
            }
            if (light1.colorRed == false && checkFailTracking[light1classifier] == true)
            {
                checkFailTracking[light1classifier] = false;
                checkTimeLeft[light1classifier] = 12f;
            }
            if (light2.colorRed == true && checkFailTracking[light2classifier] == false)
            {
                checkFailTracking[light2classifier] = true;
            }
            if (light2.colorRed == false && checkFailTracking[light2classifier] == true)
            {
                checkFailTracking[light2classifier] = false;
                checkTimeLeft[light2classifier] = 12f;
            }
            if (light3.colorRed == true && checkFailTracking[light3classifier] == false)
            {
                checkFailTracking[light3classifier] = true;
            }
            if (light3.colorRed == false && checkFailTracking[light3classifier] == true)
            {
                checkFailTracking[light3classifier] = false;
                checkTimeLeft[light3classifier] = 12f;
            }
            if (light4.colorRed == true && checkFailTracking[light4classifier] == false)
            {
                checkFailTracking[light4classifier] = true;
            }
            if (light4.colorRed == false && checkFailTracking[light4classifier] == true)
            {
                checkFailTracking[light4classifier] = false;
                checkTimeLeft[light4classifier] = 12f;
            }

            if (checkFailTracking.Count > 0)
            {
                foreach (KeyValuePair<int, bool> entry in checkFailTracking)
                {
                    if (entry.Value == true)
                    {
                        checkTimeLeft[entry.Key] = checkTimeLeft[entry.Key] - Time.deltaTime;
                        print(checkTimeLeft[entry.Key]);
                        if (checkTimeLeft[entry.Key] <= 0f)
                        {
                            faildetected = true;
                        }
                    }
                }
            }
        }
    }

}
