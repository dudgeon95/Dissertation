using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Draggable))]
public class PieceController : MonoBehaviour {

    //get the puzzle contoller
    public PuzzleController puzzle;

    //flag whether the piece is placed or not
    public bool isPlaced;

    //Current cell
    public Vector2 currentCell;
    //Correct cell
    public Vector2 correctCell;

    // get the draggable component
    Draggable drag;

    

    void Awake()
    {
        //get drag component
        drag = GetComponent<Draggable>();
    }

    void OnEnable()
    {
       drag.OnDrag += HandleDrag;
       drag.OnDrop += HandleDrop;
       puzzle.OnCompletion += HandleCompletion;
       puzzle.OnFailure += HandleFailure;
    }

   

    void OnDisable()
    {
        drag.OnDrag -= HandleDrag;
        drag.OnDrop -= HandleDrop;
        puzzle.OnCompletion -= HandleCompletion;
        puzzle.OnFailure -= HandleFailure;
    }

    //Handle a successful completion
    void HandleCompletion()
    {
        //Block the piece
        drag.SetBlockState(true);
        
    }

    //Handle an unsuccessful completion 
    void HandleFailure()
    {
        //send the piece back to its original position
        drag.SendToSpawnPos();
        isPlaced = false;
    }

    void HandleDrop()
    {
        //Raycast hits
        RaycastHit[] hits;

        //Find elements
        hits = Physics.RaycastAll(transform.position, -transform.forward, 20);



        //Check if the piece is being placed on puzzle
        for (int i = 0; i < hits.Length; i++)
        {
            //Check if the element we found was the puzzle
            if (hits[i].collider.gameObject.GetInstanceID() == puzzle.gameObject.GetInstanceID())
            {
                //the puzzle was there
                HandlePuzzleDrop(hits[i].point);

                //Exit the loop
                break;
            }

        }

        //Handle puzzle drop
    }

   void HandleDrag()
    {
        isPlaced = false;
    }
    //Handles what happens when a piece is dropped into the puzzle
     void HandlePuzzleDrop(Vector3 pointDrop)
    {
        //get the cell col and row positions
        Vector2 cell = puzzle.GetCellFromPoint(pointDrop);

        //Check the cell is not taken
        if (!puzzle.IsTaken(cell))
        {
            //Position the piece at the center of the cell
            Vector3 newPos = puzzle.GetCellPosition(cell);


            //Position piece on that point
            transform.position = newPos;

            //Make them face to opposite to the panels forward (Fixes the rotation of the object so they lie flat on the panel)
            transform.forward = puzzle.transform.forward;

            //update cell information
            isPlaced = true;
            currentCell = cell;

            

            //Check puzzle completion
            puzzle.CheckCompletion();
        }

        //If cell is taken send the piece back to its original position
        else
        {
            drag.SendToSpawnPos();
        }
        
    }

    //Check if piece is in the right place
    public bool CheckCorrect()
    {
        return currentCell == correctCell;
    }

    public void BlockPiece(bool block)
    {
        drag.SetBlockState(block);
    }
}
