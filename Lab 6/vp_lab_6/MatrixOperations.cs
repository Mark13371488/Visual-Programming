namespace vp_lab_6
{
    public static class MatrixOperations
    {
        public static Matrix Add(Matrix A, Matrix B)
        {
            return A + B;
        }
        public static Matrix Substract(Matrix A, Matrix B)
        {
            return A - B;
        }
        public static Matrix Multiple(Matrix A, Matrix B)
        {
            return A * B;
        }
        public static double Trace(Matrix A)
        {
            
            double trace = 0;
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    if(i==j)
                        trace += A[i, j];
                }
            }
            return trace;
        }
    }
}
