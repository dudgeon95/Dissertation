using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleController : MonoBehaviour {

    //The menu
    public PuzzleMenus backMenu;

    //number of columns
    public int cols;

    //number of rows
    public int rows;

    //width/height of the piece (Square)
    public float pieceSize;
    float width;
    float height;
    float depth;

    //offsets for the z pos
    float LeftOffset;
    float RightOffset;
    //pieces
    public PieceController[] pieces;

    //Timer
    public PuzzleTimer timer;

    //Successful completion
    public event Action OnCompletion;

    //Unsuccessful completion
    public event Action OnFailure;

     void Awake()
    {
        //Sizes in Unity units
        width = cols * pieceSize;
        height = rows * pieceSize;
        //Reset for every new scene
        LeftOffset = 0.0f;
        RightOffset = 0.0f;
        for (int i = 0; i < pieces.Length; i++)
        {
            if(pieces[i].transform.position.x < 0)
            {
                //leftangle
                MovePiece(StaticClass.Left, pieces[i],LeftOffset);
                LeftOffset += 1.5f;
            }
            else if(pieces[i].transform.position.x > 0)
            {
                //rightangle
                MovePiece(StaticClass.Right, pieces[i],RightOffset);
                RightOffset += 1.5f;
            }
            //pieces[i].transform.position = new Vector3(pieces[i].transform.position.x, pieces[i].transform.position.y, 2.5f);
            // Debug.Log(pieces[i].name);
            // Debug.Log(pieces[i].transform.position.z);

            //Pieces alays face user
            //get direction (pos of the piece - pos of the camera)
            Vector3 direction = pieces[i].transform.position - Camera.main.transform.position;

            //set forward of the piece negative as want the tex to face player
            pieces[i].transform.forward = -direction;

        }
    }

    
    //Determine new position of pieces based of the angle passed(ROM angle)
    void MovePiece(float angle, PieceController piece, float offset)
    {
        //Edit angle based off progression so far
        if (StaticClass.PuzzleIncrement == 0.0f)
        {
            angle -= 5.0f;
        }
        else if(StaticClass.PuzzleIncrement == 1.0f)
        {
            //Angle does not change
        }
        else if(StaticClass.PuzzleIncrement == 2.0f)
        {
            angle += 5.0f;
        }

        //Convert angle in radians
        angle = angle * (Mathf.Deg2Rad);
        print(angle);

        //Calculate z position
        float zPos =(Mathf.Abs(piece.transform.position.x)) / Mathf.Tan(angle);


        //Change piece position
        piece.transform.position = new Vector3(piece.transform.position.x, piece.transform.position.y, zPos + offset);
    }


    //get the cell - col and row info from the point passed
    public Vector2 GetCellFromPoint(Vector3 point)
    {
        //Get the local point
        Vector3 localPoint = transform.InverseTransformPoint(point);//World space to local space

        //Get the depth
        depth = localPoint.z;

        //Need to scale the local points by the size of the panel to get the right coords
        localPoint = Vector3.Scale(localPoint, transform.localScale); //does scale for each coord x,y and z

       
        //Get the cell - col and row 
        //Assumptions:
        //1) The horizontal is x not z, z is the depth
        //2) the anchor point of the puzzle is its center
        //3) No padding all the pices fit the puzzles perfectly
        //4) the face of the puzzle is not rotated about x or z

        //get col
        float column = Mathf.Floor(localPoint.x + width / 2) / pieceSize;

        //get row
        float row = Mathf.Floor(localPoint.y + height / 2) / pieceSize;

        return new Vector2(column, row);
    }

    //Check if a cell already has a puzzle piece
    public bool IsTaken(Vector2 cell)
    {
        //Go through each piece and check if it is in that cell
        for (int i = 0; i < pieces.Length; i++)
        {
            //If the piece is there then taken
            if (pieces[i].isPlaced && pieces[i].currentCell == cell)
            {
                print("Cell taken");
                return true;
            }
            
        }
        //If not then free
        return false;
    }

    //Given a cells col and row get the global coord of the center of the cell
    public Vector3 GetCellPosition(Vector2 cell)
    {
        //cell info - local point
        //Go from middle to far left + half the size of a piece + the amount of cells we are across / adjust for scale
        float x = ((-width / 2) + (pieceSize / 2) + (cell.x * pieceSize)) / (transform.localScale.x);
        float y = ((-height / 2) + (pieceSize / 2) + (cell.y * pieceSize)) / (transform.localScale.y);

        Vector3 localPoint = new Vector3(x, y, depth);

        //local point to global coord
        Vector3 globalPoint = transform.TransformPoint(localPoint);

        return globalPoint;

    }

    // Checks for the completion of the puzzle
    public void CheckCompletion()
    {
        bool correctCheck = true;

        //Loop through all the pieces
        for (int i = 0; i < pieces.Length; i++)
        {
            //Check that the piece is placed
            if (!pieces[i].isPlaced)
            {
                //If not placed exit the check
                return;
            }

            //Check if the piece is in the right place
            correctCheck = correctCheck && pieces[i].CheckCorrect();

            
        }
        print(correctCheck);

        //If they are all correct call a puzzle competion event
        if (correctCheck)
        {
            if (OnCompletion != null)
            {
                OnCompletion();
                //Load the back menu
                backMenu.LoadBackMenu();
                //Stop timer
                timer.ToggleActive(false);
                //Save the timer
                StaticClass.Timer = timer.GetElapsed();
            }
        }
        //If not correct send pieces to original position
        else
        {
            if (OnFailure != null)
            {
                OnFailure();
            }
        }
        

    }
}
