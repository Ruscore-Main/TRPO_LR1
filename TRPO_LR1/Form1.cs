using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPO_LR1
{
    public partial class Form1 : MaterialForm
    {
        int i;
        int sum = 0;
        double water = 3;
        int conditition = 0;
        Timer timer = new Timer();
        Timer making_timer = new Timer();
        Product[] products = new Product[5];

        private class Product
        {
            string name;
            int quantity;
            int cost;

            public Product()
            {
                this.name = "";
                this.quantity = 0;
                this.cost = 0;
            }

            public Product(string name, int quantity, int cost)
            {
                this.name = name;
                this.quantity = quantity;
                this.cost = cost;
            }

            public string Get_Name()
            {
                return this.name;
            }

            public int Get_Quantity()
            {
                return this.quantity;
            }

            public int Get_Cost()
            {
                return this.cost;
            }

            public void Set_Quantity(int quantity)
            {
                this.quantity = quantity;
            }
        }

        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.Orange100, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightGreen100, TextShade.BLACK);


            timer.Interval = 10000;
            timer.Tick += Timer_Tick;

            making_timer.Interval = 5000;
            making_timer.Tick += Making_Timer_Tick;

            Products_Initialize(products);
            Begin_Conditition();
        }
        private void Making_Timer_Tick(object sender, EventArgs e)
        {
            screen.Text = "Возьмите напиток";
            conditition = 5;
            product_button.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sum != 0)
            {
                screen.Text = "Заберите деньги: " + sum + "p";
                sum = 0;
            }
            else
                Begin_Conditition();
        }

        private void Begin_Conditition()
        {
            cancel_button.Enabled = true;
            change_button.Enabled = false;
            timer.Stop();
            sum = 0;
            i = 0;
            conditition = 0;

            if (Water_Check())
            {
                screen.Text = "Выберите напиток";
                conditition = 1;
            }
            else if (!Water_Check())
            {
                screen.Text = "Нет воды!\n\nИзвините!";
            }
        }

        private void Making()
        {
            screen.Text = "Напиток готовится...";
            products[i].Set_Quantity(products[i].Get_Quantity() - 1);
            water -= 0.5;
            product_button.Enabled = false;
            cancel_button.Enabled = false;
        }

        private void Chek()
        {
            string s = "Чек\n" + products[i].Get_Name() + " - " + products[i].Get_Cost() + "р\nНаличные: " + sum + "р\nСдача: " + Change_computation() + "р";
            byte[] sb = Encoding.Default.GetBytes(s);
            FileStream file = new FileStream("chek.txt", FileMode.OpenOrCreate);
            file.Write(sb, 0, sb.Length);
            file.Close();

        }

        private void Products_Initialize(Product[] products)
        {
            products[0] = new Product("Чай", 0, 30);
            products[1] = new Product("Эспрессо", 2, 40);
            products[2] = new Product("Каппучино", 20, 45);
            products[3] = new Product("Американо", 20, 35);
            products[4] = new Product("Латте", 20, 45);
        }

        private bool Water_Check()
        {
            if (water < 0.5)
            {
                screen.Text = "Недостаточно воды в автомате";
                return false;
            }
            else
                return true;
        }

        private int Change_computation()
        {
            return sum - products[i].Get_Cost();
        }

        private void tea_button_Click(object sender, EventArgs e)
        {
            if (conditition == 1 || conditition == 2)
            {
                screen.Text = products[0].Get_Name() + " " + products[0].Get_Cost() + "p\n\nНажмите Далее";
                i = 0;
                conditition = 2;
            }
            timer.Start();
        }
        private void espresso_button_Click(object sender, EventArgs e)
        {
            if (conditition == 1 || conditition == 2)
            {
                screen.Text = products[1].Get_Name() + " " + products[1].Get_Cost() + "p\n\nНажмите Далее";
                i = 1;
                conditition = 2;
            }
            timer.Start();
        }
        private void cappuccino_button_Click(object sender, EventArgs e)
        {
            if (conditition == 1 || conditition == 2)
            {
                screen.Text = products[2].Get_Name() + " " + products[2].Get_Cost() + "p\n\nНажмите Далее";
                i = 2;
                conditition = 2;
            }
            timer.Start();
        }
        private void americano_button_Click(object sender, EventArgs e)
        {
            if (conditition == 1 || conditition == 2)
            {
                screen.Text = products[3].Get_Name() + " " + products[3].Get_Cost() + "p\n\nНажмите Далее";
                i = 3;
                conditition = 2;
            }
            timer.Start();
        }
        private void latte_button_Click(object sender, EventArgs e)
        {
            if (conditition == 1 || conditition == 2)
            {
                screen.Text = products[4].Get_Name() + " " + products[4].Get_Cost() + "p\n\nНажмите Далее";
                i = 4;
                conditition = 2;
            }
            timer.Start();
        }

        private void money500_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 500;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money100_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 100;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money50_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 50;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money10_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 10;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money5_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 5;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money2_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 2;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }
        private void money1_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 3)
            {
                change_button.Enabled = true;
                sum += 1;
                conditition = 3;
                screen.Text = "Баланс: " + sum + "p\n\nНажмите Далее";
            }
            timer.Start();
        }

        private void change_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 4)
            {
                next_button_Click(sender, e);
            }
            if (conditition == 3)
            {
                Begin_Conditition();
            }
            timer.Start();
        }

        private void product_button_Click(object sender, EventArgs e)
        {
            making_timer.Stop();
            timer.Stop();

            if (conditition == 5)
            {
                screen.Text = "Возьмите чек\n\nСпасибо за покупку!";
                sum = 0;
                i = 0;
                conditition = 0;
            }
            timer.Start();
        }

        private void next_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (conditition == 2 && products[i].Get_Quantity() > 0)
            {
                screen.Text = "Внесите наличные";
                conditition = 3;
            }
            else if (conditition == 2 && products[i].Get_Quantity() == 0)
            {
                screen.Text = "Этого товара нет в наличии";
                conditition = 0;
            }
            else if (conditition == 3 && products[i].Get_Cost() <= sum)
            {

                if (Change_computation() != 0)
                {
                    screen.Text = "Сдача: " + Change_computation() + "p\n\nВозьмите сдачу";
                }
                else
                    next_button_Click(sender, e);
                conditition = 4;
            }
            else if (conditition == 4)
            {
                Making();
                Chek();
                making_timer.Start();
            }
            else if (conditition == 3 && products[i].Get_Cost() > sum)
            {
                screen.Text = products[i].Get_Name() + " " + products[i].Get_Cost() + "р\nНедостаточно средств\nБаланс: " + sum + "р";
            }
            timer.Start();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (sum != 0)
            {
                screen.Text = "Заберите деньги: " + sum + "p";
                sum = 0;
                timer.Start();
            }
            else
                Begin_Conditition();
        }
    }
}
