using System;
using System.Collections.Generic;
using System.Diagnostics;

public class IncidenceMatrix
{
    private List<string> vertices;
    private List<Tuple<int, int>> edges;
    private int[,] matrix;

    public IncidenceMatrix()
    {
        vertices = new List<string>();
        edges = new List<Tuple<int, int>>();
    }

    public void AddVertex(string vertex)
    {
        vertices.Add(vertex);
        UpdateMatrix();
    }

    public void RemoveVertex(string vertex)
    {
        int index = vertices.IndexOf(vertex);
        if (index != -1)
        {
            vertices.RemoveAt(index);
            edges.RemoveAll(e => e.Item1 == index || e.Item2 == index);
            UpdateMatrix();
        }
    }

    public void AddEdge(int from, int to)
    {
        if (from < vertices.Count && to < vertices.Count)
        {
            edges.Add(new Tuple<int, int>(from, to));
            UpdateMatrix();
        }
    }

    public void RemoveEdge(int from, int to)
    {
        edges.RemoveAll(e => e.Item1 == from && e.Item2 == to);
        UpdateMatrix();
    }

    public bool EdgeExists(int from, int to)
    {
        return edges.Exists(e => e.Item1 == from && e.Item2 == to);
    }

    public bool IsIsolated(int vertex)
    {
        return edges.FindAll(e => e.Item1 == vertex || e.Item2 == vertex).Count == 0;
    }

    private void UpdateMatrix()
    {
        int edgeCount = edges.Count;
        int vertexCount = vertices.Count;
        matrix = new int[vertexCount, edgeCount];

        for (int i = 0; i < edgeCount; i++)
        {
            var edge = edges[i];
            matrix[edge.Item1, i] = 1; // Исходящая вершина
            matrix[edge.Item2, i] = -1; // Входящая вершина
        }
    }


}

class Program
{
    static void Main(string[] args)
    {
        IncidenceMatrix graph = new IncidenceMatrix();

        // Добавление вершин
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            string vertexName = c.ToString();
            graph.AddVertex(vertexName);
        }
        stopwatch.Stop();
        Console.WriteLine($"AddVertex Time: {stopwatch.ElapsedMilliseconds} ms");

        // Добавление рёбер
        stopwatch.Restart();
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        stopwatch.Stop();
        Console.WriteLine($"AddEdge Time: {stopwatch.ElapsedMilliseconds} ms");

        // Удаление вершины
        stopwatch.Restart();
        graph.RemoveVertex("B");
        stopwatch.Stop();
        Console.WriteLine($"RemoveVertex Time: {stopwatch.ElapsedMilliseconds} ms");

        // Проверка существования рёбра
        stopwatch.Restart();
        bool exists = graph.EdgeExists(0, 2);
        stopwatch.Stop();
        Console.WriteLine($"EdgeExists Time: {stopwatch.ElapsedTicks} ticks");

        // Проверка на изолированность вершины
        stopwatch.Restart();
        bool isolated = graph.IsIsolated(0);
        stopwatch.Stop();
        Console.WriteLine($"IsIsolated Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadLine();
    }
}
