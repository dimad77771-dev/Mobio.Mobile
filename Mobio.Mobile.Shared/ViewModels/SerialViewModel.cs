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
using System.Windows.Input;
using Xamarin.Forms;
using HtmlAgilityPack;
using Telerik.XamarinForms.DataControls.ListView;
using OneBuilder.Mobile.Views;
using Telerik.XamarinForms.DataControls;
using OneBuilder.Mobile.Behaviors;
using System.Diagnostics;
using Telerik.XamarinForms.Primitives;
using Telerik.XamarinForms.Input;
using Newtonsoft.Json;

namespace OneBuilder.Mobile.ViewModels
{
	public class SerialViewModel : PageViewModel
	{
		public Command CalcDigitCommand { get; set; }
		public Command CalcBackspaceCommand { get; set; }
		public Command CalcClearCommand { get; set; }
		public Command CalcPlusCommand { get; set; }
		public Command ScanOneBarcodeCommand { get; set; }
		public Command ScanMultipleBarcodeCommand { get; set; }
		public Command SubmitReceivedItemsCommand { get; set; }
		public Command SubmitReturnedItemsCommand { get; set; }
		public Command SubmitUseItemsCommand { get; set; }
		public Command SubmitCommand { get; set; }
		public Command CancelCommand { get; set; }
		public Command CloseHistoryCommand { get; set; }
		public Command ScanEditOpenCommand { get; set; }
		public Command NavigationBarButton1Command { get; set; }
		public string CalcValue { get; set; } = "";
		public string SelectedSerialNumbersLabel { get; set; }
		public string PickerSerialNumbersLabel { get; set; }
		public bool IsShowFoundSerialPicker { get; set; } = false;
		public CommandMode CurrentCommandMode { get; set; } = CommandMode.None;
		public bool IsShowSerialCommandBar { get; set; } = false;
		public bool IsShowInspectionPickerBar { get; set; } = false;
		public bool IsShowSelectEmployee { get; set; } = false;
		public bool IsShowSubmitBar { get; set; } = false;
		public bool IsShowAllAllocatedList { get; set; } = false;
		public FindStockItemsDTO HistoryItem { get; set; } = null;
		public bool IsShowHistoryList { get; set; } = false;
		public string HistoryHeaderText { get; set; } = "";
		public string BarcodeEditorText { get; set; } = "";
		public ObservableCollection<SelectedSerialNumberItem> SelectedSerialNumberItems { get; set; } = new ObservableCollection<SelectedSerialNumberItem>();
		public SelectedSerialNumberItem SelectedSelectedSerialNumberItem { get; set;}
		public SelectedSerialNumberItem SelectedSerialNumberScrollToRow { get; set; }
		public ObservableCollection<SelectedSerialNumberItem> PickSerialNumberItems { get; set; } = new ObservableCollection<SelectedSerialNumberItem>();
		public SelectedSerialNumberItem PickSerialNumberSelectedItem { get; set; }

		public ObservableCollection<InspectionDTO> PickInspectionItems { get; set; }
		public ObservableCollection<ProjectDTO> PickProjectItems { get; set; }
		public ObservableCollection<TimesheetDTO> PickTimesheetItems { get; set; }
		public ObservableCollection<TimesheetDTO> AllPickTimesheetItems { get; set; }
		public ObservableCollection<ProposalNumberDTO> AllProposalNumberItems { get; set; }
		public UserInfoDTO[] UserInfoItems { get; set; }
		public bool InChangePickProposalNumberItem { get; set; }

		public InspectionDTO PickInspectionSelectedItem { get; set; }
		public ProjectDTO PickProjectSelectedItem { get; set; }
		public ProposalNumberDTO PickProposalNumberItem { get; set; }
		public TimesheetDTO PickTimesheetSelectedItem { get; set; }

