using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingClasses : MonoBehaviour
{ 

    public ChildState test;

    public Dictionary<string, dynamic> dic;

    // Start is called before the first frame update
    void Start()
    {

        dic = new Dictionary<string, dynamic>();

        dic.Add("1", 1);
        dic.Add("2", 2);

        test = new ChildState(1f, dic);
        test.Test();
    }

    //public State testState = new State(4.20f, 69);


    // Update is called once per frame
    void Update()
    {
        //testState.PlayDebug();
    }


    public class State
    {
        private float f;

        private int i;

        public State(float ff, int ii)
        {
            f = ff;
            i = ii;
        }

        public void PlayDebug()
        {
            Debug.Log("float: " + f + "," + "int: " + i);
        }
        
    }

    public abstract class ParentState
    {
        float f;

        Dictionary<string, dynamic> dict;
        //int dict;
        public ParentState(float ff, Dictionary<string, dynamic> InputDict)
        {
            f = ff;
            dict = InputDict;
        }
    }

    public class ChildState : ParentState
    {
        float f;

        Dictionary<string, dynamic> dictonairy;

        public ChildState(float flooat, Dictionary<string, dynamic> dict) : base (flooat, dict) {
            f = flooat;
            dictonairy = dict;
        }

        public void Test()
        {
            Debug.Log(dictonairy["1"]);
        }
    }

    //public ParentState test = new ParentState(1f,1);

        
        
}
