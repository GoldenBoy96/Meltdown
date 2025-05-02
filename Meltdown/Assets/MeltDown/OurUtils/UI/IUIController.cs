using UnityEngine;

namespace OurUtils
{
    public interface IUIController
    {
        [SerializeField] public bool IgnoredBackKey { get; set; }
        public void OnStartUI() { }
        public void OnActiveUI() { }
        public void OnDeactiveUI() { }
        public void OnRemoveUI() { }
    }
}