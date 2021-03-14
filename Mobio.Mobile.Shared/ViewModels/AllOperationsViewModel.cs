using OneBuilder.Model;
using OneBuilder.WebServices;
using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OneBuilder.Mobile.Constants;
using System.Windows.Input;
using ZXing;
using OneBuilder.Mobile.Behaviors;
using DevExpress.XamarinForms.DataGrid;

namespace OneBuilder.Mobile.ViewModels
{
	public class AllOperationsViewModel : PageViewModel
	{
		public CurrentShowModeEnum CurrentShowMode { get; set; }
		public enum CurrentShowModeEnum { List, Filter }
		public ObservableCollection<HeaderClass> HeaderItems { get; set; } = new ObservableCollection<HeaderClass>();
		public HeaderClass HeaderSelectedItem { get; set; }
		public DataGridViewOperationBehaviorManager HeaderItemsManager { get; set; } = new DataGridViewOperationBehaviorManager();
		public ObservableCollection<DetailClass> DetailItems { get; set; } = new ObservableCollection<DetailClass>();
		public DetailClass DetailSelectedItem { get; set; }
		public DataGridViewOperationBehaviorManager DetailItemsManager { get; set; } = new DataGridViewOperationBehaviorManager();
		public String DetailItemsHeaderText { get; set; }
		public Command HeaderTapCommand { get; set; }
		public Command ResetFilterCommand { get; set; }
		public Command PrintCommand { get; set; }
		public Command FilterShowCommand { get; set; }
		public Command SearchCommand { get; set; }
		public Command FilterSubmitCommand { get; set; }
		public Command FilterCancelCommand { get; set; }
		public GetAllTimesheetMaterialRecordsResult AllData { get; set; }
		public Dictionary<Guid?, TimesheetMaterialRecordDTO[]> DictionaryTimesheetMaterialRecords { get; set; }
		public ObservableCollection<String> SearchColumnItems { get; set; }
		public int SearchColumnSelectedIndex { get; set; } = 0;
		public DateTime? SearchDateFrom { get; set; }
		public DateTime? SearchDateTo { get; set; }
		public DateTime SearchDateToStartDate { get; set; } = new DateTime(2019, 1, 1);
		public DateTime SearchDateFromStartDate { get; set; } = new DateTime(2019, 1, 1);
		public String SearchText { get; set; }
		public Int32? InitSerialNum { get; set; }
		public HeaderStyle DataGridHeaderStyle { get; set; }
		public CellStyle DataGridCellStyle { get; set; }
		public SwipeItemStyle DataGridSwipeItemStyle { get; set; }
		public bool IsShowFilter { get; set; } = false;
		public Boolean NavigationBarButton1IsVisible { get; set; } = false;
		public Boolean IsShowCommandBar { get; set; }
		public Boolean IsShowFilterCommandBar { get; set; }
		public Boolean IsShowHeaderAndDetailGrid { get; set; }
		public EmailAddressPickViewModel EmailAddressPick { get; set; } = new EmailAddressPickViewModel();


		public AllOperationsViewModel()
		{
			HeaderTapCommand = CommandFunc.CreateAsync(HeaderTap);
			ResetFilterCommand = CommandFunc.CreateAsync(ResetFilter);
			PrintCommand = CommandFunc.CreateAsync(Print);
			FilterShowCommand = CommandFunc.CreateAsync(FilterShow);
			SearchCommand = CommandFunc.Create(SearchClick);

			FilterSubmitCommand = CommandFunc.CreateAsync(FilterSubmit);
			FilterCancelCommand = CommandFunc.Create(FilterCancel);
		}


		public override async Task Init()
		{
			CustomizeIdeom();

			HeaderTitle = U.TabletMode ? "TRANSACTIONS" : "";
			BuildSearchColumn();
			

			if (InitSerialNum != null)
			{
				SearchText = InitSerialNum.Value.ToString();
				SearchColumnSelectedIndex = SearchColumnItems.IndexOf("Serial #");
			}

			this.PropertyChanged += DoPropertyChanged;

			U.RequestMainThread(async () =>
			{
			});

			CustomizeVisibleAll();
		}

		public override async Task InitAfterBinding()
		{
			await DoFind();

			var swipeItems = HeaderItemsManager.Control.StartSwipeItems;
			swipeItems[0].Tap += async (s, e) => await OnSwipePrint(s, e);
			swipeItems[1].Tap += async (s, e) => await OnSwipeEmail(s, e);
		}

