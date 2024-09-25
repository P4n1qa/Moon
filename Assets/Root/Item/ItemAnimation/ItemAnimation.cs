using DG.Tweening;
using UnityEngine;

namespace Root.Item.ItemAnimation
{
    public class ItemAnimation : MonoBehaviour
    {
        [SerializeField] private float timeAnimationCreateObject;
        [SerializeField] private float timeAnimationStackObject;

        public void AnimationDropObject(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, timeAnimationCreateObject).SetEase(Ease.Linear);
            transform.DORotate(Vector3.zero, timeAnimationStackObject).SetEase(Ease.Linear);
        }

        public void AnimationStackItem(Vector3 targetPosition)
        {
            transform.DOLocalMove(targetPosition, timeAnimationStackObject).SetEase(Ease.Linear);
            transform.DOLocalRotate(Vector3.zero, timeAnimationStackObject).SetEase(Ease.Linear);
        }
    }
}