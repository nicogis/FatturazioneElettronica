//-----------------------------------------------------------------------
// <copyright file="IFatturaElettronicaType.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace FatturazioneElettronica
{
    /// <summary>
    /// IFatturazioneElettronicaType interface. 
    /// </summary>
    public interface IFatturaElettronicaType
    {
        ///// <summary>
        ///// namespace utilizzato nell'xml
        ///// </summary>
        public string Namespace { get; }

        ///// <summary>
        ///// ds namespace utilizzato nell'xml
        ///// </summary>
        public string DsNamespace { get; }

        ///// <summary>
        ///// xsd per validare le fatture
        ///// </summary>
        public string XsdFileFatturaVersioneXsd { get; }

        ///// <summary>
        ///// file style
        ///// </summary>
        public string FileStyle { get; }

        /// <summary>
        /// versione schema
        /// </summary>
        public string VersioneFatturaSchema { get; }

    }
}