		async Task OnSwipePrint(object sender, SwipeItemTapEventArgs e)
		{
			var header = SwipeItem2HeaderClass(e);
			await PrintReportFunc.PrintOrder(header.Item.RowId, header.Item.HeaderNumber);
		}

		async Task OnSwipeEmail(object sender, SwipeItemTapEventArgs e)
		{
			var header = SwipeItem2HeaderClass(e);
			await EmailAddressPick.OpenPopup(header.Item.RowId, Page);
		}

		HeaderClass SwipeItem2HeaderClass(SwipeItemTapEventArgs e)
		{
			var header = (HeaderClass)e.DataObject;
			return header;
		}

		async Task FilterSubmit()
		{
			SearchDo();
			CurrentShowMode = CurrentShowModeEnum.List;
			CustomizeVisibleAll();
		}

		void FilterCancel()
		{
			CurrentShowMode = CurrentShowModeEnum.List;
			CustomizeVisibleAll();
		}

		void SearchClick()
		{
			SearchDo();
			if (!U.TabletMode)
			{
				FilterCancel();
			}
		}

		void SearchDo()
		{
			var headers = AllData.TimesheetMaterialRecordHeaders
				.Where(q => InHeaderSearch(q))
				.ToArray();

			HeaderItems = headers
				.OrderByDescending(q => q.HeaderNumber)
				.Select(q => new HeaderClass(q, this))
				.ToObservableCollection();
			if (HeaderItems.Any())
			{
				HeaderSelectedItem = HeaderItems[0];
			}
			RefreshDetails();
		}

		bool InHeaderSearch(TimesheetMaterialRecordHeaderDTO header)
		{
			if (SearchDateFrom != null)
			{
				if (!(header.RecordDate != null && header.RecordDate.Value >= SearchDateFrom.Value))
				{
					return false;
				}
			}

			if (SearchDateTo != null)
			{
				if (!(header.RecordDate != null && header.RecordDate.Value <= SearchDateTo.Value))
				{
					return false;
				}
			}

			if (!string.IsNullOrEmpty(SearchText))
			{
				var upperSearch = SearchText.ToUpper();
				var intSearch = default(int?);
				if (Int32.TryParse(SearchText, out int val))
				{
					intSearch = val;
				}

				var column = SearchColumnItems[SearchColumnSelectedIndex];
				var allcolumn = (column == "All");
				var find = false;

				if (allcolumn || column == "#")
				{
					if (intSearch != null)
					{
						if (header.HeaderNumber == intSearch)
						{
							find = true;
						}
					}
				}

				if (allcolumn || column == "Operator")
				{
					if ((header.CreatedByName ?? "").ToUpper().Contains(upperSearch))
					{
						find = true;
					}
				}

				if (allcolumn || column == "Employee")
				{
					if ((header.LabourName ?? "").ToUpper().Contains(upperSearch))
					{
						find = true;
					}
				}

				if (allcolumn || column == "Timesheet")
				{
					if ((header.TimesheetName ?? "").ToUpper().Contains(upperSearch))
					{
						find = true;
					}
				}

				if (allcolumn || column == "Serial #")
				{
					if (intSearch != null)
					{
						if (DictionaryTimesheetMaterialRecords.ContainsKey(header.RowId))
						{
							if (DictionaryTimesheetMaterialRecords[header.RowId].Any(q => q.StockItemSerialNum != null && q.StockItemSerialNum == intSearch))
							{
								find = true;
							}
						}
					}
				}

				if (!find)
				{
					return false;
				}

			}

			return true;
		}

		void BuildSearchColumn()
		{
			SearchColumnItems = new[] { "All", "#", "Operator", "Employee", "Timesheet", "Serial #" }.ToObservableCollection();
		}

		async Task HeaderTap(object row)
		{
			var b = row;
		}

		async Task ResetFilter()
		{
			SearchDateFrom = null;
			SearchDateTo = null;
			SearchText = "";
			SearchDo();
			if (!U.TabletMode)
			{
				FilterCancel();
			}
		}

		async Task FilterShow()
		{
			CurrentShowMode = CurrentShowModeEnum.Filter;
			CustomizeVisibleAll();
		}

