﻿/*
 * Copyright (c) 2007-2008, Ryan Conrad. All rights reserved.
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
using System.ComponentModel;

namespace CCNetConfig.Core.Components {
  /// <summary>
  /// 
  /// </summary>
  [TypeConverter ( typeof ( SvnUriTypeConverter ) )]
  public class SvnUri : Uri {
    /// <summary>
    /// 
    /// </summary>
    public static string UriSchemeSvn = "svn";
    /// <summary>
    /// 
    /// </summary>
    public static string UriSchemeSvnSsh = "svn+ssh";
    /// <summary>
    /// Initializes a new instance of the <see cref="SvnUri"/> class.
    /// </summary>
    /// <param name="url">The URL.</param>
    public SvnUri ( string url ) : base ( url ) {
      if ( !UriParser.IsKnownScheme ( SvnUri.UriSchemeSvn ) ) {
        UriParser.Register ( new SvnUriParser ( ), SvnUri.UriSchemeSvn, 3980 );
      }

      if ( !UriParser.IsKnownScheme ( SvnUri.UriSchemeSvnSsh ) ) {
        UriParser.Register ( new SvnUriParser ( ), SvnUri.UriSchemeSvnSsh, 22 );
      }
    }
  }
}
