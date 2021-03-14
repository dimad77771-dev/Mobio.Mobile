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

namespace OneBuilder.Mobile.ViewModels
{
	public class EmailAddressPickViewModel : ViewModelBase
	{
		public bool IsOpen { get; set; }
		public string Name1 { get; set; }
		public string Email1 { get; set; }
		public bool Visible1 { get; set; }
		public string Name2 { get; set; }
		public string Email2 { get; set; }
		public bool Visible2 { get; set; }
		public string Name3 { get; set; }
		public string Email3 { get; set; }
		public bool Visible3 { get; set; }
		public double WidthRequest { get; set; }
		public Command CancelCommand { get; set; }
		public Command SendCommand { get; set; }
		public Command ClearEmail1 { get; set; }
		public Command ClearEmail2 { get; set; }
		public Command ClearEmail3 { get; set; }

		public Guid HeaderRowId { get; set; }
		public OrderReportEmailsDTO[] AllEmails { get; set; }

		public EmailAddressPickViewModel()
		{
			CancelCommand = CommandFunc.Create(Cancel);
			SendCommand = CommandFunc.CreateAsync(Send);	//, CanSend);
			ClearEmail1 = CommandFunc.Create(() => { Email1 = ""; RefreshAllVisible(); });
			ClearEmail2 = CommandFunc.Create(() => Email2 = "");
			ClearEmail3 = CommandFunc.Create(() => Email3 = "");
		}

		public async Task OpenPopup(Guid headerRowId, Page page)
		{
			HeaderRowId = headerRowId;

			UIFunc.ShowLoading();
			var rez = await WebServiceFunc.GetOrderReportEmails(HeaderRowId);
			if (rez == null)
			{
				await UIFunc.AlertError(U.StandartErrorRetrieveText);
				return;
			}
			UIFunc.HideLoading();
			AllEmails = rez;

			Name1 = rez[0].Name;
			Email1 = rez[0].Email;
			Visible1 = (!string.IsNullOrEmpty(Name1));

			Name2 = rez[1].Name;
			Email2 = rez[1].Email;
			Visible2 = (!string.IsNullOrEmpty(Name2));

			Name3 = "Other Email";
			Email3 = "";
			Visible3 = (!string.IsNullOrEmpty(Name3));

			WidthRequest = U.TabletMode ? 800 : page.Width * 0.9;

			IsOpen = true;

			//Email1 = "dimad77771@gmail.com";
			//Email2 = "dimad77772@yandex.ru";
		}

		void Cancel()
		{
			IsOpen = false;
		}

		public async Task Send()
		{
			var emails = GetEmails().Select(q => q.Email).ToArray();
			if (!emails.Any())
			{
				await UIFunc.AlertError("At least one email must be specified");
				return;
			}

			UIFunc.ShowLoading("Sending email...");
			var rez = await WebServiceFunc.EmailOrderReport(HeaderRowId, new EmailOrderReportQuery { Emails = emails });
			if (!rez)
			{
				await UIFunc.AlertError("Error during sending email. Try later");
				return;
			}
			UIFunc.HideLoading();
			//await Task.Delay(2000);

			IsOpen = false;
		}
		bool CanSend()
		{
			return GetEmails().Any();
		}

		OrderReportEmailsDTO[] GetEmails()
		{
			var emails = new[]
			{
				new OrderReportEmailsDTO { Name = Name1, Email = Email1 },
				new OrderReportEmailsDTO { Name = Name2, Email = Email2 },
				new OrderReportEmailsDTO { Name = Name3, Email = Email3 },
			}
			.Where(q => !string.IsNullOrEmpty(q.Email))
			.ToArray();

			return emails;
		}

		void RefreshAllVisible()
		{ 
			CommandFunc.ChangeAllCanExecute(this);
		}


	}
}
