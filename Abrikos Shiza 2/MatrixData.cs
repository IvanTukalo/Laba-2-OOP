using System;
using System.Text;

public partial class MyMatrix
{
    private double[,] matrix;

    // Конструктор копіювання
    public MyMatrix(MyMatrix other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        InitializeMatrix(other.matrix);
    }

    // Конструктор з двовимірного масиву
    public MyMatrix(double[,] data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        InitializeMatrix(data);
    }

    // Конструктор з зубчастого масиву
    public MyMatrix(double[][] data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        ValidateJaggedArray(data);
        matrix = new double[data.Length, data[0].Length];
        FillMatrixFromJaggedArray(data);
    }

    // Конструктор з масиву рядків
    public MyMatrix(string[] rows)
    {
        if (rows == null || rows.Length == 0)
            throw new ArgumentException("Рядки не можуть бути порожніми.");
        matrix = ParseRowsToMatrix(rows);
    }
        
    // Конструктор з одного рядка, який делегує конструктору з масиву рядків
    public MyMatrix(string input) : this(
        input?.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Вхідні дані не можуть бути порожніми.");
    }

    // Приватний метод для ініціалізації матриці
    private void InitializeMatrix(double[,] source)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);
        matrix = new double[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix[i, j] = source[i, j];
    }

    // Приватний метод для перевірки зубчастого масиву
    private void ValidateJaggedArray(double[][] data)
    {
        int cols = data[0].Length;
        foreach (var row in data)
        {
            if (row.Length != cols)
                throw new ArgumentException("Масив не є прямокутним.");
        }
    }

    // Приватний метод для заповнення матриці із зубчастого масиву
    private void FillMatrixFromJaggedArray(double[][] data)
    {
        for (int i = 0; i < data.Length; i++)
            for (int j = 0; j < data[i].Length; j++)
                matrix[i, j] = data[i][j];
    }

    // Приватний метод для парсингу рядків у матрицю
    private double[,] ParseRowsToMatrix(string[] rows)
    {
        int rowCount = rows.Length;
        int colCount = rows[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        var result = new double[rowCount, colCount];
        for (int i = 0; i < rowCount; i++)
        {
            var elements = rows[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != colCount)
                throw new ArgumentException("Матриця не є прямокутною.");
            for (int j = 0; j < colCount; j++)
                result[i, j] = double.Parse(elements[j]);
        }
        return result;
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
