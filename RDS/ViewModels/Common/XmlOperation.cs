using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RDS.ViewModels.Common
{
	public static class XmlOperation
	{
		public static DataSet ReadXmlFile(string filePath)
		{
			using (DataSet result = new DataSet())
			{
				FileInfo fileInfo = new FileInfo(filePath);
				if (fileInfo.Exists)
				{
					try
					{
						result.ReadXml(filePath);
					}
					catch { }
				}
				return result;
			}
		}
	}
}
