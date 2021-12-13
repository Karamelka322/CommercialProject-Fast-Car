using UnityEngine;

namespace CodeBase.Services
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => 
            UnityAxis();

        private static Vector2 UnityAxis() => 
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}