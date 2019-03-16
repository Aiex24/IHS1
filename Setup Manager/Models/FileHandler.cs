using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Setup_Manager.Models
{
	class FileHandler
	{

		public static TextRange GetRichEditText(string filename)
		{
			FlowDocument flowDocument = new FlowDocument();
			TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);

			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				textRange.Load(fileStream, DataFormats.Rtf);
			}
			return textRange;
		}

		public static string GetText(string file)
		{
			string line = "";
			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					line += sr.ReadToEnd();
				}
			}
			catch (Exception e)
			{

				return e.ToString();
			}
			return line;

		}
	}
}
