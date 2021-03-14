using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using OneBuilder.Mobile.ViewModels;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class StyleFunc
	{
		public static Style AppStyle(string key)
		{
			return (Style)Application.Current.Resources[key];
		}
	}
}
