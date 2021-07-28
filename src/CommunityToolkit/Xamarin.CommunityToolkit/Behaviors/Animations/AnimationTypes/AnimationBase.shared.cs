using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Behaviors
{
	public abstract class AnimationBase<TView> : BindableObject
		where TView : View
	{
		public static readonly BindableProperty DurationProperty =
			BindableProperty.Create(nameof(Duration), typeof(uint), typeof(AnimationBase<TView>), default(uint),
				BindingMode.TwoWay, defaultValueCreator: GetDefaultDurationProperty);

		public uint Duration
		{
			get => (uint)GetValue(DurationProperty);
			set => SetValue(DurationProperty, value);
		}

		public static readonly BindableProperty EasingTypeProperty =
		   BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(AnimationBase<TView>), Easing.Linear,
			   BindingMode.TwoWay);

		public Easing Easing
		{
			get => (Easing)GetValue(EasingTypeProperty);
			set => SetValue(EasingTypeProperty, value);
		}

		static object GetDefaultDurationProperty(BindableObject bindable)
			=> ((AnimationBase<TView>)bindable).DefaultDuration;

		protected abstract uint DefaultDuration { get; set; }
	}

	public abstract class NonAsyncAnimationBase : AnimationBase<View>
	{
		IAnimatable? owner;
		string? name;

		/// <summary>
		/// Stops the animation.
		/// </summary>
		/// <returns>True if successful, false otherwise.</returns>
		public bool Abort() => owner.AbortAnimation(name);

		/// <summary>
		/// Runs the animation.
		/// </summary>
		public void Animate(
			string name = "",
			uint rate = 16,
			Action<double, bool>? onFinished = null,
			Func<bool>? shouldRepeat = null,
			params View[] views)
		{
			owner = views.First();
			this.name = name + Guid.NewGuid().ToString();
			CreateAnimation(views).Commit(owner, this.name, rate, Duration, Easing, onFinished, shouldRepeat);
		}

		/// <summary>
		/// Gets a value indicating whether the animation is running.
		/// </summary>
		public bool IsRunning => owner.AnimationIsRunning(name);

		protected abstract Animation CreateAnimation(params View[] views);
	}

	public abstract class AnimationBase : AnimationBase<View>
	{
		public abstract Task Animate(View? view);
	}
}