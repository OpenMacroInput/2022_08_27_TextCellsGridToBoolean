using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static IGridToBooleans;

public class PushInCsvLinkToReimportMono : MonoBehaviour
{
    public List<PathBoolCsvToObserve> m_toImport = new List<PathBoolCsvToObserve>();
    public List<DoubleGridBoolCsvPathToObserve> m_toImportDual = new List<DoubleGridBoolCsvPathToObserve>();

    public NamedBooleanFoundUnityEvent m_listenToNamedBooleanFoundEvent;
    public NamedBooleanFound m_listenToNamedBooleanFound;

    public void PushIn(PathBoolCsvToObserve toImport)
    {
        m_toImport.Add(toImport);
    }
    public void PushIn(DoubleGridBoolCsvPathToObserve toImport)
    {
        m_toImportDual.Add(toImport);
    }

    private void PushFound(string booleanName, bool booleanValue)
    {
        m_pushOnUnityThread.Enqueue(new NamedBoolean(booleanName, booleanValue));
    }
    public void Awake()
    {
        StartCoroutine( LoopCoroutineImport());
    }

    private IEnumerator LoopCoroutineImport()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (m_toImport.Count > 0)
            {
                PathBoolCsvToObserve t = m_toImport[0];
                m_toImport.RemoveAt(0);
                yield return Reimport(t, PushFound);
                yield return new WaitForEndOfFrame();
            }
            if (m_toImport.Count > 0)
            {
                DoubleGridBoolCsvPathToObserve t = m_toImportDual[0];
                m_toImport.RemoveAt(0);
                yield return Reimport(t, PushFound);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void Update()
    {
        while (m_pushOnUnityThread.Count > 0)
            Push(m_pushOnUnityThread.Dequeue());
    }

    public void Push(NamedBoolean namedBoolean)
    {
        m_listenToNamedBooleanFoundEvent.Invoke(namedBoolean.m_booleanName, namedBoolean.m_booleanValue);
    }

    private IEnumerator Reimport(PathBoolCsvToObserve item, NamedBooleanFound booleanFound)
    {
        UnityWebRequest w = UnityWebRequest.Get(item.m_csvPathToObserve);
        yield return w.SendWebRequest();
        if (w.result == UnityWebRequest.Result.Success)
        {
            string text = w.downloadHandler.text;
            if (item.m_booleanType == CsvToBooleanType.BLBVSubGrid)
                GridBLBVSubGridToBooleans.FetchBooleansFromCSV(in text, in booleanFound);
            else if (item.m_booleanType == CsvToBooleanType.DoubleColumn)
                GridDoubleColumnToBooleans.FetchBooleansFromCSV(in text, in booleanFound);
            else if (item.m_booleanType == CsvToBooleanType.PairOddColumn)
                GridPairOddColumnToBooleans.FetchBooleansFromCSV(in text, in booleanFound);
        }
        w.Dispose();
    }

    private IEnumerator Reimport(DoubleGridBoolCsvPathToObserve item, NamedBooleanFound booleanFound)
    {
        UnityWebRequest l = UnityWebRequest.Get(item.m_csvPathToObserveLabel);
        yield return l.SendWebRequest();
        UnityWebRequest v = UnityWebRequest.Get(item.m_csvPathToObserveValue);
        yield return v.SendWebRequest();
        if (l.result == UnityWebRequest.Result.Success && v.result == UnityWebRequest.Result.Success)
        {
            LabelValueDoubleGridToBooleans.FetchBooleansFromCSV(
                l.downloadHandler.text,
                v.downloadHandler.text,
                in booleanFound);
        }
        l.Dispose();
        v.Dispose();
    }

    public Queue<NamedBoolean> m_pushOnUnityThread = new Queue<NamedBoolean>();
}
