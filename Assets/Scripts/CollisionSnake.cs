using UnityEngine;

public class CollisionSnake : MonoBehaviour
{
    [SerializeField] private GameObject snake = default;
    [SerializeField] private SnakeManager snakeManager = default;

    private void Start()
    {
        snakeManager = GetComponentInParent<SnakeManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PowerUp1"))
        {
            snakeManager.GetComponent<SnakeManager>().AddPartList(snake,1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PowerUp2"))
        {
            snakeManager.GetComponent<SnakeManager>().AddPartList(snake,2);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PowerUp3"))
        {
            snakeManager.GetComponent<SnakeManager>().AddPartList(snake,3);
            Destroy(collision.gameObject);
        }
        
       
    }
}
