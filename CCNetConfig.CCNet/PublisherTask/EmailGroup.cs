﻿/*
 * Copyright (c) 2006-2008, Ryan Conrad. All rights reserved.
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
using System.Xml;
using CCNetConfig.Core;
using CCNetConfig.Core.Serialization;
using System.ComponentModel;
using CCNetConfig.Core.Enums;

namespace CCNetConfig.CCNet {
  /// <summary>
  /// Identifies which the notification policy for a set of <see cref="CCNetConfig.CCNet.User">User</see>s.
  /// </summary>
  public class Group : ISerialize, ICCNetObject, ICCNetDocumentation, ICloneable {
    private string _name = string.Empty;
    private NotificationType _notification = NotificationType.Always;

    /// <summary>
    /// The name of the group, which corresponds to the 'group' values used in the <see cref="CCNetConfig.CCNet.User">User</see>.
    /// </summary>
    [Description ( "The name of the group, which corresponds to the 'group' values used in the User." ), DefaultValue ( null ),
    DisplayName ( "(Name)" ), Category ( "Required" )]
    public string Name { get { return this._name; } set { this._name = Util.CheckRequired ( this, "name", value ); } }
    /// <summary>
    /// Determines when to send email to this group.
    /// </summary>
    [Description ( "Determines when to send email to this group." ), DefaultValue ( null ), DisplayName ( "(Notification)" ), Category ( "Required" )]
    public NotificationType Notification { get { return this._notification; } set { this._notification = Util.CheckRequired ( this, "notification", value ); } }
    #region ISerialize Members

    /// <summary>
    /// Serializes this instance.
    /// </summary>
    /// <returns></returns>
    public System.Xml.XmlElement Serialize ( ) {
      XmlDocument doc = new XmlDocument ( );
      XmlElement root = doc.CreateElement ( "group" );
      root.SetAttribute ( "name", Util.CheckRequired ( this, "name", this.Name ) );
      root.SetAttribute ( "notification", Util.CheckRequired ( this, "notification", this.Notification.ToString ( ) ) );
      return root;
    }

    /// <summary>
    /// Deserializes the specified element.
    /// </summary>
    /// <param name="element">The element.</param>
    public void Deserialize ( XmlElement element ) {
      this._name = string.Empty;
      this._notification = NotificationType.Always;
      if ( string.Compare ( element.Name, "group", false ) != 0 )
        throw new InvalidCastException ( string.Format ( "Unable to convert {0} to a {1}", element.Name, "group" ) );

      this.Name = Util.GetElementOrAttributeValue ( "name", element );
      this.Notification = ( Core.Enums.NotificationType ) Enum.Parse ( typeof ( Core.Enums.NotificationType ), Util.GetElementOrAttributeValue ( "notification", element ), true );
    }

    #endregion

    /// <summary>
    /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
    /// </returns>
    public override string ToString ( ) {
      return string.IsNullOrEmpty ( this.Name ) ? "New Group" : this.Name;
    }

    #region ICCNetDocumentation Members
    /// <summary>
    /// Gets the documentation URI.
    /// </summary>
    /// <value>The documentation URI.</value>
    [Browsable ( false )]
    public Uri DocumentationUri {
      get { return new Uri ( "http://ccnet.thoughtworks.net/display/CCNET/Email+Publisher?decorator=printable" ); }
    }

    #endregion


    #region ICloneable Members
    /// <summary>
    /// Creates a copy of this object.
    /// </summary>
    /// <returns></returns>
    public Group Clone ( ) {
      return this.MemberwiseClone ( ) as Group;
    }
    object ICloneable.Clone ( ) {
      return this.Clone ( );
    }

    #endregion
  }
}
