using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Генетика
{
    public partial class Form5 : Form
    {
        int question_count;
        int correct_answer;
        int wrong_answer;

        string[] array;

        int correct_answer_number;
        int selected_responce;


        System.IO.StreamReader Read;



        public Form5()
        {
            InitializeComponent();
        }

        void start()
        {
            var encoding = System.Text.Encoding.GetEncoding(65001);
            try
            {
                Read = new System.IO.StreamReader(
                System.IO.Directory.GetCurrentDirectory() +
                                               @"\t.txt", encoding);
                this.Text = Read.ReadLine();
                question_count=0;
                correct_answer=0;
                wrong_answer=0;
                
                array = new String[10];
            }
            catch(Exception)
            {
                MessageBox.Show("ошибка 1");
            }
            вопрос();
        }

        void вопрос()
        {
            label1.Text = Read.ReadLine();

            radioButton1.Text = Read.ReadLine();
            radioButton2.Text = Read.ReadLine();
            radioButton3.Text = Read.ReadLine();

            correct_answer_number = int.Parse(Read.ReadLine());

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            button1.Enabled = false;

            question_count= question_count + 1;

            if (Read.EndOfStream == true) button1.Text = "Завершить";
        }

        void состояниепереключения(object sender, EventArgs e)
        {
            button1.Enabled = true; button1.Focus();
            RadioButton Переключатель = (RadioButton)sender;
            var tmp = Переключатель.Name;
            selected_responce = int.Parse(tmp.Substring(11));
        }



        private void Form5_Load(object sender, EventArgs e)
        {
            button1.Text = "Следующий вопрос";
            button2.Text = "Выход";


            radioButton1.CheckedChanged += new EventHandler(состояниепереключения);
            radioButton2.CheckedChanged += new EventHandler(состояниепереключения);
            radioButton3.CheckedChanged += new EventHandler(состояниепереключения);


            start();


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selected_responce == correct_answer_number) correct_answer =
                                               correct_answer + 1;
            if (selected_responce != correct_answer_number)
            {
                wrong_answer = wrong_answer + 1;

                array[wrong_answer] = label1.Text;

            }
            if (button1.Text == "Начать тестирование сначала")
            {
                button1.Text = "Следующий вопрос";
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                start(); return;

            }
            if (button1.Text == "Завершить")
            {
                Read.Close();

                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                    
                label1.Text = String.Format("Тестирование завершено.\n" +
                    "Правильных ответов: {0} из {1}.\n" +
                    "Набранные баллы: {2:F2}.", correct_answer,
                    question_count, (correct_answer * 5.0F) / question_count);
                
                button1.Text = "Начать тестирование сначала";
                
                var Str = "Список ошибок" +
                          ":\n\n";
                for (int i = 1; i <= wrong_answer; i++)
                    Str = Str + array[i] + "\n";
                 
                if (wrong_answer != 0) MessageBox.Show(
                                         Str, "Тестирование завершено");

            }
            
            
            if (button1.Text == "Следующий вопрос") вопрос();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }
    }
}
