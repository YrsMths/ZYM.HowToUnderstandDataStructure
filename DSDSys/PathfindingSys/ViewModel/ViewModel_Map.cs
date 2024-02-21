using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingSys.ViewModel
{
    public class Map : IEnumerable<Square>
    {
        public Square[,] Squares { get; private set; }
        public int Rows { get; }
        public int Columns { get; }
        Square _StartSquare;
        /// <summary>
        /// 开始点
        /// </summary>
        public Square StartSquare { get; set; }
        Square _EndSquare;
        /// <summary>
        /// 结束点
        /// </summary>
        public Square EndSquare { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public Map()
        {
            this.Rows = 30;
            this.Columns = 30;
            GetNeighbors();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        public Map(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            Squares = new Square[rows, columns];
            GetNeighbors();
        }
        /// <summary>
        /// 获取邻居
        /// </summary>
        private void GetNeighbors()
        {
            for (int i = 0; i < Rows; i++) //行
            {
                for (int j = 0; j < Columns; j++) //列
                {
                    Squares[i, j].RowIndex = i;
                    Squares[i, j].ColumnIndex = j;
                    Squares[i, j].Neighbors.Left = (j > 0) ? Squares[i, j - 1] : null;
                    Squares[i, j].Neighbors.Top = (i > 0) ? Squares[i - 1, j] : null;
                    Squares[i, j].Neighbors.Right = (j < Columns - 1) ? Squares[i, j + 1] : null;
                    Squares[i, j].Neighbors.Bottom = (i < Rows - 1) ? Squares[i + 1, j] : null;
                }
            }
        }
        /// <summary>
        /// 二维索引器
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Square this[int rows, int columns]
        {
            get
            {
                if (rows < 0 || this.Rows < rows || columns < 0 || this.Columns < columns) throw new Exception("超出数据范围");
                return Squares[rows,columns];
            }
            set
            {
                if (rows < 0 || this.Rows < rows || columns < 0 || this.Columns < columns) throw new Exception("超出数据范围");
                Squares[rows,columns] = value;
            }
        }
        /// <summary>
        /// 设置开始点
        /// </summary>
        /// <param name="square"></param>
        /// <param name="func"></param>
        public void setStarting(Square square, Func<int, int, int, int, int> func)
        {
            if (StartSquare.Equals(square)) return; 
            bool reset = false;
            if (square.Type == SquareType_Enum.Ending) reset = true;
            foreach (var item in this)
            {
                if (reset) item.DistanEnd = null;
                if (item.Type == SquareType_Enum.Starting && !item.Equals(square)) item.Type = SquareType_Enum.Opening;
                if (item.Equals(square))
                {
                    item.Type = SquareType_Enum.Starting;
                }
                item.DistanStart = func(square.RowIndex, square.ColumnIndex, item.RowIndex, item.ColumnIndex);
            }
            StartSquare = square;
        }
        /// <summary>
        /// 设置结束点
        /// </summary>
        /// <param name="square"></param>
        /// <param name="func"></param>
        public void setEnding(Square square, Func<int, int, int, int, int> func)
        {
            if (EndSquare.Equals(square)) return;
            bool reset = false;
            if (square.Type == SquareType_Enum.Starting) reset = true;
            foreach (var item in this)
            {
                if (reset) item.DistanStart = null;
                if (item.Type == SquareType_Enum.Ending && !item.Equals(square)) item.Type = SquareType_Enum.Opening;
                if (item.Equals(square))
                {
                    item.Type = SquareType_Enum.Ending;
                }
                item.DistanEnd = func(square.RowIndex, square.ColumnIndex, item.RowIndex, item.ColumnIndex);
            }
            EndSquare = square;
        }
        
        public IEnumerator<Square> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++) //行
            {
                for (int j = 0; j < Columns; j++) //列
                {
                    yield return Squares[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
