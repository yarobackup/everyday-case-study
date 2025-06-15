using System;
using UnityEngine;

namespace CompanyName.Ui
{

    public abstract class ConfigurablePopUpView : MonoBehaviour
    {
        [SerializeField]
        private float height = 30;
        public abstract ConfigurablePopUpType ViewType { get; }
        public float Height => height;

        internal abstract void ConfigureView(IPopUpViewOptions viewOptions);
        internal virtual void Subscribe() { }
        internal virtual void Unsubscribe() { }
        internal virtual void Reset() { }
    }
}
