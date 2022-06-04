using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IJoystickDetector
{
    bool IsMoved { get; }
    Vector2 Direction { get; }
}

public class JoystickDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IJoystickDetector
{
    [SerializeField] private GameObject _joystickBackground;
    [SerializeField] private GameObject _joystickThumble;
    [SerializeField] private float _radius = 128f;

    public bool IsMoved { get; set; }
    public Vector2 Direction { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _joystickBackground.transform.position = eventData.position;
        _joystickThumble.transform.position = eventData.position;
        SetJoystickVisability(true);
        IsMoved = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var distance = Vector3.Distance(_joystickBackground.transform.position, eventData.position);
        var direction = new Vector3(eventData.position.x, eventData.position.y, 0) - _joystickBackground.transform.position;
        direction = direction.normalized;
        var normalizedDistance = Mathf.Clamp01(distance / _radius);
        Direction = direction * normalizedDistance;

        _joystickThumble.transform.position = _joystickBackground.transform.position + direction * _radius * normalizedDistance;
        //Debug.Log($"Direction: {Direction}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetJoystickVisability(false);
        IsMoved = false;
    }

    private void Start()
    {
        SetJoystickVisability(false);
    }

    private void Update()
    {
        
    }

    private void SetJoystickVisability(bool isVisible)
    {
        _joystickBackground.SetActive(isVisible);
    }

}
