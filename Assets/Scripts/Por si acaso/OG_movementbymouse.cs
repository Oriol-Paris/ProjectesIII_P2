using System.Collections.Generic;
using UnityEngine;

public class OG_MovementByMouse : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 positionDesired;
    Vector3 playerPosition;
    bool placeSelected;
    bool isMoving;
    float moveDuration; // Total time to move
    float elapsedTime;  // Time elapsed since the start of the move
    [SerializeField] private float velocity; // Speed in units per second
    [SerializeField] LineRenderer lineRenderer;
    private Vector3 controlPoint;
    [SerializeField] private int curveResolution = 20;  // Higher number for smoother curve in LineRenderer

    // Reference to the PlayerBase script
    [SerializeField] private PlayerBase playerBase;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        placeSelected = false;
        playerPosition = transform.position;
        lineRenderer.enabled = false;  // Start with LineRenderer disabled

        // Get the PlayerBase component
        playerBase = GetComponent<PlayerBase>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        mousePosition.z = 0;

        // If mouse button is pressed
        if (Input.GetMouseButtonDown(0) && !placeSelected)
        {
            positionDesired = mousePosition;
            elapsedTime = 0; // Reset elapsed time
            lineRenderer.enabled = true;  // Enable LineRenderer when starting path selection
        }
        else if (!Input.GetMouseButtonDown(0) && !isMoving)
        {
            lineRenderer.enabled = true;
            UpdateLineRenderer(mousePosition);
        }

        // While dragging the mouse
        if (Input.GetMouseButton(0) && !placeSelected)
        {
            float dragDistance = Vector3.Distance(playerPosition, mousePosition);
            float maxDistance = playerBase.GetRange(); // Get the range from PlayerBase
            dragDistance = Mathf.Min(dragDistance, maxDistance); // Limit the drag distance

            float curveIntensity = Mathf.Clamp(dragDistance / 100f, 0, 2f);

            // Adjust control point to make the curve bend to the right
            controlPoint = (playerPosition + mousePosition) / 2 + new Vector3(0, -curveIntensity, 0); // Inverted Y offset

            // Generate smoother curve points for the LineRenderer
            List<Vector3> smoothCurvePoints = GenerateBezierCurve(playerPosition, controlPoint, positionDesired, curveResolution);
            lineRenderer.positionCount = smoothCurvePoints.Count;
            lineRenderer.SetPositions(smoothCurvePoints.ToArray());
        }

        // When the mouse button is released
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            placeSelected = true;

            // Calculate the distance to the desired position
            float distanceToTarget = Vector3.Distance(playerPosition, positionDesired);
            moveDuration = distanceToTarget / velocity; // Set move duration based on distance and velocity
            elapsedTime = 0; // Reset elapsed time
            lineRenderer.enabled = false;  
        }

        // Movement along the Bezier curve
        if (placeSelected)
        {
            isMoving = true;
            elapsedTime += Time.deltaTime; // Increment elapsed time

            // Calculate normalized time (0 to 1) for the Bezier curve
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = BezierCurve(t, playerPosition, controlPoint, positionDesired);

            // Check if we have reached the end of the curve
            if (t >= 1f)
            {
                placeSelected = false;
                isMoving = false;
                playerPosition = transform.position; // Update player position to the new position
            }
        }
    }

    private void UpdateLineRenderer(Vector3 targetPosition)
    {
        float dragDistance = Vector3.Distance(playerPosition, targetPosition);
        float maxDistance = playerBase.GetRange(); // Get the range from PlayerBase
        dragDistance = Mathf.Min(dragDistance, maxDistance); // Limit the drag distance

        float curveIntensity = Mathf.Clamp(dragDistance / 100f, 0, 2f);

        // Adjust control point to make the curve bend to the right
        controlPoint = (playerPosition + targetPosition) / 2 + new Vector3(0, -curveIntensity, 0); // Inverted Y offset

        // Generate smoother curve points for the LineRenderer
        List<Vector3> smoothCurvePoints = GenerateBezierCurve(playerPosition, controlPoint, targetPosition, curveResolution);
        lineRenderer.positionCount = smoothCurvePoints.Count;
        lineRenderer.SetPositions(smoothCurvePoints.ToArray());
    }

    // Generate a list of points for a smoother Bezier curve in LineRenderer
    private List<Vector3> GenerateBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, int resolution)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            points.Add(BezierCurve(t, p0, p1, p2));
        }
        return points;
    }

    // Calculate Bezier curve position
    private Vector3 BezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2 * (1-t) * t * P1
        p += tt * p2; // t^2 * P2
        return p;
    }

    public bool GetIsMoving() { return isMoving; }
    public Vector3 GetPosition() { return playerPosition; }
}
