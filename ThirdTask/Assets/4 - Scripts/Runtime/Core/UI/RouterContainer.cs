using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace AD.Services.Router
{
    public class RouterContainer : MonoBehaviour
    {
        [SerializeField] private UIContainer[] containers;

        private readonly Dictionary<Type, UIContainer> containersCache = new();

        private UIContainer currentContainer;

        internal void Init()
        {
            foreach (var container in containers)
            {
                var type = container.GetType();

                SetContainerState(container, state: false, force: true);

                containersCache.Add(type, container);
            }
        }

        internal void GoToContainer<TContainer>()
        {
            if (currentContainer != null)
            {
                SetContainerState(currentContainer, state: false, force: false);
            }

            containersCache.TryGetValue(typeof(TContainer), out currentContainer);

            SetContainerState(currentContainer, state: true, force: false);
        }

        private void SetContainerState(UIContainer container, bool state, bool force)
        {
            if (state || force)
            {
                container.gameObject.SetActive(state);
            }

            if (container.TryGetComponent<CanvasGroup>(out var canvasGroup))
            {
                var endValue = state ? 1 : 0;
                var duration = state ? 0.1f : 0.05f;

                canvasGroup.DOKill();
                canvasGroup.interactable = state;

                if (force)
                {
                    canvasGroup.alpha = endValue;
                }
                else
                {
                    canvasGroup
                        .DOFade(endValue, duration)
                        .OnComplete(() =>
                        {
                            container.gameObject.SetActive(state);
                        });
                }
            }
        }
    }
}