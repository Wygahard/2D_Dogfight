using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commandBuffer;
    static List<ICommand> commandHistory;
    static int counter;

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        //check if a new command arrives while we're undoing
            //Create a new timeline
        while(commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }
        commandBuffer.Enqueue(command);
    }

    private void Update()
    {
        if(commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();
            //commandBuffer.Dequeue().Execute();

            commandHistory.Add(c);
            counter++;
        }
        //move in the command history
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(counter > 0)
                {
                    counter--;
                    commandHistory[counter].Undo();
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(counter < commandHistory.Count)
                {
                    commandHistory[counter].Execute();
                    counter++;
                }                
            }
        }
    }
}
