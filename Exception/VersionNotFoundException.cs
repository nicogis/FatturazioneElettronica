//-----------------------------------------------------------------------
// <copyright file="VersionNotFoundException.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    using System;

    [Serializable]
    public class VersionNotFoundException : Exception
    {
        public string Version { get; }

        public VersionNotFoundException() { }

        public VersionNotFoundException(string message)
            : base(message) { }

        public VersionNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public VersionNotFoundException(string message, string version)
            : this(message)
        {
            Version = version;
        }
    }
}
