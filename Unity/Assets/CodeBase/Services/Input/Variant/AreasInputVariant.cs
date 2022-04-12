using UnityEngine;

namespace CodeBase.Services.Input
{
    public class AreasInputVariant : IInputVariant
    {
        private readonly IInputService _inputService;
        
        private const string AreaUpLeft = "UpLeft_area";
        private const string AreaDownLeft = "DownLeft_area";
        private const string AreaUpRight = "UpRight_area";
        private const string AreaDownRight = "DownRight_area";

        private static readonly Vector2 UpLeft = new Vector2(1, -1);
        private static readonly Vector2 DownLeft = new Vector2(-1, -1);
        private static readonly Vector2 UpRight = new Vector2(1, 1);
        private static readonly Vector2 DownRight = new Vector2(-1, 1);

        private Vector2 _axis;

        public Vector2 Axis => AreasAxis();

        public AreasInputVariant(IInputService inputService)
        {
            _inputService = inputService;
        }

        private Vector2 AreasAxis()
        {
            _axis = Vector2.zero;
            
            _axis += _inputService.GetArea(AreaUpLeft) ? UpLeft : Vector2.zero;
            _axis += _inputService.GetArea(AreaDownLeft) ? DownLeft : Vector2.zero;
            _axis += _inputService.GetArea(AreaUpRight) ? UpRight : Vector2.zero;
            _axis += _inputService.GetArea(AreaDownRight) ? DownRight : Vector2.zero;
            
            return _axis;
        }
    }
}