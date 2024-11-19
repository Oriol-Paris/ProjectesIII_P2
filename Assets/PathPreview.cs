using UnityEngine;

public class PathPreview : MonoBehaviour
{
    public Vector3 mousePosition; // Mouse position in world space
    public Vector3 playerPosition; // Player's current position
    public Vector3 positionDesired; // The desired target position
    [SerializeField] private LineRenderer lineRenderer; // LineRenderer reference for drawing the path preview
    [SerializeField] private float range = 5f; // Maximum movement range for the player
    PlayerBase playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerBase>();
        playerPosition = transform.position; // Initialize player's position
        lineRenderer.enabled = false; // Start with LineRenderer disabled (it will be enabled when preview starts)
    }

    void Update()
    {
        range = playerStats.GetRange();
        playerPosition = transform.position;
        // Update mouse position in world space
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10)); // Adjust z to camera's near plane
        mousePosition.z = 0; // Keep the mouse position in the 2D plane
        Vector3 direction = (mousePosition - playerPosition).normalized;
        positionDesired = playerPosition + direction * range; // Clamp to the maximum range
        ShowPathPreview();
        // If the left mouse button is clicked, start previewing the path
        if (Input.GetMouseButtonDown(0))
        {
            positionDesired = mousePosition; // Store the desired position when the mouse is clicked
            ShowPathPreview(); // Start showing the path preview
        }

        // While dragging the mouse, update the preview line
        if (Input.GetMouseButton(0))
        {
            // Limit the desired position to the movement range
            float distanceToTarget = Vector3.Distance(playerPosition, mousePosition);
            if (distanceToTarget > range)
            {
               
            }

            // Continuously update the path preview as the mouse moves
            ShowPathPreview();
        }

        // Hide the path preview when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            // Here, you could disable the preview if needed, but for now we keep it visible until needed elsewhere.
            // For this implementation, we'll leave it on until the next click.
            //lineRenderer.enabled = false;  // Uncomment if you want to disable the path preview after releasing the mouse.
        }
    }

    // Function to show the straight line path preview using the LineRenderer
    void ShowPathPreview()
    {
        lineRenderer.enabled = true; // Ensure the LineRenderer is always visible during the preview

        // Set the line to start from the player's position and end at the desired position
        lineRenderer.positionCount = 2; // Only two points for a straight line
        lineRenderer.SetPosition(0, playerPosition); // Start point (player's position)
        lineRenderer.SetPosition(1, positionDesired); // End point (current mouse position or clamped position)
    }
}
