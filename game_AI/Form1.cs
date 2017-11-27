using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace game_AI
{

    public partial class Form1 : Form
    {
        Pile_Of_Stones[] mas = new Pile_Of_Stones[100];
        List<Label> labels = new List<Label>();
        List<RadioButton> rbuttons = new List<RadioButton>();

        public Form1()
        {
            InitializeComponent();
            NewGame();
        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void NewGame()
        {
            int m = Convert.ToInt32(textBox3.Text);
            int n = Convert.ToInt32(textBox2.Text);
            Random a = new Random(), r = new Random();
            labels.Clear();
            rbuttons.Clear();
            Array.Clear(mas, 0, n);
            
            /*
            labels = new List<Label>();
            rbuttons = new List<RadioButton>();
            */
            for (int i = 0; i < n; i++)
            {
                int pos = i / 5 + 1;
                mas[i] = new Pile_Of_Stones(a.Next(m));
                Label label = new Label();
                label.Text = "В " + (i + 1).ToString() + "-й куче камней: " + mas[i].ret_aos().ToString() + " камней";
                labels.Add(label);
                Controls.Add(labels[i]);
                labels[i].Visible = true;
                labels[i].AutoSize = false;
                labels[i].Location = new Point(40 + 100 * (i % 5), 20 + 100 * pos);
                labels[i].Show();
                labels[i].Size = new System.Drawing.Size(100, 30);
                RadioButton radiobutton = new RadioButton();
                rbuttons.Add(radiobutton);
                Controls.Add(rbuttons[i]);
                rbuttons[i].Visible = true;
                rbuttons[i].AutoSize = false;
                rbuttons[i].Location = new Point(40 + 100 * (i % 5), 45 + 100 *pos);
                rbuttons[i].Show();
                rbuttons[i].Size = new System.Drawing.Size(100, 30);

            //    MessageBox.Show(labels[i].Text);
            }
            for (int j = 0; j < n; j++)
                nulling(j);
            //InitializeComponent();
        }

        private void ClearForm()
        {
            for (int i = 0; i < labels.Count; i++)
            {
                if (labels[i] != null)
                    if (Controls.Contains(labels[i]))
                    {
                        Controls.Remove(labels[i]);
                        labels[i].Dispose();
                    }
            }
            for (int i = 0; i < rbuttons.Count; i++)
            {
                if (rbuttons[i] != null)
                    if (Controls.Contains(rbuttons[i]))
                    {
                        Controls.Remove(rbuttons[i]);
                        rbuttons[i].Dispose();
                    }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox2.Text);
            int f = 0;
            foreach (var element in textBox2.Text)
            {
                if (!Char.IsDigit(element)) f = 1; 
            }
            if (f == 0)
            {
                if ((Convert.ToInt32(textBox2.Text) > 100) || (Convert.ToInt32(textBox2.Text) < 0)) f = 1;
                if (f == 0)
                {
                    var result = MessageBox.Show("Начать новую игру?", "Новая игра",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        ClearForm();
                        NewGame();
                    }
                }
                else MessageBox.Show("Введите натуральное число от 1 до 100");
            }
            else MessageBox.Show("Введите натуральное число от 1 до 100");
        }


        public void pl_move(int max_a, int ind_of_p)
        {
            int n = Convert.ToInt32(textBox2.Text);
            //MessageBox.Show("Ваш ход");
            Form2 f = new Form2();
            int s, i = ind_of_p;
            do
            {
                f.ShowDialog();
                s = f.amount;
                //f.Show();
                if (s == -1) break ;
            }
            while ((s < 0) || (s > max_a) || (s>mas[i].ret_aos()));
            if (s == -1) return;
                {
                    mas[i].ch_of_st_am(s);
                    labels[i].Text = "В " + (i + 1).ToString() + "-й куче камней: " + mas[i].ret_aos().ToString() + " камней";
                }
                for (i = 0; i < n; i++)
                    nulling(i);
            //else MessageBox.Show("Неправильный ход");
            if (itswin(max_a, 'p') != 0)
            {
                ai_move(max_a);
                for (i = 0; i < 5; i++)
                    nulling(i);
                itswin(max_a, 'c');
            }
        }

        private void nulling(int i)
        {
            if (labels[i].Text == ("В " + (i + 1).ToString() + "-й куче камней: 0 камней"))
            {
                rbuttons[i].Enabled = false;
                rbuttons[i].Checked = false;
            }
        }
        

        private int itswin(int max_a, char w)
        {
            int n = Convert.ToInt32(textBox2.Text);
            int sum = 0,i;
            for (i = 0; i < n; i++)
            {
                sum += mas[i].ret_aos();
            }
            if ((sum == 0)&&(w=='p')) MessageBox.Show("Вы выиграли!");
            else if ((sum == 0) && (w == 'c')) MessageBox.Show("Компьютер победил!");
            return sum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox2.Text);
            textBox1.Enabled = false;
            int max_a = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < n; i++)
                if (rbuttons[i].Checked == true)
                {
                    pl_move(max_a, i);
                    break;
                }
        }

        public struct temporary
        {
            public int data;
            public int index;
        }

        public int not_empty_piles(int n)
        {
            int k = 0;
            for (int i = 0; i < n; i++)
                if (mas[i].ret_aos() != 0) k++;
            return k;
        }

        public void ai_move1(int max_a)
        {
            int n = Convert.ToInt32(textBox2.Text);
            int min_amount_of_moves_to_win;
            int max_amount_of_moves_to_win;
            //int n = not_empty_piles(5);
            int sum = 0, x = 0;
            for (int i = 0; i < n; i++)
            {
                sum += mas[i].ret_aos();
                x += mas[i].ret_aos() / max_a;
                if (mas[i].ret_aos() % max_a > 0) x++;
                //MessageBox.Show(x.ToString());
            }
            min_amount_of_moves_to_win = x;
            max_amount_of_moves_to_win = sum;
            //
            //if ((max_amount_of_moves_to_win - (max_a +1 ))- min_amount_of_moves_to_win > 1)

            // 
        }

        public void ai_move(int max_a)
        {
            int n = Convert.ToInt32(textBox2.Text);
            int i = 0, sum = 0, x = 0, k = 0, done = 0;
            while (done == 0)
            {
                x = 0;
                for (i = 0; i < n; i++)
                {
                    sum += mas[i].ret_aos();
                    x += mas[i].ret_aos() / max_a;
                    if (mas[i].ret_aos() % max_a > 0) x++;
                    //MessageBox.Show(x.ToString());
                }
                temporary[] _temp_mas = new temporary[x];
                int min_amount_of_moves_to_win = x;
                int max_amount_of_moves_to_win = sum;
                
                
                int j = 0;

                for (i = 0; i < n; i++)
                {
                    k = mas[i].ret_aos();
                    while (k > 0)
                    {
                        if (k % max_a == 0)
                        {
                            _temp_mas[j].data = max_a;
                            k -= max_a;
                        }
                        else
                        {
                            _temp_mas[j].data = k % max_a;
                            k -= k % max_a;
                        }
                        _temp_mas[j].index = i;
                        j++;
                    }
                }
                int nim = 0;
                for (i = 0; i < x; i++)
                {
                    nim = nim ^ _temp_mas[i].data;
                }

                // /*
                // 
                // Если количество реальных куч меньше, чем виртуальных,
                // уничтожаем самую большую виртуальную кучу, чтобы вернуться к Ним-игре, 
                // не обманываясь в количестве ходов до победы 
                // (по сути, отдавая преимущество, если оно было, игроку)
                //
                //if ((min_amount_of_moves_to_win == 4)&&(not_empty_piles(5)<min_amount_of_moves_to_win))
                if (not_empty_piles(n) < min_amount_of_moves_to_win)
                {
                    int max_s = _temp_mas[0].data, imax_s = 0;
                    for (i = 0; i < x; i++)
                    {
                        if (_temp_mas[i].data > max_s)
                        {
                            max_s = _temp_mas[i].data; imax_s = i;
                        }
                    }
                    done = TakeStones(_temp_mas[imax_s].index, max_s);
                    break;
                }
                 //* */

                //-----------------------------------
                // Осталась только одна непустая куча - исключение
                //-----------------------------------
                if (not_empty_piles(n) == 1)
                {
                    for (i = 0; i < n; i++)
                    {
                        if (mas[i].ret_aos() == 0) continue;
                        k = mas[i].ret_aos() % (max_a + 1);
                        if (k == 0) k = 1;
                        done = TakeStones(i, k);
                        break;
                    }
                }
                if (done == 1) break;
                //----------------------------------------

                i = 0;
                if (nim == 0)
                {
                    while (_temp_mas[i].data == 0)
                    {
                        i++;
                    }
                    done = TakeStones(_temp_mas[i].index, 1);
                }
                else
                {
                    while (((_temp_mas[i].data ^ nim) >= _temp_mas[i].data) && (i < x))
                    {
                        i++;
                    }
                    k = _temp_mas[i].data - (_temp_mas[i].data ^ nim);
                    done = TakeStones(_temp_mas[i].index, k);
                }
            }
        }

        private int TakeStones(int i, int k)
        {
            int done;
            mas[i].ch_of_st_am(k);
            MessageBox.Show("Компьютер взял " + k.ToString() + " камней из " + (i + 1).ToString() + "-й кучи.");
            labels[i].Text = "В " + (i + 1).ToString() + "-й куче камней: " + mas[i].ret_aos().ToString() + " камней";
            done = 1;
            return done;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox2.Text);
            var result = MessageBox.Show("Начать новую игру?", "Новая игра",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClearForm();
                NewGame();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "Имеется произвольное количество куч, в каждой из которых содержится произвольное количество камней. Играют двое. Каждый берёт за один ход произвольное количество камней, не больше заданного значения, но только из одной кучи. Выигрывает тот, кто своим очередным ходом забирает последние камни.";
            MessageBox.Show(str, "Помощь");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int max_a = Convert.ToInt32(textBox1.Text);
            if (itswin(max_a, 'p') != 0)
            {
                ai_move(max_a);
                for (int i = 0; i < 5; i++)
                    nulling(i);
                itswin(max_a, 'c');
            }
        }
    }
}