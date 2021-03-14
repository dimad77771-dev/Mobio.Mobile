using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class ImageSourcesStatic
	{
		public static ImageSourceEx Name2Image(string value)
		{
			var prop = typeof(ImageSourcesStatic).GetProperty(value);
			if (prop == null) throw new ArgumentException("ImageSourcesStatic." + value + " not found");
			var img = (ImageSourceEx)prop.GetValue(null);
			return img;
		}


		public static ImageSourceEx modaldialog_body_back
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("modaldialog_body_back", 0, 27, 27, 27); //@@@
			}
		}

		public static ImageSourceEx modaldialog_title_back
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("modaldialog_title_back", 27, 27, 12, 27);  //@@@
			}
		}


		public static ImageSourceEx BTN_4
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("BTN_4", 17, 17, 17, 17); //@@@
			}
		}

		public static ImageSourceEx BTN_6
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("BTN_6", 13, 14, 13, 14); //@@@
			}
		}

		public static ImageSourceEx MainHeaderV
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("MainHeaderV", 0, 0, 4, 0, false); //@@@
			}
		}

		public static ImageSourceEx timgbackground101
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("timgbackground101", 0, 0, 0, 0); //@@@
			}
		}		

		public static ImageSourceEx modalaction_item_back
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("modalaction_item_back", 20, 20, 20, 20, false); //@@@
			}
		}		

		public static ImageSourceEx modalaction_item_backd
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("modalaction_item_backd", 20, 20, 20, 20, false); //@@@
			}
		}		

		public static ImageSourceEx edittext_back
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("editText_back", 14, 14, 14, 14); //@@@
			}
		}		

		public static ImageSourceEx editText_back_disable
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("editText_back_disable", 14, 14, 14, 14); //@@@
			}
		}		

		public static ImageSourceEx editTextMultiLine_back
		{
			get
			{
				return ImageSourceEx.CreateResizableImage2("editTextMultiLine_back", 8, 8, 8, 8, false); //@@@
			}
		}

		public static ImageSourceEx GridRow1
		{
			get
			{
				return ImageSourceEx.FromFile("GridRow1");
			}
		}



		public static ImageSourceEx GridRowSelected
		{
			get
			{
				return ImageSourceEx.FromFile("GridRowSelected");
			}
		}

		public static ImageSourceEx GridRowMultiselected
		{
			get
			{
				return ImageSourceEx.FromFile("GridRowMultiselected");
			}
		}



		public static ImageSourceEx modalaction_icon_cancel
		{
			get
			{
				return ImageSourceEx.FromFile("modalaction_icon_cancel");
			}
		}		


		public static ImageSourceEx minus_button
		{
			get
			{
				return ImageSourceEx.FromFile("minus_button");
			}
		}	


		public static ImageSourceEx plus_button
		{
			get
			{
				return ImageSourceEx.FromFile("plus_button");
			}
		}	



		public static ImageSourceEx BUT_BACK
		{
			get
			{
				return ImageSourceEx.FromFile("BUT_BACK");
			}
		}

		public static ImageSourceEx BUT_BACKd
		{
			get
			{
				return ImageSourceEx.FromFile("BUT_BACKd");
			}
		}

		public static ImageSourceEx Empty
		{
			get
			{
				return ImageSourceEx.FromFile("Empty");
			}
		}


		public static ImageSourceEx modaldialog_title_icon1
		{
			get
			{
				return ImageSourceEx.FromFile("modaldialog_title_icon1");
			}
		}

		public static ImageSourceEx modaldialog_title_icon2
		{
			get
			{
				return ImageSourceEx.FromFile("modaldialog_title_icon2");
			}
		}

		public static ImageSourceEx background_2
		{
			get
			{
				return ImageSourceEx.FromFile("background_2");
			}
		}
		
		public static ImageSourceEx modalaction_icon_default
		{
			get
			{
				return ImageSourceEx.FromFile("modalaction_icon_default");
			}
		}

		public static ImageSourceEx BUT_ADD
		{
			get
			{
				return ImageSourceEx.FromFile("BUT_ADD");
			}
		}
		

		public static ImageSourceEx BUT_SAVE
		{
			get
			{
				return ImageSourceEx.FromFile("BUT_SAVE");
			}
		}


		public static ImageSourceEx OutlineSubtotals_a
		{
			get
			{
				return ImageSourceEx.FromFile("OutlineSubtotals_a");
			}
		}
		public static ImageSourceEx OutlineSubtotals_b
		{
			get
			{
				return ImageSourceEx.FromFile("OutlineSubtotals_b");
			}
		}
		public static ImageSourceEx PivotTableBlankRowsInsert_a
		{
			get
			{
				return ImageSourceEx.FromFile("PivotTableBlankRowsInsert_a");
			}
		}
		public static ImageSourceEx PivotTableBlankRowsInsert_b
		{
			get
			{
				return ImageSourceEx.FromFile("PivotTableBlankRowsInsert_b");
			}
		}
		public static ImageSourceEx RecordsTotals_a
		{
			get
			{
				return ImageSourceEx.FromFile("RecordsTotals_a");
			}
		}
		public static ImageSourceEx RecordsTotals_b
		{
			get
			{
				return ImageSourceEx.FromFile("RecordsTotals_b");
			}
		}


		public static ImageSourceEx MainMenuWarehouse_a
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuWarehouse_a");
			}
		}
		public static ImageSourceEx MainMenuWarehouse_b
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuWarehouse_b");
			}
		}

		
		public static ImageSourceEx MainMenuSales_a
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuSales_a");
			}
		}
		public static ImageSourceEx MainMenuSales_b
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuSales_b");
			}
		}

		public static ImageSourceEx MainMenuOptions_a
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuOptions_a");
			}
		}
		public static ImageSourceEx MainMenuOptions_b
		{
			get
			{
				return ImageSourceEx.FromFile("MainMenuOptions_b");
			}
		}
		public static ImageSourceEx BUT_SearchScan
		{
			get
			{
				return ImageSourceEx.FromFile("BUT_SearchScan");
			}
		}
		public static ImageSourceEx HomeTitleIcon
		{
			get
			{
				return ImageSourceEx.FromFile("HomeTitleIcon");
			}
		}

		public static ImageSourceEx GroupByBox_a
		{
			get
			{
				return ImageSourceEx.FromFile("GroupByBox_a");
			}
		}
		public static ImageSourceEx GroupByBox_b
		{
			get
			{
				return ImageSourceEx.FromFile("GroupByBox_b");
			}
		}
		
		public static ImageSourceEx FlatList_a
		{
			get
			{
				return ImageSourceEx.FromFile("FlatList_a");
			}
		}
		public static ImageSourceEx FlatList_b
		{
			get
			{
				return ImageSourceEx.FromFile("FlatList_b");
			}
		}

	}
}
