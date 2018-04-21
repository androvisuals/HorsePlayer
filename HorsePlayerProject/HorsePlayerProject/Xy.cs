using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorsePlayerProject
{
    public partial class Xy : Panel
    {
        bool move;
        Tuple<float, float> position;

        public Xy()
        {
            InitializeComponent();
            move = false;
            position = new Tuple<float, float>(0.5f, 0.5f);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawEllipse(new Pen(Color.White, 2.0f), new Rectangle((int)(position.Item1 * (float)this.Width) - 5, (int)(position.Item2 * (float)this.Height) - 5, 10, 10));
        }

        public Tuple<float, float> GetPosition()
        {
            return position;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            move = true;
            position = PositionToNormal(e.X, e.Y);
            this.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            move = false;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(move)
            {
                position = PositionToNormal(e.X, e.Y);
                this.Invalidate();
            }

            base.OnMouseMove(e);
        }

        public static T Clamp<T>(T aValue, T aMin, T aMax) where T : IComparable<T>
        {
            var _Result = aValue;
            if (aValue.CompareTo(aMax) > 0)
                _Result = aMax;
            else if (aValue.CompareTo(aMin) < 0)
                _Result = aMin;
            return _Result;
        }

        private Tuple<float, float> PositionToNormal(int x, int y)
        {
            return Tuple.Create(Clamp((float)x / (float)this.Width, 0.0f, 1.0f), Clamp((float)y / (float)this.Height, 0.0f, 1.0f));
        }
    }
}
