using PropertyChanged;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Behaviors
{
	public enum IdiomVisibleEnum { Default, Phone, Tablet }

	public class IdiomVisible
	{
		public static readonly BindableProperty VisibleProperty = BindableProperty.CreateAttached("Visible", typeof(IdiomVisibleEnum), typeof(Grid), IdiomVisibleEnum.Default);

		public static IdiomVisibleEnum GetVisible(BindableObject bindable)
		{
			return (IdiomVisibleEnum)bindable.GetValue(VisibleProperty);
		}

		public static void SetVisible(BindableObject bindable, IdiomVisibleEnum value)
		{
			bindable.SetValue(VisibleProperty, value);
		}
	}


	public static class IdiomVisibleUtlis
	{
		public static void RemoveNotValidVisualElements(Page page)
		{
			//var ggg = GetElementAndChilds(page).ToArray();
			var removed = GetElementAndChilds(page).Where(q => !IsValidElement(q)).ToArray();
			foreach(var vremove in removed)
			{
				var parentGrid = (Grid)vremove.Parent;
				parentGrid.Children.Remove((View)vremove);
			}
		}

		static bool IsValidElement(VisualElement element)
		{
			var vis = IdiomVisible.GetVisible(element);
			if ((U.TabletMode && vis == IdiomVisibleEnum.Phone) || (!U.TabletMode && vis == IdiomVisibleEnum.Tablet))
			{
				return false;
			}
			return true;
		}

		public static IEnumerable<VisualElement> GetElementAndChilds(VisualElement element)
		{
			yield return element;

			var childens = GetChilds(element);
			foreach(var childen in childens)
			{
				foreach(var el in GetElementAndChilds(childen))
				{
					yield return el;
				}
			}
		}

		public static IEnumerable<VisualElement> GetChilds(VisualElement element)
		{
			if (element is ContentPage)
			{
				var content = (element as ContentPage).Content;
				yield return content;
			}
			else if (element is Grid)
			{
				var grid = (Grid)element;
				foreach (var child in grid.Children)
				{
					yield return child;
				}
			}
		}

	}
}
