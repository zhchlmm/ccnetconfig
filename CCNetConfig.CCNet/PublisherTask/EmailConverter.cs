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
using CCNetConfig.Core.Components;
namespace CCNetConfig.CCNet {
  /// <summary>
  /// The Converters configuration specifies rules for transforming user names (from User elements) to email 
  /// addresses in cases where the User element does not specify an address. The converters are 
  /// ignored when the User element specifies an address.
  /// </summary>
  [ReflectorName("regexConverter"), MinimumVersion("1.4")]
  public class Converter : ISerialize, ICCNetObject, ICCNetDocumentation, ICloneable {
    /// <summary>
    /// Gets or sets the find.
    /// </summary>
    /// <value>The find.</value>
    [Description("The pattern to find"), ReflectorName("find"), DefaultValue(null),
    MinimumVersion("1.4"), Required, DisplayName("(Find)"), Category("Required")]
    public string Find { get; set; }
    /// <summary>
    /// Gets or sets the replace.
    /// </summary>
    /// <value>The replace.</value>
    [Description(" The value to substitute with"), ReflectorName("replace"), DefaultValue(null),
    MinimumVersion ( "1.4" ), Required, DisplayName ( "(Replace)" ), Category ( "Required" )]
    public string Replace { get; set; }
    #region ISerialize Members

    /// <summary>
    /// Serializes this instance.
    /// </summary>
    /// <returns></returns>
    public XmlElement Serialize ( ) {
      return new Serializer<Converter>().Serialize(this);
    }

    /// <summary>
    /// Deserializes the specified element.
    /// </summary>
    /// <param name="element">The element.</param>
    public void Deserialize ( XmlElement element ) {
      this.Find = Util.GetElementOrAttributeValue ( "find", element );
      this.Replace = Util.GetElementOrAttributeValue ( "replace", element );
    }

    #endregion

    #region ICCNetDocumentation Members

    /// <summary>
    /// Gets the documentation URI.
    /// </summary>
    /// <value>The documentation URI.</value>
    [Browsable(false),EditorBrowsable(EditorBrowsableState.Never),ReflectorIgnore]
    public Uri DocumentationUri {
      get { return new Uri ( "http://confluence.public.thoughtworks.org/display/CCNET/Email+Publisher?decorator=printable" ); }
    }

    #endregion

    #region ICloneable Members

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns></returns>
    public Converter Clone ( ) {
      return this.MemberwiseClone ( ) as Converter;
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone ( ) {
      return this.Clone ( );
    }

    #endregion

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    public override string ToString ( ) {
      return string.IsNullOrEmpty ( this.Find ) ? this.GetType ( ).Name : this.Find;
    }
  }
}
