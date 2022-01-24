using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace Vouchers
{
    class XMLManager
    {
        public static string[] SelectNote(string file,
                                          string date)
        {
            string[] elems = new string[]{ "remainder", "probeg", "prihod", "mileage", "consumption" };
            XmlDocument xDoc = new XmlDocument();
            List<string> list = new List<string>();
            xDoc.Load(file);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes == null || xnode.Attributes.Count <= 0) continue;
                XmlNode attr = xnode.Attributes.GetNamedItem("date");
                if (attr == null) continue;
                if (!attr.Value.Equals(date)) continue;
                foreach (var elem in elems)
                {
                    list.Add(xnode[elem].InnerXml.ToString());
                }
                return list.ToArray();
            }
            return new string[] { };
        }
        public static void AddNote(string file,
                                        string parentValue,
                                        string remainderValue,
                                        string probegValue,
                                        string prihodValue,
                                        string mileageValue,
                                        string consumptionValue
                                        )
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);
                XmlElement xRoot = xDoc.DocumentElement;

                XmlElement noteElem = xDoc.CreateElement("note");
                XmlAttribute noteAttr = xDoc.CreateAttribute("date");
                XmlText dateText = xDoc.CreateTextNode(parentValue);

                XmlElement remainderElem = xDoc.CreateElement("remainder");
                XmlText remainderText = xDoc.CreateTextNode(remainderValue);

                XmlElement probegElem = xDoc.CreateElement("probeg");
                XmlText probegText = xDoc.CreateTextNode(probegValue);

                XmlElement prihodElem = xDoc.CreateElement("prihod");
                XmlText prihodText = xDoc.CreateTextNode(prihodValue);

                XmlElement mileageElem = xDoc.CreateElement("mileage");
                XmlText mileageText = xDoc.CreateTextNode(mileageValue);

                XmlElement consumptionElem = xDoc.CreateElement("consumption");
                XmlText consumptionText = xDoc.CreateTextNode(consumptionValue);

                noteAttr.AppendChild(dateText);

                remainderElem.AppendChild(remainderText);
                probegElem.AppendChild(probegText);
                prihodElem.AppendChild(prihodText);
                mileageElem.AppendChild(mileageText);
                consumptionElem.AppendChild(consumptionText);

                noteElem.Attributes.Append(noteAttr);
                noteElem.AppendChild(remainderElem);
                noteElem.AppendChild(probegElem);
                noteElem.AppendChild(prihodElem);
                noteElem.AppendChild(mileageElem);
                noteElem.AppendChild(consumptionElem);

                xRoot.AppendChild(noteElem);
                xDoc.Save(file);
            }
        }
    }
