﻿/*

* ==============================================================================
*
* Filename: HeaderParser
* Description: 
*
* Version: 1.0
* Created: 2020/8/13 5:05:18
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace AutumnBox.JSRT.Util
{
    public sealed class MetadataReader
    {
        public IReadOnlyDictionary<string, string> KV { get; private set; }
        public string this[string key]
        {
            get
            {
                return KV[key];
            }
        }
        private readonly static Regex headerMetadataRegex =
            new Regex(@"^//\s@(?<key>[\w|\d]+)\s+(?<value>.+)$", RegexOptions.Compiled | RegexOptions.Multiline);
        public MetadataReader(string jsScript)
        {
            var tmp = new Dictionary<string, string>();
            foreach (var match in from Match match in headerMetadataRegex.Matches(jsScript)
                                  where match.Success
                                  select match)
            {
                tmp.Add(match.Result("${key}"), match.Result("${value}").Replace("\r", ""));
            }

            this.KV = tmp;
        }
    }
}
