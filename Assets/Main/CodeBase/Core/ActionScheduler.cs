using UnityEngine;

namespace Main.CodeBase.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction _currentAction;

        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            _currentAction?.CancelAction();
            _currentAction = action;
        }
    }
}