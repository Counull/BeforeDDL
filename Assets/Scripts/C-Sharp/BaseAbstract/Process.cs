using System.Collections;
using UnityEngine;

namespace C_Sharp.BaseAbstract {
    public abstract class Process {
        public abstract void Start();

        protected virtual void Complete() {
            _isComplete = true;
        }

        private bool _isComplete;

        public bool IsComplete => _isComplete;
    }

    public abstract class AsyncProcess : Process {
        public delegate void OnProcessComplete();

        public virtual event OnProcessComplete OnComplete;

        protected override void Complete() {
            base.Complete();
            OnComplete?.Invoke();
        }

        IEnumerator WaitingForProcess() {
            yield return new WaitUntil(() => IsComplete);
        }
    }
}