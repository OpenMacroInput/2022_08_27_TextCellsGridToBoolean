using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static IGridToBooleans;

public class ReimportAllNamedBooleanFromCsvLinkMono : MonoBehaviour
{
    public CsvWebLinkObserverRegisterMono m_register;
    public PushInCsvLinkToReimportMono m_csvLinkImporter;
    [ContextMenu("Reimport All With Coroutine")]
    private void ReimportAll()
    {
        foreach (var item in m_register.m_observer.m_csvPathOnRequeted)
        {
            m_csvLinkImporter.PushIn(item);
        }
        foreach (var item in m_register.m_observer.m_csvLabelValuePathOnRequeted)
        {
            m_csvLinkImporter.PushIn(item);
        }
        foreach (var item in m_register.m_observer.m_csvPathCyclic)
        {
            m_csvLinkImporter.PushIn(item);
        }
        foreach (var item in m_register.m_observer.m_csvLabelValuePathCyclic)
        {
            m_csvLinkImporter.PushIn(item);
        }
    }
    
}
