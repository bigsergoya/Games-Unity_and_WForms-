using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PositionClasses
{
    class Position //Рефакторинг в один класс с наследием с аналогичным классом из Филда
    {
        private int x;
        private int z;
        private double f;
        private double h;
        private double g;
        Position relativeCell;
        public int X
        {
            get
            {
                return x;
            }
        }

        public int Z
        {
            get
            {
                return z;
            }
        }

        public double F
        {
            get
            {
                return f;
            }

            set
            {
                f = value;
            }
        }

        public Position RelativeCell
        {
            get
            {
                return relativeCell;
            }

            set
            {
                relativeCell = value;
            }
        }

        public double H
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
            }
        }

        public double G
        {
            get
            {
                return g;
            }

            set
            {
                g = value;
            }
        }

        public Position(float x, float z, double f, double h, double g, Position relativeCell)
        {
            this.x = Convert.ToInt32(MathematicRound(x, 0));
            this.z = Convert.ToInt32(MathematicRound(z, 0));
            this.f = f;
            this.h = h;
            this.g = g;
            this.relativeCell = relativeCell;
        }
        public static double MathematicRound(float value, int digits)
        {
            double scale = Math.Pow(10.0, digits);
            double round = Math.Floor(Math.Abs(value) * scale + 0.5);
            return (Math.Sign(value) * round / scale);
        }
        public Vector3 GettPositionAsVector3()
        {
            return new Vector3(x, 1f, z);
        }
    }
}
