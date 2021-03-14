using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneBuilder.Mobile.Behaviors
{
	public class AnimationBlinkingBehavior : Behavior<View>
	{
		private View associatedObject;
		public string CurrentAnimationCode { get; set; }

		public static readonly BindableProperty StartAnimationProperty =
			BindableProperty.Create(nameof(StartAnimation), typeof(bool), typeof(AnimationBlinkingBehavior), false, propertyChanged: OnStartAnimationChanged, defaultBindingMode: BindingMode.TwoWay);
		public static readonly BindableProperty StopAnimationProperty =
			BindableProperty.Create(nameof(StopAnimation), typeof(bool), typeof(AnimationBlinkingBehavior), false, propertyChanged: OnStopAnimationChanged, defaultBindingMode: BindingMode.TwoWay);
		public static readonly BindableProperty RepeatCountProperty =
			BindableProperty.Create(nameof(RepeatCount), typeof(int), typeof(AnimationBlinkingBehavior), 3);
		public static readonly BindableProperty DurationProperty =
			BindableProperty.Create(nameof(Duration), typeof(int), typeof(AnimationBlinkingBehavior), 1000);


		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);
			associatedObject = bindable;
			bindable.BindingContextChanged += (sender, e) =>
			{
				BindingContext = associatedObject.BindingContext;
				//System.Diagnostics.Debug.WriteLine("YYYYYYYYYYYY=" + associatedObject.GetHashCode() + ";"
				//	+ (BindingContext as ViewModels.NopOrderScanViewModel.ModelRow)?.StartRedAnimation + ";"
				//	+ (BindingContext as ViewModels.NopOrderScanViewModel.ModelRow)?.SerialNum + ";"
				//	+ "class=" + (BindingContext == null ? "null" : BindingContext.GetType().FullName) + ";"
				//	+ "");

				if (BindingContext != null)
				{
					var view = associatedObject;
					if (view != null)
					{
						//if (!string.IsNullOrEmpty(CurrentAnimationCode))
						{
							if (!StartAnimation)
							{
								//System.Diagnostics.Debug.WriteLine("AAAAAAAA=AbortAnimation=" +
								//	(BindingContext as ViewModels.NopOrderScanViewModel.ModelRow)?.SerialNum + ";" + associatedObject.GetHashCode());
								view.AbortAnimation("AnimationBlinkingBehaviorAnimations");
								view.Opacity = 0;
							}
						}
					}
				}
			};

			associatedObject.BindingContextChanged += (sender, e) =>
			{
				//System.Diagnostics.Debug.WriteLine("ZZZZZZZZZZZZZZ");
				//ViewExtensions.CancelAnimations(associatedObject);
			};

			bindable.Opacity = 0;
		}

		protected override void OnDetachingFrom(View bindable)
		{
			associatedObject = null;
			base.OnDetachingFrom(bindable);
		}

		static void OnStartAnimationChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as AnimationBlinkingBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				var attachBehavior = (bool)newValue;
				if (attachBehavior)
				{
					var view = behavior.associatedObject;


					var parentAnimation = new Animation();
					var animation1 = new Animation(v => view.Opacity = v, 0, 1, Easing.Linear);
					var animation2 = new Animation(v => view.Opacity = v, 1, 0, Easing.Linear);
					parentAnimation.Add(0, 0.5, animation1);
					parentAnimation.Add(0.5, 1, animation2);
					behavior.CurrentAnimationCode = "AnimationBlinkingBehaviorAnimations";// + "--" + Guid.NewGuid();
					var repeat = behavior.RepeatCount;
					//System.Diagnostics.Debug.WriteLine("AAAAAAAA=StartAnimation=" 
					//	+ (behavior.BindingContext as ViewModels.NopOrderScanViewModel.ModelRow)?.SerialNum + ";" + view.GetHashCode());
					parentAnimation.Commit(
						view,
						behavior.CurrentAnimationCode,
						length: (uint)behavior.Duration,
						repeat: () => 
						{
							repeat--;
							return repeat > 0;
						},
						finished: (a, b) => 
						{
							view.Opacity = 0;
							behavior.CurrentAnimationCode = "";
							behavior.SetValue(AnimationBlinkingBehavior.StartAnimationProperty, false);
						});

					//Device.BeginInvokeOnMainThread(async () => {
					//	for (int k = 0; k < behavior.RepeatCount; k++)
					//	{
					//		await ViewExtensions.FadeTo(view, 0, (uint)behavior.Duration);
					//		await ViewExtensions.FadeTo(view, 1, (uint)behavior.Duration);
					//		view.Opacity = 0;
					//	}
					//});

					//Task.Factory.StartNew(async () => {
					//	for (int k = 0; k < behavior.RepeatCount; k++)
					//	{
					//		await ViewExtensions.FadeTo(view, 0, (uint)behavior.Duration);
					//		await ViewExtensions.FadeTo(view, 1, (uint)behavior.Duration);
					//		view.Opacity = 0;
					//	}
					//});
				}

			}
		}

		static void OnStopAnimationChanged(BindableObject vw, object oldValue, object newValue)
		{
			var behavior = vw as AnimationBlinkingBehavior;
			if (behavior != null && behavior.associatedObject != null)
			{
				var attachBehavior = (bool)newValue;
				if (attachBehavior)
				{
					behavior.SetValue(AnimationBlinkingBehavior.StopAnimationProperty, false);

					var view = behavior.associatedObject;
					view.AbortAnimation("AnimationBlinkingBehaviorAnimations");
					view.Opacity = 0;
				}
			}
		}

		public bool StartAnimation
		{
			get
			{
				return (bool)GetValue(StartAnimationProperty);
			}
			set
			{
				SetValue(StartAnimationProperty, value);
			}
		}

		public bool StopAnimation
		{
			get
			{
				return (bool)GetValue(StopAnimationProperty);
			}
			set
			{
				SetValue(StopAnimationProperty, value);
			}
		}


		public int RepeatCount
		{
			get
			{
				return (int)GetValue(RepeatCountProperty);
			}
			set
			{
				SetValue(RepeatCountProperty, value);
			}
		}

		public int Duration
		{
			get
			{
				return (int)GetValue(DurationProperty);
			}
			set
			{
				SetValue(DurationProperty, value);
			}
		}
	}
}
