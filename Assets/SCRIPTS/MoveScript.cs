using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    
    //publics
        public enemyTriggerScript enemyScript;
        public float moveDistance = 1f;
        public float rotationAngle = 90f;
        public float movementDelay = 0.5f;
        public float rotationDelay = 0.5f;
        public GameObject frontalColl;
        public GameObject rearColl;
        public AudioClip[] footstepSounds;
        public bool canMove = true;
    //privates
        private AudioSource audioSource;
        private bool canMoveForward = true;
        private bool canMoveBackward = true;
        private bool forwardCol;
        private bool backwardCol;
        private bool canRotate = true;
        private bool isTweeningMove = false;
        private bool isTweeningRotate = false;
        private bool isRotating = false;
        private bool isMoving = false;
    

    private void Update(){
        HandleRotationInput();
        HandleMovementInput();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayRandomFootstepSound()
    {
        // Check if there are footstep sounds assigned
        if (footstepSounds != null && footstepSounds.Length > 0)
        {
            // Choose a random footstep sound from the array
            int randomIndex = Random.Range(0, footstepSounds.Length);
            AudioClip randomFootstepSound = footstepSounds[randomIndex];

            // Play the randomly chosen footstep sound
            audioSource.PlayOneShot(randomFootstepSound);
        }
        else
        {
            Debug.LogWarning("No footstep sounds assigned!");
        }
    }

    private void HandleMovementInput(){
        if (!isTweeningMove && canMove && !isRotating)
        {
            backwardCol = CollisionRear();
            forwardCol = CollisionFront();
            switch ((forwardCol, backwardCol))
            {
                case (true, false):
                    //Debug.Log("Colliding on the front");
                    canMoveForward = false;
                    canMoveBackward = true;
                    StartCoroutine(MoveCoroutine());
                    break;
                case (false, true):
                    canMoveForward = true;
                    canMoveBackward = false;
                    StartCoroutine(MoveCoroutine());
                    break;
                case (false, false):
                    canMoveForward = true;
                    canMoveBackward = true;
                    StartCoroutine(MoveCoroutine());
                    break;
                default:
                    //Debug.Log("This shouldn't appear");
                    break;
            }
            
        }
    }

    private void HandleRotationInput()
    {

        if (canRotate && !isTweeningRotate && !isMoving && !isRotating)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Debug.Log("Horizontal Input: " + horizontalInput);

            if (horizontalInput < 0)
            {
                StartCoroutine(RotateCoroutine(-rotationAngle));
            }
            else if (horizontalInput > 0)
            {
                StartCoroutine(RotateCoroutine(rotationAngle));
            }
        }
    }

    IEnumerator MoveCoroutine()
{
    Vector3 direction = Vector3.zero;

    if (canMoveForward && canMoveBackward)
    {
        direction = transform.forward * Input.GetAxisRaw("Vertical");
    }
    else if (canMoveForward && !canMoveBackward && Input.GetAxisRaw("Vertical") > 0)
    {
        direction = transform.forward;
    }
    else if (!canMoveForward && canMoveBackward && Input.GetAxisRaw("Vertical") < 0)
    {
        direction = -transform.forward;
    }
    if (direction != Vector3.zero)
    {
        Vector3 targetPosition = transform.position + direction.normalized * moveDistance;

        
        isMoving = true; 
        canMove = false; 
        canRotate = false; 
        isTweeningMove = true; 
        PlayRandomFootstepSound();

        transform.DOMove(targetPosition, movementDelay).OnComplete(() =>
        {
            isTweeningMove = false;
            isMoving = false; 
            canMove = true; 
            canRotate = true; 

            
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                enemyScript.EnemyTrigger();
            }
        });

        
        yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.01f);
    }

    // Unlock movement in case it wasn't unlocked due to some error
    canMove = true;
}


    IEnumerator RotateCoroutine(float angle)
    {
        isRotating = true; // Set rotating flag
        
        canRotate = false; // Lock rotation
        canMove = false; // Lock movement

        isTweeningRotate = true;
        transform.DORotate(transform.eulerAngles + new Vector3(0, angle, 0), rotationDelay).OnComplete(() =>
        {
            isTweeningRotate = false;
            isRotating = false; 
            canMove = true; 
        });


        yield return new WaitForSeconds(rotationDelay);

        
        canRotate = true; 
    }

    private bool CollisionFront()
    {
        if (frontalColl != null && frontalColl.GetComponent<Collider>() != null)
        {
            Collider[] colliders = Physics.OverlapBox(frontalColl.transform.position, frontalColl.GetComponent<Collider>().bounds.extents);
            foreach (Collider collider in colliders)
            {
                if (collider != frontalColl.GetComponent<Collider>())
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CollisionRear()
    {
        if (rearColl != null && rearColl.GetComponent<Collider>() != null)
        {
            Collider[] colliders = Physics.OverlapBox(rearColl.transform.position, rearColl.GetComponent<Collider>().bounds.extents);
            foreach (Collider collider in colliders)
            {
                if (collider != rearColl.GetComponent<Collider>())
                {
                    return true;
                }
            }
        }
        return false;
    }
}