		public ObservableCollection<TimesheetMaterialRecordItem> AllAllocatedItems { get; set; } = new ObservableCollection<TimesheetMaterialRecordItem>();
		public TimesheetMaterialRecordItem AllAllocatedSelectedItem { get; set; }
		public TimesheetMaterialRecordItem AllAllocatedScrollToRow { get; set; }
		public EditorExOperationBehaviorManager BarcodeEditorManager { get; set; } = new EditorExOperationBehaviorManager();
		public RadListViewOperationBehaviorManager AllAllocatedManager { get; set; } = new RadListViewOperationBehaviorManager();
		public RadListViewOperationBehaviorManager HistoryListManager { get; set; } = new RadListViewOperationBehaviorManager();
		public RadListViewOperationBehaviorManager SelectedSerialNumberManager { get; set; } = new RadListViewOperationBehaviorManager();
		public RadTabViewOperationBehaviorManager TabViewManager { get; set; } = new RadTabViewOperationBehaviorManager();
		public ObservableCollection<LabourDTO> EmployeeItems { get; set; }
		public RadListPickerOperationBehaviorManager EmployeeItemsManager { get; set; } = new RadListPickerOperationBehaviorManager();
		public RadListPickerOperationBehaviorManager PickProjectItemsManager { get; set; } = new RadListPickerOperationBehaviorManager();
		public RadListPickerOperationBehaviorManager PickProposalNumberManager { get; set; } = new RadListPickerOperationBehaviorManager();
		public RadListPickerOperationBehaviorManager PickTimesheetItemsManager { get; set; } = new RadListPickerOperationBehaviorManager();
		public LabourDTO EmployeeSelectedItem { get; set; }
		public String SelectEmployeeText { get; set; } = "Select Employee";
		public ObservableCollection<TimesheetMaterialRecordItem> HistoryListItems { get; set; } = new ObservableCollection<TimesheetMaterialRecordItem>();
		public TimesheetMaterialRecordItem HistoryListSelectedItem { get; set; }
		public Boolean NavigationBarButton1IsVisible { get; set; } = true;
		public String NavigationBarButton1Text { get; set; } = "History";
		public TimesheetMaterialRecordHeaderDTO LastUpdateTimesheetMaterialRecordHeader { get; set; }
		public bool IsShowLastUpdateInfo { get; set; } = false;
		public Command PrintLastOrderCommand { get; set; }
		public Command EmailLastOrderCommand { get; set; }
		public string PrintLastOrderText { get; set; }
		public string EmailLastOrderText { get; set; }
		public bool IsLogoVisible { get; set; } = true;
		public object SelectedTab { get; set; }
		public string ItemsTabText { get; set; }
		public double ScanBarcodeButtonFontSize { get; set; }
		public Thickness ScanBarcodeButtonPadding { get; set; }
		public Thickness ScanBarcodeButtonMargin { get; set; }
		public Thickness TabViewMargin { get; set; }
		public GridLength ScanBarcodeRowHeight { get; set; }
		public GridLength CalcValueRowHeight { get; set; }
		public double CalcValueFontSize { get; set; }
		public EmailAddressPickViewModel EmailAddressPick { get; set; } = new EmailAddressPickViewModel();



		public SerialViewModel()
		{
			//HeaderTitle = U.LabourName + "'s Timesheet";

			CalcDigitCommand = CommandFunc.Create(CalcDigit);
			CalcBackspaceCommand = CommandFunc.Create(CalcBackspace);
			CalcClearCommand = CommandFunc.Create(CalcClear);
			CalcPlusCommand = CommandFunc.CreateAsync(CalcPlus, CanCalcPlusCommand);
			ScanOneBarcodeCommand = CommandFunc.CreateAsync(ScanOneBarcode);
			ScanMultipleBarcodeCommand = CommandFunc.CreateAsync(ScanMultipleBarcode);
			SubmitReceivedItemsCommand = CommandFunc.CreateAsync(SubmitReceivedItems, CanSubmitReceivedItemsCommand);
			SubmitReturnedItemsCommand = CommandFunc.CreateAsync(SubmitReturnedItems, CanSubmitReturnedItemsCommand);
			SubmitUseItemsCommand = CommandFunc.CreateAsync(SubmitUseItems, CanSubmitUseItemsCommand);
			SubmitCommand = CommandFunc.CreateAsync(Submit, CanSubmitCommand);
			CancelCommand = CommandFunc.CreateAsync(Cancel);
			CloseHistoryCommand = CommandFunc.CreateAsync(CloseHistory);
			ScanEditOpenCommand = CommandFunc.CreateAsync(ScanEditOpen);
			NavigationBarButton1Command = CommandFunc.CreateAsync(OpenAllOperations);
			PrintLastOrderCommand = CommandFunc.CreateAsync(PrintLastOrder);
			EmailLastOrderCommand = CommandFunc.CreateAsync(EmailLastOrder);
		}


		public override async Task Init()
		{
			HeaderTitle = "";
			IsBackVisible = false;

			RefreshAllVisible();
			this.PropertyChanged += DoPropertyChanged;
		}

		public override async Task InitAfterBinding()
		{
			CustomizeIdeom();
			U.RequestMainThread(async () =>
			{
				await Task.Yield();
				await UnhandledExceptionProccesing.SendErrorServer();
				await RecentlySubmittedUpdate(isFirst: true);

				//var serialTab2View = Page.FindByName<SerialTab2View>("serialTab2View");
				//var radButtonAllocate = serialTab2View.FindByName<RadButton>("radButtonAllocate");
			});
		}

