using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Setup_Manager.ViewModels
{
	class Initialize : ObservableObject
	{
		//private string _message;
		private readonly ObservableCollection<string> _log = new ObservableCollection<string>();
		//private readonly TextConverter _textConverter
		//= new TextConverter(s => s.ToUpper());
		public void Check()
		{
			_log.Add("Checking installed components...");
		}


		//public string Message
		//{
		//    get { return _message; }
		//    set
		//    {
		//        _message = value;
		//        RaisePropertyChangedEvent("Message");
		//    }
		//}

		public IEnumerable<string> Log
		{
			get { return _log; }
		}


		public ICommand ConvertTextCommand
		{
			get { return new DelegateCommand(Check); }
		}

		/*  public object DataContext { get; private set; */
	}

	//private void CheckText()
	//{
	//    if (string.IsNullOrWhiteSpace(Message)) return;
	//    AddToLog(_textConverter.ConvertText(Message));
	//    Message = string.Empty;
	//}

	//private void AddToLog(string item)
	//{
	//    _log.Add(item);
	//    _log.Add(item);

	//}
}
