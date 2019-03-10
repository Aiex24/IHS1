using Setup_Manager.ViewModels;
using System;
using System.Windows;
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
				((MainWindow)Application.Current.MainWindow).UpdateLayout();
			}));
		}
	}
}