		public SkiaSharp.Views.Forms.SKBitmapImageSource RRBackgroundImage
		{
			get
			{
				var helloBitmap = new SkiaSharp.SKBitmap(1000, 1000);
				var bitmapCanvas = new SkiaSharp.SKCanvas(helloBitmap);

				bitmapCanvas.Clear();
				

				var paint = new SkiaSharp.SKPaint();
				paint.Color = new SkiaSharp.SKColor(0, 127, 56, 127);
				bitmapCanvas.DrawRect(100, 100, 800, 800, paint);

				var textPaint = new SkiaSharp.SKPaint { TextSize = 48 };
				bitmapCanvas.DrawText("HHHHHHHHH", 300, 300, textPaint);

				//textPaint.Dispose();
				//helloBitmap.Dispose();

				//var ww = radButtonAllocate.Width;
				//var hh = radButtonAllocate.Height;
				var imgsrc = new SkiaSharp.Views.Forms.SKBitmapImageSource { Bitmap = helloBitmap };
				return imgsrc;
			}
		}


		void CalcDigit(string digit)
		{
			CalcValue += digit;
			CalcPlusCommand.ChangeCanExecute();
		}

		void CalcBackspace()
		{
			if (CalcValue.Length > 0)
			{
				CalcValue = CalcValue.Substring(0, CalcValue.Length - 1);
			}
			CalcPlusCommand.ChangeCanExecute();
		}

		void CalcClear()
		{
			CalcValue = "";
			CalcPlusCommand.ChangeCanExecute();
		}

		async Task CalcPlus()
		{
			if (string.IsNullOrEmpty(CalcValue))
			{
				return;
			}

			UIFunc.ShowLoading();
			var rez = await WebServiceFunc.FindStockItemsBySerialNumber(U.BusinessRowId, serialNumber: CalcValue);
			if (rez == null)
			{
				await UIFunc.AlertError(U.StandartErrorRetrieveText);
				return;
			}
			UIFunc.HideLoading();

			if (rez.Any())
			{
				if (rez.Length == 1)
				{
					AddSerialNumber(new SelectedSerialNumberItem(rez[0], this));
					PickSerialNumberItems.Clear();
				}
				else
				{
					var items = rez.Select(q => new SelectedSerialNumberItem(q, this)).OrderBy(q => q.Item.Name).ToArray();
					PickSerialNumberSelectedItem = null;
					PickSerialNumberItems = new ObservableCollection<SelectedSerialNumberItem>(items);
				}

				CalcValue = "";
				RefreshAllVisible();
			}
		}
		bool CanCalcPlusCommand()
		{
			return (!string.IsNullOrEmpty(CalcValue));
		}

		void RefreshPickTimesheetItems()
		{
			if (PickProjectSelectedItem != null)
			{
				PickTimesheetItems = AllPickTimesheetItems
					.Where(q => q.ProjectRowId == PickProjectSelectedItem.RowId)
					.OrderBy(q => q.StartDate)
					.ToObservableCollection();
			}
			else
			{
				PickTimesheetItems = new ObservableCollection<TimesheetDTO>();
			}

			if (PickTimesheetSelectedItem != null)
			{
				if (!PickTimesheetItems.Any(q => q.RowId == PickTimesheetSelectedItem.RowId))
				{
					PickTimesheetSelectedItem = null;
				}
			}
		}


		void AddSerialNumber(SelectedSerialNumberItem item)
		{
			if (!SelectedSerialNumberItems.Any(q => q.Item.StockItemRowId == item.Item.StockItemRowId))
			{
				var item0 = item;
				SelectedSerialNumberItems.Add(item);

				var childs = item.Item.ChildItems.OrderBy(q => q.SerialNum).ToArray();
				foreach (var child in childs)
				{
					var existsChilds = SelectedSerialNumberItems.Where(q => q.Item.StockItemRowId == child.StockItemRowId).ToArray();
					SelectedSerialNumberItems.RemoveRange(existsChilds);

					var childitem = new SelectedSerialNumberItem(child, this)
					{
						InBox = true,
						IsLastInBox = (child == childs.Last()),
					};
					SelectedSerialNumberItems.Add(childitem);
				}

				IsShowLastUpdateInfo = false;
				RefreshAllVisible();
				U.RequestMainThread(async () =>
				{
					//await Task.Delay(1000);
					await Task.Yield();
					//SelectedSelectedSerialNumberItem = item0;
					SelectedSerialNumberScrollToRow = item0;
					SelectedSerialNumberScrollToRow = null;
					await Task.Delay(100);
					await Task.Yield();
					SelectedSerialNumberScrollToRow = item0;
				});
			}
		}

		private void DoPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(PickSerialNumberSelectedItem))
			{
				if (PickSerialNumberSelectedItem != null)
				{
					AddSerialNumber(PickSerialNumberSelectedItem);
					PickSerialNumberItems.Clear();
					RefreshAllVisible();
				}
			}

