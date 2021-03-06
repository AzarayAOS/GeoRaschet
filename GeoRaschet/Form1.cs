﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoRaschet
{
    public partial class Form1 : Form
    {
        private Data a, b, c, al, bt;
        private int regim = 0;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((regim != 2) && (!a.f)) || ((regim == 2) && (a.f)))
                if ((textBox1.Text.Length != a.size) || (double.Parse(textBox1.Text) != a.inf))
                {
                    if ((textBox1.Text.Length != 0))
                    {
                        a.inf = double.Parse(textBox1.Text);
                        if (!a.f) regim++;
                        a.f = true;
                    }
                    if ((textBox1.Text.Length == 0) && (a.f)) { a.inf = 0; a.f = false; regim--; }
                    a.size = textBox1.Text.Length;
                }

            GetData();
        }

        public Form1()
        {
            InitializeComponent();

            a = new Data();
            b = new Data();
            c = new Data();
            al = new Data();
            bt = new Data();
        }

        /// <summary>Выводим результаты вычислений</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SetData
        private void SetData()
        {
            if (regim == 2)
            {
                if (!a.f) textBox1.Text = a.inf.ToString();
                if (!b.f) textBox2.Text = b.inf.ToString();
                if (!c.f) textBox3.Text = c.inf.ToString();
                if (!al.f) textBox4.Text = al.inf.ToString();
                if (!bt.f) textBox5.Text = bt.inf.ToString();
                textBox6.Text = (a.inf * 12).ToString();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (((regim != 2) && (!b.f)) || ((regim == 2) && (b.f)))
                if ((textBox2.Text.Length != b.size) || (double.Parse(textBox2.Text) != b.inf))
                {
                    if ((textBox2.Text.Length != 0)) { b.inf = double.Parse(textBox2.Text); if (!b.f) regim++; b.f = true; }
                    if ((textBox2.Text.Length == 0) && (b.f)) { b.inf = 0; b.f = false; regim--; }
                    b.size = textBox2.Text.Length;
                }
            GetData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (((regim != 2) && (!c.f)) || ((regim == 2) && (c.f)))
                if ((textBox3.Text.Length != c.size) || (double.Parse(textBox3.Text) != c.inf))
                {
                    if ((textBox3.Text.Length != 0)) { c.inf = double.Parse(textBox3.Text); if (!c.f) regim++; c.f = true; }
                    if ((textBox3.Text.Length == 0) && (c.f)) { c.inf = 0; c.f = false; regim--; }
                    c.size = textBox3.Text.Length;
                }
            GetData();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (((regim != 2) && (!al.f)) || ((regim == 2) && (al.f)))
                if ((textBox4.Text.Length != al.size) || (double.Parse(textBox4.Text) != al.inf))
                {
                    if ((textBox4.Text.Length != 0)) { al.inf = double.Parse(textBox4.Text); if (!al.f) regim++; al.f = true; }
                    if ((textBox4.Text.Length == 0) && (al.f)) { al.inf = 0; al.f = false; regim--; }
                    al.size = textBox4.Text.Length;
                }
            GetData();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (((regim != 2) && (!bt.f)) || ((regim == 2) && (bt.f)))
                if ((textBox5.Text.Length != bt.size) || (double.Parse(textBox5.Text) != bt.inf))
                {
                    if ((textBox5.Text.Length != 0)) { bt.inf = double.Parse(textBox5.Text); if (!bt.f) regim++; bt.f = true; }
                    if ((textBox5.Text.Length == 0) && (bt.f)) { bt.inf = 0; bt.f = false; regim--; }
                    bt.size = textBox5.Text.Length;
                }
            GetData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.') e.KeyChar = ',';

            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') &&
                    (((TextBox)sender).Text.IndexOf(",") == -1) &&
                    (((TextBox)sender).Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>Взятие значений с текстовых полей</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetData
        private void GetData()
        {
            // при вводе двух разных значений блокируем остальные
            if (regim == 2)
            {
                textBox1.Enabled = a.f;
                textBox2.Enabled = b.f;
                textBox3.Enabled = c.f;
                textBox4.Enabled = al.f;
                textBox5.Enabled = bt.f;
            }
            // если введено меньше двух, то разблокирываем все поля вводу
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }

            // при вводе а
            if (a.f)
            {
                // при вводе b
                if (b.f)
                {
                    c.inf = Math.Sqrt(Math.Pow(a.inf, 2) + Math.Pow(b.inf, 2));
                    double sinal = b.inf / c.inf;
                    al.inf = Math.Asin(sinal);
                    al.inf = RadToGrad(al.inf);

                    bt.inf = 90 - al.inf;
                }
                // при вводе угла альфа
                if (al.f)
                {
                    double temp = GradToRad(al.inf);
                    c.inf = a.inf / Math.Cos(temp);
                    b.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(a.inf, 2));
                    bt.inf = 90 - al.inf;
                }

                // при вводе угла бетта
                if (bt.f)
                {
                    double temp = GradToRad(bt.inf);
                    c.inf = a.inf / Math.Sin(temp);
                    b.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(a.inf, 2));
                    al.inf = 90 - bt.inf;
                }
                // при вводе c
                if (c.f)
                {
                    b.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(a.inf, 2));
                    double sinal = b.inf / c.inf;
                    al.inf = Math.Asin(sinal);
                    al.inf = RadToGrad(al.inf);

                    bt.inf = 90 - al.inf;
                }
            }

            // при вводе b
            if (b.f)
            {
                // при вводе угла альфа
                if (al.f)
                {
                    double temp = GradToRad(al.inf);
                    c.inf = b.inf / Math.Sin(temp);
                    a.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(b.inf, 2));
                    bt.inf = 90 - al.inf;
                }

                // при вводе угла бетта
                if (bt.f)
                {
                    double temp = GradToRad(bt.inf);
                    c.inf = b.inf / Math.Cos(temp);
                    a.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(b.inf, 2));

                    al.inf = 90 - bt.inf;
                }

                // при вводе с
                if (c.f)
                {
                    a.inf = Math.Sqrt(Math.Pow(c.inf, 2) - Math.Pow(b.inf, 2));

                    double sinal = b.inf / c.inf;
                    al.inf = Math.Asin(sinal);
                    al.inf = RadToGrad(al.inf);

                    bt.inf = 90 - al.inf;
                }
            }

            // при вводе с
            if (c.f)
            {
                // при вводе угла альфа
                if (al.f)
                {
                    double temp = GradToRad(al.inf);
                    b.inf = c.inf * Math.Sin(temp);
                    a.inf = c.inf * Math.Cos(temp);

                    bt.inf = 90 - al.inf;
                }

                // при вводе угла бетта
                if (bt.f)
                {
                    double temp = GradToRad(bt.inf);
                    a.inf = c.inf * Math.Sin(bt.inf);
                    b.inf = c.inf * Math.Cos(temp);

                    al.inf = 90 - bt.inf;
                }
            }
            ;
            // обновляем данные
            SetData();
        }

        /// <summary>Перевод градусов в радианы</summary>
        /// <param name="gr">Значение угла в градусах</param>
        /// <returns>Угол в радианах</returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GradToRad
        private double GradToRad(double gr)
        {
            double temp = 0;
            temp = gr * Math.PI / 180;
            return temp;
        }

        /// <summary>Перевод радиан в градусы</summary>
        /// <param name="rd">Значение угла в радианах</param>
        /// <returns>Угол в градусах</returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RadToGrad
        private double RadToGrad(double rd)
        {
            double temp = 0;
            temp = rd * 180 / Math.PI;
            return temp;
        }
    }
}