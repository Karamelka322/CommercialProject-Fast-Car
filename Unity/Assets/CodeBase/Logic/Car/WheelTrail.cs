namespace CodeBase.Logic.Car
{
    public class WheelTrail : StaticObjectPool<TrailWrapper>
    {
        private TrailWrapper _currentTrail;
        
        public void StartDrawing()
        {
            if (_currentTrail != null)
                return;
            
            if (TakeDisableObject(out _currentTrail)) 
                _currentTrail.Enabled = true;
        }

        public void StopDrawing()
        {
            if(_currentTrail == null)
                return;
            
            _currentTrail.transform.parent = null;
            _currentTrail.ResetAfterStoppingDrawing();
            _currentTrail = null;
        }
    }
}