			if (e.PropertyName == nameof(EmployeeSelectedItem) || e.PropertyName == nameof(PickInspectionSelectedItem) 
				|| e.PropertyName == nameof(PickProjectSelectedItem) || e.PropertyName == nameof(PickTimesheetSelectedItem))
			{
				RefreshAllVisible();
			}

			if (e.PropertyName == nameof(SelectedTab))
			{
				OnSelectedTabChange();
			}

			//if (e.PropertyName == nameof(EmployeeSelectedItem))
			//{
			//	if (CurrentCommandMode == CommandMode.Use)
			//	{
			//		if (PickProjectSelectedItem == null)
			//		{
			//			PickProjectItemsManager.ToggleCommand();
			//		}
			//	}
			//}

			if (e.PropertyName == nameof(PickProjectSelectedItem))
			{
				RefreshPickTimesheetItems();
				if (PickTimesheetItems.Count == 1)
				{
					PickTimesheetSelectedItem = PickTimesheetItems[0];
				}
				else if (PickTimesheetItems.Count > 1)
				{
					if (!InChangePickProposalNumberItem)
					{
						PickTimesheetItemsManager.ToggleCommand();
					}
				}
			}


			if (e.PropertyName == nameof(PickProposalNumberItem))
			{
				if (PickProposalNumberItem != null)
				{
					InChangePickProposalNumberItem = true;
					PickProjectSelectedItem = PickProjectItems.FirstOrDefault(q => q.RowId == PickProposalNumberItem.ProjectRowId);
					if (PickProjectSelectedItem != null)
					{
						PickTimesheetSelectedItem = PickTimesheetItems.FirstOrDefault(q => q.RowId == PickProposalNumberItem.TimesheetRowId);
					}
					InChangePickProposalNumberItem = false;
				}
			}

