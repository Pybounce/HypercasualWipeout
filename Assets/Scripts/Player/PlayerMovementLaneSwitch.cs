using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLaneSwitch : MonoBehaviour, IListenRightSwipe, IListenLeftSwipe
{

    private int currentLane = 0;
    private int laneCount;
    private float laneWidth;
    [SerializeField] private float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        LevelManager levelSettings = FindObjectOfType<LevelManager>();
        laneWidth = levelSettings.GetLevel().laneWidth;
        laneCount = levelSettings.GetLevel().laneCount;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void SwipedRight()
    {
        if (currentLane < laneCount) { currentLane += 1; }
    }

    public void SwipedLeft()
    {
        if (currentLane > -laneCount) { currentLane -= 1; }
    }

    private void MovePlayer()
    {
        float targetXPos = currentLane * laneWidth;
        if (transform.localPosition.x == targetXPos) { return; }
        float moveDelta = targetXPos - transform.localPosition.x;
        float maxMoveDelta = speed * Time.deltaTime;
        if (transform.localPosition.x > targetXPos)
        {
            maxMoveDelta = -maxMoveDelta;
        }

        if (Mathf.Abs(moveDelta) <= Mathf.Abs(maxMoveDelta))
        {
            transform.localPosition += new Vector3(moveDelta, 0f, 0f);
        }
        else
        {
            transform.localPosition += new Vector3(maxMoveDelta, 0f, 0f);
        }
    }
}
