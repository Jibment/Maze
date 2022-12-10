using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{

    MapGenerator mapGenerator;

    public enum DIRECTION
    {
        TOP,
        RIGHT,
        DOWN,
        LEFT,
    }


    public DIRECTION direction;
    public List<int> actions = new List<int>();

    public Vector2Int currentPos, nextPos;

    public bool startflg = false;
    private int currentDirection = 0;

    public Text TextTutorial;
    public Text TextGoal;
    public Text TextActions;

    int[,] move =
    {
        {0,-1},  //TOP
        {1,0},  //RIGHT
        {0,1},  //DOWN
        {-1,0 }   //LEFT
    };


    private void Start()
    {
        mapGenerator = transform.parent.GetComponent<MapGenerator>();
        TextTutorial = GameObject.Find("TextTutorial").GetComponent<Text>();
        TextGoal = GameObject.Find("TextGoal").GetComponent<Text>();
        TextActions = GameObject.Find("TextActions").GetComponent<Text>();

    }
    // Update is called once per frame
    private void Update()
    {
        if(startflg == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = DIRECTION.TOP;
                actions.Add((int)direction);
                textActions();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = DIRECTION.RIGHT;
                actions.Add((int)direction);
                textActions();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = DIRECTION.DOWN;
                actions.Add((int)direction);
                textActions();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = DIRECTION.LEFT;
                actions.Add((int)direction);
                textActions();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startflg = !startflg;
            textActivate(startflg);
            if(startflg == false)
            {
                _reset();
            }
            
        }

        if (startflg == true)
        {
            _move();

        }


    }

    void _reset()
    {

        //position reset
        nextPos = mapGenerator.FIRSTPLAYERPOSITION;
        transform.localPosition = mapGenerator.ScreenPos(nextPos);
        currentPos = nextPos;

        //action reset
        actions.Clear();
        currentDirection = 0;

        //text reset
        TextActions.text = "Set Actions";
        TextGoal.text = "";
    }

    void _move()
    {
            nextPos = currentPos + new Vector2Int(move[actions[currentDirection], 0], move[actions[currentDirection], 1]);
            if (mapGenerator.GetNextMapType(nextPos) != MapGenerator.MAP_TYPE.WALL)
            {
                transform.localPosition = mapGenerator.ScreenPos(nextPos);
                currentPos = nextPos;
            }
            if (mapGenerator.GetNextMapType(currentPos) == MapGenerator.MAP_TYPE.ACTION)
            {
                if (currentDirection + 1 < actions.Count)
                {
                    currentDirection++;
                }
            }
        
      }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Goal(Clone)")
        {
            goalActivate();
        }
    }
   


    /*テキスト関連の処理*/

    private void textActivate(bool startflg)
    {
        if (startflg == false)
        {
            TextTutorial.text = "Set Actions and push \"SPACE\" to start!!";
        }
        if (startflg == true)
        {
            TextTutorial.text = "push \"SPACE\" to reset!!"; ;
        }
    }

    private void textActions()
    {
        string decidesdactions = "";
        if(actions.Count == 0)
        {
            TextActions.text = "No Actions!";
        }
        else
        {
            for(int i=0; i < actions.Count; i++)
            {
                switch (actions[i])
                {
                    case 0: decidesdactions += "↑"; break;
                    case 1: decidesdactions += "→"; break;
                    case 2: decidesdactions += "↓"; break;
                    case 3: decidesdactions += "←"; break;
                }
            }
        }

        TextActions.text = decidesdactions;
    }

    private void goalActivate()
    {
       TextGoal.text = "GOAL";
    }

}
