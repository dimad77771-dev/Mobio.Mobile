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
	public static class SoundFunc
	{
		public static void PlayFile(string file)
		{
			var player = CrossSimpleAudioPlayer.Current;
			player.Load(file);
			player.Play();
		}

		public static void PlayScanSuccess()
		{
			PlayFile("ScanSuccess.mp3");
		}

		public static void PlayScanError()
		{
			PlayFile("ScanError.mp3");
		}
	}
}
