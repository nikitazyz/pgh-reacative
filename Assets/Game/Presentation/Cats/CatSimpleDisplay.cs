using System;
using Reacative.Domain.Cats;
using UnityEngine;

namespace Reacative.Presentation
{
    public class CatSimpleDisplay : MonoBehaviour
    {
        private static readonly int Skin = Animator.StringToHash("Skin");
        [SerializeField] private Animator _animator;
        [SerializeField] private CatColor _catColor;

        private void OnEnable()
        {
            _animator.SetFloat(Skin, (int)_catColor);
        }
    }
}
