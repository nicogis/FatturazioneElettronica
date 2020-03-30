//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaType.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica.Type.V_1_2
{
    using System.Xml.Serialization;

    public partial class FatturaElettronicaType : FatturaElettronicaSchema, IFatturaElettronicaType
    {

        public FatturaElettronicaType()
        {
            base.Init("1.2", "1.2.1");
        }

        public override string FileStyle
        {
            get
            {

                string styleFile = null;
                if (this.versione == FormatoTrasmissioneType.FPA12)
                {
                    styleFile = this.FileNameStylePA;
                }
                else if (this.versione == FormatoTrasmissioneType.FPR12)
                {
                    styleFile = this.FileNameStyleOrdinaria;
                }

                return styleFile;

            }
        }
    }
}