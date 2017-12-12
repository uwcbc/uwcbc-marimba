using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba.Comparers
{
    class CompareListItemsClass : IComparer<ListViewItem>
    {
        private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
        private SortOrder OrderOfSort;
        private int ColumnToSort;

        public CompareListItemsClass(int columnIndex, SortOrder sortOrder)
        {
            ColumnToSort = columnIndex;
            OrderOfSort = sortOrder;
        }

        public int Compare(ListViewItem listviewX, ListViewItem listviewY)
        {
            int compareResult;
            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }
}
