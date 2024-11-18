public partial class MyMatrix
{
    // Оператор додавання
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.Height != b.Height || a.Width != b.Width)
            throw new InvalidOperationException("Матриці мають різні розміри.");

        var result = new double[a.Height, a.Width];
        for (int i = 0; i < a.Height; i++)
            for (int j = 0; j < a.Width; j++)
                result[i, j] = a[i, j] + b[i, j];

        return new MyMatrix(result);
    }

    // Оператор множення
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.Width != b.Height)
            throw new InvalidOperationException("Матриці не можна множити (некоректні розміри).");

        var result = new double[a.Height, b.Width];
        for (int i = 0; i < a.Height; i++)
            for (int j = 0; j < b.Width; j++)
                for (int k = 0; k < a.Width; k++)
                    result[i, j] += a[i, k] * b[k, j];

        return new MyMatrix(result);
    }

    // Транспонування
    private double[,] GetTransponedArray()
    {
        var result = new double[Width, Height];
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
                result[j, i] = matrix[i, j];
        return result;
    }

    public MyMatrix GetTransponedCopy() => new MyMatrix(GetTransponedArray());

    public void TransponeMe() => matrix = GetTransponedArray();

    // Додаткове: визначник
    private double? determinantCache = null;

    public double CalcDeterminant()
    {
        if (Height != Width)
            throw new InvalidOperationException("Визначник обчислюється тільки для квадратних матриць.");

        if (determinantCache != null)
            return determinantCache.Value;

        determinantCache = CalculateDeterminantInternal((double[,])matrix.Clone());
        return determinantCache.Value;
    }

    private double CalculateDeterminantInternal(double[,] data)
    {
        int n = data.GetLength(0);
        if (n == 1)
            return data[0, 0];
        if (n == 2)
            return data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];

        double determinant = 0;
        for (int col = 0; col < n; col++)
        {
            var minor = GetMinor(data, 0, col);
            determinant += (col % 2 == 0 ? 1 : -1) * data[0, col] * CalculateDeterminantInternal(minor);
        }

        return determinant;
    }

    private double[,] GetMinor(double[,] matrix, int row, int col)
    {
        int n = matrix.GetLength(0);
        var minor = new double[n - 1, n - 1];
        for (int i = 0, mi = 0; i < n; i++)
        {
            if (i == row) continue;
            for (int j = 0, mj = 0; j < n; j++)
            {
                if (j == col) continue;
                minor[mi, mj++] = matrix[i, j];
            }
            mi++;
        }
        return minor;
    }
}
