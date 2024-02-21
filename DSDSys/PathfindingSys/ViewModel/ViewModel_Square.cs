using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PathfindingSys.ViewModel
{
    public class Square : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变更通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性变更通知
        /// </summary>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        bool _Checked;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get => _Checked; set { _Checked = value; OnPropertyChanged("Checked"); } }
        SquareType_Enum _Type;
        /// <summary>
        /// 方格类型
        /// </summary>
        public SquareType_Enum Type { get => _Type; set { _Type = value; OnPropertyChanged("Type"); } }
        bool _Closed;
        /// <summary>
        /// 不在路径上
        /// </summary>
        public bool Closed { get => _Closed; set { _Closed = value; OnPropertyChanged("Closed"); } }
        /// <summary>
        /// 路径上的下一个方格
        /// </summary>
        public Square Next { get; set; }
        /// <summary>
        /// 路径上的上一个方格
        /// </summary>
        public Square Last { get; set; }
        double _Cost;
        /// <summary>
        /// 代价
        /// </summary>
        public double Cost { get => _Cost; set { _Cost = value; OnPropertyChanged("Cost"); } }
        /// <summary>
        /// 邻居
        /// </summary>
        public Neighbors Neighbors { get; set; }
        /// <summary>
        /// 开始位置的距离
        /// </summary>
        public int? DistanStart { get; set; }
        /// <summary>
        /// 结束位置的距离
        /// </summary>
        public int? DistanEnd { get; set; }
    }

    /// <summary>
    /// 邻居
    /// </summary>
    public class Neighbors : IEnumerable
    {
        private Square[] squares = new Square[4]{null, null, null, null};
        /// <summary>
        /// 左邻居
        /// </summary>
        public Square Left { get => squares[0]; set => squares[0] = value; }
        /// <summary>
        /// 上邻居
        /// </summary>
        public Square Top { get => squares[1]; set => squares[1] = value; }
        /// <summary>
        /// 右邻居
        /// </summary>
        public Square Right { get => squares[2]; set => squares[2] = value; }
        /// <summary>
        /// 下邻居
        /// </summary>
        public Square Bottom { get => squares[3]; set => squares[3] = value; }

        public IEnumerator GetEnumerator()
        {
            int i = 0;
            while (i < 4 && null != squares[i])
            {
                yield return squares[i];
            }
        }
    }
}
