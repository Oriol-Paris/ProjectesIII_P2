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
        // Update mouse position
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        mousePosition.z = 0;

        // If mouse button is pressed
        if (Input.GetMouseButtonDown(0) && !placeSelected)
        {
            positionDesired = mousePosition; // Store desired position at click
            playerPosition = transform.position; // Set player position at the time of click
            elapsedTime = 0; // Reset elapsed time
            controlPoint = (playerPosition + positionDesired) / 2 + new Vector3(0, -1f, 0); // Set initial control point
            lineRenderer.enabled = true;  // Enable LineRenderer when starting path selection
        }

        // While dragging the mouse
        if (Input.GetMouseButton(0) && !placeSelected)
        {
            // Update the end position while dragging
            UpdateLineRenderer(mousePosition);
        }

        // When the mouse button is released
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            placeSelected = true;
            positionDesired = mousePosition;
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
            Vector3 newPosition = BezierCurve(t, playerPosition, controlPoint, positionDesired);
            transform.position = newPosition; // Update the player's position

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
        // Update the control point based on the initial position and the new target position
        float curveIntensity = Mathf.Clamp(Vector3.Distance(playerPosition, targetPosition) / 100f, 0, 2f);
        controlPoint = (playerPosition + positionDesired) /2 + new Vector3(0, -curveIntensity, 0); // Adjust height

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
