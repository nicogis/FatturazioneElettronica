//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaType.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica.Type.V_1_0
{
    sealed public partial class FatturaElettronicaType : FatturaElettronicaSchema, IFatturaElettronicaType
    {
        public FatturaElettronicaType()
        {
            base.Init("1.0", "1.0");
        }

        public override string FileStyle
        {
            get
            {
                string styleFile = null;
                if (this.versione == VersioneSchemaType.Item10)
                {
                    styleFile = this.FileNameStylePA;
                }

                return styleFile;

            }
        }      
    }
}
