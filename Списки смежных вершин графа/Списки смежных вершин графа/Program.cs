using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Graph
{
    private Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<int>();
        }
    }

    public void RemoveVertex(int vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            adjacencyList.Remove(vertex);
            foreach (var list in adjacencyList.Values)
            {
                list.Remove(vertex);
            }
        }
    }

    public void AddEdge(int vertex1, int vertex2)
    {
        if (adjacencyList.ContainsKey(vertex1) && adjacencyList.ContainsKey(vertex2))
        {
            adjacencyList[vertex1].Add(vertex2);
            adjacencyList[vertex2].Add(vertex1);
        }
    }

    public void RemoveEdge(int vertex1, int vertex2)
    {
        if (adjacencyList.ContainsKey(vertex1) && adjacencyList.ContainsKey(vertex2))
        {
            adjacencyList[vertex1].Remove(vertex2);
            adjacencyList[vertex2].Remove(vertex1);
        }
    }

    public bool EdgeExists(int vertex1, int vertex2)
    {
        return adjacencyList.ContainsKey(vertex1) && adjacencyList[vertex1].Contains(vertex2);
    }

    public bool IsIsolated(int vertex)
    {
        return adjacencyList.ContainsKey(vertex) && adjacencyList[vertex].Count == 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();
        Stopwatch stopwatch = new Stopwatch();

        // Измерение времени для AddVertex
        stopwatch.Start();
        for (int i = 0; i < 1000; i++) // Добавление 1000 вершин
        {
            graph.AddVertex(i);
        }
        stopwatch.Stop();
        Console.WriteLine($"AddVertex (1000 вершин) Time: {stopwatch.ElapsedMilliseconds} ms");

        // Измерение времени для AddEdge
        stopwatch.Restart();
        for (int i = 0; i < 500; i++) // Добавление 500 рёбер
        {
            graph.AddEdge(i, (i + 1) % 1000);
        }
        stopwatch.Stop();
        Console.WriteLine($"AddEdge (500 рёбер) Time: {stopwatch.ElapsedMilliseconds} ms");

        // Измерение времени для RemoveVertex
        stopwatch.Restart();
        for (int i = 0; i < 500; i++) // Удаление 500 вершин
        {
            graph.RemoveVertex(i);
        }
        stopwatch.Stop();
        Console.WriteLine($"RemoveVertex (500 вершин) Time: {stopwatch.ElapsedMilliseconds} ms");

        // Измерение времени для EdgeExists
        stopwatch.Restart();
        bool exists = graph.EdgeExists(0, 1);
        stopwatch.Stop();
        Console.WriteLine($"EdgeExists Time: {stopwatch.ElapsedTicks} ticks");

        // Измерение времени для IsIsolated
        stopwatch.Restart();
        bool isolated = graph.IsIsolated(999);
        stopwatch.Stop();
        Console.WriteLine($"IsIsolated Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadLine();
    }
}
