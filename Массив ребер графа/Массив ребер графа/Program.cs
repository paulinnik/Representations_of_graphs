using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
public class Graph
{
    private HashSet<int> vertices;
    private HashSet<Tuple<int, int>> edges;

    public Graph()
    {
        vertices = new HashSet<int>();
        edges = new HashSet<Tuple<int, int>>();
    }

    public void AddVertex(int vertex)
    {
        vertices.Add(vertex);
    }

    public void RemoveVertex(int vertex)
    {
        vertices.Remove(vertex);
        edges.RemoveWhere(e => e.Item1 == vertex || e.Item2 == vertex);
    }

    public void AddEdge(int vertex1, int vertex2)
    {
        if (vertices.Contains(vertex1) && vertices.Contains(vertex2))
        {
            edges.Add(Tuple.Create(vertex1, vertex2));
        }
    }

    public void RemoveEdge(int vertex1, int vertex2)
    {
        edges.Remove(Tuple.Create(vertex1, vertex2));
    }

    public bool EdgeExists(int vertex1, int vertex2)
    {
        return edges.Contains(Tuple.Create(vertex1, vertex2));
    }

    public bool IsIsolated(int vertex)
    {
        return vertices.Contains(vertex) && !edges.Any(e => e.Item1 == vertex || e.Item2 == vertex);
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
