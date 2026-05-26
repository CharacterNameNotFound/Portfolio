namespace Game.GameMode.Session.Gameplay.Inputs
{
    // reading both UI and Keyboard into same schema so we can one interface of interaction
    public class InputBuffer
    {
        public bool[] ActivatedLines = new bool[4];

        public void Clear()
        {
            for (int i = 0; i < ActivatedLines.Length; i++)
            {
                ActivatedLines[i] = false;
            }
        }
    }
}