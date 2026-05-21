namespace GameWideSystems.GameStateManagement
{
    public enum PreviousStateReloadType
    {
        /// <summary>
        /// Current state will be closed
        /// </summary>
        None,
        
        /// <summary>
        /// Use hot save to load state 
        /// </summary>
        Load,
        
        /// <summary>
        /// Open state without using save data as it was new state
        /// </summary>
        Reopen,
    }
}