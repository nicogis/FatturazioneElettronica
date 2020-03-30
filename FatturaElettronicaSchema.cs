//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaSchema.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public abstract class FatturaElettronicaSchema : IFatturaElettronicaType
    {
        
        [XmlIgnore]
        protected string FileNameStylePA { get; private set; }

        [XmlIgnore]
        protected string FileNameStyleOrdinaria { get; private set; }

        [XmlIgnore]
        protected string XsdFatturaHttp { get; private set; }

        [XmlIgnore]
        protected string XsdFileFatturaVersione { get; private set; }

        [XmlIgnore]
        public string XsdFileFatturaVersioneXsd { get; private set; }

        [XmlIgnore]
        public string Namespace { get; private set; }

        [XmlIgnore]
        public string DsNamespace { get; private set; }

        [XmlIgnore]
        public string XsiNamespace { get; private set; }

        [XmlIgnore]
        public abstract string FileStyle { get; }

        [XmlIgnore]
        public string VersioneFatturaSchema{ get; private set; }

        [XmlIgnore]
        public string VersioneSpecifiche { get; private set; }

        /// <summary>
        /// xsi:schemaLocation
        /// </summary>
        [XmlAttribute("schemaLocation", AttributeName = "schemaLocation", Namespace = FatturaElettronicaReferences.XsiNamespace)]
        public string SchemaLocation;

        protected void Init(string versioneXsd, string versioneSpecifiche)
        {
            string schema = null;
            if (string.Compare(versioneXsd, Versioni.Versione1_0, StringComparison.Ordinal) == 0)
            {
                schema = Globals.PrefixSchemaXsdV1_0;
            }
            else
            {
                schema = Globals.PrefixSchemaXsd;
            }
            
            this.XsdFileFatturaVersioneXsd = $"{Globals.FolderSchemi}.{schema}{versioneXsd}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";

            using (XmlReader reader = new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{typeof(FatturaElettronica).Namespace}.{this.XsdFileFatturaVersioneXsd}")))
            {
                XmlSchema xmlSchema = XmlSchema.Read(reader, null);
                this.VersioneFatturaSchema = xmlSchema.Version;
                this.Namespace = xmlSchema.TargetNamespace;
                this.DsNamespace = xmlSchema.Namespaces.ToArray().First(n => string.Compare(n.Name, FatturaElettronicaReferences.prefixDigitalSignatures, StringComparison.Ordinal) == 0).Namespace;
            }

            this.VersioneSpecifiche = versioneSpecifiche;

            this.FileNameStylePA = $"{Globals.FolderStili}.{Globals.PrefixStilePA}{this.VersioneFatturaSchema}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsl)}";
            this.FileNameStyleOrdinaria = $"{Globals.FolderStili}.{Globals.PrefixStilePA}{this.VersioneFatturaSchema}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsl)}";

            this.XsdFileFatturaVersione = $"{schema}{this.VersioneFatturaSchema}.{Enum.GetName(typeof(EstensioniFile), EstensioniFile.xsd)}";
            
            this.XsdFatturaHttp = new Uri(FatturaElettronicaReferences.PathSchemaLocation).Combine($"v{this.VersioneSpecifiche}", this.XsdFileFatturaVersione).AbsoluteUri;
            
            this.SchemaLocation = $"{this.Namespace} {this.XsdFatturaHttp}";
        }


    }
}
