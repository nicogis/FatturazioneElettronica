//-----------------------------------------------------------------------
// <copyright file="ProgressivoFile.cs" company="Studio A&T s.r.l.">
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
// <author>Nicogis</author>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica
{
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Text.RegularExpressions;

    /// <summary>
    /// generazione progressivo univoco del file https://www.fatturapa.gov.it/export/fatturazione/it/c-11.htm
    /// il file trasmetto dovrà essere [Codice Paese][Identificativo univoco del Trasmittente]_[Progressivo univoco del file]
    /// Es. IT01234567891_XXXXX dovè XXXXX è il progressivo univoco del file trasmetto per trasmittente
    /// Occorre passare un numero progressivo numerico compreso tra 1..60466176 (ProgressivoFile.GetNumeroProgressivo("ZZZZZ"))
    /// e verrà restituita una codifica base36 con 5 caratteri
    /// </summary>
    public class ProgressivoFile
    {      
        private static char[] charset36;

        static ProgressivoFile()
        {
            ProgressivoFile.charset36 = Enumerable.Range('0', 10).Select(x => ((char)x)).Union(Enumerable.Range('A', 26).Select(x => ((char)x))).ToArray();
        }

        public ProgressivoFile(long numeroProgressivo)
        {
            this.NumeroProgressivo = numeroProgressivo;          
        }

        private ProgressivoFile()
        {   
        }

        public long NumeroProgressivo
        {
            get;
            set;
        }

        /// <summary>
        /// restituisce il progressivo univoco del file
        /// </summary>
        /// <returns></returns>
        public string GetProgressivoFile()
        {
            if (!this.IsValid())
            {
                throw new ArgumentOutOfRangeException("Numero progressivo fuori intervallo consentito!");
            }

            return ProgressivoFile.Encode(this.NumeroProgressivo - 1).PadLeft(5, '0');
        }

        /// <summary>
        /// verifica se il numero progressivo è valido
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return this.NumeroProgressivo > 0 && this.NumeroProgressivo <= GetNumeroProgressivo(new String('Z', 5));
        }

        /// <summary>
        /// restituisce il numero progressivo dal numero progressivo del file
        /// </summary>
        /// <param name="value">numero progressivo del file</param>
        /// <returns>numero progressivo</returns>
        public static long GetNumeroProgressivo(string value)
        {
            if (!Regex.IsMatch(value, @"\A[A-Z,0-9]{5}"))
            {
                throw new ArgumentOutOfRangeException("Progressivo file fuori intervallo consentito!");
            }
            
            var decoded = 0L;
            int l = ProgressivoFile.charset36.Length;
            string s = new String(ProgressivoFile.charset36);

            for (var i = 0; i < value.Length; ++i)
                decoded += s.IndexOf(value[i]) * (long)BigInteger.Pow(l, value.Length - i - 1);

            return decoded + 1;
        }

        /// <summary>
        /// base 36 di un numero
        /// </summary>
        /// <param name="value">numero da codifice in base 36</param>
        /// <returns>valore codificato in base 36</returns>
        private static string Encode(long value)
        {
            var encoded = string.Empty;
            int l = ProgressivoFile.charset36.Length;
            do
                encoded = ProgressivoFile.charset36[(int)(value % l)] + encoded;
            while ((value /= l) != 0);

            return encoded;
        }
    }
}
