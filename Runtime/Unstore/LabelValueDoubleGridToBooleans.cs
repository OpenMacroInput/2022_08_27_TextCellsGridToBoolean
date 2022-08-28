using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IGridToBooleans;

public class LabelValueDoubleGridToBooleans 
{
    public static void FetchBooleansFromCSVWebLink(in string urlLabel, in string urlValue, in NamedBooleanFound listener)
    {

        CellsGrid2DUtility.FetchGridsFromCSVWebLink(in urlLabel,in urlValue, out CellsGrid2D labelGrid, out CellsGrid2D valueGrid);
        FetchBooleansFromGrids(in labelGrid, in valueGrid, in listener);
    }
    public static void FetchBooleansFromCSV(in string labelGridCsv, in string valueGridCsv, in NamedBooleanFound listener)
    {
        CellsGrid2DUtility.FetchGridsFromCSV(in labelGridCsv, in valueGridCsv, out CellsGrid2D labelGrid, out CellsGrid2D valueGrid);
        FetchBooleansFromGrids(in labelGrid, in valueGrid, in listener);
    }
    public static void FetchBooleansFromGrids(in CellsGrid2D labelGrid,in CellsGrid2D valueGrid, in NamedBooleanFound listener)
    {
        if (listener != null) { 
            for (int i = 0; i < labelGrid.GetMax1DSize(); i++)
            {
                int line = i / labelGrid.GetColumnSize();
                int column = i % labelGrid.GetColumnSize();
                if (valueGrid.IsValideIndex2D(in column,in line))
                {
                    labelGrid.GetText(in column,in  line, out string label);
                    if (string.IsNullOrEmpty(label))
                        continue;
                    valueGrid.GetText(in column, in line, out string value);
                    value = value.Trim().ToLower();
                    bool isTrue = CellsGrid2DUtility.IsTrue(in value);
                    listener(label, isTrue);
                }
            }
        }
    }
}


