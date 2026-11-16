using System;

namespace OOPLab2
{
    public partial class MyMatrix
    {
        public static MyMatrix operator +(MyMatrix a, MyMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
            {
                throw new ArgumentException("Для додавання матриці повинні мати однаковий розмір!");
            }

            double[,] res = new double[a.Height, a.Width];

            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    res[i, j] = a[i, j] + b[i, j];
                }
            }

            return new MyMatrix(res);
        }

        public static MyMatrix operator *(MyMatrix a, MyMatrix b)
        {
            if (a.Width != b.Height)
            {
                throw new ArgumentException("Кількість стовпчиків першої матриці повинна дорівнювати кількості рядків другої!");
            }

            int rows = a.Height;
            int cols = b.Width;
            int common = a.Width;

            double[,] res = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < common; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    res[i, j] = sum;
                }
            }

            return new MyMatrix(res);
        }

        protected double[,] GetTransponedArray()
        {
            int rows = Height;
            int cols = Width;

            double[,] transponed = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transponed[j, i] = matrix[i, j];
                }
            }

            return transponed;
        }

        public MyMatrix GetTransponedCopy()
        {
            double[,] transponedArr = GetTransponedArray();
            return new MyMatrix(transponedArr);
        }

        public void TransponeMe()
        {
            this.matrix = GetTransponedArray();
            isDetCached = false;
        }

        public double CalcDeterminant()
        {
            if (Height != Width)
            {
                throw new InvalidOperationException("Детермінант можна обчислити лише для квадратної матриці!");
            }

            if (isDetCached)
            {
                return cachedDet;
            }

            int n = Height;

            double[,] copy = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            double det = 1.0;
            const double EPS = 1e-9;

            for (int i = 0; i < n; i++)
            {
                int pivotRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(copy[k, i]) > Math.Abs(copy[pivotRow, i]))
                    {
                        pivotRow = k;
                    }
                }

                if (Math.Abs(copy[pivotRow, i]) < EPS)
                {
                    cachedDet = 0.0;
                    isDetCached = true;
                    return 0.0;
                }

                if (pivotRow != i)
                {
                    for (int j = i; j < n; j++)
                    {
                        double temp = copy[i, j];
                        copy[i, j] = copy[pivotRow, j];
                        copy[pivotRow, j] = temp;
                    }
                    det = -det;
                }

                det *= copy[i, i];

                for (int k = i + 1; k < n; k++)
                {
                    double factor = copy[k, i] / copy[i, i];
                    for (int j = i; j < n; j++)
                    {
                        copy[k, j] -= factor * copy[i, j];
                    }
                }
            }

            cachedDet = det;
            isDetCached = true;

            return det;
        }
    }
}
