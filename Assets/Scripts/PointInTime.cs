using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public bool clickE;
    public bool clickLeftClick;

    public PointInTime (Vector3 _position, Quaternion _rotation, bool _clickE, bool _clickLeftClick) {
        position = _position;
        rotation = _rotation;
        clickE = _clickE;
        clickLeftClick = _clickLeftClick;
    }
}
