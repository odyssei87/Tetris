using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Figurs
    {
        public int x;                      // позиция на поле
        public int y;
        public int[,] matrix;             // массив с текущей фигурой
        public int[,] nextMatrix;          // массив следующей фигуры
        public int sizeMatrix;             // размер матрицы
        public int nextSizeMatrix;         // размер следующей матрицы


        public int[,] figur1 = new int[4, 4]       // палка
        {
            {0,0,1,0 },
            {0,0,1,0 },
            {0,0,1,0 },
            {0,0,1,0 },
        };

        public int[,] figur2 = new int[3, 3]    //  молния
        {
            {0,2,0 },
            {0,2,2 },
            {0,0,2 },
        };

        public int[,] figur3 = new int[3, 3]         // Т- образная
        {
            {0,0,0 },
            {3,3,3 },
            {0,3,0 },
        };

        public int[,] figur4 = new int[3, 3]       // Г- образная 
        {
            { 4,0,0 },
            {4,0,0 },
            {4,4,0 },
        };
        public int[,] figur5 = new int[2, 2]      // квадрат 
        {
            { 5,5 },
            { 5,5 },
           
        };

        public Figurs(int _x, int _y) 
        {
            if (matrix==null)
            {
                matrix = GenerateMatrix();
            }
            else
            {
                matrix = nextMatrix;
            }
            x = _x;
            y = _y;
            sizeMatrix = (int)Math.Sqrt(matrix.Length);                     // получение размера  матрицы  (массив двумерный, и вернется не 3 а 9, поэтому извлечем корень)
           nextMatrix=GenerateMatrix();
            nextSizeMatrix= (int)Math.Sqrt(nextMatrix.Length);             // получение размера  следующей матрицы  (массив двумерный, и вернется не 3 а 9, поэтому извлечем корень)
        }

        public int[,] GenerateMatrix()                                     // генерация фигур
        {
            
            int[,] _matrix=figur1;
            Random r = new Random();
            
            switch (r.Next(1, 6))
            {
                case 1:
                    
                    _matrix = figur1;
                    break;
                case 2:
                    
                    _matrix = figur2;
                    break;
                case 3:
                   
                    _matrix = figur3;
                    break;
                case 4:
                    
                    _matrix = figur4;
                    break;
                case 5:
                    
                    _matrix = figur5;
                    break;
            }
            return _matrix;
        }
        public void RotateFigurs()                                 // поворот фигур
        {
            int[,] tempMatrix = new int[sizeMatrix, sizeMatrix];
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    tempMatrix[i, j] = matrix[j, (sizeMatrix - 1) - i];
                }
            }
            matrix = tempMatrix;
            int offset1 = (8 - (x + sizeMatrix));
            if (offset1 < 0)
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    MoveLeft();
            }

            if (x < 0)
            {
                for (int i = 0; i < Math.Abs(x) + 1; i++)
                    MoveRight();
            }
        }

        public void ResetFigurs(int _x, int _y)
        {
            x = _x;
            y = _y;
            matrix = nextMatrix;
            sizeMatrix = (int)Math.Sqrt(matrix.Length);
            nextMatrix = GenerateMatrix();
            nextSizeMatrix = (int)Math.Sqrt(nextMatrix.Length);
        }

        public void MoveDown()
        {
            
            y++;
        }
        public void MoveRight()
        {
            
            x++;
        }
        public void MoveLeft()
        {
          
            x--;
        }

    }
}
