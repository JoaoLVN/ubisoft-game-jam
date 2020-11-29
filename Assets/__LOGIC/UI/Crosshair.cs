using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public bool hideCursor = false;

    private Transform _transform;
    private Camera _camera;

    private void Start()
    {
        _transform = transform;
        _camera = Camera.main;

        if (Application.platform == RuntimePlatform.WindowsPlayer || hideCursor)
            Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _transform.position = pos;
    }
}
