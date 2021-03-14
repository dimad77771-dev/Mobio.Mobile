using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace OneBuilder.Mobile
{
	public class GridEx : Grid
	{
		public GridEx()
		{
			this.ColumnSpacing = 0;
			this.RowSpacing = 0;
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
		}


		//protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
		//{
		//	var size = base.OnSizeRequest(widthConstraint, heightConstraint);
		//	return size;
		//}

		//protected SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
		//{
		//	if (!InternalChildren.Any())
		//		return new SizeRequest(new Size(0, 0));

		//	MeasureGrid(widthConstraint, heightConstraint, true);

		//	double columnWidthSum = 0;
		//	double nonStarColumnWidthSum = 0;
		//	for (var index = 0; index < _columns.Count; index++)
		//	{
		//		ColumnDefinition c = _columns[index];
		//		columnWidthSum += c.ActualWidth;
		//		if (!c.Width.IsStar)
		//			nonStarColumnWidthSum += c.ActualWidth;
		//	}
		//	double rowHeightSum = 0;
		//	double nonStarRowHeightSum = 0;
		//	for (var index = 0; index < _rows.Count; index++)
		//	{
		//		RowDefinition r = _rows[index];
		//		rowHeightSum += r.ActualHeight;
		//		if (!r.Height.IsStar)
		//			nonStarRowHeightSum += r.ActualHeight;
		//	}

		//	var request = new Size(columnWidthSum + (_columns.Count - 1) * ColumnSpacing, rowHeightSum + (_rows.Count - 1) * RowSpacing);
		//	var minimum = new Size(nonStarColumnWidthSum + (_columns.Count - 1) * ColumnSpacing, nonStarRowHeightSum + (_rows.Count - 1) * RowSpacing);

		//	var result = new SizeRequest(request, minimum);
		//	return result;
		//}
	}
}
