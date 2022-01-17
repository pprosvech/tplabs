using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsCars
{
    public class DumpTruck
    {
        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { private set; get; }
        /// <summary>
        /// Вес автомобиля
        /// </summary>
        public float Weight { private set; get; }
        /// <summary>
        /// Цвет кузова
        /// </summary>
        public Color BodyColor { private set; get; }
        /// <summary>
        /// Левая координата отрисовки автомобиля
        /// </summary>
        private float? _startPosX = null;
        /// <summary>
        /// Верхняя кооридната отрисовки автомобиля
        /// </summary>
        private float? _startPosY = null;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int? _pictureWidth = null;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int? _pictureHeight = null;
        /// <summary>
        /// Ширина отрисовки автомобиля
        /// </summary>
        protected readonly int _carWidth = 80;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        protected readonly int _carHeight = 50;
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="speed">Скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="bodyColor">Цвет кузова</param>
        public void Init(int speed, float weight, Color bodyColor)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
        }
        /// <summary>
        /// Установка позиции автомобиля
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        /// <summary>
        /// Смена границ формы отрисовки
        /// </summary>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void ChangeBorders(int width, int height)
        {
            _pictureWidth = width;
            _pictureHeight = height;
            if (_startPosX + _carWidth > width)
            {
                _startPosX = width - _carWidth;
            }
            if (_startPosY + _carHeight > height)
            {
                _startPosY = height - _carHeight;
            }
        }
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MoveTransport(Direction direction)
        {
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            float step = Speed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + _carWidth + step < _pictureWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    // TODO: Продумать логику
                    break;
                //вверх
                case Direction.Up:
                    // TODO: Продумать логику
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + _carHeight + step < _pictureHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка автомобиля
        /// </summary>
        /// <param name="g"></param>
        public void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new(Color.Black);
            //границы автомобиля
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value + 30, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value + 70, _startPosY.Value, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value + 70, _startPosY.Value + 30, 20, 20);
            g.DrawRectangle(pen, _startPosX.Value - 1, _startPosY.Value + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX.Value + 80, _startPosY.Value + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX.Value + 10, _startPosY.Value - 1, 70, 52);
            //задние фары
            Brush brRed = new SolidBrush(Color.Red);
            g.FillEllipse(brRed, _startPosX.Value, _startPosY.Value, 20, 20);
            g.FillEllipse(brRed, _startPosX.Value, _startPosY.Value + 30, 20, 20);
            //передние фары
            Brush brYellow = new SolidBrush(Color.Yellow);
            g.FillEllipse(brYellow, _startPosX.Value + 70, _startPosY.Value, 20, 20);
            g.FillEllipse(brYellow, _startPosX.Value + 70, _startPosY.Value + 30, 20, 20);
            //кузов
            Brush br = new SolidBrush(BodyColor);
            g.FillRectangle(br, _startPosX.Value, _startPosY.Value + 10, 10, 30);
            g.FillRectangle(br, _startPosX.Value + 80, _startPosY.Value + 10, 10, 30);
            g.FillRectangle(br, _startPosX.Value + 10, _startPosY.Value, 70, 50);
            //стекла
            Brush brBlue = new SolidBrush(Color.LightBlue);
            g.FillRectangle(brBlue, _startPosX.Value + 60, _startPosY.Value + 5, 5, 40);
            g.FillRectangle(brBlue, _startPosX.Value + 20, _startPosY.Value + 5, 5, 40);
            g.FillRectangle(brBlue, _startPosX.Value + 25, _startPosY.Value + 3, 35, 2);
            g.FillRectangle(brBlue, _startPosX.Value + 25, _startPosY.Value + 46, 35, 2);
            //выделяем рамкой крышу
            g.DrawRectangle(pen, _startPosX.Value + 25, _startPosY.Value + 5, 35, 40);
            g.DrawRectangle(pen, _startPosX.Value + 65, _startPosY.Value + 10, 25, 30);
            g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value + 10, 15, 30);
        }
    }
}
        


