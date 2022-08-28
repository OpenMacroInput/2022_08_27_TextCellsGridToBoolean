using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IGridToBooleans;

public class GridBLBVSubGridToBooleans 
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
    public static void FetchBooleansFromGrid(in CellsGrid2D labelGrid,  in NamedBooleanFound listener)
    {
        if (listener == null)
            return;
        List<int> subGridTopLeftCorner= new List<int>();
        for (int i = 0; i < labelGrid.GetMax1DSize(); i++)
        {
            labelGrid.GetCellCount(in i, out int count);
            if (count <2) {
            }
            else if (count == 2)
            {
                labelGrid.GetText(in i, out string t);
                if (t.Length == 2 && (t[0] == 'B' || t[0] == 'b') && (t[1] == 'L' || t[1] == 'l'))
                {
                    subGridTopLeftCorner.Add(i);
                }
            }
            else if (count>7)
            {
                labelGrid.GetText(in i, out string t);
                t = t.Trim().ToLower();
                if (t == "boolean label")
                {
                    subGridTopLeftCorner.Add(i);
                }
            }
        }

        for (int i = 0; i < subGridTopLeftCorner.Count; i++)
        {
            int index = subGridTopLeftCorner[i];
            int line = (index / labelGrid.GetColumnSize() )+1;
            int column = index % labelGrid.GetColumnSize();
            while (labelGrid.IsValideIndex2D(in column, in line) && labelGrid.IsValideIndex2D(column+1, in line) )
            {
                labelGrid.GetCellCount(in column, in line, out int count);
                if (count <= 0) break;
                listener(labelGrid.GetText(in column, in line), CellsGrid2DUtility.IsTrue( labelGrid.GetText(column + 1, in line)));
           
                line++;
            }
        }


        //if (listener != null)
        //{
        //    int labelColumn = 0; int valueColumn = 1;
        //    for (int iLine = 0; iLine < labelGrid.GetRawSize(); iLine++)
        //    {
        //        bool isValideLine = labelGrid.IsValideIndex2D(in labelColumn, in iLine) &&
        //          labelGrid.IsValideIndex2D(in valueColumn, in iLine) ;
        //        if (isValideLine) {
        //            string label = labelGrid.GetText(in labelColumn, in iLine);
        //            if (string.IsNullOrEmpty(label))
        //                continue;
        //            labelGrid.GetText(in labelColumn, in iLine, out string value);
        //            value = value.Trim().ToLower();
        //            bool isTrue = value == "1" || value == "v" || value == "true";
        //            listener(label, isTrue);
        //        }
        //    }
        //}
    }

   
}

