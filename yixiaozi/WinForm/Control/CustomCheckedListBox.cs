///////////////////////////////////////////////////////////////////////////
//
// Copyright 2015 Qodex Software.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF, OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
///////////////////////////////////////////////////////////////////////////
//
//        FILE: CustomCheckedListBox.cs
//
//      AUTHOR: Tim Bomgardner
//
// DESCRIPTION: A CheckedListBox that can set item font, text color, and
//				background color.  Properties and events will appear in
//				Visual Studio.
//
///////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
namespace yixiaozi.WinForm.Control
{
	public class CustomCheckedListBox : CheckedListBox
	{
		public delegate Color GetColorDelegate(CustomCheckedListBox listbox, DrawItemEventArgs e);
		public delegate Font GetFontDelegate(CustomCheckedListBox listbox, DrawItemEventArgs e);

        [Description("Supply a foreground color for each item")]
        public event GetColorDelegate GetForeColor = null;
        [Description("Supply a background color for each item")]
        public event GetColorDelegate GetBackColor = null;
        [Description("Supply a font for each item")]
        public event GetFontDelegate GetFont = null;

        [Description("Set this if you don't like the standard selection appearance")]
        public bool DrawFocusedIndicator { get; set; }

        public override int ItemHeight { get; set; }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Parameterless ctor for Visual Studio.
		/// </summary>
		public CustomCheckedListBox() : this (null, null, null) { }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Standard ctor for everyone else.
		/// </summary>
		/// <param name="back">Delegate to provide a background color</param>
		/// <param name="fore">Delegate to provide a foreground color</param>
		/// <param name="font">Delegate to provide a font</param>
		public CustomCheckedListBox(GetColorDelegate back = null, GetColorDelegate fore = null, GetFontDelegate font = null)
		{
			GetForeColor = fore;
			GetBackColor = back;
			GetFont = font;

            //******************************************************************
            // If you want to set the item height to a specific value that can
            // be independent of the font size, this is the place to do it.
            //******************************************************************
            ItemHeight = 14;
        }

		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Override the CheckedListBox method to allow changes to the
		/// foreground, background, and font.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
            //Color foreColor = (GetForeColor != null) ? GetForeColor(this, e) : e.ForeColor;
            //Color backColor = (GetBackColor != null) ? GetBackColor(this, e) : e.BackColor;

            ////
            //// The CheckListBox is going to ignore the font in the event
            //// args and use its own.  So, make its own the one we want it
            //// to be.  For this to always work right, we will have to shut
            //// down the OnFontChanged method below.
            ////
            //if (GetFont != null)
            //{
            //	this.Font = GetFont(this, e);
            //}

            ////
            //// If desired, draw an item focused, but not selected.
            ////
            //DrawItemState state = (DrawFocusedIndicator)
            //	? ((e.State & DrawItemState.Focus) == DrawItemState.Focus )
            //		? DrawItemState.Focus 
            //		: DrawItemState.None
            //	: e.State;

            ////
            //// e.Font is going to be ignored.
            ////
            //DrawItemEventArgs e2 = new DrawItemEventArgs(e.Graphics, 
            //	e.Font, e.Bounds, e.Index, state, foreColor, backColor);

            //base.OnDrawItem(e2);
            if (e.Index < 0) return;
            //if the item state is selected them change the back color
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State ^ DrawItemState.Selected,
                                            e.ForeColor,
                                            Color.LightGray);//Yellow
            }
            else
            {
				//this.CheckedIndices.Contains
                if (this.CheckedIndices.IndexOf(e.Index)<0)
                {
                    e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State,
                                            e.ForeColor,
                                            Color.FromArgb(245,245,245));
                }
			}
			// Draw the background of the ListBox control for each item.
			e.DrawBackground();
			// Draw the current item text
			e.Graphics.DrawString(((MyListBoxItem)Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
			// If the ListBox has focus, draw a focus rectangle around the selected item.
			//e.DrawFocusRectangle();

		}
		/// ////////////////////////////////////////////////////////////////////
		/// <summary>
		/// If base.OnFontChanged fires, and we have changed the font size,
		/// that could cause problems.  Fire base.OnFontChanged only if
		/// we have not supplied a font.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnFontChanged(EventArgs e)
		{
			if (GetFont == null) base.OnFontChanged(e);
		}
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
                        if (((MyListBoxItem)Items[counter - 1]).Text.Substring(0,2).CompareTo(((MyListBoxItem)Items[counter]).Text.Substring(0,2)) == -1)
                        {
                            object temp = Items[counter];
                            Items[counter] = Items[counter - 1];
                            Items[counter - 1] = temp;
                            swapped = true;
                        }
                        counter -= 1;
                    }
                    //int counter = 0;
                    //swapped = false;
                    //while (counter < Items.Count - 1)
                    //{
                    //    if (((MyListBoxItem)Items[counter]).Text.CompareTo(((MyListBoxItem)Items[counter + 1]).Text) == -1)
                    //    {
                    //        object temp = Items[counter + 1];
                    //        Items[counter + 1] = Items[counter];
                    //        Items[counter] = temp;
                    //        swapped = true;
                    //    }
                    //    counter++;
                    //}
                }
                while ((swapped == true));
			}
		}
	}
}
