using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Sample.ViewModels.Animations
{
	public class AnimationViewModel : BaseViewModel
	{
		NonAsyncAnimationBase? currentAnimation;
		AnimationDetailViewModel? selectedAnimation;

		public ObservableCollection<AnimationDetailViewModel> Animations { get; }

		public AnimationDetailViewModel? SelectedAnimation
		{
			get => selectedAnimation;
			set => SetProperty(ref selectedAnimation, value);
		}

		public Command StartAnimationCommand { get; }

		public Command StopAnimationCommand { get; }

		public AnimationViewModel()
		{
			Animations = new ObservableCollection<AnimationDetailViewModel>
			{
				new AnimationDetailViewModel("Tada", () => new TadaAnimationType()),
				new AnimationDetailViewModel("RubberBand", () => new RubberBandAnimationType())
			};

			SelectedAnimation = Animations.First();
			StartAnimationCommand = new Command<View>(OnStart, (view) => !(SelectedAnimation is null) && currentAnimation?.IsRunning != true);
			StopAnimationCommand = new Command(OnStop, () => currentAnimation?.IsRunning == true);
		}

		void OnStart(View view)
		{
			if (currentAnimation != null)
			{
				currentAnimation.Abort();
			}

			currentAnimation = SelectedAnimation!.CreateAnimation();

			currentAnimation.Animate(
				onFinished: (d, b) =>
				{
					StartAnimationCommand.ChangeCanExecute();
					StopAnimationCommand.ChangeCanExecute();
				},
				views: view);

			StopAnimationCommand.ChangeCanExecute();
		}

		void OnStop()
		{
			if (currentAnimation != null)
			{
				currentAnimation.Abort();
			}
		}
	}
}

