//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaReferences.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    using System;

    public static class FatturaElettronicaReferences
    {
        /// <summary>
        /// namespace utilizzato nell'xml
        /// </summary>
        public const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// path schema location
        /// </summary>
        public static readonly string PathSchemaLocation = "https://www.fatturapa.gov.it/export/fatturazione/sdi/fatturapa";

        /// <summary>
        /// Xmldsig_core_schema per validare le fatture
        /// </summary>
        public static readonly string Xmldsig_core_schema = $"{Globals.FolderSchemi}.xmldsig-core-schema.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";

        public const string prefixNamespace = "p";

        public const string prefixSchema = "xsi";

        public const string prefixDigitalSignatures = "ds";

        public const string attributoVersione = "versione";


    }
}



