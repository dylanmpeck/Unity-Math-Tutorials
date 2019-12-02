using System.Collections;
using System.Collections.Generic;
using System;

public class Matrix
{
    float[] values;
    int rows;
    int cols;

    static float[] IDENTITY = {1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1};

    public Matrix(int r, int c, float[] v)
    {
        rows = r;
        cols = c;
        values = new float[rows * cols];
        Array.Copy(v, values, rows * cols);
    }

    public Coords AsCoords()
    {
        if (rows == 4 && cols == 1)
            return new Coords(values[0], values[1], values[2], values[3]);
        return null;
    }

    public override string ToString()
    {
        string matrix = "";

        for (int i = 0; i < rows; i++)
        {
            matrix += "[ ";
            for (int j = 0; j < cols; j++)
            {
                matrix += values[cols * i + j].ToString() + " ";
            }
            matrix += "]\n";
        }
        return matrix;
    }

    static public Matrix operator+(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
            return null;

        float[] addValues = new float[a.values.Length];
        for (int i = 0; i < a.values.Length; i++)
        {
            addValues[i] = a.values[i] + b.values[i];
        }
        return new Matrix(a.rows, a.cols, addValues);
    }

    static public Matrix operator*(Matrix a, Matrix b)
    {
        if (a.cols != b.rows)
            return null;

        int r = a.rows;
        int c = b.cols;
        float[] vals = new float[r * c];
        int vCounter = 0;
        for (int aRow = 0; aRow < a.rows; aRow++)
        {
            for (int bCol = 0; bCol < b.cols; bCol++)
            {
                float value = 0;
                for (int bRow = 0; bRow < b.rows; bRow++)
                {
                    value += a.values[a.cols * aRow + bRow] * b.values[b.cols * bRow + bCol];
                }
                vals[vCounter] = value;
                vCounter++;
            }
        }
        return new Matrix(r, c, vals);
    }

    public float GetValue(int r, int c)
    {
        float mVal = 0;
        mVal = values[r * cols + c];
        return mVal;
    }

    static public Matrix GetTranslationMatrix(Coords vector)
    {
        float[] transVals = new float[IDENTITY.Length];
        Array.Copy(IDENTITY, transVals, transVals.Length);
        transVals[4 * 0 + 3] = vector.x;
        transVals[4 * 1 + 3] = vector.y;
        transVals[4 * 2 + 3] = vector.z;
        return new Matrix(4, 4, transVals);
    }
}
