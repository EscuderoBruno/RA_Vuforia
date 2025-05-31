using UnityEngine;
using UnityEngine.AI;

public class MoveOnClick : MonoBehaviour
{
    public Camera currentCamera; // La cámara que usamos para el raycast
    public NavMeshAgent agent;   // El agente NavMesh del personaje

    private bool isPressed = false;
    private Vector3 targetPosition;
    private Vector2 pos;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            pos = Input.mousePosition;
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if ((t.phase == TouchPhase.Stationary) || (t.phase == TouchPhase.Moved && t.deltaPosition.magnitude < 1.5f))
            {
                isPressed = true;
                pos = t.position;
            }
        }
#endif

        if (isPressed)
        {
            Ray ray = currentCamera.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
                agent.SetDestination(targetPosition);
            }
            isPressed = false;
        }
    }
}
