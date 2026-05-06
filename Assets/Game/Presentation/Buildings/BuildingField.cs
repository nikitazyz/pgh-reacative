using System;
using Reacative.Presentation.InteractionSystem;
using UnityEngine;

namespace Reacative.Presentation.Buildings
{
    public class BuildingField : MonoBehaviour
    {
        public event Action OnPurchase;
        
        [SerializeField] private InteractionReceiver _receiver;

        private void Awake()
        {
            _receiver.OnInteract += _ => OnPurchase?.Invoke();
        }
    }
}
