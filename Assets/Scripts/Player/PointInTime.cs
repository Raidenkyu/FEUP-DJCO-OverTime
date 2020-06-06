using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Quaternion cameraAngle;
    public bool clickE;
    public bool clickLeftClick;

    public PointInTime (Vector3 _position, Quaternion _rotation, Quaternion _cameraAngle, bool _clickE, bool _clickLeftClick) {
        position = _position;
        rotation = _rotation;
        cameraAngle = _cameraAngle;
        clickE = _clickE;
        clickLeftClick = _clickLeftClick;
    }
}
