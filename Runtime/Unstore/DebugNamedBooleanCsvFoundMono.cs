using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IGridToBooleans;

public class DebugNamedBooleanCsvFoundMono : MonoBehaviour
{
    public List<NamedBoolean> m_collected;
    public int m_maxValue = 20;

    public void PushIn(NamedBoolean toPush) { m_collected.Insert(0, toPush);
        if (m_collected.Count > m_maxValue)
            m_collected.RemoveAt(m_collected.Count - 1);
    }
    public void PushIn(string  name, bool value) { PushIn(new NamedBoolean(name, value)); }
}
