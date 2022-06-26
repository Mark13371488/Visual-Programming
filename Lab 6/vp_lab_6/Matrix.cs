using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp_lab_6
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }

        public double[,] Elem { get; }

        public Matrix()
        {
            Rows = 1;
            Columns = 1; 
            Elem = new double[Rows,Columns];
        }
        public Matrix(double[,] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            double[,] arr = new double[Rows, Columns];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arr[i, j] = array[i, j];
                }
            }
            Elem = arr;
        }
        public void IsSquare()
        {
            if (Rows != Columns)
                throw new MatrixException("Матриця повинна бути квадратна");
        }
        public double this[int row, int column]
        {
            get
            {
                try
                {
                    return Elem[row, column];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new MatrixException("Немає такого елемента з таким індексом");
                }
            }
            set
            {
                try
                {
                    Elem[row, column] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new MatrixException("Немає такого елемента з таким індексом");
                }
            }

        }
        public static Matrix operator+(Matrix A, Matrix B)
        {
            if (A == null || B == null)
            {
                throw new MatrixException("Матриці пусті!");
            }
            if (A.Rows != B.Rows || A.Columns != B.Columns)
            {
                throw new MatrixException("Розміри матриці не рівні!");
            }
            double[,] result = new double[A.Rows, B.Columns];
            for (int i = 0; i < B.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }
            return new Matrix(result);
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A == null || B == null)
            {
                throw new MatrixException("Матриці пусті!");
            }
            if (A.Rows != B.Rows || A.Columns != B.Columns)
            {
                throw new MatrixException("Розміри матриці не рівні!");
            }
            double[,] result = new double[A.Rows, B.Columns];
            for (int i = 0; i < B.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    result[i, j] = A[i, j] - B[i, j];
                }
            }
            return new Matrix(result);
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A == null || B == null)
            {
                throw new MatrixException("Матриці пусті!");
            }
            if (A.Columns != B.Rows)
            {
                throw new MatrixException("Множення не можливе!");
            }
            var multipliedMatrix = new double[A.Rows, B.Columns];
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    multipliedMatrix[i, j] = 0;
                    for (var k = 0; k < A.Columns; k++)
                    {
                        multipliedMatrix[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return new Matrix(multipliedMatrix);
        }
    }
}
