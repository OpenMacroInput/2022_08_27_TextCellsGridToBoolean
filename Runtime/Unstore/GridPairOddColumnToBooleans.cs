using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IGridToBooleans;

public class GridPairOddColumnToBooleans : MonoBehaviour
{
    public static void FetchBooleansFromCSVWebLink(in string linkUrl, in NamedBooleanFound listener)
    {
        CellsGrid2DUtility.FetchGridFromCSVWebLink(in linkUrl, out CellsGrid2D grid);
        FetchBooleansFromGrid(in grid, in listener);
    }

    public static void FetchBooleansFromCSV(in string csvText, in NamedBooleanFound listener)
    {
        CellsGrid2DUtility.FetchGridFromCSV(in csvText, out CellsGrid2D grid);
        FetchBooleansFromGrid(in grid, in listener);
    }
    public static void FetchBooleansFromGrid(in CellsGrid2D labelGrid, in NamedBooleanFound listener)
    {
        if (listener != null)
        {
            for (int column = 0; column < labelGrid.GetColumnSize(); column+=2)
            {
                int labelColumn = column; int valueColumn = column+1;
                for (int iLine = 0; iLine < labelGrid.GetRawSize(); iLine++)
                {
                    bool isValideLine = labelGrid.IsValideIndex2D(in labelColumn, in iLine) &&
                      labelGrid.IsValideIndex2D(in valueColumn, in iLine);
                    if (isValideLine)
                    {
                        string label = labelGrid.GetText(in labelColumn, in iLine);
                        if (string.IsNullOrEmpty(label))
                            continue;
                        labelGrid.GetText(in valueColumn, in iLine, out string value);
                        listener(label, CellsGrid2DUtility.IsTrue(in value));
                    }
                }

            }
            
        }
    }
}
