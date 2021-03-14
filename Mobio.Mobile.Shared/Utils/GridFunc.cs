using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class GridFunc
	{
		static GridLengthTypeConverter gridLengthTypeConverter = new GridLengthTypeConverter();

		public static void SetRowDefinitions(this Grid grid, params string[] lens)
		{
			grid.RowDefinitions.Clear();
			foreach(var len in lens)
			{
				var rowDefinition = new RowDefinition();
				rowDefinition.Height = (GridLength)gridLengthTypeConverter.ConvertFromInvariantString(len);
				grid.RowDefinitions.Add(rowDefinition);
			}
		}

		public static void SetColumnDefinition(this Grid grid, params string[] lens)
		{
			grid.ColumnDefinitions.Clear();
			foreach (var len in lens)
			{
				var columnDefinition = new ColumnDefinition();
				columnDefinition.Width = (GridLength)gridLengthTypeConverter.ConvertFromInvariantString(len);
				grid.ColumnDefinitions.Add(columnDefinition);
			}
		}

		public static void RemoveAllChildren(this Grid grid)
		{
			var childs = grid.Children.ToArray();
			foreach(var child in childs)
			{
				grid.Children.Remove(child);
			}
		}

	}
}
