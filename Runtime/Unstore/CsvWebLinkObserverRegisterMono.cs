using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CsvWebLinkObserverRegisterMono : MonoBehaviour
{
    public CsvWebLinkObserverRegister m_observer;
}

[System.Serializable]
public class CsvWebLinkObserverRegister
{
    public List<PathBoolCsvToObserveWithRefreshTime> m_csvPathCyclic = new List<PathBoolCsvToObserveWithRefreshTime>();
    public List<DoubleGridBoolCsvPathToObserveWithRefreshTime> m_csvLabelValuePathCyclic = new List<DoubleGridBoolCsvPathToObserveWithRefreshTime>();
    public List<PathBoolCsvToObserve> m_csvPathOnRequeted = new List<PathBoolCsvToObserve>();
    public List<DoubleGridBoolCsvPathToObserve> m_csvLabelValuePathOnRequeted = new List<DoubleGridBoolCsvPathToObserve>();
}


public enum CsvToBooleanType { DoubleColumn, PairOddColumn, BLBVSubGrid}



[System.Serializable]
public class PathBoolCsvToObserveWithRefreshTime : PathBoolCsvToObserve
{
    public float m_timeBetweenRefreshInSeconds;

    public PathBoolCsvToObserveWithRefreshTime(float timeBetweenRefreshInSeconds , string urlToObserve) :base(urlToObserve)
    {
        m_timeBetweenRefreshInSeconds = timeBetweenRefreshInSeconds;
    }
}

[System.Serializable]
public class PathBoolCsvToObserve
{
    public CsvToBooleanType m_booleanType;
    public string m_csvPathToObserve;
    public PathBoolCsvToObserve( string urlToObserve)
    {
        m_csvPathToObserve = urlToObserve;
    }
}

[System.Serializable]
public class DoubleGridBoolCsvPathToObserve
{
    public string m_csvPathToObserveLabel;
    public string m_csvPathToObserveValue;
    public DoubleGridBoolCsvPathToObserve(string urlToObserveLabel,  string urlToObserveValue)
    {
        m_csvPathToObserveLabel = urlToObserveLabel;
        m_csvPathToObserveValue = urlToObserveValue;
    }
}


[System.Serializable]
public class DoubleGridBoolCsvPathToObserveWithRefreshTime : DoubleGridBoolCsvPathToObserve
{
    public float m_timeBetweenRefreshLabelInSeconds;
    public float m_timeBetweenRefreshValueInSeconds;

    public DoubleGridBoolCsvPathToObserveWithRefreshTime(float timeBetweenRefreshLabelInSeconds, float timeBetweenRefreshValueInSeconds,
        string urlToObserveLabel, string urlToObserveValue) 
        : base(urlToObserveLabel,urlToObserveValue)
    {
        m_timeBetweenRefreshLabelInSeconds = timeBetweenRefreshLabelInSeconds;
        m_timeBetweenRefreshValueInSeconds = timeBetweenRefreshValueInSeconds;
    }
}