using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Makina.Input
{
    /// <summary>
    /// Manages keyboard, mouse, and gamepad input states.
    /// </summary>
    public class InputManager
    {
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        // Optional: Support multiple gamepads if needed
        private GamePadState _currentGamePadState;
        private GamePadState _previousGamePadState;
        private readonly PlayerIndex _playerIndex = PlayerIndex.One; // Default to player one

        public Point MousePosition => _currentMouseState.Position;
        public int MouseScrollWheelValue => _currentMouseState.ScrollWheelValue;
        public int MouseScrollWheelDelta => _currentMouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue;

        public void Initialize()
        {
            // Initial state capture to avoid nulls on first frame
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
            _currentGamePadState = GamePad.GetState(_playerIndex);

            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;
            _previousGamePadState = _currentGamePadState;
        }

        public void Update(GameTime gameTime) // gameTime might be useful for input buffering/timing later
        {
            // Store previous states
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;
            _previousGamePadState = _currentGamePadState;

            // Get current states
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
            _currentGamePadState = GamePad.GetState(_playerIndex);
            // TODO: Add logic for checking if gamepad is connected
        }

        // --- Keyboard Methods ---

        /// <summary>
        /// Checks if a key is currently held down.
        /// </summary>
        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a key was just pressed (down now, up previously).
        /// </summary>
        public bool IsKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Checks if a key was just released (up now, down previously).
        /// </summary>
        public bool IsKeyReleased(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
        }

        // --- Mouse Methods ---

        public bool IsMouseButtonDown(MouseButton button)
        {
            return GetMouseButtonState(_currentMouseState, button) == ButtonState.Pressed;
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            return GetMouseButtonState(_currentMouseState, button) == ButtonState.Pressed &&
                   GetMouseButtonState(_previousMouseState, button) == ButtonState.Released;
        }

        public bool IsMouseButtonReleased(MouseButton button)
        {
            return GetMouseButtonState(_currentMouseState, button) == ButtonState.Released &&
                   GetMouseButtonState(_previousMouseState, button) == ButtonState.Pressed;
        }

        private ButtonState GetMouseButtonState(MouseState state, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left: return state.LeftButton;
                case MouseButton.Right: return state.RightButton;
                case MouseButton.Middle: return state.MiddleButton;
                case MouseButton.XButton1: return state.XButton1;
                case MouseButton.XButton2: return state.XButton2;
                default: return ButtonState.Released;
            }
        }

        // --- GamePad Methods (Example) ---
        // Add similar methods for GamePad buttons (IsButtonDown, IsButtonPressed, IsButtonReleased)
        // Example:
        public bool IsGamePadButtonDown(Buttons button)
        {
             return _currentGamePadState.IsButtonDown(button);
        }

         public bool IsGamePadButtonPressed(Buttons button)
        {
             return _currentGamePadState.IsButtonDown(button) && _previousGamePadState.IsButtonUp(button);
        }

        public bool IsGamePadButtonReleased(Buttons button)
        {
             return _currentGamePadState.IsButtonUp(button) && _previousGamePadState.IsButtonDown(button);
        }

        public Vector2 GetGamePadLeftThumbStick()
        {
            return _currentGamePadState.ThumbSticks.Left;
        }
         public Vector2 GetGamePadRightThumbStick()
        {
            return _currentGamePadState.ThumbSticks.Right;
        }

        // Add methods for triggers, DPad etc. as needed
    }

    /// <summary>
    /// Enum to represent mouse buttons consistently.
    /// </summary>
    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }
} 