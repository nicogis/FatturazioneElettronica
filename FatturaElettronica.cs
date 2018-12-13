//-----------------------------------------------------------------------
// <copyright file="FatturaElettronica.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Helper Fattura Elettronica
    /// </summary>
    public static class FatturaElettronica
    {
        /// <summary>
        /// crea il file XML della fattura senza style
        /// </summary>
        /// <param name="fatturaElettronicaType">oggetto fattura</param>
        /// <param name="pathFileName">percorso e nome file di output</param>
        /// <returns>true se la creazione è andata a buon termine altrimenti rigetta l'errore</returns>
        /// <example>
        ///    fatturaElettronicaType.CreateXML("c:\temp\IT01234567890_FPA01.xml");
        /// </example>
        public static bool CreateXML(this FatturaElettronicaType fatturaElettronicaType, string pathFileName)
        {
            return FatturaElettronica.CreateXML(fatturaElettronicaType, pathFileName, false);
        }

        /// <summary>
        /// crea il file XML della fattura
        /// </summary>
        /// <param name="fatturaElettronicaType">oggetto fattura</param>
        /// <param name="pathFileName">percorso e nome file di output</param>
        /// <param name="useStyle">usa lo style per visualizzare la fattura</param>
        /// <returns>true se la creazione è andata a buon termine altrimenti rigetta l'errore</returns>
        /// <example>
        ///    fatturaElettronicaType.CreateXML("c:\temp\IT01234567890_FPA01.xml", false);
        /// </example>
        public static bool CreateXML(this FatturaElettronicaType fatturaElettronicaType, string pathFileName, bool useStyle)
        {
            try
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

                xmlSerializerNamespaces.Add("xsi", FatturaElettronicaReferences.XsiNamespace);
                xmlSerializerNamespaces.Add("p", FatturaElettronicaReferences.PNamespace);
                xmlSerializerNamespaces.Add("ds", FatturaElettronicaReferences.DsNamespace);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FatturaElettronicaType));

                using (TextWriter textWriter = new StreamWriter(pathFileName))
                {
                    if (useStyle)
                    {
                        string styleFile = null;
                        if (fatturaElettronicaType.versione == FormatoTrasmissioneType.FPA12)
                        {
                            styleFile = FatturaElettronicaReferences.FileNameStylePA;
                        }
                        else if (fatturaElettronicaType.versione == FormatoTrasmissioneType.FPR12)
                        {
                            styleFile = FatturaElettronicaReferences.FileNameStyleOrdinaria;
                        }

                        string pathFileStyle = Path.Combine(Path.GetDirectoryName(pathFileName), styleFile);
                        if (!File.Exists(pathFileStyle))
                        {
                            using (FileStream f = File.Create(pathFileStyle))
                            {
                                Assembly.GetExecutingAssembly().GetManifestResourceStream($"{typeof(FatturaElettronica).Namespace}.{styleFile}").CopyTo(f);
                            }   
                        }

                        using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
                        {
                            xmlWriter.WriteStartDocument();
                            xmlWriter.WriteProcessingInstruction("xml-stylesheet", $"type=\"text/xsl\" href=\"{styleFile}\"");
                            xmlSerializer.Serialize(xmlWriter, fatturaElettronicaType, xmlSerializerNamespaces);
                        }
                    }
                    else
                    {
                        xmlSerializer.Serialize(textWriter, fatturaElettronicaType, xmlSerializerNamespaces);
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Valida il file XML
        /// </summary>
        /// <param name="pathFileNameXML">percorso e nome file XML da validare</param>
        /// <param name="messages">lista degli errori riscontrati nella validazione dell'XML</param>
        /// <returns>true se il file XML è formalmente corretto altrimenti false. Se il metodo va in errore rigetta l'errore</returns>
        /// <example>
        ///    List<string> errors;
        ///    if (!FatturaElettronica.TryValidateXML("c:\temp\IT01234567890_FPA01.xml", out List<string> errors))
        ///    {
        ///        ...
        ///    }
        /// </example>
        public static bool TryValidateXML(this FatturaElettronicaType fatturaElettronicaType, out List<string> messages)
        {
            var settings = new XmlReaderSettings() { DtdProcessing = DtdProcessing.Ignore };
            messages = null;
            try
            {

                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

                xmlSerializerNamespaces.Add("xsi", FatturaElettronicaReferences.XsiNamespace);
                xmlSerializerNamespaces.Add("p", FatturaElettronicaReferences.PNamespace);
                xmlSerializerNamespaces.Add("ds", FatturaElettronicaReferences.DsNamespace);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FatturaElettronicaType));
                using (MemoryStream stream = new MemoryStream())
                {
                    xmlSerializer.Serialize(XmlWriter.Create(stream), fatturaElettronicaType, xmlSerializerNamespaces);
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    using (XmlReader xr = XmlReader.Create(stream, settings))
                    {
                        XDocument xdoc = XDocument.Load(xr);
                        var schemas = new XmlSchemaSet();
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        schemas.Add(FatturaElettronicaReferences.PNamespace, XmlReader.Create(assembly.GetManifestResourceStream($"{typeof(FatturaElettronica).Namespace}.{FatturaElettronicaReferences.XsdFileFatturaVersioneXSD}")) );

                        StreamReader xmldsig = new StreamReader(assembly.GetManifestResourceStream($"{typeof(FatturaElettronica).Namespace}.{FatturaElettronicaReferences.Xmldsig_core_schema}"));

                        using (var reader = XmlReader.Create(xmldsig, new XmlReaderSettings()
                        {
                            DtdProcessing = DtdProcessing.Ignore
                        }))
                        {
                            schemas.Add(FatturaElettronicaReferences.DsNamespace, reader);
                        }

                        List<string> errors = new List<string>();
                        xdoc.Validate(schemas, (o, e) =>
                        {
                            errors.Add(e.Message);
                        });

                        bool r = errors.Count == 0;

                        if (!r)
                        {
                            messages = new List<string>();
                            messages.AddRange(errors);
                        }

                        return r;

                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// crea oggetto fattura da file
        /// </summary>
        /// <param name="pathFileName">percorso e nome del file</param>
        /// <param name="fatturaElettronicaType">oggetto fattura</param>
        /// <returns>true se l'operazione è avvenuta con success altrimenti false. Se il metodo va in errore rigetta l'errore</returns>
        /// <example>
        ///    FatturaElettronicaType fatturaElettronicaType; 
        ///    if (!FatturaElettronica.CreateInvoice("c:\temp\IT01234567890_FPA01.xml", out FatturaElettronicaType fatturaElettronicaType))
        ///    {
        ///        fatturaElettronicaType ....
        ///    }
        /// </example>
        public static bool CreateInvoice(string pathFileName, out FatturaElettronicaType fatturaElettronicaType)
        {
            fatturaElettronicaType = null;
            try
            {
                if (!File.Exists(pathFileName))
                {
                    throw new FileNotFoundException();
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FatturaElettronicaType));
                
                using (XmlReader reader = XmlReader.Create(pathFileName))
                {
                    if (xmlSerializer.CanDeserialize(reader))
                    {
                        fatturaElettronicaType = (FatturaElettronicaType)xmlSerializer.Deserialize(reader);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
