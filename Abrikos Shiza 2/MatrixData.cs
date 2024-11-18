using System;
using System.Text;

public partial class MyMatrix
{
    private double[,] matrix;

    // Конструктор з двовимірного масиву
    public MyMatrix(double[,] data)
    {
        matrix = (double[,])data.Clone();
    }

    // Конструктор з зубчастого масиву
    public MyMatrix(double[][] data)
    {
        int rows = data.Length;
        int cols = data[0].Length;

        foreach (var row in data)
        {
            if (row.Length != cols)
                throw new ArgumentException("Масив не є прямокутним.");
        }

        matrix = new double[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix[i, j] = data[i][j];
    }

    // Конструктор з масиву рядків
    public MyMatrix(string[] rows)
    {
        int rowCount = rows.Length;
        int colCount = rows[0].Split(' ').Length;

        matrix = new double[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            var elements = rows[i].Split(' ');
            if (elements.Length != colCount)
                throw new ArgumentException("Кількість чисел у рядках відрізняється.");

            for (int j = 0; j < colCount; j++)
                matrix[i, j] = double.Parse(elements[j]);
        }
    }

    // Конструктор з одного рядка
    public MyMatrix(string input)
    {
        var rows = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        int rowCount = rows.Length;
        int colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        matrix = new double[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            var elements = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != colCount)
                throw new ArgumentException("Матриця не є прямокутною.");

            for (int j = 0; j < colCount; j++)
                matrix[i, j] = double.Parse(elements[j]);
        }
    }

    // Властивості
    public int Height => matrix.GetLength(0);
    public int Width => matrix.GetLength(1);

    public int GetHeight() => Height;
    public int GetWidth() => Width;

    // Індексатор
    public double this[int row, int col]
    {
        get
        {
            ValidateIndices(row, col);
            return matrix[row, col];
        }
        set
        {
            ValidateIndices(row, col);
            matrix[row, col] = value;
        }
    }

    // Getter/Setter у Java-стилі
    public double GetElement(int row, int col) => this[row, col];
    public void SetElement(int row, int col, double value) => this[row, col] = value;

    // Переозначення ToString
    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
                sb.Append(matrix[i, j] + "\t");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    private void ValidateIndices(int row, int col)
    {
        if (row < 0 || row >= Height || col < 0 || col >= Width)
            throw new IndexOutOfRangeException("Індекси виходять за межі матриці.");
    }
}
