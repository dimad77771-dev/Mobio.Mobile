using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Foundation;
using OneBuilder.Mobile;
using Telerik.XamarinForms.Input;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EditorEx), typeof(Mobio.Mobile.iOS.EditorExRenderer))]
namespace Mobio.Mobile.iOS
{
	class EditorExRenderer : EditorRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);


			if (e.NewElement != null)
			{
				//Control.Bor = UITextBorderStyle.None;
				UpdateEtcOption();
				if (ElementEx.BecomeFirstResponderAction > 0)
				{
					RunBecomeFirstResponder();
				}
				AddToolbarHide();
				//AddToolbarHide2();
			}
		}


		void UpdateEtcOption()
		{
			var etcOptions = ElementEx.EtcOption;
			if (etcOptions != null)
			{
				if (etcOptions.ReturnKeyType != EditorEx.UIReturnKeyType.NoUse)
				{
					Control.ReturnKeyType = (UIReturnKeyType)etcOptions.ReturnKeyType;
				}
				if (etcOptions.AutocorrectionType != EditorEx.UITextAutocorrectionType.NoUse)
				{
					Control.AutocorrectionType = (UITextAutocorrectionType)etcOptions.AutocorrectionType;
				}
				if (etcOptions.AutocapitalizationType != EditorEx.UITextAutocapitalizationType.NoUse)
				{
					Control.AutocapitalizationType = (UITextAutocapitalizationType)etcOptions.AutocapitalizationType;
				}
				if (etcOptions.KeyboardType != EditorEx.UIKeyboardType.NoUse)
				{
					if (etcOptions.KeyboardType == EditorEx.UIKeyboardType.IntegerPad)
					{
						Control.KeyboardType = UIKeyboardType.NumberPad;
					}
					else
					{
						Control.KeyboardType = (UIKeyboardType)etcOptions.KeyboardType;
					}
				}

			}
		}

		void RunBecomeFirstResponder()
		{
			Control.BecomeFirstResponder();
		}

		void AddToolbarHide()
		{
			var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 200f, 44.0f));
			//toolbar.TintColor = UIColor.White;
			//toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;

			var toolbarItems = new List<UIBarButtonItem>();

			toolbarItems.Add(new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
			toolbarItems.Add(new UIBarButtonItem("Hide", UIBarButtonItemStyle.Plain, (s, e) =>
			{
				Control.ResignFirstResponder();
			}));
			if (ElementEx.HasDoneToolbarItem)
			{
				toolbarItems.Add(new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) =>
				{
					Control.ResignFirstResponder();
					//var mthods = typeof(Editor).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					//var bbb = string.Join(";", mthods.Select(q => q.Name).ToArray());
					//U.Log4(bbb);
					var method = typeof(Editor).GetMethod("SendCompleted", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					method.Invoke(ElementEx, null);
				}));
			}

			//HasDoneToolbarItem
			//textNoteEditor.KeyboardAppearance = UIKeyboardAppearance.Dark;
			toolbar.Items = toolbarItems.ToArray();
			Control.InputAccessoryView = toolbar;
		}



		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == EditorEx.EndEditingActionProperty.PropertyName)
			{
				Control.EndEditing(true);
			}
			else if (e.PropertyName == EditorEx.BecomeFirstResponderActionProperty.PropertyName)
			{
				RunBecomeFirstResponder();
			}
		}


		EditorEx ElementEx
		{
			get
			{
				return (EditorEx)Element;
			}
		}
	}
}
