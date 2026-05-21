using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace GameWideSystems.Logger
{
    public class Logger
    {
        [Conditional(conditionString:"ENABLE_LOGGING")]
        public void Log(string line)
        {
            Debug.Log(line);
        }

        [Conditional(conditionString:"ENABLE_LOGGING")]
        public void Warn(string line)
        {
            Debug.LogWarning(line);
        }

        [Conditional(conditionString:"ENABLE_LOGGING")]
        public void Error(string line)
        {
            Debug.LogError(line);
        }
    }
}