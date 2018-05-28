using System.IO;
using System.Xml;
using NUnit.Engine.Addins;

public class NUnit3ToNUnit2Converter
{
    public static void Main(string[] args)
    {
        var xmldoc = new XmlDataDocument();
        var fileStream
            = new FileStream(args[0], FileMode.Open, FileAccess.Read);
        xmldoc.Load(fileStream);
        var xmlnode = xmldoc.GetElementsByTagName("test-run").Item(0);

        var writer = new NUnit2XmlResultWriter();
        writer.WriteResultFile(xmlnode, "TestResultV2.xml");
    }
}
