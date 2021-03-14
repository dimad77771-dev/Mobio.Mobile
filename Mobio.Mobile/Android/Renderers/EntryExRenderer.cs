using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Drawing;
using Android.Views.InputMethods;
using Android.Widget;
using Android.Views;
using System.Reflection;
using Android.Text;
using System.Threading.Tasks;
using Android.Content;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Droid.Renderers;
using OneBuilder.Mobile.Droid.Utils;

[assembly: ExportRenderer(typeof(EntryEx), typeof(EntryExRenderer))]
namespace OneBuilder.Mobile.Droid.Renderers
{
	public class EntryExRenderer : EntryRenderer, TextView.IOnEditorActionListener
	{
		public EntryExRenderer(Android.Content.Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			

			if (e.NewElement != null)
			{
				Control.Background = DrawableSimpleExt.FromResource(R.Drawable.Empty);
				//Control.Background = DrawableSimpleExt.FromResource(R.Drawable.editText_back);
				Control.SetPadding(0, 0, 0, 0);
				//Control.SetCursorVisible(true);

				//	Control.BorderStyle = UITextBorderStyle.None;
				UpdateEtcOption();
				if (ElementEx.BecomeFirstResponderAction > 0)
				{
					SysFunc.RequestMainThread(async () =>
					{
						await Task.Yield();
						RunBecomeFirstResponder();
					});
				}
				//!!!!!RemoveOnKeyboardBackPressedEvent();


				UpdateVerticalTextAlignment();

				//	AddToolbarHide();
				//	//AddToolbarHide2();
			}
		}


		////удаление "лишнего" события
		////непонятно зачем оно вообще
		////при "Numeric" клавиатуре приводит при закрытии клавиатуры клавишей "Back" к "перепрыгу" клвиатуры к текстовому виду, а затем уже к закрытию
		//void RemoveOnKeyboardBackPressedEvent()
		//{
		//	//var aaa = typeof(EntryEditText).GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		//	//var aaa1 = string.Join(";", aaa.Select(q => q.Name));
		//	//U.Log(aaa1);
		//	var f1 = typeof(EntryEditText).GetField("OnKeyboardBackPressed", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		//	object obj = f1.GetValue(Control);
		//	//var pi = ElementEx.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
		//	//var pi2 = Element.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance );
		//	//var list = (EventHandlerList)pi.GetValue(Control, null);
		//	//list.RemoveHandler(obj, list[obj]);

		//	//var mobject = obj.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		//	//var aaa19 = string.Join(";", mobject.Select(q => q.Name));
		//	//U.Log(aaa19);

		//	//var methods = typeof(MulticastDelegate).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		//	//var aaa12 = string.Join(";", methods.Select(q => q.Name));
		//	var meth = typeof(MulticastDelegate).GetMethod("GetInvocationList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		//	//var met1 = methods.First(q => q.Name == "GetInvocationList");
		//	var z1 = (Delegate[])meth.Invoke(obj, new object[0]);
		//	var b10 = (EventHandler)z1[0];
		//	//var removemethod = typeof(MulticastDelegate).GetMethod("RemoveImpl", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		//	//removemethod.Invoke(obj, new object[] { z1[0] });

