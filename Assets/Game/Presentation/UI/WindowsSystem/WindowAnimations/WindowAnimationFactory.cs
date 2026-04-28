using System;
using PrimeTween;
using UnityEngine;

namespace Reacative.Presentation.UI.WindowsSystem
{
    public class WindowAnimationFactory
    {
        public static Sequence ToBottom(VirtualWindow target, float duration = 0.15f, Ease ease = Ease.InCirc)
        {
            float targetHeight = -target.WindowSize.y / 2-10f;

            return Sequence.Create()
                .Group(Tween.Scale(target.transform, new TweenSettings<Vector3>(Vector3.zero, duration, ease)))
                .Group(Tween.PositionY(target.transform, new TweenSettings<float>(targetHeight, 0.15f, ease)));
        }

        public static Sequence FromBottom(VirtualWindow target, float duration = 0.15f, Ease ease = Ease.OutCirc)
        {
            target.transform.localScale = new Vector3(0, 0, 0);
            var targetPosition = target.transform.position;
            target.transform.position = new Vector3(targetPosition.x, -target.WindowSize.y/2-10f, 0);

            return Sequence.Create()
                .Group(Tween.Scale(target.transform, new TweenSettings<Vector3>(Vector3.one, duration, ease)))
                .Group(Tween.PositionY(target.transform,
                    new TweenSettings<float>(targetPosition.y, duration, ease)));
        }
        
        public static Sequence ScaleDown(VirtualWindow target, float duration = 0.15f, Ease ease = Ease.InCirc)
        {
            return Sequence.Create()
                .Group(Tween.Scale(target.transform, new TweenSettings<Vector3>(Vector3.zero, duration, ease)));
        }

        public static Sequence ScaleUp(VirtualWindow target, float duration = 0.15f, Ease ease = Ease.OutCirc)
        {
            target.transform.localScale = Vector3.zero;
            return Sequence.Create()
                .Group(Tween.Scale(target.transform, new TweenSettings<Vector3>(Vector3.one, duration, ease)));
        }

        public static Sequence GetInAnimation(WindowInAnimation inAnimation, VirtualWindow target, float duration = 0.15f, Ease ease = Ease.OutCirc)
        {
            return inAnimation switch
            {
                WindowInAnimation.ScaleUp => ScaleUp(target, duration, ease),
                WindowInAnimation.FromBottom => FromBottom(target, duration, ease),
                _ => throw new ArgumentOutOfRangeException(nameof(inAnimation), inAnimation, null)
            };
        }

        public static Sequence GetOutAnimation(WindowOutAnimation outAnimation, VirtualWindow target,
            float duration = 0.15f, Ease ease = Ease.InCirc)
        {
            return outAnimation switch
            {
                WindowOutAnimation.ScaleDown => ScaleDown(target, duration, ease),
                WindowOutAnimation.ToBottom => ToBottom(target, duration, ease),
                _ => throw new ArgumentOutOfRangeException(nameof(outAnimation), outAnimation, null)
            };
        }
    }

    public enum WindowInAnimation
    {
        None,
        ScaleUp,
        FromBottom
    }

    public enum WindowOutAnimation
    {
        None,
        ScaleDown,
        ToBottom,
    }
}