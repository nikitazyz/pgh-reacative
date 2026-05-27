using System;
using System.Collections.Generic;
using Reacative.Domain.Cats;
using UnityEngine;

namespace Reacative.Presentation.Configs
{
    [CreateAssetMenu(fileName = "CatsResourcePack", menuName = "RP/Cats")]
    public class CatsResourcePack : ScriptableObject
    {
        [SerializeField] private CatColorAssets[] _catColorAssets;

        private Dictionary<CatColor, CatColorAssets> _catColorAssetsMap;

        private void Awake()
        {
            if (_catColorAssets == null)
            {
                return;
            }
            Initialize();
        }

        public Sprite GetCatIcon(CatColor catColor)
        {
            if (_catColorAssetsMap == null)
            {
                Initialize();
            }
            return _catColorAssetsMap[catColor].Icon;
        }

        private void Initialize()
        {
            _catColorAssetsMap = new Dictionary<CatColor, CatColorAssets>();
            foreach (var catColorAssets in _catColorAssets)
            {
                _catColorAssetsMap.Add(catColorAssets.CatColor, catColorAssets);
            }
        }
        

        [Serializable]
        private class CatColorAssets
        {
            [SerializeField] private CatColor _catColor;
            [SerializeField] private Sprite _icon;

            public CatColor CatColor => _catColor;

            public Sprite Icon => _icon;
        }
    }
}