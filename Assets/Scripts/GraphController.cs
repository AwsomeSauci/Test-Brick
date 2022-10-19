using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
[Serializable]
public struct Edge
{
    public int point1;
    public int point2;
    public Edge(int p1, int p2)
    {
        this.point1 = p1;
        this.point2 = p2;
    }
}
public class GraphController : MonoBehaviour
{
    [SerializeField] private GraphItem[] graph;

    private List<Edge> Edges= new List<Edge>();    
    private List<int> tempPoints= new List<int>();    
    private List<int> list= new List<int>();
    private List<List<int>> listWays= new List<List<int>>();

    private int tempID;

    public void UnlearnAllItems()
    {
        for (int i = 0; i < graph.Length; i++)
        {
            if (graph[i].CheckLearn())
            graph[i].UnLearnItem();
        }
    }

    public bool CheckUnlearnPossibility(GraphItem graphItem)
    {
        MakeLearnedGraph();
        tempID = graphItem.GetID();
        if (list != null)
            list.Clear();
        if (listWays != null)
            listWays.Clear();
        return CheckAvaliableWay(Edges, tempID, list, listWays);
    }

    private void MakeLearnedGraph()
    {
        if(Edges!=null)
        Edges.Clear();
        for(int i = 0; i < graph.Length; i++)
        {
            if (graph[i].CheckLearn())
            {
                tempPoints = graph[i].GetLearnedItems();
                tempID = graph[i].GetID();
                for (int j = 0; j < tempPoints.Count; j++)
                {
                    Edges.Add(new Edge(tempID, tempPoints[j]));
                }
            }
            
        }
    }

    private bool Find(List<int> list, int num)
    {
        if (list == null) return false;

        foreach (var i in list)
        {
            if (i == num) return true;
        }

        return false;
    }   

    private bool CheckAvaliableWay(List<Edge> edges, int numberEdge, List<int> list, List<List<int>> listWays)
    {
        foreach (var edge in edges)
        {
            if (!Find(list, numberEdge))
            {
                if (edge.point1 == numberEdge)
                {
                    list.Add(numberEdge);

                    if (!Find(list, edge.point2)) CheckAvaliableWay(edges, edge.point2, list, listWays);
                    else
                    {
                        AddWay(list, listWays);
                    }
                    list.Remove(numberEdge);
                }
                else if (edge.point2 == numberEdge)
                {
                    list.Add(numberEdge);

                    if (!Find(list, edge.point1)) CheckAvaliableWay(edges, edge.point1, list, listWays);
                    else
                    {
                        AddWay(list, listWays);

                    }
                    list.Remove(numberEdge);
                }
            }
            if(listWays.Count>0)
            if (listWays[listWays.Count - 1][listWays[listWays.Count - 1].Count - 1] == 0)
            {
                return true;
            }
        }
        return false;
    }

    private void AddWay(List<int> templist, List<List<int>> listWays)
    {
        int i = 0;
        bool temp = true;
        while (i < listWays.Count && temp)
        {
            if (templist.SequenceEqual(listWays[i])) temp = false;
            else i++;
        }
        if (temp || i == listWays.Count)
        {
            listWays.Add(new List<int>());
            foreach (var j in templist)
            {
                listWays[listWays.Count - 1].Add(j);
            }
        }
    }    
}
