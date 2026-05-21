namespace GameWideSystems.UIManagement.Screen.StateMachineGeneric
{
    public abstract class SMUIScreen<TUISMContext, TScreenDependencies, TUISMScreeenState, TParams> 
        : UIScreen<TParams, TScreenDependencies>
        where TUISMContext : IUISMContext
        where TScreenDependencies : IUIScreenDependencies 
        where TUISMScreeenState: IUISMScreenState
        where TParams : IScreenParams
    {

        protected TUISMContext Context;
        protected TUISMScreeenState CurrentView;
        
    }
}