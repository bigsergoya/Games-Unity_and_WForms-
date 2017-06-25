using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2_SecondTarget {
    class LogicalCell {
        bool isABomb;
        bool isOpened;
        bool isMarkedWithFlag;

        public LogicalCell() {
            isABomb = false;
            isOpened = false;
            isMarkedWithFlag = false;
        }

        public bool IsOpened { get { return isOpened; } set { isOpened = value; } }

        public bool IsMarkedWithFlag { get { return isMarkedWithFlag; } set { isMarkedWithFlag = value; } }

        public bool IsABomb { get { return isABomb; } set { isABomb = value; } }

        public override string ToString() {
            return $"IsOpened = {isOpened}, IsABomb = {isABomb}, IsMarkedWithFlag = {isMarkedWithFlag}";
        }
    }
}
