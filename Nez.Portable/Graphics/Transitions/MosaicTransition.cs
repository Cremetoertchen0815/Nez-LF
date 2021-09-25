using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Nez.Tweens;
using System;

namespace Nez
{
	class MosaicTransition : SceneTransition
	{
		public Vector2 TargetResolution = Vector2.One;
		public float FadeDuration = 0.4f;

		/// <summary>
		/// delay to start fading out
		/// </summary>
		public float DelayBeforeFadeInDuration = 0.1f;

		/// <summary>
		/// ease equation to use for the fade
		/// </summary>
		public EaseType FadeEaseType = EaseType.QuartOut;
		Vector2 _factor;
		Rectangle _destinationRect;


		public MosaicTransition(Func<Scene> sceneLoadAction) : base(sceneLoadAction, true)
		{
			_destinationRect = PreviousSceneRender.Bounds;
		}

		public override IEnumerator OnBeginTransition()
		{

			Vector2 SourceSize = new Vector2(PreviousSceneRender.Width, PreviousSceneRender.Height);
			var elapsed = 0f;
			while (elapsed < FadeDuration)
			{
				elapsed += Time.AltDeltaTime;
				_factor = Lerps.Ease(FadeEaseType, SourceSize, TargetResolution, elapsed, FadeDuration);

				yield return null;
			}

			// load up the new Scene
			yield return Core.StartCoroutine(LoadNextScene());

			// dispose of our previousSceneRender. We dont need it anymore.
			PreviousSceneRender.Dispose();
			PreviousSceneRender = null;

			yield return Coroutine.WaitForSeconds(DelayBeforeFadeInDuration);

			elapsed = 0f;
			while (elapsed < FadeDuration)
			{
				elapsed += Time.AltDeltaTime;
				_factor = Lerps.Ease(FadeEaseType, TargetResolution, SourceSize, elapsed, FadeDuration);

				yield return null;
			}

			TransitionComplete();
		}

		public override void Render(Batcher batcher)
		{
			Core.GraphicsDevice.SetRenderTarget(null);
			batcher.Begin(BlendState.NonPremultiplied, Core.DefaultSamplerState, DepthStencilState.None, null);

			// we only render the previousSceneRender while fading to _color. It will be null after that.
			if (!_isNewSceneLoaded)
				batcher.Draw(PreviousSceneRender, _destinationRect, Color.White);

			batcher.End();
		}
	}
}
