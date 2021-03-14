using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OneBuilder.Mobile.ViewModels
{
	public class AllOperations111View : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
