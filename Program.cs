//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronicaPA
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// main class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// versione 1.1 formato trasmissione della fatturazione elettronica
        /// </summary>
        private const FormatoTrasmissioneType formatoTrasmissioneType = FormatoTrasmissioneType.SDI11;

        /// <summary>
        /// versione 1.1 schema fatturazione elettronica
        /// </summary>
        private const VersioneSchemaType versioneSchemaType = VersioneSchemaType.Item11;

        /// <summary>
        /// namespace utilizzato nell'xml
        /// </summary>
        private const string xsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// namespace utilizzato nell'xml
        /// </summary>
        private const string pNamespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1";

        /// <summary>
        /// namespace utilizzato nell'xml
        /// </summary>
        private const string dsNamespace = "http://www.w3.org/2000/09/xmldsig#";

        /// <summary>
        /// nome file per visualizzare l'anteprima della fattura elettronica
        /// </summary>
        private const string fileNameStyle = "fatturapa_v1.1.xsl";

        /// <summary>
        /// nome file dell'anteprima della fatturazione elettronica
        /// </summary>
        private const string fileNamePreview = "Preview";

        /// <summary>
        /// main method
        /// </summary>
        /// <param name="args">arguments from console</param>
        public static void Main(string[] args)
        {
            bool preview = true;

            FatturaElettronicaType fatturaElettronica = new FatturaElettronicaType();
            fatturaElettronica.versione = Program.versioneSchemaType;

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

            xmlSerializerNamespaces.Add("xsi", Program.xsiNamespace);
            xmlSerializerNamespaces.Add("p", Program.pNamespace);
            xmlSerializerNamespaces.Add("ds", Program.dsNamespace);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FatturaElettronicaType));

            FatturaElettronicaHeaderType fatturaElettronicaHeaderType = new FatturaElettronicaHeaderType();

            DatiTrasmissioneType datiTrasmissioneType = new DatiTrasmissioneType();
            IdFiscaleType idFiscaleTypeTrasmissione = new IdFiscaleType();
            IdFiscaleType idFiscaleTypeCedente = new IdFiscaleType();
            
            idFiscaleTypeTrasmissione.IdCodice = "";
            
            idFiscaleTypeTrasmissione.IdPaese = "";

            idFiscaleTypeCedente.IdPaese = "";
            idFiscaleTypeCedente.IdCodice = "";

            datiTrasmissioneType.IdTrasmittente = idFiscaleTypeTrasmissione;
            datiTrasmissioneType.ProgressivoInvio = ""; //esempio "1"
            datiTrasmissioneType.FormatoTrasmissione = Program.formatoTrasmissioneType;
            datiTrasmissioneType.CodiceDestinatario = "";

            fatturaElettronicaHeaderType.DatiTrasmissione = datiTrasmissioneType;

            CedentePrestatoreType cedentePrestatoreType = new CedentePrestatoreType();

            DatiAnagraficiCedenteType datiAnagraficiCedenteType = new DatiAnagraficiCedenteType();
            datiAnagraficiCedenteType.IdFiscaleIVA = idFiscaleTypeCedente;

            AnagraficaType anagraficaType = new AnagraficaType();

            //if (societa)
            //{
            //    anagraficaType.Items = new string[] { "Company srl" };
            //    anagraficaType.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Denominazione };
            //}
            //else if (professionista)
            //{
            //    anagraficaType.Items = new string[] { "Mario", "Rossi" };
            //    anagraficaType.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Nome, ItemsChoiceType.Cognome };
            //}

            datiAnagraficiCedenteType.Anagrafica = anagraficaType;

            cedentePrestatoreType.DatiAnagrafici = datiAnagraficiCedenteType;

            datiAnagraficiCedenteType.RegimeFiscale = RegimeFiscaleType.RF01;

            IndirizzoType indirizzoType = new IndirizzoType();
            indirizzoType.Indirizzo = "";
            indirizzoType.NumeroCivico = "";
            indirizzoType.CAP = "";
            indirizzoType.Comune = "";
            indirizzoType.Provincia = "";
            indirizzoType.Nazione = "";
            cedentePrestatoreType.Sede = indirizzoType;

            fatturaElettronicaHeaderType.CedentePrestatore = cedentePrestatoreType;

            CessionarioCommittenteType cessionarioCommittenteType = new CessionarioCommittenteType();

            DatiAnagraficiCessionarioType datiAnagraficiCessionarioType = new DatiAnagraficiCessionarioType();

            //if (pivaCommittente))
            //{
            //    IdFiscaleType idFiscaleTypeCommittente = new IdFiscaleType();
            //    idFiscaleTypeCommittente.IdCodice = pivaCommittente;
            //    idFiscaleTypeCommittente.IdPaese = idPaeseCommittente;
            //    datiAnagraficiCessionarioType.IdFiscaleIVA = idFiscaleTypeCommittente;
            //}

            //if (cfCommittente)
            //{
            //    datiAnagraficiCessionarioType.CodiceFiscale = cfCommittente;
            //}

            AnagraficaType anagraficaTypeCommittente = new AnagraficaType();
            anagraficaTypeCommittente.Items = new string[] { "LaDenominazione" };
            anagraficaTypeCommittente.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Denominazione };

            datiAnagraficiCessionarioType.Anagrafica = anagraficaTypeCommittente;

            cessionarioCommittenteType.DatiAnagrafici = datiAnagraficiCessionarioType;

            IndirizzoType indirizzoTypeCommittente = new IndirizzoType();
            //indirizzoTypeCommittente.Indirizzo = indirizzoCommittente;
            //indirizzoTypeCommittente.CAP = capCommittente;
            //indirizzoTypeCommittente.Comune = comuneCommittente;
            //indirizzoTypeCommittente.Provincia = provinciaCommittente;
            //indirizzoTypeCommittente.Nazione = idPaeseCommittente;

            cessionarioCommittenteType.Sede = indirizzoTypeCommittente;

            fatturaElettronicaHeaderType.CessionarioCommittente = cessionarioCommittenteType;
            fatturaElettronica.FatturaElettronicaHeader = fatturaElettronicaHeaderType;

            FatturaElettronicaBodyType fatturaElettronicaBodyType = new FatturaElettronicaBodyType();
            DatiGeneraliType datiGeneraliType = new DatiGeneraliType();
            DatiGeneraliDocumentoType datiGeneraliDocumentoType = new DatiGeneraliDocumentoType();

            datiGeneraliDocumentoType.TipoDocumento = TipoDocumentoType.TD01;
            datiGeneraliDocumentoType.Divisa = "";
            datiGeneraliDocumentoType.Data = DateTime.Now;
            //datiGeneraliDocumentoType.Numero = string.Format("{0}{1}E", numero.ToString(), siglaNotaAccredito);

            //if (professionista)
            //{
            //    DatiRitenutaType datiRitenutaType = new DatiRitenutaType();
            //    datiRitenutaType.TipoRitenuta = TipoRitenutaType.RT01; //ritenuta persone fisiche
            //    datiRitenutaType.ImportoRitenuta = ritenuta.Value;
            //    datiRitenutaType.AliquotaRitenuta = ritenutaCorrente.Value;
            //    datiRitenutaType.CausalePagamento = CausalePagamentoType.A; //da mod. 770semplificato

            //    datiGeneraliDocumentoType.DatiRitenuta = datiRitenutaType;

            //    DatiCassaPrevidenzialeType datiCassaPrevidenzialeType = new DatiCassaPrevidenzialeType();
            //    datiCassaPrevidenzialeType.TipoCassa = TipoCassaType.TC04;
            //    datiCassaPrevidenzialeType.AlCassa = aliquotaCassaCorrente.Value;
            //    datiCassaPrevidenzialeType.ImportoContributoCassa = importoContributoCassa.Value;
            //    datiCassaPrevidenzialeType.ImponibileCassa = importo.Value;
            //    datiCassaPrevidenzialeType.AliquotaIVA = aliquotaIVA.Value;

            //    datiGeneraliDocumentoType.DatiCassaPrevidenziale = new DatiCassaPrevidenzialeType[] { datiCassaPrevidenzialeType };

            //}

            datiGeneraliType.DatiGeneraliDocumento = datiGeneraliDocumentoType;

            fatturaElettronicaBodyType.DatiGenerali = datiGeneraliType;

            //if ((!string.IsNullOrEmpty(idDocumentoDOAFE)) || (dataDOAFE.HasValue) || (!string.IsNullOrEmpty(codiceCommessaConvenzioneDOAFE)) || (!string.IsNullOrEmpty(codiceCUPDOAFE)) || (!string.IsNullOrEmpty(codiceCIGDOAFE)))
            //{
            //    DatiDocumentiCorrelatiType datiDocumentiCorrelatiType = new DatiDocumentiCorrelatiType();

            //    if (!string.IsNullOrEmpty(idDocumentoDOAFE))
            //    {
            //        datiDocumentiCorrelatiType.IdDocumento = idDocumentoDOAFE;
            //    }

            //    datiDocumentiCorrelatiType.DataSpecified = dataDOAFE.HasValue;
            //    if (dataDOAFE.HasValue)
            //    {
            //        datiDocumentiCorrelatiType.Data = dataDOAFE.Value;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCommessaConvenzioneDOAFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCommessaConvenzione = codiceCommessaConvenzioneDOAFE;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCUPDOAFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCUP = codiceCUPDOAFE;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCIGDOAFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCIG = codiceCIGDOAFE;
            //    }

            //    datiGeneraliType.DatiOrdineAcquisto = new DatiDocumentiCorrelatiType[] { datiDocumentiCorrelatiType };
            //}

            //if ((!string.IsNullOrEmpty(idDocumentoDCFE)) || (dataDCFE.HasValue) || (!string.IsNullOrEmpty(codiceCommessaConvenzioneDCFE)) || (!string.IsNullOrEmpty(codiceCUPDCFE)) || (!string.IsNullOrEmpty(codiceCIGDCFE)))
            //{
            //    DatiDocumentiCorrelatiType datiDocumentiCorrelatiType = new DatiDocumentiCorrelatiType();

            //    if (!string.IsNullOrEmpty(idDocumentoDCFE))
            //    {
            //        datiDocumentiCorrelatiType.IdDocumento = idDocumentoDCFE;
            //    }

            //    datiDocumentiCorrelatiType.DataSpecified = dataDCFE.HasValue;
            //    if (dataDCFE.HasValue)
            //    {
            //        datiDocumentiCorrelatiType.Data = dataDCFE.Value;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCommessaConvenzioneDCFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCommessaConvenzione = codiceCommessaConvenzioneDCFE;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCUPDCFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCUP = codiceCUPDCFE;
            //    }

            //    if (!string.IsNullOrEmpty(codiceCIGDCFE))
            //    {
            //        datiDocumentiCorrelatiType.CodiceCIG = codiceCIGDCFE;
            //    }

            //    datiGeneraliType.DatiContratto = new DatiDocumentiCorrelatiType[] { datiDocumentiCorrelatiType };
            //}

            //if (salFE.HasValue)
            //{
            //    DatiSALType datiSALType = new DatiSALType();
            //    datiSALType.RiferimentoFase = salFE.Value.ToString();
            //    datiGeneraliType.DatiSAL = new DatiSALType[] { datiSALType };
            //}

            DatiBeniServiziType datiBeniServiziType = new DatiBeniServiziType();
            DettaglioLineeType dettaglioLineeType = new DettaglioLineeType();
            dettaglioLineeType.NumeroLinea = ""; // "1"; 

            //if (oggetto.Length > Program.DescrizioneLunghezzaMax)
            //{
            //    throw new Exception(string.Format("La lunghezza dell'oggetto della fattura supera il limite consentito {0}!", Program.DescrizioneLunghezzaMax));
            //}

            dettaglioLineeType.Descrizione = "";

            //dettaglioLineeType.PrezzoUnitario = imponibile.Value;
            //dettaglioLineeType.PrezzoTotale = imponibile.Value;
            
            //dettaglioLineeType.AliquotaIVA = aliquotaIVA.Value;
            datiBeniServiziType.DettaglioLinee = new DettaglioLineeType[] { dettaglioLineeType };

            DatiRiepilogoType datiRiepilogoType = new DatiRiepilogoType();
            //datiRiepilogoType.AliquotaIVA = aliquotaIVA.Value;

            //datiRiepilogoType.ImponibileImporto = imponibile.Value;
            
            //datiRiepilogoType.Imposta = iva.Value;

            //datiRiepilogoType.EsigibilitaIVASpecified = (esigibilitaIVAFE != null);
            if (datiRiepilogoType.EsigibilitaIVASpecified)
            {
                //EsigibilitaIVAType esigibilitaIVAType;
                //if (Enum.TryParse(esigibilitaIVAFE, out esigibilitaIVAType))
                //{
                //    datiRiepilogoType.EsigibilitaIVA = esigibilitaIVAType;
                //}
                //else
                //{
                //    throw new Exception(string.Format("L'esigibilità '{0}' non è stata trovata!", esigibilitaIVAFE));
                //}
            }

            datiBeniServiziType.DatiRiepilogo = new DatiRiepilogoType[] { datiRiepilogoType };
            fatturaElettronicaBodyType.DatiBeniServizi = datiBeniServiziType;

            DatiPagamentoType datiPagamentoType = new DatiPagamentoType();
            //datiPagamentoType.CondizioniPagamento = condizioniPagamento;
            DettaglioPagamentoType dettaglioPagamentoType = new DettaglioPagamentoType();
            //dettaglioPagamentoType.ModalitaPagamento = modalitaPagamento;
            dettaglioPagamentoType.DataScadenzaPagamentoSpecified = true;
            //dettaglioPagamentoType.DataScadenzaPagamento = dataPagamento.Value;
            //dettaglioPagamentoType.ImportoPagamento = importoConIVA.Value;
            dettaglioPagamentoType.IBAN = "";
            datiPagamentoType.DettaglioPagamento = new DettaglioPagamentoType[] { dettaglioPagamentoType };

            fatturaElettronicaBodyType.DatiPagamento = new DatiPagamentoType[] { datiPagamentoType };

            fatturaElettronica.FatturaElettronicaBody = new FatturaElettronicaBodyType[] { fatturaElettronicaBodyType };

            string nameFile = null;
            if (preview)
            {
                //nameFile = Program.fileNamePreview;
            }
            else
            {
                //nameFile = string.Format("{0}{1}_{2}", idPaese, piva, string.Format("{0}", progressivoNumero.Value).PadLeft(Program.ProgressivoFileLunghezzaMax, '0'));
            }

            string pathFileName = null;
            //pathFileName = Path.ChangeExtension(System.IO.Path.Combine(Settings.Default.PathFattureElettroniche, nameFile), Enum.GetName(typeof(EstensioniFile), EstensioniFile.xml)));

            if (!preview)
            {
                string msg = null;
                if (File.Exists(pathFileName))
                {
                    msg = string.Format("Attenzione è già presente il file ('{0}'). Se si continua verrà sovrascritto.", nameFile);
                }

                if (File.Exists(Path.ChangeExtension(pathFileName, Enum.GetName(typeof(EstensioniFile), EstensioniFile.p7m))))
                {
                    msg += "Inoltre è presente anche il file firmato. Se si continua dovrà essere rifirmato.";
                }

                if (msg != null)
                {
                    msg += "Confermi?";
                    if (MessageBox.Show(msg, "File già esistente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            using (TextWriter textWriter = new StreamWriter(pathFileName))
            {
                if (preview)
                {
                    using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteProcessingInstruction("xml-stylesheet", string.Format("type=\"text/xsl\" href=\"{0}\"", Program.fileNameStyle));
                        xmlSerializer.Serialize(xmlWriter, fatturaElettronica, xmlSerializerNamespaces);
                    }
                }
                else
                {
                    xmlSerializer.Serialize(textWriter, fatturaElettronica, xmlSerializerNamespaces);
                }
            }

            if (preview)
            {
                System.Diagnostics.Process.Start(pathFileName);
            }
            else
            {
                MessageBox.Show(string.Format("Il file {0} è stato creato con successo!", nameFile), "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
