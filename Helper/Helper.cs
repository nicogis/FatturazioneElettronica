//-----------------------------------------------------------------------
// <copyright file="Helper.cs" company="Studio A&T s.r.l.">
//     Author: nicogis
//     Copyright (c) Studio A&T s.r.l. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace FatturazioneElettronica.Helper
{
	using System.IO;
	using System.Xml;
	using System.Xml.Xsl;

	internal static class Helper
    {
		public static string TransformXMLToHTML(string inputXml, string xsltString)
		{
			XslCompiledTransform transform = new XslCompiledTransform();
			using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
			{
				transform.Load(reader);
			}

			Utf8StringWriter results = new Utf8StringWriter();
			using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
			{
				transform.Transform(reader, null, results);
			}

			return results.ToString();
		}
	}
}
