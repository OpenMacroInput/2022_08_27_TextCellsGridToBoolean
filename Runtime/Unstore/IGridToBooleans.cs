using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGridToBooleans
{
    public void FetchBooleanInRef(in CellsGrid2D grid, ref List<NamedBoolean> booleans);
    public void FetchBooleanOut(in CellsGrid2D grid, out List<NamedBoolean> booleans);
    public void FetchBoolean(in CellsGrid2D grid, in NamedBooleanFound listener);

    [System.Serializable]
    public struct NamedBoolean {
        public string m_booleanName;
        public bool m_booleanValue;

        public NamedBoolean(string booleanName, bool booleanValue)
        {
            m_booleanName = booleanName;
            m_booleanValue = booleanValue;
        }
    }
    public delegate void NamedBooleanFound(string booleanName, bool booleanValue);
    [System.Serializable]
    public class NamedBooleanFoundUnityEvent: UnityEvent<string, bool>{};
}

[System.Serializable]
public class GridToBooleansReturn {
    public IGridToBooleans m_converter;
    public CellsGrid2D m_gridGiven;
    public List<IGridToBooleans.NamedBoolean> m_booleans;
}



public abstract class AGridToBooleans : IGridToBooleans
{
    public abstract void FetchBoolean(in CellsGrid2D grid, in IGridToBooleans.NamedBooleanFound listener);
    public void FetchBooleanInRef(in CellsGrid2D grid, ref List<IGridToBooleans.NamedBoolean> booleans)
    {
        if (booleans == null)
            booleans = new List<IGridToBooleans.NamedBoolean>();
        booleans.Clear();
        List<IGridToBooleans.NamedBoolean> list = booleans;
        FetchBoolean(in grid, (l, v) => { list.Add(new IGridToBooleans.NamedBoolean() { m_booleanName = l, m_booleanValue = v }); });
    }

    public void FetchBooleanOut(in CellsGrid2D grid, out List<IGridToBooleans.NamedBoolean> booleans)
    {
        booleans = new List<IGridToBooleans.NamedBoolean>();
        FetchBooleanInRef(in grid, ref booleans);
    }
}
