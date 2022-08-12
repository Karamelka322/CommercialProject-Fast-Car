using CodeBase.Logic.Car;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(WheelCurve))]
    public class WheelCurveEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UpdateCurve();
        }
        
        private void UpdateCurve()
        {
            WheelCurve wheelCurve = target as WheelCurve;
            WheelCollider wheelCollider = wheelCurve.GetComponent<WheelCollider>();

            wheelCurve.ForwardFriction = FrictionCurve(wheelCollider.forwardFriction);
            wheelCurve.SidewaysFriction = FrictionCurve(wheelCollider.sidewaysFriction);
        }

        private static AnimationCurve FrictionCurve(in WheelFrictionCurve friction)
        {
            return new AnimationCurve()
            {
                keys = new[]
                { 
                    new Keyframe(0 ,0),
                    new Keyframe(friction.extremumSlip, friction.extremumValue),
                    new Keyframe(friction.asymptoteSlip, friction.asymptoteValue)
                }
            };
        }
    }
}