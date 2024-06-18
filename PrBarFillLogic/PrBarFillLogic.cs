using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Tools.PrBarFillLogic
{
    /// <summary>
    /// The PrBarFillLogic class is responsible for the logic of filling the progress bar and updating the progress bar text.
    /// </summary>
    public class PrBarFillLogic
    {
        private readonly RectTransform _progressBarFillImage;
        private readonly TextMeshProUGUI _progressBarText;
        private readonly TextType _textType;
        private readonly float _size;

        
        /// <summary>
        /// Initializes a new instance of the PrBarFillLogic class with the specified parameters.
        /// </summary>
        /// <param name="progressBarFillImage">A RectTransform object representing the progress bar fill image.</param>
        /// <param name="progressBarText">A TextMeshProUGUI object representing the progress bar text.</param>
        /// <param name="textType">The type of progress bar text (percent or value).</param>
        public PrBarFillLogic(RectTransform progressBarFillImage, TextMeshProUGUI progressBarText = null, TextType textType = TextType.Percent)
        {
            _progressBarFillImage = progressBarFillImage;
            _progressBarText = progressBarText;
            _size = progressBarFillImage.rect.width;
            _textType = textType;
        }

        
        /// <summary>
        /// Fills the progress bar over the specified time. Calls the onComplete action when finished, if it is set.
        /// </summary>
        /// <param name="loadTime">The time to fill the progress bar.</param>
        /// <param name="onComplete">The action to be called upon completion of the fill.</param>
        public void LoadingFill(float loadTime, Action onComplete = null)
        {
            var size = _progressBarFillImage.rect.width;
            _progressBarFillImage.sizeDelta = new Vector2(0, _progressBarFillImage.sizeDelta.y);

            _progressBarFillImage.DOSizeDelta(new Vector2(size, _progressBarFillImage.sizeDelta.y), loadTime)
                .SetEase(Ease.Linear)
                .SetLink(_progressBarFillImage.gameObject)
                .OnUpdate(() => SetProgressText())
                .OnComplete(() =>
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }
        
        /// <summary>
        /// Updates the progress bar fill based on the current and maximum values. Calls the onComplete action when finished, if it is set.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="time">The time to update the progress bar.</param>
        /// <param name="onComplete">The action to be called upon completion of the update.</param>
        public void UpdateBarFill(float current, float max, float time = 0.1f , Action onComplete = null)
        {
            _progressBarFillImage.sizeDelta = new Vector2(0, _progressBarFillImage.sizeDelta.y);
            float newWidth = (current / max) * _size;
            Vector2 currentScaleVector = newWidth > _size ? new Vector2(_size, _progressBarFillImage.sizeDelta.y) : new Vector2(newWidth, _progressBarFillImage.sizeDelta.y);
            
            _progressBarFillImage.DOSizeDelta(currentScaleVector, time)
                .SetEase(Ease.Linear)
                .SetLink(_progressBarFillImage.gameObject)
                .OnUpdate(() => SetProgressText(current, max))
                .OnComplete(() =>
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }

        /// <summary>
        /// Sets the progress bar text depending on the text type (_textType). If _textType is Percent, the text will be in percent format. If _textType is Value, the text will be in the format of the current/maximum value.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="max">The maximum value.</param>
        private void SetProgressText(float current = 1f, float max = 1f)
        {
            if (!_progressBarText)
            {
                return;
            }

            switch (_textType)
                {
                    case TextType.Percent:
                        _progressBarText.text = $"{(int)(_progressBarFillImage.sizeDelta.x / _size * 100)}%";
                        break;
                    case TextType.Value:
                        _progressBarText.text = $"{(int)current}/{(int)max}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
    }
}