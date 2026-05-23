using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameWideSystems.UIManagement.UIManagerRequests
{
    public class UIRequest
    {
        public static UIRequest New => new UIRequest();
        
        internal bool IsLockInputs = true;
        
        // ToDo (low): It will be preferable to pool, so we can use them anytime without garbage creation
        internal readonly List<IUIAction> Actions = new();
        
        internal UIManager UIManager;
        
        public void AppendAction(IUIAction action)
        {
            if (Actions.Count > 0 && !Validate(action, Actions.Last()))
            {
                throw new Exception("Attempted illegal action append attempt");
            }
            
            Actions.Add(action);
        }

        /// <summary>
        /// Start handling task queue
        /// </summary>
        /// <returns> Opened a screen life UniTask handle </returns>
        public virtual async UniTask<UniTask> Handle(CancellationToken cancellationToken)
        {
            if (IsLockInputs)
            {
                UIManager.InputControlFacade.SetInputsAvailable(false);
            }
            
            UniTask screenHandle = UniTask.CompletedTask;
            
            foreach (IUIAction item in Actions)
            {
                screenHandle = await item.Handle(UIManager, cancellationToken);
            }

            if (IsLockInputs)
            {
                UIManager.InputControlFacade.SetInputsAvailable(true);
            }
            
            return screenHandle;
        }

        private bool Validate(IUIAction currentAction, IUIAction lastAction)
        {
            return currentAction.ActionType != UIActionType.Close || lastAction.ActionType != UIActionType.Open;
        }

        public UniTask<UniTask> Play(CancellationToken cancellationToken)
        {
            return Handle(cancellationToken);
        }
    }
}