		void CustomizeVisibleAll()
		{
			if (U.TabletMode)
			{
				IsShowFilter = true;
				IsShowFilterCommandBar = false;
				IsShowHeaderAndDetailGrid = true;
				IsShowCommandBar = false;
			}
			else
			{
				IsShowFilter = (CurrentShowMode == CurrentShowModeEnum.Filter);
				IsShowFilterCommandBar = (CurrentShowMode == CurrentShowModeEnum.Filter);
				IsShowHeaderAndDetailGrid = (CurrentShowMode == CurrentShowModeEnum.List);
				IsShowCommandBar = (CurrentShowMode == CurrentShowModeEnum.List);
			}
		}


		async Task Print()
		{
			//var ff = Page.FindByName<DataGridView>("headerList");
			//var bb1 = ff.Width;
			//var bb2 = ff.Height;
			//var bb3 = ff.X;
			//var bb4 = ff.Y;

			var headerRowIds = new List<Guid>();
			var manager = HeaderItemsManager.Control;
			for(int r = 0; r < HeaderItems.Count; r++)
			{
				var indx = manager.GetSourceRowIndex(r);
				var headerRowId = HeaderItems[indx].Item.RowId;
				headerRowIds.Add(headerRowId);
			}

			var reportParm = new GenerateFullOrdersReportParm
			{
				TimesheetMaterialRecordHeaderRowIds = headerRowIds.ToArray(),
			};
			if (!string.IsNullOrEmpty(SearchText) && SearchColumnItems[SearchColumnSelectedIndex] == "Serial #")
			{
				if (Int32.TryParse(SearchText, out int serialnum))
				{
					reportParm.SerialNum = serialnum;
				}
			}
			reportParm.SearchText = SearchText ?? "";
			reportParm.SearchColumn = SearchColumnItems[SearchColumnSelectedIndex];
			reportParm.SearchDateFrom = SearchDateFrom;
			reportParm.SearchDateTo = SearchDateTo;

			UIFunc.ShowLoading();
			var bytes = await WebServiceFunc.GenerateFullOrdersReport(U.BusinessRowId, reportParm);
			UIFunc.HideLoading();
			if (bytes == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return;
			}

			await PrintReportFunc.PrintCore(bytes, "Orders.pdf", "Transaction List");
		}


		async Task DoFind()
		{
			UIFunc.ShowLoading();
			var query = new GetAllTimesheetMaterialRecordsQuery();
			var rez = await WebServiceFunc.GetAllTimesheetMaterialRecords(U.BusinessRowId, query);
			if (rez == null)
			{
				await UIFunc.AlertError(U.StandartErrorRetrieveText);
				return;
			}
			UIFunc.HideLoading();

			AllData = rez;
			DictionaryTimesheetMaterialRecords = AllData.TimesheetMaterialRecords.GroupBy(q => q.HeaderRowId).ToDictionary(q => q.Key, q => q.ToArray());

			SearchDo();
		}

		void DoPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(HeaderSelectedItem))
			{
				RefreshDetails();
			}

