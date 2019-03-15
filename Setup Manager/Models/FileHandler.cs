using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup_Manager.Models
{
	class FileHandler
	{
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

				return e.ToString() ;
			}
			return line;

		}
	}
}
