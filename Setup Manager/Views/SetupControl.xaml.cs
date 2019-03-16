using Setup_Manager.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Setup_Manager.Views
{
	public partial class SetupControl
	{
		public SetupControl()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() =>
			{
				var viewModel = (Initialize)DataContext;
				if (viewModel.StartProcessCommand.CanExecute(null))
					viewModel.StartProcessCommand.Execute(null);
				LicenseBox.Selection.Load(new FileStream(@"../../Resources\Files\License.rtf", FileMode.Open), DataFormats.Rtf);
				LicenseBox.Selection.Select(LicenseBox.Selection.Start, LicenseBox.Selection.Start);

				((MainWindow)Application.Current.MainWindow).UpdateLayout();
			}));
		}
	}
}
