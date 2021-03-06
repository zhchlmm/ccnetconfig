/* Copyright (c) 2006, Ryan Conrad. All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 * - Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * 
 * - Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the 
 *    documentation and/or other materials provided with the distribution.
 * 
 * - Neither the name of the Camalot Designs nor the names of its contributors may be used to endorse or promote products derived from this software 
 *    without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
 * GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
 * DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;

namespace CCNetConfig.Core.Components {

  /// <summary>
  /// A dropdown UITypeEditor that allows the selection of multiple Days of the Week.
  /// </summary>
  public class DayOfWeekUIEditor : UITypeEditor {
    IWindowsFormsEditorService frmsvr = null;
    /// <summary>
    /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle"></see> method.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that can be used to gain additional context information.</param>
    /// <param name="provider">An <see cref="T:System.IServiceProvider"></see> that this editor can use to obtain services.</param>
    /// <param name="value">The object to edit.</param>
    /// <returns>
    /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
    /// </returns>
    public override object EditValue ( ITypeDescriptorContext context, IServiceProvider provider, object value ) {
      frmsvr = (IWindowsFormsEditorService)provider.GetService ( typeof ( IWindowsFormsEditorService ) );
      if ( frmsvr != null ) {
        // create the listbox
        CheckedListBox lst = new CheckedListBox ();
        // hide the border
        lst.BorderStyle = BorderStyle.None;
        // attach some events
        lst.ItemCheck += new ItemCheckEventHandler ( OnListBoxItemCheck );
        // set so when item clicked, it checks/unchecks
        lst.CheckOnClick = true;

        CloneableList<DayOfWeek> objValue = (CloneableList<DayOfWeek>)value;
        // add a "Default" item
        lst.Items.Add("(Default)",false);
        // here we add all the DayOfWeek items to the listbox
        // checking if any are already in the value that we have, if so, set checked to true.
        foreach ( DayOfWeek dow in Enum.GetValues ( typeof ( DayOfWeek ) ) )
          lst.Items.Add ( dow, objValue != null ? objValue.IndexOf ( dow ) > -1 : false );

        // create our output variable
        CloneableList<DayOfWeek> values = new CloneableList<DayOfWeek> ();
        // show the dropdown
        frmsvr.DropDownControl ( lst );

        // get the items that are checked
        foreach ( object o in lst.CheckedItems ) {
          // if it is a DayOfWeek and not the String we added then add it to the out variable
          if ( o.GetType () == typeof (DayOfWeek) )
            values.Add ((DayOfWeek)o);
        }

        if ( lst.CheckedItems.Count == 0 || lst.GetItemChecked ( 0 ) )
          return null;
        else
          return values;
      }
      return value;
    }

    /// <summary>
    /// Called when [list box item check].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.Windows.Forms.ItemCheckEventArgs"/> instance containing the event data.</param>
    void OnListBoxItemCheck( object sender, ItemCheckEventArgs e ) {
      CheckedListBox clst = (CheckedListBox)sender;
      // if default is checked, uncheck the rest
      if ( clst.Items[ e.Index ].GetType () == typeof ( String ) && e.NewValue == CheckState.Checked ) {
        for ( int x = e.Index + 1; x < clst.Items.Count; x++ )
          clst.SetItemCheckState ( x, CheckState.Unchecked );
        frmsvr.CloseDropDown ();
      }

    }

    /// <summary>
    /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"></see> method.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that can be used to gain additional context information.</param>
    /// <returns>
    /// A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle"></see> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"></see> method. If the <see cref="T:System.Drawing.Design.UITypeEditor"></see> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle"></see> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None"></see>.
    /// </returns>
    public override UITypeEditorEditStyle GetEditStyle ( ITypeDescriptorContext context ) {
      return UITypeEditorEditStyle.DropDown;
    }
  }

  /// <summary>
  /// Converts a List&lt;DayOfWeek&gt; to comma separated string of days
  /// </summary>
  public class DayOfWeekListTypeConverter : TypeConverter {

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type, using the specified context.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="T:System.Type"></see> that represents the type you want to convert to.</param>
    /// <returns>
    /// true if this converter can perform the conversion; otherwise, false.
    /// </returns>
    public override bool CanConvertTo ( ITypeDescriptorContext context, Type destinationType ) {
      if ( destinationType == typeof ( String ) )
        return true;
      else if ( destinationType == typeof ( List<> ) )
        return true;
      else
        return false;
    }

    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo"></see>. If null is passed, the current culture is assumed.</param>
    /// <param name="value">The <see cref="T:System.Object"></see> to convert.</param>
    /// <param name="destinationType">The <see cref="T:System.Type"></see> to convert the value parameter to.</param>
    /// <returns>
    /// An <see cref="T:System.Object"></see> that represents the converted value.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
    /// <exception cref="T:System.ArgumentNullException">The destinationType parameter is null. </exception>
    public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType ) {
      if ( destinationType == typeof ( string ) ) {
        StringBuilder str = new StringBuilder ();
        CloneableList<DayOfWeek> lst = (CloneableList<DayOfWeek>)context.PropertyDescriptor.GetValue ( context.Instance );
        if ( lst == null || lst.Count == 0 )
          return "(Default)";
        else {
          foreach ( DayOfWeek item in lst )
            str.Append ( string.Format ( "{0},", item.ToString () ) );
          if ( str.Length > 0 )
            str.Remove ( str.Length - 1, 1 );
          return str.ToString ();
        }
      } else
        throw new NotSupportedException ( string.Format ( "Unsupported type: {0}", destinationType.ToString () ) );
    }
  }
}
