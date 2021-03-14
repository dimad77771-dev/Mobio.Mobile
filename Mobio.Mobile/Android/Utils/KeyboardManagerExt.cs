using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Views.InputMethods;

namespace OneBuilder.Mobile.Droid.Utils
{
	public static class KeyboardManagerExt
	{
		public static void HideKeyboard(this View inputView, bool overrideValidation = false)
		{
			if (inputView == null)
			{
				throw new ArgumentNullException("inputView must be set before the keyboard can be hidden.");
			}
			using (InputMethodManager inputMethodManager = (InputMethodManager)inputView.Context.GetSystemService("input_method"))
			{
				if (!overrideValidation && !(inputView is EditText) && !(inputView is TextView) && !(inputView is SearchView))
				{
					throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");
				}
				IBinder windowToken = inputView.WindowToken;
				if (windowToken != null && inputMethodManager != null)
				{
					inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
				}
			}
		}

		public static void ShowKeyboard(this View inputView)
		{
			if (inputView == null)
			{
				throw new ArgumentNullException("inputView must be set before the keyboard can be shown.");
			}
			using (InputMethodManager inputMethodManager = (InputMethodManager)inputView.Context.GetSystemService("input_method"))
			{
				if (!(inputView is EditText) && !(inputView is TextView) && !(inputView is SearchView))
				{
					throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");
				}
				if (inputMethodManager != null)
				{
					inputMethodManager.ShowSoftInput(inputView, ShowFlags.Forced);
					inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
				}
			}
		}
	}
}
