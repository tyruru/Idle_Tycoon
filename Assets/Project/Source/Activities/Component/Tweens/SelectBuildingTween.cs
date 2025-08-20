using System.Collections;
using UnityEngine;

namespace Project.Source.Activities.Component.Tweens
{
    public class SelectBuildingTween : MonoBehaviour
    {
        [SerializeField] private float _scaleUpFactor = 1.1f;  
        [SerializeField] private float _duration = 0.2f;       

        private Vector3 _originalScale;
        private Coroutine _tweenRoutine;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void PlaySelectTween()
        {
            if (_tweenRoutine != null)
                StopCoroutine(_tweenRoutine);

            _tweenRoutine = StartCoroutine(TweenScale(_originalScale * _scaleUpFactor, _originalScale));
        }

        private IEnumerator TweenScale(Vector3 targetUp, Vector3 targetBack)
        {
            yield return ScaleTo(targetUp, _duration);

            yield return ScaleTo(targetBack, _duration);

            _tweenRoutine = null;
        }

        private IEnumerator ScaleTo(Vector3 target, float time)
        {
            Vector3 start = transform.localScale;
            float elapsed = 0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / time);
                transform.localScale = Vector3.Lerp(start, target, t);
                yield return null;
            }

            transform.localScale = target;
        }
    }
}