			if (e.PropertyName == nameof(BarcodeEditorText))
			{
				U.RequestMainThread(async () => await OnBarcodeEditorTextChange());
			}
		}

		async Task OnBarcodeEditorTextChange()
		{
			var btext = BarcodeEditorText;
			btext = (btext ?? "").Replace("\r", "");
			if (btext.EndsWith("\n"))
			{
				var lines = btext.Substring(0, btext.Length - 1).Split("\n");
				if (lines.Length > 0)
				{
					var line = lines.Last();
					line = string.Join("", line.Where(q => char.IsDigit(q)));
					if (!string.IsNullOrEmpty(line))
					{
						//await UIFunc.AlertInfo("line=" + line);
						await OnScanCodeCore(line);
					}
				}
			}
		}

		async Task ScanOneBarcode()
		{
			await ScanBarcode(isMultiple: false);
		}

		async Task ScanMultipleBarcode()
		{
			await ScanBarcode(isMultiple: true);
		}

		async Task ScanBarcode(bool isMultiple)
		{
			var vmodel = new ScanBarcodeViewModel
			{
				ParentView = this,
				IsMultiple = isMultiple,
			};
			await NavFunc.NavigateToAsync(vmodel);

			//this.SubscribeOnClosed(vmodel, async () =>
			//{
			//	if (vmodel.IsCommit)
			//	{
			//		await OnScanCodeCore(vmodel.ScanCode);
			//	}
			//});
		}

		bool IsValidSelectedSerialNumberItems()
		{
			return (SelectedSerialNumberItems.Any() && SelectedSerialNumberItems.All(q => !q.IsRedBorder));
		}

		bool isAllocatedTabOpened = false;
		void OnSelectedTabChange()
		{
			if ((SelectedTab as TabViewItem).Content is SerialTab3View)
			{
				if (!isAllocatedTabOpened)
				{
					isAllocatedTabOpened = true;

					U.RequestMainThread(async () =>
					{
						await Task.Yield();
						AllAllocatedManager.CollapseAll();
					});
				}
			}

			if ((SelectedTab as TabViewItem).Content is SerialTab2View)
			{
				U.RequestMainThread(async () =>
				{
					await Task.Yield();
					//var editor = BarcodeEditorManager.Control;
					//editor.BecomeFirstResponder();
				});
			}

		}

		async Task SubmitReceivedItems()
		{
			await LoadLookups();
			PickInspectionSelectedItem = null;
			EmployeeSelectedItem = GetDefaultLabour();
			CurrentCommandMode = CommandMode.Receive;
			RefreshAllVisible();
			EmployeeItemsManager.ToggleCommand();
		}
		bool CanSubmitReceivedItemsCommand()
		{
			return IsValidSelectedSerialNumberItems();
		}

		async Task SubmitReturnedItems()
		{
			UIFunc.ShowLoading(U.StandartUpdatingText);
			var items = SelectedSerialNumberItems.Where(q => q.IsIncluded).Select(q => new FindStockItemsDTO
			{
				StockItemRowId = q.Item.StockItemRowId,
				BoxItemRowId = q.Item.BoxItemRowId,
				Name = q.Item.Name
			}).ToArray();
			var laborRowId = GetSingleAllocatedLaborRowId();
			if (laborRowId == null)
			{
				await UIFunc.AlertError(U.InternalError);
				return;
			}
			var rez = await WebServiceFunc.SubmitMaterialRecordReturnedItems(laborRowId.Value, U.UserRowId, items);
			if (rez == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return;
			}
			LastUpdateTimesheetMaterialRecordHeader = rez;
			await AfterSuccessOperation();
			UIFunc.HideLoading();
		}
		bool CanSubmitReturnedItemsCommand()
		{
			return IsValidSelectedSerialNumberItems() && GetSingleAllocatedLaborRowId() != null;
		}

		Guid? GetSingleAllocatedLaborRowId()
		{
			var rowIds = SelectedSerialNumberItems
				.Where(q => q.Item.StockStatus == TimesheetMaterialStatus.Allocated)
				.Select(q => q.Item.LaborRowId)
				.Distinct()
				.ToArray();
			if (rowIds.Length != 1)
			{
				return null;
			}
			return rowIds[0];
		}

		async Task SubmitUseItems()
		{
			if (!await LoadLookups()) return;
			EmployeeSelectedItem = GetDefaultLabour();
			PickProjectSelectedItem = null;
			PickProposalNumberItem = null;
			PickTimesheetSelectedItem = null;
			PickInspectionSelectedItem = null;
			CurrentCommandMode = CommandMode.Use;
			RefreshAllVisible();
			StartTimesheetWizard();
		}
		bool CanSubmitUseItemsCommand()
		{
			return IsValidSelectedSerialNumberItems();
		}

		LabourDTO GetDefaultLabour()
		{
			if (UserInfoItems != null)
			{
				var userrow = UserInfoItems.FirstOrDefault(q => q.RowId == U.UserRowId);
				if (userrow != null)
				{
					var item = EmployeeItems.FirstOrDefault(q => (q.Email ?? "").ToLower() == (userrow.Email ?? "").ToLower());
					return item;
				}
			}

			return null;
		}

		void StartTimesheetWizard()
		{
			EmployeeItemsManager.ToggleCommand();
		}

		async Task<bool> LoadLookups()
		{
			if (PickInspectionItems == null || EmployeeItems == null || PickProjectItems == null)
			{
				UIFunc.ShowLoading(U.StandartUpdatingText);
				var task1 = WebServiceFunc.GetAllInspections(U.BusinessRowId);
				var task2 = WebServiceFunc.GetAllLabours(U.BusinessRowId);
				var task3 = WebServiceFunc.GetAllProjects(U.BusinessRowId);
				var task4 = WebServiceFunc.GetAllTimesheets(U.BusinessRowId);
				var task5 = WebServiceFunc.GetAllUsers(U.BusinessRowId);
				var task6 = WebServiceFunc.GetAllProposalNumbers(U.BusinessRowId);
				await Task.WhenAll(task1, task2, task3, task4, task5, task6);
				if (task1.Result == null || task2.Result == null || task3.Result == null || task4.Result == null || task5.Result == null || task6.Result == null)
				{
					await UIFunc.AlertError(U.StandartErrorUpdateText);
					return false;
				}
				UIFunc.HideLoading();

				PickInspectionItems = task1.Result.OrderBy(q => q.Name).ToObservableCollection();
				EmployeeItems = task2.Result.OrderBy(q => q.Name).ToObservableCollection();
				PickProjectItems = task3.Result.OrderBy(q => q.Name).ToObservableCollection();
				AllPickTimesheetItems = task4.Result.OrderBy(q => q.Name).ToObservableCollection();
				AllProposalNumberItems = task6.Result.OrderBy(q => q.ProposalNumber).ToObservableCollection();
				UserInfoItems = task5.Result;
				PickProjectItems = PickProjectItems.Where(q => AllPickTimesheetItems.Any(z => z.ProjectRowId == q.RowId)).ToObservableCollection();
				RefreshPickTimesheetItems();
			}
			return true;
		}


		async Task Submit()
		{
			UIFunc.ShowLoading(U.StandartUpdatingText);
			var items = SelectedSerialNumberItems.Where(q => q.IsIncluded).Where(q => q.IsIncluded).Select(q => new FindStockItemsDTO
			{
				StockItemRowId = q.Item.StockItemRowId,
				BoxItemRowId = q.Item.BoxItemRowId,
				Name = q.Item.Name
			}).ToArray();

			if (CurrentCommandMode == CommandMode.Use)
			{
				var rez = await WebServiceFunc.SubmitMaterialRecordUsedItems(EmployeeSelectedItem.RowId, U.UserRowId, PickTimesheetSelectedItem.RowId, items);
				if (rez == null)
				{
					await UIFunc.AlertError(U.StandartErrorUpdateText);
					return;
				}
				LastUpdateTimesheetMaterialRecordHeader = rez;
			}
			else if (CurrentCommandMode == CommandMode.Receive)
			{
				var rez = await WebServiceFunc.SubmitMaterialRecordReceivedItems(EmployeeSelectedItem.RowId, U.UserRowId, items);
				if (rez == null)
				{
					await UIFunc.AlertError(U.StandartErrorUpdateText);
					return;
				}
				LastUpdateTimesheetMaterialRecordHeader = rez;
			}

			await AfterSuccessOperation();
			UIFunc.HideLoading();
		}
		bool CanSubmitCommand()
		{
			if (CurrentCommandMode == CommandMode.Receive)
			{
				return (EmployeeSelectedItem != null && IsValidSelectedSerialNumberItems());
			}
			else if (CurrentCommandMode == CommandMode.Use)
			{
				return (PickTimesheetSelectedItem != null && EmployeeSelectedItem != null && IsValidSelectedSerialNumberItems());
			}
			else
			{
				return false;
			}
		}

		async Task AfterSuccessOperation()
		{
			//var page = Page as SerialView;
			//var bb = page.radListView.GroupDescriptors;
			//var data = page.radListView.GetDataView();
			//var rootGroups = data.GetGroups();

			//var b = bb[0];
			//PropertyGroupDescriptor aaa;
			//b.Pro


			await RecentlySubmittedUpdate(isFirst: false);
			IsShowLastUpdateInfo = true;
			SelectedSerialNumberItems.Clear();
			RefreshAllVisible();
		}


		async Task Cancel()
		{
			CurrentCommandMode = CommandMode.None;
			RefreshAllVisible();
		}

		async Task CloseHistory()
		{
			HistoryItem = null;
			RefreshAllVisible();
		}

		async Task ScanEditOpen()
		{
			var tabview = TabViewManager.Control;
			tabview.SelectedItem = tabview.Items[1];
			U.RequestMainThread(async () =>
			{
				await Task.Yield();
				BarcodeEditorManager.Control.BecomeFirstResponder();
			});
		}



		public async Task OnScanCodeCore(string scancode)
		{
			CalcValue = (scancode ?? "").Replace("SN", "").Trim();
			await CalcPlus();
		}

		async Task DeleteItem(SelectedSerialNumberItem row)
		{
			//await Task.Delay(1000);
			SelectedSerialNumberManager.EndItemSwipe();

			if (row.InBox)
			{
				row.IsIncluded = !row.IsIncluded;
			}
			else if (row.IsBoxItem)
			{
				SelectedSerialNumberItems.RemoveRange(q => q.Item.BoxItemRowId == row.Item.StockItemRowId);
				SelectedSerialNumberItems.Remove(row);
			}
			else
			{
				SelectedSerialNumberItems.Remove(row);
			}
			
			RefreshAllVisible();
		}

		async Task ShowHistoryItem(SelectedSerialNumberItem row)
		{
			var serialNum = row.Item.SerialNum;
			var vmodel = new AllOperationsViewModel { InitSerialNum = serialNum };
			await NavFunc.NavigateToAsync(vmodel);

			//await Task.Delay(1000);
			SelectedSerialNumberManager.EndItemSwipe();

			//UIFunc.ShowLoading();
			//var stockItemRowId = row.Item.RowId;
			//var rows = await WebServiceFunc.GetTimesheetMaterialRecordHistory(U.BusinessRowId, stockItemRowId);
			//if (rows == null)
			//{
			//	await UIFunc.AlertError(U.StandartErrorUpdateText);
			//	return;
			//}
			//UIFunc.HideLoading();

			//HistoryItem = row.Item;
			//HistoryHeaderText = "History \"" + HistoryItem.Name + "\"";         // --- 123321 ---- END";
			//HistoryListItems = rows.OrderByDescending(q => q.SequenceNo).Select(q => new TimesheetMaterialRecordItem(q, this)).ToObservableCollection();
			//RefreshAllVisible();
		}
		

		async Task SelectAllocatedItem(TimesheetMaterialRecordItem row)
		{
			AllAllocatedManager.EndItemSwipe();

			//var fitem = new FindStockItemsDTO
			//{
			//	StockItemRowId = row.Item.StockItemRowId,
			//	BoxItemRowId = row.Item.BoxItemRowId,
			//	ItemType = row.Item.ItemType,
			//	Name = row.Item.StockItemName,
			//	SerialNum = row.Item.StockItemSerialNum,
			//	LaborRowId = row.Item.LabourRowId,
			//	ShortInfo = row.Item.ShortInfo,
			//	StockStatus = row.Item.StockStatus,
			//};
			//AddSerialNumber(new SelectedSerialNumberItem(fitem, this));
			//RefreshAllVisible();


			UIFunc.ShowLoading();
			var rez = await WebServiceFunc.FindStockItemsBySerialNumber(U.BusinessRowId, stockItemRowId: row.Item.StockItemRowId);
			if (rez == null)
			{
				await UIFunc.AlertError(U.StandartErrorRetrieveText);
				return;
			}
			UIFunc.HideLoading();

			if (rez.Any())
			{
				AddSerialNumber(new SelectedSerialNumberItem(rez[0], this));
				RefreshAllVisible();
			}


		}



		async Task RecentlySubmittedUpdate(bool isFirst)
		{
			UIFunc.ShowLoading();
			var rows = await WebServiceFunc.GetAllocatedSubmitMaterialRecords(U.BusinessRowId);
			if (rows == null)
			{
				await UIFunc.AlertError(U.StandartErrorUpdateText);
				return;
			}
			UIFunc.HideLoading();

			var arows = new List<TimesheetMaterialRecordItem>();
			var labours = rows.Select(q => q.LabourName).Distinct().OrderBy(q => q).ToArray();
			foreach(var labour in labours)
			{
				var lrows = rows.Where(q => q.LabourName == labour).ToArray();
				var boxes = lrows.Where(q => q.ItemType == StockItemType.Box).OrderBy(q => q.StockItemSerialNum).ToArray();
				foreach (var box in boxes)
				{
					arows.Add(new TimesheetMaterialRecordItem(box, this));
					var childs = lrows
						.Where(q => q.BoxItemRowId == box.StockItemRowId && q.ItemType == StockItemType.ProductItem)
						.OrderBy(q => q.StockItemSerialNum)
						.Select(q => new TimesheetMaterialRecordItem(q, this) { InBox = true })
						.ToArray();
					arows.AddRange(childs);
				}
				arows.AddRange(lrows
					.Where(q => q.BoxItemRowId == null && q.ItemType == StockItemType.ProductItem)
					.OrderBy(q => q.StockItemSerialNum)
					.Select(q => new TimesheetMaterialRecordItem(q, this)));
			}

			AllAllocatedItems = arows.ToObservableCollection();
			if (isFirst)
			{
				if (U.TabletMode)
				{
					//await Task.Delay(4000);
					await Task.Yield();
					AllAllocatedManager.CollapseAll();
				}
			}
		}

		string GetLabelSelectedSerialNumberItems()
		{
			var boxCount = SelectedSerialNumberItems.Count(q => q.IsBoxItem && q.IsIncluded);
			var itemCount = SelectedSerialNumberItems.Count(q => !q.IsBoxItem && q.IsIncluded);
			return itemCount + (boxCount == 0 ? "" : boxCount == 1 ? " + 1 box" : " + " + boxCount + " boxes");
		}

		void RefreshAllVisible()
		{
			SelectedSerialNumbersLabel = "Selected S/N(" + GetLabelSelectedSerialNumberItems() + "):";
			ItemsTabText = "Items (" + GetLabelSelectedSerialNumberItems() + ")" + "\n";

			IsShowFoundSerialPicker = PickSerialNumberItems.Any();
			PickerSerialNumbersLabel = "Found " + PickSerialNumberItems.Count + " serial numbers. Please choose one of them:";

			if (SelectedSerialNumberItems.Count == 0)
			{
				IsShowSerialCommandBar = false;
				IsShowInspectionPickerBar = false;
				IsShowSelectEmployee = false;
				IsShowSubmitBar = false;
				CurrentCommandMode = CommandMode.None;
			}
			else
			{
				if (CurrentCommandMode == CommandMode.None)
				{
					IsShowSerialCommandBar = true;
					IsShowInspectionPickerBar = false;
					IsShowSelectEmployee = false;
					IsShowSubmitBar = false;
				}
				else if (CurrentCommandMode == CommandMode.Receive)
				{
					IsShowSerialCommandBar = false;
					IsShowInspectionPickerBar = false;
					IsShowSelectEmployee = true;
					IsShowSubmitBar = true;
				}
				else if (CurrentCommandMode == CommandMode.Use)
				{
					IsShowSerialCommandBar = false;
					IsShowInspectionPickerBar = true;
					IsShowSelectEmployee = true;
					IsShowSubmitBar = true;
				}
				else throw new Exception();
			}

			IsShowAllAllocatedList = (HistoryItem == null);
			IsShowHistoryList = (HistoryItem != null);

			if (LastUpdateTimesheetMaterialRecordHeader != null)
			{
				var headerNumber = LastUpdateTimesheetMaterialRecordHeader.HeaderNumber;
				PrintLastOrderText = "Print Order #" + headerNumber;
				EmailLastOrderText = "Email Order #" + headerNumber;
			}

			CommandFunc.ChangeAllCanExecute(this);
		}

		async Task OpenAllOperations()
		{
			var vmodel = new AllOperationsViewModel();
			await NavFunc.NavigateToAsync(vmodel);
		}

		async Task PrintLastOrder()
		{
			await PrintReportFunc.PrintOrder(LastUpdateTimesheetMaterialRecordHeader.RowId, LastUpdateTimesheetMaterialRecordHeader.HeaderNumber);
		}

		async Task EmailLastOrder()
		{
			await EmailAddressPick.OpenPopup(LastUpdateTimesheetMaterialRecordHeader.RowId, Page);
		}


		void CustomizeIdeom()
		{
			ScanBarcodeButtonFontSize = U.TabletMode ? 16 : 16;
			ScanBarcodeButtonPadding = U.TabletMode ? 10 : 3;
			ScanBarcodeButtonMargin = U.TabletMode ? 6 : 5;
			ScanBarcodeRowHeight = U.TabletMode ? 100 : 60;
			CalcValueRowHeight = U.TabletMode ? 100 : 60;
			CalcValueFontSize = U.TabletMode ? 40 : 30;
			TabViewMargin = Device.RuntimePlatform == Device.iOS ? new Thickness(0, 0, 0, 25) : new Thickness(0);
		}

		public override async Task<bool> BeforePageClose()
		{
			return false;
		}

		public class SelectedSerialNumberItem : ViewModelBase
		{
			public FindStockItemsDTO Item { get; set; }
			public SerialViewModel Parent { get; set; }
			public Command DeleteCommand { get; set; }
			public Command HistoryCommand { get; set; }
			public Boolean IsRedBorder { get; set; }
			public Boolean IsIncluded { get; set; } = true;
			public Boolean InBox { get; set; }
			public Boolean IsLastInBox { get; set; }
			public Boolean IsBoxItem => Item.ItemType == StockItemType.Box;

			public Thickness Margin => IsLastInBox ? new Thickness(20, 0, 0, 20) : InBox ? new Thickness(20, 0, 0, 0) : new Thickness(0);
			public Color BackgroundColor => 
				!IsIncluded ? Color.FromHex("#DDA1A1") :
				InBox || IsBoxItem ? Color.FromHex("#CED1E0") :
				Color.FromHex("#E5E5E5");
			public String DeleteSwipeText =>
				InBox && IsIncluded ? "Exclude" :
				InBox && !IsIncluded ? "Include" :
				"Delete";
			public Color DeleteSwipeBackgroundColor =>
				InBox && IsIncluded ? Color.FromHex("#FF6932") :
				InBox && !IsIncluded ? Color.FromHex("#007F46") :
				Color.FromHex("#FF0000") ;

			public SelectedSerialNumberItem(FindStockItemsDTO item, SerialViewModel parent)
			{
				Item = item;
				IsRedBorder = new[] { TimesheetMaterialStatus.Used, TimesheetMaterialStatus.Sold }.Contains(Item.StockStatus);
				Parent = parent;
				DeleteCommand = CommandFunc.CreateAsync(Delete);
				HistoryCommand = CommandFunc.CreateAsync(History);
			}

			async Task Delete()
			{
				await Parent.DeleteItem(this);
			}

			async Task History()
			{
				await Parent.ShowHistoryItem(this);
			}

		}

		public class TimesheetMaterialRecordItem
		{
			public TimesheetMaterialRecordDTO Item { get; set; }
			public SerialViewModel Parent { get; set; }
			public Command SelectItemCommand { get; set; }
			public String Key { get; set; }
			public Boolean InBox { get; set; }
			public Boolean IsBoxItem => Item.ItemType == StockItemType.Box;

			public Thickness Margin => InBox ? new Thickness(20, 0, 0, 0) : new Thickness(0);
			public Color BackgroundColor =>
				InBox || IsBoxItem ? Color.FromHex("#CED1E0") :
				Color.FromHex("#E5E5E5");


			public TimesheetMaterialRecordItem(TimesheetMaterialRecordDTO item, SerialViewModel parent)
			{
				Item = item;
				Key = Item.LabourName;
				Parent = parent;
				SelectItemCommand = CommandFunc.CreateAsync(Select);
			}

			async Task Select()
			{
				await Parent.SelectAllocatedItem(this);
			}

			public string AllocatedInfoText => "Allocated by " + Item.CreatedByName + ", " + Item.RecordDate.FormatShort();
		}


		public enum CommandMode { None, Receive, Use }

	}
}
