using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Graph
{
    private int[,] adjacencyMatrix;
    private int vertexCount;

    public Graph(int size)
    {
        vertexCount = size;
        adjacencyMatrix = new int[size, size];
    }

    public void AddVertex()
    {
        int[,] newMatrix = new int[vertexCount + 1, vertexCount + 1];
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = 0; j < vertexCount; j++)
            {
                newMatrix[i, j] = adjacencyMatrix[i, j];
            }
        }
        adjacencyMatrix = newMatrix;
        vertexCount++;
    }

    public void RemoveVertex(int vertex)
    {
        int[,] newMatrix = new int[vertexCount - 1, vertexCount - 1];
        for (int i = 0, newI = 0; i < vertexCount; i++)
        {
            if (i == vertex) continue;
            for (int j = 0, newJ = 0; j < vertexCount; j++)
            {
                if (j == vertex) continue;
                newMatrix[newI, newJ] = adjacencyMatrix[i, j];
                newJ++;
            }
            newI++;
        }
        adjacencyMatrix = newMatrix;
        vertexCount--;
    }

    public void AddEdge(int start, int end)
    {
        adjacencyMatrix[start, end] = 1;
        adjacencyMatrix[end, start] = 1; // Для неориентированного графа
    }

    public void RemoveEdge(int start, int end)
    {
        adjacencyMatrix[start, end] = 0;
        adjacencyMatrix[end, start] = 0; // Для неориентированного графа
    }

    public bool EdgeExists(int start, int end)
    {
        return adjacencyMatrix[start, end] == 1;
    }

    public bool IsIsolated(int vertex)
    {
        for (int i = 0; i < vertexCount; i++)
        {
            if (adjacencyMatrix[vertex, i] == 1)
            {
                return false;
            }
        }
        return true;
    }
}
class Program
{
    static void Main(string[] args)
    {
        int initialSize = 1000; // Начальный размер графа
        Graph graph = new Graph(initialSize);

        Stopwatch stopwatch = new Stopwatch();

        // Измерение времени для AddVertex
        stopwatch.Start();
        graph.AddVertex();
        stopwatch.Stop();
        Console.WriteLine($"AddVertex Time: {stopwatch.ElapsedMilliseconds} ms");

        // Измерение времени для RemoveVertex
        stopwatch.Restart();
        graph.RemoveVertex(initialSize / 2);
        stopwatch.Stop();
        Console.WriteLine($"RemoveVertex Time: {stopwatch.ElapsedMilliseconds} ms");

        // Измерение времени для AddEdge
        stopwatch.Restart();
        graph.AddEdge(0, 1);
        stopwatch.Stop();
        Console.WriteLine($"AddEdge Time: {stopwatch.ElapsedTicks} ticks");

        // Измерение времени для RemoveEdge
        stopwatch.Restart();
        graph.RemoveEdge(0, 1);
        stopwatch.Stop();
        Console.WriteLine($"RemoveEdge Time: {stopwatch.ElapsedTicks} ticks");

        // Измерение времени для EdgeExists
        stopwatch.Restart();
        bool exists = graph.EdgeExists(0, 1);
        stopwatch.Stop();
        Console.WriteLine($"EdgeExists Time: {stopwatch.ElapsedTicks} ticks");

        // Измерение времени для IsIsolated
        stopwatch.Restart();
        bool isolated = graph.IsIsolated(0);
        stopwatch.Stop();
        Console.WriteLine($"IsIsolated Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.ReadLine();
    }
}
