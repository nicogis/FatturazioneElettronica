## Fatturazione elettronica verso la Pubblica Amministrazione e privati

### Descrizione
La libreria è stato sviluppata in c# in base alla documentazione fornita al seguente link http://www.fatturapa.gov.it/export/fatturazione/it/normativa/f-2.htm

La libreria è completa di tutti i type per creare una fattura completa v. 1.2.1 in base alle proprie esigenze

Sono presenti due metodi:

- TryValidateXML per validare la fattura

- CreateXML per generale il file XML

### Requisiti

E' richiesto il framework Microsoft .NET 4.5

### Esempio di creazione fattura

![References](images/References.PNG)

```csharp
//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------

using FatturazioneElettronica;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {

        FatturaElettronicaType fatturaElettronica = new FatturaElettronicaType();
        fatturaElettronica.versione = FormatoTrasmissioneType.FPA12;

        FatturaElettronicaHeaderType fatturaElettronicaHeaderType = new FatturaElettronicaHeaderType();

        DatiTrasmissioneType datiTrasmissioneType = new DatiTrasmissioneType();

        IdFiscaleType idFiscaleTypeTrasmissione = new IdFiscaleType();
        idFiscaleTypeTrasmissione.IdCodice = "01234567890";
        idFiscaleTypeTrasmissione.IdPaese = "IT";

        datiTrasmissioneType.IdTrasmittente = idFiscaleTypeTrasmissione;
        datiTrasmissioneType.ProgressivoInvio = "00001";
        datiTrasmissioneType.FormatoTrasmissione = FormatoTrasmissioneType.FPA12;
        datiTrasmissioneType.CodiceDestinatario = "AAAAAA";

        fatturaElettronicaHeaderType.DatiTrasmissione = datiTrasmissioneType;

        CedentePrestatoreType cedentePrestatoreType = new CedentePrestatoreType();

        DatiAnagraficiCedenteType datiAnagraficiCedenteType = new DatiAnagraficiCedenteType();

        IdFiscaleType idFiscaleTypeCedente = new IdFiscaleType();
        idFiscaleTypeCedente.IdPaese = "IT";
        idFiscaleTypeCedente.IdCodice = "01234567890";

        datiAnagraficiCedenteType.IdFiscaleIVA = idFiscaleTypeCedente;

        AnagraficaType anagraficaType = new AnagraficaType();

        
        anagraficaType.Items = new string[] { "ALPHA SRL" };
        anagraficaType.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Denominazione };
        
        datiAnagraficiCedenteType.Anagrafica = anagraficaType;

        cedentePrestatoreType.DatiAnagrafici = datiAnagraficiCedenteType;

        datiAnagraficiCedenteType.RegimeFiscale = RegimeFiscaleType.RF19;

        IndirizzoType indirizzoType = new IndirizzoType();
        indirizzoType.Indirizzo = "VIALE ROMA";
        indirizzoType.NumeroCivico = "543";
        indirizzoType.CAP = "07100";
        indirizzoType.Comune = "SASSARI";
        indirizzoType.Provincia = "SS";
        indirizzoType.Nazione = "IT";
        cedentePrestatoreType.Sede = indirizzoType;

        fatturaElettronicaHeaderType.CedentePrestatore = cedentePrestatoreType;

        CessionarioCommittenteType cessionarioCommittenteType = new CessionarioCommittenteType();

        DatiAnagraficiCessionarioType datiAnagraficiCessionarioType = new DatiAnagraficiCessionarioType();

        AnagraficaType anagraficaTypeCommittente = new AnagraficaType();
        anagraficaTypeCommittente.Items = new string[] { "AMMINISTRAZIONE BETA" };
        anagraficaTypeCommittente.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Denominazione };

        datiAnagraficiCessionarioType.Anagrafica = anagraficaTypeCommittente;
        datiAnagraficiCessionarioType.CodiceFiscale = "09876543210";
        cessionarioCommittenteType.DatiAnagrafici = datiAnagraficiCessionarioType;

        IndirizzoType indirizzoTypeCommittente = new IndirizzoType();
        indirizzoTypeCommittente.Indirizzo = "VIA TORINO 38-B";
        indirizzoTypeCommittente.CAP = "00145";
        indirizzoTypeCommittente.Comune = "ROMA";
        indirizzoTypeCommittente.Provincia = "RM";
        indirizzoTypeCommittente.Nazione = "IT";

        cessionarioCommittenteType.Sede = indirizzoTypeCommittente;

        fatturaElettronicaHeaderType.CessionarioCommittente = cessionarioCommittenteType;
        fatturaElettronica.FatturaElettronicaHeader = fatturaElettronicaHeaderType;

        FatturaElettronicaBodyType fatturaElettronicaBodyType = new FatturaElettronicaBodyType();
        DatiGeneraliType datiGeneraliType = new DatiGeneraliType();
        DatiGeneraliDocumentoType datiGeneraliDocumentoType = new DatiGeneraliDocumentoType();

        datiGeneraliDocumentoType.TipoDocumento = TipoDocumentoType.TD01;
        datiGeneraliDocumentoType.Divisa = "EUR";
        datiGeneraliDocumentoType.Data = new DateTime(2017,1,18);
        datiGeneraliDocumentoType.Numero = "123";
        datiGeneraliDocumentoType.Causale = new string[] {
            "LA FATTURA FA RIFERIMENTO AD UNA OPERAZIONE AAAA BBBBBBBBBBBBBBBBBB CCC DDDDDDDDDDDDDDD E FFFFFFFFFFFFFFFFFFFF GGGGGGGGGG HHHHHHH II LLLLLLLLLLLLLLLLL MMM NNNNN OO PPPPPPPPPPP QQQQ RRRR SSSSSSSSSSSSSS",
            "SEGUE DESCRIZIONE CAUSALE NEL CASO IN CUI NON SIANO STATI SUFFICIENTI 200 CARATTERI AAAAAAAAAAA BBBBBBBBBBBBBBBBB"
        };

        datiGeneraliType.DatiGeneraliDocumento = datiGeneraliDocumentoType;

        fatturaElettronicaBodyType.DatiGenerali = datiGeneraliType;

        DatiDocumentiCorrelatiType datiOrdineAcquistoType = new DatiDocumentiCorrelatiType();
        datiOrdineAcquistoType.RiferimentoNumeroLinea = new string[] { "1" };
        datiOrdineAcquistoType.IdDocumento = "66685";
        datiOrdineAcquistoType.NumItem = "1";
        datiOrdineAcquistoType.CodiceCUP = "123abc";
        datiOrdineAcquistoType.CodiceCIG = "456def";
        
        datiGeneraliType.DatiOrdineAcquisto = new DatiDocumentiCorrelatiType[] { datiOrdineAcquistoType };

        DatiDocumentiCorrelatiType datiContrattoType = new DatiDocumentiCorrelatiType();
        datiContrattoType.RiferimentoNumeroLinea = new string[] { "1" };
        datiContrattoType.IdDocumento = "123";
        datiContrattoType.NumItem = "5";
        datiContrattoType.CodiceCUP = "123abc";
        datiContrattoType.CodiceCIG = "456def";
        datiGeneraliType.DatiContratto = new DatiDocumentiCorrelatiType[] { datiContrattoType };

        DatiDocumentiCorrelatiType datiConvenzioneType = new DatiDocumentiCorrelatiType();
        datiConvenzioneType.RiferimentoNumeroLinea = new string[] { "1" };
        datiConvenzioneType.IdDocumento = "456";
        datiConvenzioneType.NumItem = "5";
        datiConvenzioneType.CodiceCUP = "123abc";
        datiConvenzioneType.CodiceCIG = "456def";
        datiGeneraliType.DatiConvenzione = new DatiDocumentiCorrelatiType[] { datiConvenzioneType };

        DatiDocumentiCorrelatiType datiRicezioneType = new DatiDocumentiCorrelatiType();
        datiRicezioneType.RiferimentoNumeroLinea = new string[] { "1" };
        datiRicezioneType.IdDocumento = "789";
        datiRicezioneType.NumItem = "5";
        datiRicezioneType.CodiceCUP = "123abc";
        datiRicezioneType.CodiceCIG = "456def";
        datiGeneraliType.DatiRicezione = new DatiDocumentiCorrelatiType[] { datiRicezioneType };

        DatiTrasportoType datiTrasportoType = new DatiTrasportoType();
        DatiAnagraficiVettoreType datiAnagraficiVettore = new DatiAnagraficiVettoreType();
        IdFiscaleType idFiscaleType = new IdFiscaleType();
        idFiscaleType.IdPaese = "IT";
        idFiscaleType.IdCodice = "4681012141";
        datiAnagraficiVettore.IdFiscaleIVA = idFiscaleType;
        AnagraficaType anagraficaTypeDT = new AnagraficaType();
        anagraficaTypeDT.Items = new string[] { "Trasporto spa" };
        anagraficaTypeDT.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.Denominazione };
        datiAnagraficiVettore.Anagrafica = anagraficaTypeDT;
        datiTrasportoType.DatiAnagraficiVettore = datiAnagraficiVettore;
        datiTrasportoType.DataOraConsegnaSpecified = true;
        datiTrasportoType.DataOraConsegna = new DateTime(2017, 01, 16, 16, 46, 12);
        datiGeneraliType.DatiTrasporto = datiTrasportoType;

        DatiBeniServiziType datiBeniServiziType = new DatiBeniServiziType();
        DettaglioLineeType dettaglioLineeType = new DettaglioLineeType();
        dettaglioLineeType.NumeroLinea = "1";
        dettaglioLineeType.Descrizione = "DESCRIZIONE DELLA FORNITURA";
        dettaglioLineeType.Quantita = 5.00M;
        dettaglioLineeType.PrezzoUnitario = 1.00M;
        dettaglioLineeType.PrezzoTotale = 5.00M;
        dettaglioLineeType.AliquotaIVA = 22.00M;
        datiBeniServiziType.DettaglioLinee = new DettaglioLineeType[] { dettaglioLineeType };

        DatiRiepilogoType datiRiepilogoType = new DatiRiepilogoType();
        datiRiepilogoType.AliquotaIVA = 22.00M;
        datiRiepilogoType.ImponibileImporto = 5.00M;
        datiRiepilogoType.Imposta = 1.10M;
        datiRiepilogoType.EsigibilitaIVASpecified = true;
        datiRiepilogoType.EsigibilitaIVA = EsigibilitaIVAType.I;

        datiBeniServiziType.DatiRiepilogo = new DatiRiepilogoType[] { datiRiepilogoType };
        fatturaElettronicaBodyType.DatiBeniServizi = datiBeniServiziType;

        DatiPagamentoType datiPagamentoType = new DatiPagamentoType();
        datiPagamentoType.CondizioniPagamento = CondizioniPagamentoType.TP01;
        DettaglioPagamentoType dettaglioPagamentoType = new DettaglioPagamentoType();
        dettaglioPagamentoType.ModalitaPagamento = ModalitaPagamentoType.MP01;
        dettaglioPagamentoType.DataScadenzaPagamentoSpecified = true;
        dettaglioPagamentoType.DataScadenzaPagamento = new DateTime(2017, 2, 18);
        dettaglioPagamentoType.ImportoPagamento = 6.10M;
        datiPagamentoType.DettaglioPagamento = new DettaglioPagamentoType[] { dettaglioPagamentoType };

        fatturaElettronicaBodyType.DatiPagamento = new DatiPagamentoType[] { datiPagamentoType };

        fatturaElettronica.FatturaElettronicaBody = new FatturaElettronicaBodyType[] { fatturaElettronicaBodyType };

        try
        {
            if (!fatturaElettronica.TryValidateXML(out List<string> messages))
            {
                Console.WriteLine("Fattura non valida!");
                messages.ForEach(f => Console.WriteLine(f));
            }
            else
            {
                Console.WriteLine("Fattura valida!");

                // crea XML fattura
                fatturaElettronica.CreateXML(@"c:\temp\IT01234567890_FPA01.xml");

                // crea XML fattura da visualizzare con lo stile
                //fatturaElettronica.CreateXML(@"c:\temp\IT01234567890_FPA01.xml", true);
            }

            Console.ReadKey();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}!");
        }
    }
}
```

### License

Il progetto è rilasciato sotto licenza GNU Library General Public License (LGPL).





