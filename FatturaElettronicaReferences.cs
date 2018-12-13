//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaReferences.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
using FatturazioneElettronica;
using System;
using System.Xml.Serialization;

public static class FatturaElettronicaReferences
{
    /// <summary>
    /// namespace utilizzato nell'xml
    /// </summary>
    public const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

    /// <summary>
    /// versione schema fattura
    /// </summary>
    public const string Versione = "1.2";

    /// <summary>
    /// versione xsd
    /// </summary>
    public const string VersioneXSD = "1.2.1";

    /// <summary>
    /// namespace utilizzato nell'xml
    /// </summary>
    public static readonly string PNamespace = $"http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v{FatturaElettronicaReferences.Versione}";

    /// <summary>
    /// path schema location
    /// </summary>
    public static readonly string PathSchemaLocation = $"http://www.fatturapa.gov.it/export/fatturazione/sdi/fatturapa";

    /// <summary>
    /// namespace utilizzato nell'xml
    /// </summary>
    public const string DsNamespace = "http://www.w3.org/2000/09/xmldsig#";

    /// <summary>
    /// folder contenitori stili
    /// </summary>
    public const string FolderStili = "Stili";

    /// <summary>
    /// folder contenitore schemi
    /// </summary>
    public const string FolderSchemi = "Schemi";

    /// <summary>
    /// xsd fatture
    /// </summary>
    public static readonly string XsdFileFatturaVersione = $"{FatturaElettronicaReferences.FolderSchemi}.Schema_del_file_xml_FatturaPA_versione_{FatturaElettronicaReferences.Versione}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";

    /// <summary>
    /// xsd per validare le fatture
    /// </summary>
    public static readonly string XsdFileFatturaVersioneXSD = $"{FatturaElettronicaReferences.FolderSchemi}.Schema_del_file_xml_FatturaPA_versione_{FatturaElettronicaReferences.VersioneXSD}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";

    /// <summary>
    /// Xmldsig_core_schema per validare le fatture
    /// </summary>
    public static readonly string Xmldsig_core_schema = $"{FatturaElettronicaReferences.FolderSchemi}.xmldsig-core-schema.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";

    /// <summary>
    /// nome file per visualizzare l'anteprima della fattura elettronica PA
    /// </summary>
    public static readonly string FileNameStylePA = $"{FatturaElettronicaReferences.FolderStili}.fatturaPA_v{FatturaElettronicaReferences.VersioneXSD}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsl)}";

    /// <summary>
    /// nome file per visualizzare l'anteprima della fattura elettronica
    /// </summary>
    public static readonly string FileNameStyleOrdinaria = $"{FatturaElettronicaReferences.FolderStili}.fatturaordinaria_v{FatturaElettronicaReferences.VersioneXSD}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsl)}";

    /// <summary>
    /// url xsd fattura http
    /// </summary>
    public static readonly string XsdFatturaHttp = new Uri(FatturaElettronicaReferences.PathSchemaLocation).Combine($"v{FatturaElettronicaReferences.Versione}", FatturaElettronicaReferences.XsdFileFatturaVersione).AbsoluteUri;
    
}

public partial class FatturaElettronicaType
{
    /// <summary>
    /// xsi:schemaLocation
    /// </summary>
    [XmlAttribute("schemaLocation", AttributeName = "schemaLocation", Namespace = FatturaElettronicaReferences.XsiNamespace)]
    public string SchemaLocation = $"{FatturaElettronicaReferences.PNamespace} {FatturaElettronicaReferences.XsdFatturaHttp}";
}

