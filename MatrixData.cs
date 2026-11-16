using System;

namespace OOPLab2
{
    public partial class MyMatrix
    {
        private double[,] matrix;

        private double cachedDet;
        private bool isDetCached = false;

        public MyMatrix(MyMatrix other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("Матриця для копіювання не може бути порожньою.");
            }

            int rows = other.Height;
            int cols = other.Width;
            matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = other.matrix[i, j];
                }
            }
        }

        public MyMatrix(double[,] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException("Масив не може бути null.");
            }

            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = arr[i, j];
                }
            }
        }

        public MyMatrix(double[][] jagged)
        {
            if (jagged == null || jagged.Length == 0 || jagged[0] == null || jagged[0].Length == 0)
            {
                throw new ArgumentException("Зубчастий масив порожній або некоректний.");
            }

            int rows = jagged.Length;
            int cols = jagged[0].Length;

            for (int i = 0; i < rows; i++)
            {
                if (jagged[i] == null || jagged[i].Length != cols)
                {
                    throw new ArgumentException("Зубчастий масив не є прямокутним!");
                }
            }

            matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = jagged[i][j];
                }
            }
        }

        public MyMatrix(string[] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                throw new ArgumentException("Масив рядків порожній.");
            }

            char[] separators = new char[] { ' ', '\t' };

            string[] firstRowParts = rows[0].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int height = rows.Length;
            int width = firstRowParts.Length;

            if (width == 0)
            {
                throw new ArgumentException("Рядки не містять чисел.");
            }

            matrix = new double[height, width];

            for (int i = 0; i < height; i++)
            {
                string[] parts = rows[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != width)
                {
                    throw new ArgumentException("Різна кількість чисел у рядках матриці!");
                }

                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = double.Parse(parts[j]);
                }
            }
        }

        public MyMatrix(string text)
            : this(text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
        }

        public int Height
        {
            get { return matrix.GetLength(0); }
        }

        public int Width
        {
            get { return matrix.GetLength(1); }
        }

        public int getHeight()
        {
            return matrix.GetLength(0);
        }

        public int getWidth()
        {
            return matrix.GetLength(1);
        }

        public double this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі матриці.");
                }
                return matrix[row, col];
            }
            set
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі матриці.");
                }
                matrix[row, col] = value;

                isDetCached = false;
            }
        }

        public double getElement(int row, int col)
        {
            return this[row, col];
        }

        public void setElement(int row, int col, double value)
        {
            this[row, col] = value;
        }

        override public String ToString()
        {
            string result = "";
            int rows = Height;
            int cols = Width;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result += matrix[i, j];
                    if (j < cols - 1)
                    {
                        result += "\t";
                    }
                }
                if (i < rows - 1)
                {
                    result += "\n";
                }
            }

            return result;
        }
    }
}
