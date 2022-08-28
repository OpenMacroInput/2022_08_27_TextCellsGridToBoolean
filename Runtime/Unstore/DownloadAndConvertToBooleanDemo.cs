using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadAndConvertToBooleanDemo : MonoBehaviour
{

    public string m_doubleGrid_labelLink= "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ4AjXf5FCBvtFvK-ar-nALN2c7UsjluhP2TovsBVdqt_UAQgbhzZjonJFiQM3rgn6cuBrj5anuUWzK/pub?gid=1921437720&single=true&output=csv";
    public string m_doubleGrid_valueLink= "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ4AjXf5FCBvtFvK-ar-nALN2c7UsjluhP2TovsBVdqt_UAQgbhzZjonJFiQM3rgn6cuBrj5anuUWzK/pub?gid=1631095918&single=true&output=csv";
    public string m_blbvSubGridLink= "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ4AjXf5FCBvtFvK-ar-nALN2c7UsjluhP2TovsBVdqt_UAQgbhzZjonJFiQM3rgn6cuBrj5anuUWzK/pub?gid=0&single=true&output=csv";
    public string m_doubleColumnGridLink= "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ4AjXf5FCBvtFvK-ar-nALN2c7UsjluhP2TovsBVdqt_UAQgbhzZjonJFiQM3rgn6cuBrj5anuUWzK/pub?gid=786152333&single=true&output=csv";
    public string m_pairOddGridLink= "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ4AjXf5FCBvtFvK-ar-nALN2c7UsjluhP2TovsBVdqt_UAQgbhzZjonJFiQM3rgn6cuBrj5anuUWzK/pub?gid=1519826132&single=true&output=csv";

    [ContextMenu("DownloadAndConvert")]
    private void DownloadAndConvert()
    {
        m_debugText = "Double Grids\n";
        LabelValueDoubleGridToBooleans.FetchBooleansFromCSVWebLink(m_doubleGrid_labelLink, m_doubleGrid_valueLink, AppendDebugEnd);

        m_debugText += ">BLBV\n";
        GridBLBVSubGridToBooleans.FetchBooleansFromCSVWebLink(m_blbvSubGridLink, AppendDebugEnd);

        m_debugText += ">Double Column\n"; 
        GridDoubleColumnToBooleans.FetchBooleansFromCSVWebLink(m_doubleColumnGridLink, AppendDebugEnd);

        m_debugText += ">Pair Odd\n"; 
        GridPairOddColumnToBooleans.FetchBooleansFromCSVWebLink(m_pairOddGridLink, AppendDebugEnd);
    }




    [TextArea(0, 20)]
    public string m_debugText;
    private void AppendDebugStart(string booleanName, bool booleanValue)

    {
        m_debugText = string.Format("{0}-{1}\n", booleanName, booleanValue) + m_debugText;
    }
    private void AppendDebugEnd(string booleanName, bool booleanValue)

    {
        m_debugText += string.Format("{0}-{1}\n", booleanName, booleanValue) ;
    }
}

