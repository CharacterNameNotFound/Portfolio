namespace GameWideSystems.InputManager.GestureReaders.Keyboard
{
    public class KeyboardText : IGesture
    {
        public InputType InputType => InputType.Keyboard;
        public char Key;

        public KeyboardText(char c)
        {
            Key = c;
        }
    }
}