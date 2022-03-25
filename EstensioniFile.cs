//-----------------------------------------------------------------------
// <copyright file="EstensioniFile.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    /// <summary>
    /// estensioni file
    /// </summary>
    public enum EstensioniFile
    {
        /// <summary>
        /// file firmato
        /// </summary>
        p7m,

        /// <summary>
        /// file non firmato
        /// </summary>
        xml,

        /// <summary>
        /// file formato stile fattura
        /// </summary>
        xsl,

        /// <summary>
        /// file schema xml
        /// </summary>
        xsd,

        /// <summary>
        /// file formato anteprima fattura
        /// </summary>
        html

    }
}
