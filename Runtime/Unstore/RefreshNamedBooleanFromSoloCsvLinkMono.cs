using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshNamedBooleanFromSoloCsvLinkMono : MonoBehaviour
{
    public CsvWebLinkObserverRegisterMono m_register;
    public PushInCsvLinkToReimportMono m_csvLinkImporter;
    public void Awake()
    {
        m_current = DateTime.Now;
        m_previous = DateTime.Now;
    }
    public DateTime m_previous;
    public DateTime m_current;
    public double m_deltaTime;
    private void Update()
    {
        RemoveTime();
    }
    public void RemoveTime() {

        RefreshCounters();

        m_current = DateTime.Now;
         m_deltaTime = (m_current - m_previous).TotalSeconds;
        if (m_deltaTime > 0f) { 
            for (int i = 0; i < m_refreshCsv.Count; i++)
            {
                m_refreshCsv[i].m_timeLeft -= m_deltaTime;
                if (m_refreshCsv[i].m_timeLeft < 0f)
                {
                    m_refreshCsv[i].m_timeLeft = m_refreshCsv[i].m_timeReset;
                    if(m_refreshCsv[i].m_onZeroAction!=null)
                        m_refreshCsv[i].m_onZeroAction();
                }
            }

            m_previous = m_current;
        }
    }

    private void RefreshCounters()
    {
        foreach (PathBoolCsvToObserveWithRefreshTime item in m_register.m_observer.m_csvPathCyclic)
        {

            if (!m_dictionary.ContainsKey(item.m_csvPathToObserve)) {
                IdTimer t =(new IdTimer()
                {
                    m_timeReset = item.m_timeBetweenRefreshInSeconds
                ,
                    m_timeLeft = item.m_timeBetweenRefreshInSeconds
                ,
                    id = item.m_csvPathToObserve
                ,
                    m_onZeroAction = () => { m_csvLinkImporter.PushIn(item); }
                }) ;

                m_refreshCsv.Add(t);
                m_dictionary.Add(item.m_csvPathToObserve, t);
            }
        }   
    }
    public Dictionary<string, IdTimer> m_dictionary = new Dictionary<string, IdTimer>();
    public List<IdTimer> m_refreshCsv = new List<IdTimer>();

    [System.Serializable]
    public class IdTimer {
        public string id;
        public double m_timeLeft;
        public double m_timeReset =10;
        public Action m_onZeroAction;
    }
}
