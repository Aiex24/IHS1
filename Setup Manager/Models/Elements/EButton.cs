using Setup_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup_Manager.Models.Elements
{
	public class EButton : ObservableObject
	{
		private string _content;
		private bool _isEnabled;

		public string Content
		{
			get { return _content; }
			set
			{
				_content = value;
				RaisePropertyChangedEvent("Content");
			}
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				_isEnabled = value;
				RaisePropertyChangedEvent("IsEnabled");
			}
		}

	}
}
