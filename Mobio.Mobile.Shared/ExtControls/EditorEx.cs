using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace OneBuilder.Mobile
{
	public class EditorEx : Editor
	{
		public static readonly BindableProperty EtcOptionProperty = BindableProperty.Create("EtcOption", typeof(EtcOptions), typeof(EditorEx), null);
		public static readonly BindableProperty EndEditingActionProperty = BindableProperty.Create("EndEditingAction", typeof(Int32), typeof(EditorEx), 0);
		public static readonly BindableProperty BecomeFirstResponderActionProperty = BindableProperty.Create("BecomeFirstResponderAction", typeof(Int32), typeof(EditorEx), 0);
		public static readonly BindableProperty HasDoneToolbarItemProperty = BindableProperty.Create("HasDoneToolbarItem", typeof(Boolean), typeof(EditorEx), false);
		//public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create("VerticalTextAlignment", typeof(TextAlignment), typeof(EditorEx), TextAlignment.Center);

		public EtcOptions EtcOption
		{
			get
			{
				return (EtcOptions)this.GetValue(EditorEx.EtcOptionProperty);
			}
			set
			{
				this.SetValue(EditorEx.EtcOptionProperty, (object)value);
			}
		}

		public Int32 EndEditingAction
		{
			get
			{
				return (Int32)this.GetValue(EditorEx.EndEditingActionProperty);
			}
			set
			{
				this.SetValue(EditorEx.EndEditingActionProperty, (object)value);
			}
		}

		public Int32 BecomeFirstResponderAction
		{
			get
			{
				return (Int32)this.GetValue(EditorEx.BecomeFirstResponderActionProperty);
			}
			set
			{
				this.SetValue(EditorEx.BecomeFirstResponderActionProperty, (object)value);
			}
		}

		public Boolean HasDoneToolbarItem
		{
			get
			{
				return (Boolean)this.GetValue(EditorEx.HasDoneToolbarItemProperty);
			}
			set
			{
				this.SetValue(EditorEx.HasDoneToolbarItemProperty, (object)value);
			}
		}

		//public TextAlignment VerticalTextAlignment
		//{
		//	get
		//	{
		//		return (TextAlignment)this.GetValue(EditorEx.VerticalTextAlignmentProperty);
		//	}
		//	set
		//	{
		//		this.SetValue(EditorEx.VerticalTextAlignmentProperty, (object)value);
		//	}
		//}


		public void EndEditing(bool force)
		{
			EndEditingAction++;
		}
		public void BecomeFirstResponder()
		{
			BecomeFirstResponderAction++;
		}



		public EditorEx()
		{
		}




		public class EtcOptions
		{
			public UIReturnKeyType ReturnKeyType { get; set; } = UIReturnKeyType.NoUse;
			public UITextAutocorrectionType AutocorrectionType { get; set; } = UITextAutocorrectionType.NoUse;
			public UITextAutocapitalizationType AutocapitalizationType { get; set; } = UITextAutocapitalizationType.NoUse;
			public UIKeyboardType KeyboardType { get; set; } = UIKeyboardType.NoUse;

		}



		public enum UIReturnKeyType
		{
			NoUse = -1,
			Default = 0,
			Go = 1,
			Google = 2,
			Join = 3,
			Next = 4,
			Route = 5,
			Search = 6,
			Send = 7,
			Yahoo = 8,
			Done = 9,
			EmergencyCall = 10,
			Continue = 11
		}

		public enum UITextAutocorrectionType
		{
			NoUse = -1,
			Default = 0,
			No = 1,
			Yes = 2
		}

		public enum UITextAutocapitalizationType
		{
			NoUse = -1,
			None = 0,
			Words = 1,
			Sentences = 2,
			AllCharacters = 3
		}


		public enum UIKeyboardType
		{
			NoUse = -1,
			Default = 0,
			ASCIICapable = 1,
			NumbersAndPunctuation = 2,
			Url = 3,
			NumberPad = 4,
			PhonePad = 5,
			NamePhonePad = 6,
			EmailAddress = 7,
			DecimalPad = 8,
			Twitter = 9,
			WebSearch = 10,




			IntegerPad = 9991,
		}


	}
}