			if (e.PropertyName == nameof(SearchDateFrom) || e.PropertyName == nameof(SearchDateTo) || e.PropertyName == nameof(SearchColumnSelectedIndex))
			{
				SearchDo();
			}
		}




		void RefreshDetails()
		{
			if (HeaderSelectedItem != null)
			{
				var headerRowId = HeaderSelectedItem.Item.RowId;
				if (DictionaryTimesheetMaterialRecords.ContainsKey(headerRowId))
				{
					DetailItems = DictionaryTimesheetMaterialRecords[headerRowId]
						.Where(q => q.ItemType == StockItemType.ProductItem)
						.OrderBy(q => q.BoxItemSerialNum)
						.ThenBy(q => q.StockItemSerialNum)
						.Select(q => new DetailClass(q, this))
						.ToObservableCollection();
				}
				else
				{
					DetailItems = new ObservableCollection<DetailClass>();
				}
				DetailItemsManager.Control.Columns[0].Caption = "Order #" + HeaderSelectedItem.Item.HeaderNumber;
			}
			else
			{
				DetailItems = new ObservableCollection<DetailClass>();
				DetailItemsManager.Control.Columns[0].Caption = "-";
			}
		}

		void CustomizeIdeom()
		{
			if (!U.TabletMode)
			{
				var headerListGrid = Page.FindByName<Grid>("headerListGrid");
				var detailListGrid = Page.FindByName<Grid>("detailListGrid");
				var grid = (Grid)detailListGrid.Parent;
				var сhildren = grid.Children;
				grid.SetRowDefinitions("3*","*");
				grid.SetColumnDefinition("*");
				сhildren.Remove(headerListGrid);
				сhildren.Remove(detailListGrid);
				сhildren.Add(headerListGrid, 0, 0);
				сhildren.Add(detailListGrid, 0, 1);
			}

			if (!U.TabletMode)
			{
				var grid = Page.FindByName<Grid>("filterGrid");
				var сhildren = grid.Children;
				grid.RemoveAllChildren();
				grid.SetColumnDefinition("Auto", "*");
				grid.SetRowDefinitions("Auto", "Auto", "Auto", "Auto");
				var rownum = 0;
				сhildren.Add(Page.FindByName<View>("labelSearchColumn"), 0, rownum);
				сhildren.Add(Page.FindByName<View>("searchColumn"), 1, rownum);
				rownum++;
				сhildren.Add(Page.FindByName<View>("searchText"), 0, 2, rownum, rownum + 1);
				rownum++;
				сhildren.Add(Page.FindByName<View>("labelDateFrom"), 0, rownum);
				сhildren.Add(Page.FindByName<View>("dateFrom"), 1, rownum);
				rownum++;
				сhildren.Add(Page.FindByName<View>("labelDateTo"), 0, rownum);
				сhildren.Add(Page.FindByName<View>("dateTo"), 1, rownum);

				var margin = new Thickness(0, 15, 10, 0);
				Page.FindByName<View>("searchText").Margin = margin;
				Page.FindByName<View>("labelDateFrom").Margin = margin;
				Page.FindByName<View>("dateFrom").Margin = margin;
				Page.FindByName<View>("labelDateTo").Margin = margin;
				Page.FindByName<View>("dateTo").Margin = margin;
			}


			IsShowCommandBar = !U.TabletMode;

			DataGridHeaderStyle = U.TabletMode ?
				new HeaderStyle { Padding = new Size(1, 10), FontAttributes = FontAttributes.Bold, FontSize = 11 } :
				new HeaderStyle { Padding = new Size(1, 3), FontSize = 8 };

			DataGridCellStyle = U.TabletMode ?
				new CellStyle { Padding = new Size(1, 3), FontSize = 10 } :
				new CellStyle { Padding = new Size(1, 3), FontSize = 8 };

			DataGridSwipeItemStyle = U.TabletMode ?
				new SwipeItemStyle { FontSize = 10 } :
				new SwipeItemStyle { FontSize = 8, Width = 70 };

			var headerList = Page.FindByName<DataGridView>("headerList");
			var columns = headerList.Columns;
			columns["Item.HeaderNumber"].Width = U.TabletMode ?	55 : 30;
			columns["Item.RecordDate"].Width = U.TabletMode ?	95 : 40;
			columns["Item.RecordTypeString"].Width = U.TabletMode ?	60 : 35;
			columns["Item.CreatedByName"].Width = U.TabletMode ? GridLength.Star : GridLength.Star;
			columns["Item.LabourName"].Width = U.TabletMode ? GridLength.Star :	GridLength.Star;
			columns["Item.TimesheetFullName"].Width = U.TabletMode ? GridLength.Star :	GridLength.Star;
		}


		public class HeaderClass
		{
			public TimesheetMaterialRecordHeaderDTO Item { get; set; }
			public AllOperationsViewModel Parent { get; set; }
			//public Command SwipeCommand { get; set; }
			public String Key { get; set; }

			public HeaderClass(TimesheetMaterialRecordHeaderDTO item, AllOperationsViewModel parent)
			{
				Item = item;
				Key = Item.LabourName;
				Parent = parent;
				//SwipeCommand = CommandFunc.CreateAsync(Swipe);
			}

			async Task Swipe()
			{
				//await Parent.Swipe(this);
			}
		}

		public class DetailClass
		{
			public TimesheetMaterialRecordDTO Item { get; set; }
			public AllOperationsViewModel Parent { get; set; }
			public Command SelectItemCommand { get; set; }
			public String Key { get; set; }

			public DetailClass(TimesheetMaterialRecordDTO item, AllOperationsViewModel parent)
			{
				Item = item;
				Key = Item.LabourName;
				Parent = parent;
				SelectItemCommand = CommandFunc.CreateAsync(Select);
			}

			async Task Select()
			{
				//await Parent.SelectAllocatedItem(this);
			}
		}



	}
}
