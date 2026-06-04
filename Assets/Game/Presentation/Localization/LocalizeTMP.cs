using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Reacative.Presentation
{
    public class LocalizeTMP : MonoBehaviour
    {
        [SerializeField] private LocalizedString _localizedString;

        private async void Awake()
        {
            try
            {
                var tmp = GetComponent<TMP_Text>();
                _localizedString.StringChanged += text => tmp.text = text;
                tmp.text = await _localizedString.GetLocalizedStringAsync().Task;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
