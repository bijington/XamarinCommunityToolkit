﻿using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Behaviors
{
	public class RubberBandAnimationType : NonAsyncAnimationBase
	{
		protected override uint DefaultDuration { get; set; } = 1000;

		protected override Animation CreateAnimation(params View[] views)
		{
            var animation = new Animation();

            foreach (var view in views)
            {
                animation.Add(0, 0.3, new Animation(v => view.ScaleX = v, 1, 1.25));
                animation.Add(0, 0.3, new Animation(v => view.ScaleY = v, 1, 0.75));

                animation.Add(0.3, 0.4, new Animation(v => view.ScaleX = v, 1.25, 0.75));
                animation.Add(0.3, 0.4, new Animation(v => view.ScaleY = v, 0.75, 1.25));

                animation.Add(0.4, 0.5, new Animation(v => view.ScaleX = v, 0.75, 1.15));
                animation.Add(0.4, 0.5, new Animation(v => view.ScaleY = v, 1.25, 0.85));

                animation.Add(0.5, 0.65, new Animation(v => view.ScaleX = v, 1.15, 0.95));
                animation.Add(0.5, 0.65, new Animation(v => view.ScaleY = v, 0.85, 1.05));

                animation.Add(0.65, 0.75, new Animation(v => view.ScaleX = v, 0.95, 1.05));
                animation.Add(0.65, 0.75, new Animation(v => view.ScaleY = v, 1.05, 0.95));

                animation.Add(0.75, 1, new Animation(v => view.ScaleX = v, 1.05, 1));
                animation.Add(0.75, 1, new Animation(v => view.ScaleY = v, 0.95, 1));
            }

            return animation;
        }
	}
}
