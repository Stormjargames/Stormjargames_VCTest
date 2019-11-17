using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpawner : MonoBehaviour
{
    public GameObject playerToFollow;
    public float stopBoundsSize = 3;
    public float yOffset = -3;
    public float lerpSpeed = 1;
    public bool moving = false;
    public GameObject prefab;
    public float returnToMovementDelay = 5;
    public float jumpDistMultiplier = 3;
    public bool stop = false;

    void Start()
    {
        //moving = true;
    }

    public void StartFollow()
    {
        moving = true;
    }

    void Update()
    {
        Rect r = new Rect(playerToFollow.transform.position.x-stopBoundsSize/2, playerToFollow.transform.position.z - stopBoundsSize / 2, stopBoundsSize, stopBoundsSize);

        if (moving)
        {
            if (r.Contains(new Vector2(transform.position.x, transform.position.z)) == false)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, yOffset, transform.position.z),
                    new Vector3(playerToFollow.transform.position.x, yOffset, playerToFollow.transform.position.z),
                    Time.deltaTime * 1 / lerpSpeed);
            }
            else if (r.Contains(new Vector2(transform.position.x, transform.position.z)))
            {
                moving = false;
                if(!stop)
                {
                    StartCoroutine(iAttack());
                    stop = true;
                }
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent cube at the transforms position
        if (stop)
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
        }
        Gizmos.DrawCube(transform.position, new Vector3(1, 0.01f, 1));
        Gizmos.DrawCube(playerToFollow.transform.position, new Vector3(stopBoundsSize, 0.01f, stopBoundsSize));

    }

    public IEnumerator iAttack()
    {
        Instantiate(prefab, transform.position+new Vector3(0, -yOffset,0), Quaternion.identity);
        Vector3 dif = playerToFollow.transform.position - transform.position;
        transform.position += new Vector3(jumpDistMultiplier*dif.x, 0, jumpDistMultiplier * dif.z);
        yield return new WaitForSeconds(returnToMovementDelay);
        moving = true;
        stop = false;
        yield return null;
    }
}
