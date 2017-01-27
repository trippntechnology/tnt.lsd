using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TNTObjects;

namespace SerializeTest
{
	using TNTObjList = List<TNTObject>;
	using TNTObjListList = List<List<TNTObject>>;

	class Program
	{
		static void Main(string[] args)
		{
			Point []p = { new Point(10, 10), new Point(20, 20) };
			List<Point> points = p.ToList();
			TNTLine line = new TNTLine(points, Color.Blue);
			TNTObjList objList = new TNTObjList();

			for (int i = 0; i < 2; i++)
			{
				objList.Add(line.Clone());
			}

			TNTCircle circle = new TNTCircle(10, 10, 100, 100, Color.Blue);

			objList.Add(circle.Clone());

			string rtnValue = string.Empty;

			TNTObjListList ll = new TNTObjListList();

			ll.Add(objList);
			ll.Add(objList);

			Type [] expectedTypes = { typeof(TNTLine), typeof(TNTCircle) };

			using (StringWriter sw = new StringWriter())
			using (XmlTextWriter tw = new XmlTextWriter(sw))
			{
				tw.Formatting = Formatting.Indented;

				//XmlSerializer ser = new XmlSerializer(objList.GetType(), expectedTypes);
				//ser.Serialize(tw, objList);
				XmlSerializer ser = new XmlSerializer(ll.GetType(), expectedTypes);
				ser.Serialize(tw, ll);
				rtnValue = sw.ToString();
			}

			using (StringReader sr = new StringReader(rtnValue))
			using (XmlTextReader tr = new XmlTextReader(sr))
			{
				//XmlSerializer deser = new XmlSerializer(objList.GetType(),expectedTypes);
				//TNTObjList ol = (TNTObjList)deser.Deserialize(tr);
				XmlSerializer deser = new XmlSerializer(ll.GetType(), expectedTypes);
				TNTObjListList oll = (TNTObjListList)deser.Deserialize(tr);
			}
		}
	}
}