		//	Control.OnKeyboardBackPressed -= b10;
		//	//var z2 = met1.Invoke(obj, new object[0]);
		//}


		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == EntryEx.EndEditingActionProperty.PropertyName)
			{
				Control.HideKeyboard();
				//Control.EndEditing(true);
			}
			else if (e.PropertyName == EntryEx.BecomeFirstResponderActionProperty.PropertyName)
			{
				RunBecomeFirstResponder();
			}
			else if (e.PropertyName == EntryEx.VerticalTextAlignmentProperty.PropertyName)
			{
				UpdateVerticalTextAlignment();
			}
		}

		void UpdateVerticalTextAlignment()
		{
			var gravity = Element.HorizontalTextAlignment.ToHorizontalGravityFlags();
			switch (ElementEx.VerticalTextAlignment)
			{
				case Xamarin.Forms.TextAlignment.Center:
					gravity |= GravityFlags.CenterVertical;
					break;
				case Xamarin.Forms.TextAlignment.Start:
					gravity |= GravityFlags.Top;
					break;
				case Xamarin.Forms.TextAlignment.End:
					gravity |= GravityFlags.Bottom;
					break;
			}
			Control.Gravity = gravity;
		}

		void RunBecomeFirstResponder()
		{
			Control.RequestFocus();
			Control.ShowKeyboard();
		}


		void UpdateEtcOption()
		{
			var etcOptions = ElementEx.EtcOption;
			if (etcOptions != null)
			{
				if (etcOptions.ReturnKeyType != EntryEx.UIReturnKeyType.NoUse)
				{
					UpdateEtcOptionReturnKeyType(etcOptions.ReturnKeyType);
				}
				if (etcOptions.AutocorrectionType != EntryEx.UITextAutocorrectionType.NoUse)
				{
					if (etcOptions.AutocorrectionType == EntryEx.UITextAutocorrectionType.No)
					{
						Control.InputType |= Android.Text.InputTypes.TextFlagNoSuggestions;
					}
					else if (etcOptions.AutocorrectionType == EntryEx.UITextAutocorrectionType.Yes)
					{
						Control.InputType &= ~Android.Text.InputTypes.TextFlagNoSuggestions;
					}
				}
				if (etcOptions.AutocapitalizationType != EntryEx.UITextAutocapitalizationType.NoUse)
				{
					if (etcOptions.AutocapitalizationType == EntryEx.UITextAutocapitalizationType.None)
					{
						Control.InputType &= ~Android.Text.InputTypes.TextFlagCapWords;
						Control.InputType &= ~Android.Text.InputTypes.TextFlagCapSentences;
						Control.InputType &= ~Android.Text.InputTypes.TextFlagCapCharacters;
					}
					else if (etcOptions.AutocapitalizationType == EntryEx.UITextAutocapitalizationType.Words)
					{
						Control.InputType |= Android.Text.InputTypes.TextFlagCapWords;
					}
					else if (etcOptions.AutocapitalizationType == EntryEx.UITextAutocapitalizationType.Sentences)
					{
						Control.InputType |= Android.Text.InputTypes.TextFlagCapSentences;
					}
					else if (etcOptions.AutocapitalizationType == EntryEx.UITextAutocapitalizationType.AllCharacters)
					{
						Control.InputType |= Android.Text.InputTypes.TextFlagCapCharacters;
					}
				}
				if (etcOptions.KeyboardType != EntryEx.UIKeyboardType.NoUse)
				{
					if (etcOptions.KeyboardType == EntryEx.UIKeyboardType.IntegerPad)
					{
						Control.InputType |= InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;
					}
					else if (etcOptions.KeyboardType == EntryEx.UIKeyboardType.DecimalPad)
					{
						Control.InputType |= InputTypes.ClassNumber | InputTypes.NumberFlagDecimal;
					}
					else if (etcOptions.KeyboardType == EntryEx.UIKeyboardType.NumberPad)
					{
						Control.InputType |= InputTypes.ClassNumber | InputTypes.NumberFlagSigned;
					}
				}

			}
		}


		void UpdateEtcOptionReturnKeyType(EntryEx.UIReturnKeyType returnKeyType)
		{
			switch (returnKeyType)
			{
				case EntryEx.UIReturnKeyType.Default:
					Control.ImeOptions = ImeAction.Unspecified;
					break;
				case EntryEx.UIReturnKeyType.Go:
					Control.ImeOptions = ImeAction.Go;
					break;
				case EntryEx.UIReturnKeyType.Google:
					Control.ImeOptions = ImeAction.Search;
					break;
				case EntryEx.UIReturnKeyType.Join:
					Control.ImeOptions = ImeAction.Unspecified;
					break;
				case EntryEx.UIReturnKeyType.Next:
					Control.ImeOptions = ImeAction.Next;
					break;
				case EntryEx.UIReturnKeyType.Route:
					Control.ImeOptions = ImeAction.Search;
					break;
				case EntryEx.UIReturnKeyType.Search:
					Control.ImeOptions = ImeAction.Search;
					break;
				case EntryEx.UIReturnKeyType.Send:
					Control.ImeOptions = ImeAction.Send;
					break;
				case EntryEx.UIReturnKeyType.Yahoo:
					Control.ImeOptions = ImeAction.Search;
					break;
				case EntryEx.UIReturnKeyType.Done:
					Control.ImeOptions = ImeAction.Done;
					break;
				case EntryEx.UIReturnKeyType.EmergencyCall:
					Control.ImeOptions = ImeAction.Unspecified;
					break;
				case EntryEx.UIReturnKeyType.Continue:
					Control.ImeOptions = ImeAction.Next;
					break;
				default:
					return;
			}
		}

		bool TextView.IOnEditorActionListener.OnEditorAction(TextView v, ImeAction actionId, KeyEvent e)
		{
			//base.AddTouchables(null);

			Control.ClearFocus();
			v.HideKeyboard();

			//Element.SendCompleted();
			var methodSendCompleted = typeof(Entry).GetMethod("SendCompleted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod);
			methodSendCompleted.Invoke(Element, new object[0]);

			return true;
		}



		EntryEx ElementEx
		{
			get
			{
				return (EntryEx)Element;
			}
		}
	}
}
