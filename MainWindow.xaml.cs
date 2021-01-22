using SecurityDashboard.Interfaces;
using SecurityDashboard.Utils;
using SecurityDashboard.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecurityDashboard
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool clicked = false;
		private Point lmAbs = new Point();
		private string view = "";
		
		public MainWindow()
		{
			InitializeComponent();
			ServiceConfig.Initialization();
			this.DataContext = new MainViewModel(RichText);
			view = $"{new string('=',100)}\n{DateTime.Now} : Run {Application.ResourceAssembly.FullName}";
			Log.WriteLog(view);
			//LogTb.Text += view;
		}

		/// <summary>
		/// Create Services for work
		/// </summary>
		ILogService Log => Service.CreateLog();
		/// <summary>
		/// Gets the exception handler.
		/// </summary>
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		public RichTextBox RichText { get; set; }


		// ---------------------- Simple methods ----------------------
		#region----------- Simple methods (close/maximize/minimize and etc.) -----------
		void PnMouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (clicked)
				{
					Point MousePosition = e.GetPosition(this);
					Point MousePositionAbs = new Point();
					MousePositionAbs.X = Convert.ToInt16(this.Left) + MousePosition.X;
					MousePositionAbs.Y = Convert.ToInt16(this.Top) + MousePosition.Y;
					this.Left = this.Left + (MousePositionAbs.X - this.lmAbs.X);
					this.Top = this.Top + (MousePositionAbs.Y - this.lmAbs.Y);
					this.lmAbs = MousePositionAbs;
				}
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			clicked = true;
			this.lmAbs = e.GetPosition(this);
			this.lmAbs.Y = Convert.ToInt16(this.Top) + this.lmAbs.Y;
			this.lmAbs.X = Convert.ToInt16(this.Left) + this.lmAbs.X;
		}
		private void Window_MouseUp(object sender, MouseButtonEventArgs e)
		{
			clicked = false;
		}
		private void btn_Maximize_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Normal)
				WindowState = WindowState.Maximized;
			else
				WindowState = WindowState.Normal;

		}
		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scroll = (ScrollViewer)sender;
			scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
			e.Handled = true;
		}
		private void btn_Minimize_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}
		private void Btn_Exit_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		#endregion

		private void Window_Closed(object sender, EventArgs e)
		{
			view = $"Closed {Application.ResourceAssembly.FullName}\n{new string('=', 100)}\n";
			Log.WriteLog(view);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			view = $"Closing {Application.ResourceAssembly.FullName}\n";
			Log.WriteLog(view);
		}
	}
}
