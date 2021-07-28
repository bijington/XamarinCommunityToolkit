using System;
using Xamarin.CommunityToolkit.Behaviors;

namespace Xamarin.CommunityToolkit.Sample.ViewModels.Animations
{
	public class AnimationDetailViewModel : BaseViewModel
	{
		public string Name { get; }

		public Func<NonAsyncAnimationBase> CreateAnimation { get; }

		public AnimationDetailViewModel(string name, Func<NonAsyncAnimationBase> createAnimation)
		{
			Name = name;
			CreateAnimation = createAnimation;
		}
	}
}
