using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;

namespace yixiaozi.WinForm.Control
{
    public class SortByTimeListBox : ListBox
    {
        public SortByTimeListBox() : base()
        {
        }

        // Overrides the parent class Sort to perform a simple
        // bubble sort on the length of the string contained in each item.
        protected override void Sort()
        {
            if (Items.Count > 1)
            {
                bool swapped;
                do
                {
                    int counter = Items.Count - 1;
                    swapped = false;

                    while (counter > 0)
                    {
                        // Compare the items' length. 
                        if (((MyListBoxItemRemind)Items[counter]).Time
                            < ((MyListBoxItemRemind)Items[counter - 1]).Time)
                        {
                            // Swap the items.
                            object temp = Items[counter];
                            Items[counter] = Items[counter - 1];
                            Items[counter - 1] = temp;
                            swapped = true;
                        }
                        // Decrement the counter.
                        counter -= 1;
                    }
                }
                while ((swapped == true));
            }
        }
    }
}
