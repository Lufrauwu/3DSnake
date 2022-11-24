using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private float distanceBetween = 0.2f;
    [SerializeField] private float speed = 150;
    [SerializeField] private float turnSpeed = 100;
    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] private List<GameObject> snakeBody = new List<GameObject>();
    private float horizontal;
    private float vertical;
    private float counter = 0;

    void Start()
    {
        CreateBodyPart();
    }
    private void FixedUpdate()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyPart();
        }
        SnakeMovement();
    }

    void SnakeMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        snakeBody[0].GetComponent<Rigidbody>().velocity = snakeBody[0].transform.forward * speed * Time.deltaTime;

        if (horizontal != 0)
        {
            snakeBody[0].transform.Rotate(new Vector3(0, turnSpeed * horizontal * Time.deltaTime, 0));
        }
        if (vertical != 0)
        {
            snakeBody[0].transform.Rotate(new Vector3(turnSpeed * -vertical * Time.deltaTime, 0, 0));
        }
        if (snakeBody.Count > 1)
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                MarkersManager markM = snakeBody[i - 1].GetComponent<MarkersManager>();
                snakeBody[i].transform.position = markM.markerList[0].position;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }
    }

    void CreateBodyPart()
    {
        if (snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation,
            transform);

            if (!temp1.GetComponent<MarkersManager>())
            {
                temp1.AddComponent<MarkersManager>();
            }
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }
        MarkersManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkersManager>();
        
        if (counter == 0)
        {
            markM.ClearMarkerList();
        }
        counter += Time.deltaTime;
        if (counter >= distanceBetween)
        {
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation,
                transform);
            if (!temp.GetComponent<MarkersManager>())
            {
                temp.AddComponent<MarkersManager>();
            }
            
            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            temp.GetComponent<MarkersManager>().ClearMarkerList();
            counter = 0;
        }
    }
    
    public void AddPartList(GameObject Snake, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            bodyParts.Add(Snake); 
        }
    }
}
