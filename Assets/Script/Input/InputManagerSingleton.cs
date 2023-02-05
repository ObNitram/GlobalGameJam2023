namespace Script
{
    public static class InputManagerSingleton
    {
        /*
        private static InputManager _inputManager;
        public static InputManager Instance
        {
            get
            {
                if (_inputManager == null)
                {
                    _inputManager = new InputManager();
                }
                return _inputManager;
            }
        }
        */
        
        private static InputManager _inputManager;
        public static InputManager Instance => _inputManager ??= new InputManager();
    }
    
    
}

