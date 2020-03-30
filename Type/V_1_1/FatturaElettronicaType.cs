//-----------------------------------------------------------------------
// <copyright file="FatturaElettronicaType.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica.Type.V_1_1
{
    public partial class FatturaElettronicaType : FatturaElettronicaSchema, IFatturaElettronicaType
    {
        
        public FatturaElettronicaType()
        {
            base.Init("1.1", "1.1");
        }
      
        public override string FileStyle
        {
            get
            {

                string styleFile = null;
                if (this.versione == VersioneSchemaType.Item11)
                {
                    styleFile = this.FileNameStylePA;
                }

                return styleFile;

            }
        }
    }
}