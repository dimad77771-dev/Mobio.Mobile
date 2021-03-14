using HtmlAgilityPack;
using OneBuilder.Mobile;
using OneBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace OneBuilder.Mobile
{
	public static class HtmlFunc
	{
		public static string GetHtmlNodeClass(this HtmlNode node)
		{
			return node.Attributes.FirstOrDefault(q => q.Name == "class")?.Value;
		}

		public static string GetHtmlNodeHref(this HtmlNode node)
		{
			return node.Attributes.FirstOrDefault(q => q.Name == "href")?.Value;
		}

		public static string GetHtmlNodeSrc(this HtmlNode node)
		{
			return node.Attributes.FirstOrDefault(q => q.Name == "src")?.Value;
		}

		public static IEnumerable<HtmlNode> GetAllChildNodes(this HtmlNode item)
		{
			foreach (var child in item.ChildNodes)
			{
				yield return child;
				foreach (var child2 in GetAllChildNodes(child))
				{
					yield return child2;
				}
			}
		}

	}
}
