using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WindowsFormsApp4_Valuta
{
    public partial class Form1 : Form
    {
        Class_Create_Valuta Class_Create_Valuta; 

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        public double multi1, multi2;   //    multi2/multi1 - во столько раз  валюта  больше рубля (multi1 = 1, для рубля)
                                        //    multi1/multi2 - во столько раз  валюта  меньше рубля (multi1 = 1, для рубля)
        public string UndeWin1, UndeWin2;   // маленькие текстовые окна сразу под вводом числа валюты (подсказки)

        public string light1, light2;          //запоминают, что горит слева, а что - справа

        

        
        public void Light1(string name_valuta) // для левых валют подстветка
        {
            //Class_Create_Valuta = new Class_Create_Valuta(this);
            if ( name_valuta == "RUR")
            {
                this.buttonRUR1.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonRUR1.BackColor = Color.White;
            }

            if(name_valuta == "USD")
            {
                this.buttonUSD1.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonUSD1.BackColor = Color.White;
            }

            if (name_valuta == "EUR")
            {
                this.buttonEUR1.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonEUR1.BackColor = Color.White;
            }

            if (name_valuta == "GBP")
            {
                this.buttonGPB1.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonGPB1.BackColor = Color.White;
            }


        }

        public void Light2(string name_valuta)  // для правых валют подстветка
        {
            //Class_Create_Valuta = new Class_Create_Valuta(this);
            if (name_valuta == "USD")
            {
                this.buttonUSD2.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonUSD2.BackColor = Color.White;
            }

            if (name_valuta == "RUR")
            {
                this.buttonRUR2.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonRUR2.BackColor = Color.White;
            }

            if (name_valuta == "USD")
            {
                this.buttonUSD2.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonUSD2.BackColor = Color.White;
            }

            if (name_valuta == "EUR")
            {
                this.buttonEUR2.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonEUR2.BackColor = Color.White;
            }

            if (name_valuta == "GBP")
            {
                this.buttonGPB2.BackColor = Color.FromArgb(33, 190, 65);
            }
            else
            {
                this.buttonGPB2.BackColor = Color.White;
            }

        }

        public void Valuta(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', string Unde = "AUD") // для тех валют у которых в ID  5 символов и слева (окно для ввода)
        {
            WebClient web = new WebClient();
            string Text = web.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js"); //сайт наш 

            for (int i = 0; i < Text.Length; i++)
                if (Text[i] == R0) // ищем ID валюты EUR = R01239
                    if (Text[i + 1] == R1)
                        if (Text[i + 2] == R2)
                            if (Text[i + 3] == R3)
                                if (Text[i + 4] == R4)
                                    if (Text[i + 5] == R5)
                                        for (int j = i + 5; j < Text.Length; j++)
                                            if (Text[j] == 'V')    // ищем внитри валюты название Valye из EUR                                     
                                                if (Text[j + 1] == 'a')
                                                    if (Text[j + 2] == 'l')
                                                        if (Text[j + 3] == 'u')
                                                            if (Text[j + 4] == 'e')
                                                            {
                                                                int Nominal = 0;    // ищем Nominal валюты 
                                                                for (int nom = i + 5; nom < Text.Length; nom++)
                                                                    if (Text[nom] == 'N')
                                                                        if (Text[nom + 1] == 'o')
                                                                            if (Text[nom + 2] == 'm')
                                                                                if (Text[nom + 3] == 'i')
                                                                                    if (Text[nom + 4] == 'n')
                                                                                        if (Text[nom + 5] == 'a')
                                                                                            if (Text[nom + 6] == 'l')
                                                                                            {
                                                                                                int nom2 = nom + 10;     // первая цифра в номинале
                                                                                                string nominal_string = "";
                                                                                                while (Text[nom2] != ',') // до , делаем
                                                                                                {
                                                                                                    nominal_string += Text[nom2].ToString();
                                                                                                    nom2++;
                                                                                                }
                                                                                                Nominal = int.Parse(nominal_string);
                                                                                                nom = Text.Length;
                                                                                            }

                                                                int g = j + 8;    // после Value идут 3 ненужных символа  ":(пробел)  => добавим 3 к j и назовем g
                                                                string multi2_string = "";
                                                                while (Text[g] != ',') // сейчас мы находимся на строчке  ("Value": 86.3949,)  - будем делать до ,
                                                                {
                                                                    multi2_string += Text[g].ToString();
                                                                    g++;
                                                                }
                                                                multi2 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;    // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                richTextBoxWin2.Text = (Math.Round((double.Parse( richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                UndeWin2 = Unde;
                                                                label7.Text = "1 " +UndeWin1 + " = " + Math.Round((multi1 / multi2), 4) + " " + UndeWin2;
                                                                label8.Text = "1 " + UndeWin2 + " = " + Math.Round((multi2 / multi1), 4) + " " + UndeWin1;
                                                                Light2(Unde);
                                                                light2 = Unde;
                                                                break;
                                                            }
        }

        public void Valuta2(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', string Unde = "AUD") // для тех валют у которых в ID  5 символов и справа (окно для просмотра справа)
        {
            WebClient web = new WebClient();
            string Text = web.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js"); //сайт наш 

            for (int i = 0; i < Text.Length; i++)
                if (Text[i] == R0) // ищем ID валюты EUR = R01239
                    if (Text[i + 1] == R1)
                        if (Text[i + 2] == R2)
                            if (Text[i + 3] == R3)
                                if (Text[i + 4] == R4)
                                    if (Text[i + 5] == R5)
                                        for (int j = i + 5; j < Text.Length; j++)
                                            if (Text[j] == 'V')    // ищем внитри валюты название Valye из EUR                                     
                                                if (Text[j + 1] == 'a')
                                                    if (Text[j + 2] == 'l')
                                                        if (Text[j + 3] == 'u')
                                                            if (Text[j + 4] == 'e')
                                                            {
                                                                int Nominal = 0;    // ищем Nominal валюты 
                                                                for (int nom = i + 5; nom < Text.Length; nom++)
                                                                    if (Text[nom] == 'N')
                                                                        if (Text[nom + 1] == 'o')
                                                                            if (Text[nom + 2] == 'm')
                                                                                if (Text[nom + 3] == 'i')
                                                                                    if (Text[nom + 4] == 'n')
                                                                                        if (Text[nom + 5] == 'a')
                                                                                            if (Text[nom + 6] == 'l')
                                                                                            {
                                                                                                int nom2 = nom + 10;     // первая цифра в номинале
                                                                                                string nominal_string = "";
                                                                                                while (Text[nom2] != ',') // до , делаем
                                                                                                {
                                                                                                    nominal_string += Text[nom2].ToString();
                                                                                                    nom2++;
                                                                                                }
                                                                                                Nominal = int.Parse(nominal_string);
                                                                                                nom = Text.Length;
                                                                                            }

                                                                int g = j + 8;    // после Value идут 3 ненужных символа  ":(пробел)  => добавим 3 к j и назовем g
                                                                string multi2_string = "";
                                                                while (Text[g] != ',') // сейчас мы находимся на строчке  ("Value": 86.3949,)  - будем делать до ,
                                                                {
                                                                    multi2_string += Text[g].ToString();
                                                                    g++;
                                                                }
                                                                multi1 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;    // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                richTextBoxWin2.Text = (Math.Round((double.Parse(richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                UndeWin1 = Unde;
                                                                label7.Text = "1 " + UndeWin1 + " = " + Math.Round((multi1 / multi2), 4) + " " + UndeWin2;
                                                                label8.Text = "1 " + UndeWin2 + " = " + Math.Round((multi2 / multi1), 4) + " " + UndeWin1;
                                                                Light1(Unde);
                                                                light1 = Unde;
                                                                break;
                                                            }
        }




        //RUR - USD  (левое окно - RUR. Правое USD)
        private void fm1_Load(object sender, EventArgs e)
        {
            UndeWin1 = "RUR"; // под окном ввода
            UndeWin2 = "USD"; // под окном просмотра
            Light1("RUR");   // сразу подсветим рубль и USD справа
            Light2("USD");
            light1 = "RUR";  
            light2 = "USD";
            multi1 = 1;
            Valuta('R', '0', '1', '2', '3', '5', "USD"); // подсветка  USD справа

        }

        private void buttonRUR1_Click(object sender, EventArgs e)
        {
            //this.buttonRUR1.BackColor = Color.FromArgb(33, 190, 65);
            WebClient web = new WebClient();
            string Text = web.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js"); //страница в инете
            multi1 = 1;
            richTextBoxWin2.Text = (Math.Round((double.Parse(richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString(); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой
            UndeWin1 = "RUR";
            label7.Text = "1 " + UndeWin1 + " = " + Math.Round((multi1 / multi2), 4) + " " + UndeWin2;
            label8.Text = "1 " + UndeWin2 + " = " + Math.Round((multi2 /multi1), 4) + " " + UndeWin1;
            Light1("RUR");
            light1 = "RUR";
        }

       

        private void richTextBoxWin1_TextChanged(object sender, EventArgs e)
        {
            string s1 = this.richTextBoxWin1.Text;      //  запрет на ввод
            Regex regex1 = new Regex(@"(^\d+),?(\d*$)");  // вначале цифра всегда, потом может быть запятая, а потом ещё цифра
            //Regex regex11 = new Regex(@"(^\d+)(,+$)");
            MatchCollection matches1 = regex1.Matches(s1);
            //MatchCollection matches11 = regex11.Matches(s1);
            if (matches1.Count > 0   )
            {
                if (richTextBoxWin1.Text != "" & richTextBoxWin1.Text != ".")
                {
                    richTextBoxWin2.Text = (Math.Round((double.Parse(richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // пересрасчет при изменении текста
                    //richTextBoxWin1.Text = (double.Parse(richTextBoxWin1.Text)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU"));
                    //string[] lines = new []{ richTextBoxWin2.Text };
                    //int i = 0;
                    //while (lines[i] != "," | i < lines.Length)
                    //{
                    //    lines[i] = lines[i].Insert(lines[i].IndexOf(',') + 3, " ");
                    //}




                }
                else
                {
                    richTextBoxWin2.Text = "";
                }
            } 

        }


        //EUR справа
        private void buttonEUR2_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '2', '3', '9', "EUR");  
        }

        //RUR справа
        private void buttonRUR2_Click(object sender, EventArgs e)
        {
            multi2 = 1;
            richTextBoxWin2.Text = (Math.Round((double.Parse(richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString(); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой
            UndeWin2 = "RUR";
            Light2("RUR");
            light2 = "RUR";
            label7.Text = "1 " + UndeWin1 + " = " + Math.Round((multi1 / multi2), 4) + " " + UndeWin2;
            label8.Text = "1 " + UndeWin2 + " = " + Math.Round((multi2 / multi1), 4) + " " + UndeWin1;
        }


        //USD
        private void buttonUSD2_Click(object sender, EventArgs e) //USD2 - означает правое окна. 1 - левое основное
        {
            Valuta('R', '0', '1', '2', '3', '5', "USD");

        }


        //GBP и повторое нажатие на изменунную валюту в кнопке buttonGPB2
        private void buttonGPB2_Click(object sender, EventArgs e)
        {
            Class_Create_Valuta class_Create_Valuta = new Class_Create_Valuta(this);

            if (buttonGPB2.Text == "GBP")
                Valuta('R', '0', '1', '0', '3', '5', "GBP");

            if (buttonGPB2.Text == "AUD")
                class_Create_Valuta.AUD_Click(null, null);

            if (buttonGPB2.Text == "AZN")
                class_Create_Valuta.AZN_Click(null, null);

            if (buttonGPB2.Text == "GBP")
                class_Create_Valuta.GBP_Click(null, null);

            if (buttonGPB2.Text == "AMD")
                class_Create_Valuta.AMD_Click(null, null);

            if (buttonGPB2.Text == "BYN")
                class_Create_Valuta.BYN_Click(null, null);

            if (buttonGPB2.Text == "BGN")
                class_Create_Valuta.BGN_Click(null, null);

            if (buttonGPB2.Text == "BRL")
                class_Create_Valuta.BRL_Click(null, null);

            if (buttonGPB2.Text == "HUF")
                class_Create_Valuta.HUF_Click(null, null);

            if (buttonGPB2.Text == "HKD")
                class_Create_Valuta.HKD_Click(null, null);

            if (buttonGPB2.Text == "DKK")
                class_Create_Valuta.DKK_Click(null, null);

            if (buttonGPB2.Text == "USD")
                class_Create_Valuta.USD_Click(null, null);

            if (buttonGPB2.Text == "INR")
                class_Create_Valuta.INR_Click(null, null);

            if (buttonGPB2.Text == "KZT")
                class_Create_Valuta.KZT_Click(null, null);

            if (buttonGPB2.Text == "CAD")
                class_Create_Valuta.CAD_Click(null, null);

            if (buttonGPB2.Text == "KGS")
                class_Create_Valuta.KGS_Click(null, null);

            if (buttonGPB2.Text == "CNY")
                class_Create_Valuta.CNY_Click(null, null);

            if (buttonGPB2.Text == "MDL")
                class_Create_Valuta.MDL_Click(null, null);

            if (buttonGPB2.Text == "NOK")
                class_Create_Valuta.NOK_Click(null, null);

            if (buttonGPB2.Text == "PLN")
                class_Create_Valuta.PLN_Click(null, null);

            if (buttonGPB2.Text == "RON")
                class_Create_Valuta.RON_Click(null, null);

            if (buttonGPB2.Text == "XDR")
                class_Create_Valuta.XDR_Click(null, null);

            if (buttonGPB2.Text == "SGD")
                class_Create_Valuta.SGD_Click(null, null);

            if (buttonGPB2.Text == "TJS")
                class_Create_Valuta.TJS_Click(null, null);

            if (buttonGPB2.Text == "TRY")
                class_Create_Valuta.TRY_Click(null, null);

            if (buttonGPB2.Text == "TMT")
                class_Create_Valuta.TMT_Click(null, null);

            if (buttonGPB2.Text == "UZS")
                class_Create_Valuta.UZS_Click(null, null);

            if (buttonGPB2.Text == "UAH")
                class_Create_Valuta.UAH_Click(null, null);

            if (buttonGPB2.Text == "CZK")
                class_Create_Valuta.CZK_Click(null, null);

            if (buttonGPB2.Text == "SEK")
                class_Create_Valuta.SEK_Click(null, null);

            if (buttonGPB2.Text == "CHF")
                class_Create_Valuta.CHF_Click(null, null);

            if (buttonGPB2.Text == "ZAR")
                class_Create_Valuta.ZAR_Click(null, null);

            if (buttonGPB2.Text == "KRW")
                class_Create_Valuta.KRW_Click(null, null);

            if (buttonGPB2.Text == "JPY")
                class_Create_Valuta.JPY_Click(null, null);

        }


        //EUR
        private void buttonEUR1_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '2', '3', '9', "EUR");

        }


        //GBP и повторое нажатие на изменунную валюту в кнопке buttonGPB1
        private void buttonGPB1_Click(object sender, EventArgs e)
        {
            if(buttonGPB1.Text == "GBP")
            {
                Valuta2('R', '0', '1', '0', '3', '5', "GBP");
            }


            Class_Create_Valuta class_Create_Valuta = new Class_Create_Valuta(this);

            if (buttonGPB1.Text == "AUD")
                class_Create_Valuta.AUD1_Click(null, null);

            if (buttonGPB1.Text == "AZN")
                class_Create_Valuta.AZN1_Click(null, null);

            if (buttonGPB1.Text == "GBP")
                class_Create_Valuta.GBP1_Click(null, null);

            if (buttonGPB1.Text == "AMD")
                class_Create_Valuta.AMD1_Click(null, null);

            if (buttonGPB1.Text == "BYN")
                class_Create_Valuta.BYN1_Click(null, null);

            if (buttonGPB1.Text == "BGN")
                class_Create_Valuta.BGN1_Click(null, null);

            if (buttonGPB1.Text == "BRL")
                class_Create_Valuta.BRL1_Click(null, null);

            if (buttonGPB1.Text == "HUF")
                class_Create_Valuta.HUF1_Click(null, null);

            if (buttonGPB1.Text == "HKD")
                class_Create_Valuta.HKD1_Click(null, null);

            if (buttonGPB1.Text == "DKK")
                class_Create_Valuta.DKK1_Click(null, null);

            if (buttonGPB1.Text == "USD")
                class_Create_Valuta.USD1_Click(null, null);

            if (buttonGPB1.Text == "INR")
                class_Create_Valuta.INR1_Click(null, null);

            if (buttonGPB1.Text == "KZT")
                class_Create_Valuta.KZT1_Click(null, null);

            if (buttonGPB1.Text == "CAD")
                class_Create_Valuta.CAD1_Click(null, null);

            if (buttonGPB1.Text == "KGS")
                class_Create_Valuta.KGS1_Click(null, null);

            if (buttonGPB1.Text == "CNY")
                class_Create_Valuta.CNY1_Click(null, null);

            if (buttonGPB1.Text == "MDL")
                class_Create_Valuta.MDL1_Click(null, null);

            if (buttonGPB1.Text == "NOK")
                class_Create_Valuta.NOK1_Click(null, null);

            if (buttonGPB1.Text == "PLN")
                class_Create_Valuta.PLN1_Click(null, null);

            if (buttonGPB1.Text == "RON")
                class_Create_Valuta.RON1_Click(null, null);

            if (buttonGPB1.Text == "XDR")
                class_Create_Valuta.XDR1_Click(null, null);

            if (buttonGPB1.Text == "SGD")
                class_Create_Valuta.SGD1_Click(null, null);

            if (buttonGPB1.Text == "TJS")
                class_Create_Valuta.TJS1_Click(null, null);

            if (buttonGPB1.Text == "TRY")
                class_Create_Valuta.TRY1_Click(null, null);

            if (buttonGPB1.Text == "TMT")
                class_Create_Valuta.TMT1_Click(null, null);

            if (buttonGPB1.Text == "UZS")
                class_Create_Valuta.UZS1_Click(null, null);

            if (buttonGPB1.Text == "UAH")
                class_Create_Valuta.UAH1_Click(null, null);

            if (buttonGPB1.Text == "CZK")
                class_Create_Valuta.CZK1_Click(null, null);

            if (buttonGPB1.Text == "SEK")
                class_Create_Valuta.SEK1_Click(null, null);

            if (buttonGPB1.Text == "CHF")
                class_Create_Valuta.CHF1_Click(null, null);

            if (buttonGPB1.Text == "ZAR")
                class_Create_Valuta.ZAR1_Click(null, null);

            if (buttonGPB1.Text == "KRW")
                class_Create_Valuta.KRW1_Click(null, null);

            if (buttonGPB1.Text == "JPY")
                class_Create_Valuta.JPY1_Click(null, null);

        }

        
        
        void button5_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem AUD = new ToolStripMenuItem("Австралийский доллар               AUD");
            ToolStripMenuItem AZN = new ToolStripMenuItem("Азербайджанский манат            AZN");
            ToolStripMenuItem GBP = new ToolStripMenuItem("Фунт стерлингов Соед. кор.       GBP");
            ToolStripMenuItem AMD = new ToolStripMenuItem("Армянских драмов                      AMD");
            ToolStripMenuItem BYN = new ToolStripMenuItem("Белорусский рубль                      BYN");
            ToolStripMenuItem BGN = new ToolStripMenuItem("Болгарский лев                             BGN");
            ToolStripMenuItem BRL = new ToolStripMenuItem("Бразильский реал                         BRL");
            ToolStripMenuItem HUF = new ToolStripMenuItem("Венгерских форинтов                  HUF");
            ToolStripMenuItem HKD = new ToolStripMenuItem("Гонконгских долларов                HKD");
            ToolStripMenuItem DKK = new ToolStripMenuItem("Датская крона                                DKK");
            //ToolStripMenuItem USD = new ToolStripMenuItem("Доллар США                                USD");
            //contextMenuStrip2.Items.Add("Евро   EUR");
            ToolStripMenuItem INR = new ToolStripMenuItem("Индийских рупий                          INR");
            ToolStripMenuItem KZT = new ToolStripMenuItem("Казахстанских тенге                      KZT");
            ToolStripMenuItem CAD = new ToolStripMenuItem("Канадский доллар                         CAD");
            ToolStripMenuItem KGS = new ToolStripMenuItem("Киргизских сомов                         KGS");
            ToolStripMenuItem CNY = new ToolStripMenuItem("Китайский юань                            CNY");
            ToolStripMenuItem MDL = new ToolStripMenuItem("Молдавских леев                          MDL");
            ToolStripMenuItem NOK = new ToolStripMenuItem("Норвежских крон                         NOK");
            ToolStripMenuItem PLN = new ToolStripMenuItem("Польский злотый                          PLN");
            ToolStripMenuItem RON = new ToolStripMenuItem("Румынский лей                             RON");
            ToolStripMenuItem XDR = new ToolStripMenuItem("СДР(права заимствования)        XDR");
            ToolStripMenuItem SGD = new ToolStripMenuItem("Сингапурский доллар                 SGD");
            ToolStripMenuItem TJS = new ToolStripMenuItem("Таджикских сомони                     TJS");
            ToolStripMenuItem TRY = new ToolStripMenuItem("Турецких лир                                 TRY");
            ToolStripMenuItem TMT = new ToolStripMenuItem("Новый туркменский манат        TMT");
            ToolStripMenuItem UZS = new ToolStripMenuItem("Узбекских сумов                           UZS");
            ToolStripMenuItem UAH = new ToolStripMenuItem("Украинских гривен                      UAH");
            ToolStripMenuItem CZK = new ToolStripMenuItem("Чешских крон                               CZK");
            ToolStripMenuItem SEK = new ToolStripMenuItem("Шведских крон                              SEK");
            ToolStripMenuItem CHF = new ToolStripMenuItem("Швейцарский франк                    CHF");
            ToolStripMenuItem ZAR = new ToolStripMenuItem("Южноафриканских рэндов        ZAR");
            ToolStripMenuItem KRW = new ToolStripMenuItem("Вон Республики Корея                KRW");
            ToolStripMenuItem JPY = new ToolStripMenuItem("Японских иен                                 JPY");

            contextMenuStrip2.Items.Clear();
            contextMenuStrip2.Items.AddRange(new[] { AUD, AZN, GBP, AMD, BYN, BGN, BRL, HUF, HKD, DKK,  INR, KZT, CAD, KGS, CNY, MDL, NOK, PLN, RON, XDR, SGD, TJS, TRY, TMT, UZS, UAH, CZK, SEK, CHF, ZAR, KRW, JPY });
            contextMenuStrip2.Show(button5, new Point(0, button5.Height));

            // создаём клик по кнопке
            Class_Create_Valuta = new Class_Create_Valuta(this);  // работаем с классом Class_Create_Valuta
            AUD.Click += Class_Create_Valuta.AUD1_Click;
            AZN.Click += Class_Create_Valuta.AZN1_Click;
            GBP.Click += Class_Create_Valuta.GBP1_Click;
            AMD.Click += Class_Create_Valuta.AMD1_Click;
            BYN.Click += Class_Create_Valuta.BYN1_Click;
            BGN.Click += Class_Create_Valuta.BGN1_Click;
            BRL.Click += Class_Create_Valuta.BRL1_Click;
            HUF.Click += Class_Create_Valuta.HUF1_Click;
            HKD.Click += Class_Create_Valuta.HKD1_Click;
            DKK.Click += Class_Create_Valuta.DKK1_Click;
            //USD.Click += Class_Create_Valuta.USD1_Click;
            INR.Click += Class_Create_Valuta.INR1_Click;
            KZT.Click += Class_Create_Valuta.KZT1_Click;
            CAD.Click += Class_Create_Valuta.CAD1_Click;
            KGS.Click += Class_Create_Valuta.KGS1_Click;
            CNY.Click += Class_Create_Valuta.CNY1_Click;
            MDL.Click += Class_Create_Valuta.MDL1_Click;
            NOK.Click += Class_Create_Valuta.NOK1_Click;
            PLN.Click += Class_Create_Valuta.PLN1_Click;
            RON.Click += Class_Create_Valuta.RON1_Click;
            XDR.Click += Class_Create_Valuta.XDR1_Click;
            SGD.Click += Class_Create_Valuta.SGD1_Click;
            TJS.Click += Class_Create_Valuta.TJS1_Click;
            TRY.Click += Class_Create_Valuta.TRY1_Click;
            TMT.Click += Class_Create_Valuta.TMT1_Click;
            UZS.Click += Class_Create_Valuta.UZS1_Click;
            UAH.Click += Class_Create_Valuta.UAH1_Click;
            CZK.Click += Class_Create_Valuta.CZK1_Click;
            SEK.Click += Class_Create_Valuta.SEK1_Click;
            CHF.Click += Class_Create_Valuta.CHF1_Click;
            ZAR.Click += Class_Create_Valuta.ZAR1_Click;
            KRW.Click += Class_Create_Valuta.KRW1_Click;
            JPY.Click += Class_Create_Valuta.JPY1_Click;

        }

 

        private void richTextBoxWin2_KeyPress(object sender, KeyPressEventArgs e) // запрет на ввод любого символа
        {
            char number = e.KeyChar;
            if (e.KeyChar != 0)
            {
                e.Handled = true;
            }
        }

        private void richTextBoxWin1_KeyPress(object sender, KeyPressEventArgs e) // только цифры и ,
        {
            char number = e.KeyChar;
            if (!(e.KeyChar >= 48 & e.KeyChar <= 58) & (e.KeyChar != 44) & e.KeyChar != 8 & e.KeyChar != 0 )
            {
                e.Handled = true;
            }
        }

        private void textBoxWin1_KeyPress(object sender, KeyPressEventArgs e)  // запрет на ввод любого символа
        {
            char number = e.KeyChar;
            if (e.KeyChar != 0)
            {
                e.Handled = true;
            }
        }

        private void textBoxWin2_KeyPress(object sender, KeyPressEventArgs e) // запрет на ввод любого символа
        {
            char number = e.KeyChar;
            if (e.KeyChar != 0)
            {
                e.Handled = true;
            }
        }



        // Если введено в поле ввода не соответствующая информация (цифры и ,) то пусть в поле  просмотра запишется 1
        private void richTextBoxWin1_Leave(object sender, EventArgs e)
        {
            //string s1 = this.richTextBoxWin1.Text;
            //Regex regex1 = new Regex(@"(^\d+),?(\d*$)");
            //Regex regex11 = new Regex(@"(^\d+)(,+$)");
            //MatchCollection matches1 = regex1.Matches(s1);
            //MatchCollection matches11 = regex11.Matches(s1);
            //if (matches1.Count > 0) { }
            //else
            //    this.richTextBoxWin1.Text = "1";
            
           
        }


        // Если введено в поле ввода не соответствующая информация (буквы и т.п.) то пусть в поле просмотра запишется 1
        private void richTextBoxWin1_MouseLeave(object sender, EventArgs e)
        {
            string s1 = this.richTextBoxWin1.Text;
            Regex regex1 = new Regex(@"(^\d+),?(\d*$)");
            Regex regex11 = new Regex(@"(^\d+)(,+$)");
            MatchCollection matches1 = regex1.Matches(s1);
            MatchCollection matches11 = regex11.Matches(s1);
            if (matches1.Count > 0) 
            {
                //richTextBoxWin1.Text = (double.Parse(richTextBoxWin1.Text)).ToString("N0", CultureInfo.GetCultureInfo("ru-RU"));
            }
            else
                this.richTextBoxWin1.Text = "1";
        }

        private void button_Obmenn_MouseEnter(object sender, EventArgs e)
        {
            this.button_Obmenn.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.Снимок2;
        }

        private void button_Obmenn_MouseLeave(object sender, EventArgs e)
        {
            this.button_Obmenn.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.СнимокВП444;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            this.button5.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.СнимокВниз666;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            this.button5.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.СнимокВниз555;
        }

        
         private void button6_MouseEnter(object sender, EventArgs e)
         {
             this.button6.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.СнимокВниз666;
         }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            this.button6.BackgroundImage = global::WindowsFormsApp4_Valuta.Properties.Resources.СнимокВниз555;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // запоним список валютами
            ToolStripMenuItem AUD = new ToolStripMenuItem("Австралийский доллар               AUD");
            ToolStripMenuItem AZN = new ToolStripMenuItem("Азербайджанский манат            AZN");
            ToolStripMenuItem GBP = new ToolStripMenuItem("Фунт стерлингов Соед. кор.       GBP");
            ToolStripMenuItem AMD = new ToolStripMenuItem("Армянских драмов                      AMD");
            ToolStripMenuItem BYN = new ToolStripMenuItem("Белорусский рубль                      BYN");
            ToolStripMenuItem BGN = new ToolStripMenuItem("Болгарский лев                             BGN");
            ToolStripMenuItem BRL = new ToolStripMenuItem("Бразильский реал                         BRL");
            ToolStripMenuItem HUF = new ToolStripMenuItem("Венгерских форинтов                  HUF");
            ToolStripMenuItem HKD = new ToolStripMenuItem("Гонконгских долларов                HKD");
            ToolStripMenuItem DKK = new ToolStripMenuItem("Датская крона                                DKK");
            //contextMenuStrip2.Items.Add("Доллар США   USD");
            //contextMenuStrip2.Items.Add("Евро   EUR");
            ToolStripMenuItem INR = new ToolStripMenuItem("Индийских рупий                          INR");
            ToolStripMenuItem KZT = new ToolStripMenuItem("Казахстанских тенге                      KZT");
            ToolStripMenuItem CAD = new ToolStripMenuItem("Канадский доллар                         CAD");
            ToolStripMenuItem KGS = new ToolStripMenuItem("Киргизских сомов                         KGS");
            ToolStripMenuItem CNY = new ToolStripMenuItem("Китайский юань                            CNY");
            ToolStripMenuItem MDL = new ToolStripMenuItem("Молдавских леев                          MDL");
            ToolStripMenuItem NOK = new ToolStripMenuItem("Норвежских крон                         NOK");
            ToolStripMenuItem PLN = new ToolStripMenuItem("Польский злотый                          PLN");
            ToolStripMenuItem RON = new ToolStripMenuItem("Румынский лей                             RON");
            ToolStripMenuItem XDR = new ToolStripMenuItem("СДР(права заимствования)        XDR");
            ToolStripMenuItem SGD = new ToolStripMenuItem("Сингапурский доллар                 SGD");
            ToolStripMenuItem TJS = new ToolStripMenuItem("Таджикских сомони                     TJS");
            ToolStripMenuItem TRY = new ToolStripMenuItem("Турецких лир                                 TRY");
            ToolStripMenuItem TMT = new ToolStripMenuItem("Новый туркменский манат        TMT");
            ToolStripMenuItem UZS = new ToolStripMenuItem("Узбекских сумов                           UZS");
            ToolStripMenuItem UAH = new ToolStripMenuItem("Украинских гривен                      UAH");
            ToolStripMenuItem CZK = new ToolStripMenuItem("Чешских крон                               CZK");
            ToolStripMenuItem SEK = new ToolStripMenuItem("Шведских крон                              SEK");
            ToolStripMenuItem CHF = new ToolStripMenuItem("Швейцарский франк                    CHF");
            ToolStripMenuItem ZAR = new ToolStripMenuItem("Южноафриканских рэндов        ZAR");
            ToolStripMenuItem KRW = new ToolStripMenuItem("Вон Республики Корея                KRW");
            ToolStripMenuItem JPY = new ToolStripMenuItem("Японских иен                                 JPY");
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.AddRange(new[] { AUD, AZN, GBP, AMD, BYN, BGN, BRL, HUF, HKD, DKK, INR, KZT, CAD, KGS, CNY, MDL, NOK, PLN, RON, XDR, SGD, TJS, TRY, TMT, UZS, UAH, CZK, SEK, CHF, ZAR, KRW, JPY });
            contextMenuStrip1.Show(button6, new Point(0, button6.Height ));


            // запишем  клики валют
            Class_Create_Valuta = new Class_Create_Valuta(this);  // работаем с классом Class_Create_Valuta
            AUD.Click += Class_Create_Valuta.AUD_Click;
            AZN.Click += Class_Create_Valuta.AZN_Click;
            GBP.Click += Class_Create_Valuta.GBP_Click;
            AMD.Click += Class_Create_Valuta.AMD_Click;
            BYN.Click += Class_Create_Valuta.BYN_Click;
            BGN.Click += Class_Create_Valuta.BGN_Click;
            BRL.Click += Class_Create_Valuta.BRL_Click;
            HUF.Click += Class_Create_Valuta.HUF_Click;
            HKD.Click += Class_Create_Valuta.HKD_Click;
            DKK.Click += Class_Create_Valuta.DKK_Click;
            INR.Click += Class_Create_Valuta.INR_Click;
            KZT.Click += Class_Create_Valuta.KZT_Click;
            CAD.Click += Class_Create_Valuta.CAD_Click;
            KGS.Click += Class_Create_Valuta.KGS_Click;
            CNY.Click += Class_Create_Valuta.CNY_Click;
            MDL.Click += Class_Create_Valuta.MDL_Click;
            NOK.Click += Class_Create_Valuta.NOK_Click;
            PLN.Click += Class_Create_Valuta.PLN_Click;
            RON.Click += Class_Create_Valuta.RON_Click;
            XDR.Click += Class_Create_Valuta.XDR_Click;
            SGD.Click += Class_Create_Valuta.SGD_Click;
            TJS.Click += Class_Create_Valuta.TJS_Click;
            TRY.Click += Class_Create_Valuta.TRY_Click;
            TMT.Click += Class_Create_Valuta.TMT_Click;
            UZS.Click += Class_Create_Valuta.UZS_Click;
            UAH.Click += Class_Create_Valuta.UAH_Click;
            CZK.Click += Class_Create_Valuta.CZK_Click;
            SEK.Click += Class_Create_Valuta.SEK_Click;
            CHF.Click += Class_Create_Valuta.CHF_Click;
            ZAR.Click += Class_Create_Valuta.ZAR_Click;
            KRW.Click += Class_Create_Valuta.KRW_Click;
            JPY.Click += Class_Create_Valuta.JPY_Click;

        }

        //USD левое окно - для ввода
        private void buttonUSD1_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '2', '3', '5', "USD");
        }


        // Кнопка обмена валютами
        private void button1_Click(object sender, EventArgs e)
        {
            // меняет названия если была нажата кнопка с GBP
            if (light2 == "GBP" & light1 != "GBP")
                buttonGPB1.Text = buttonGPB2.Text;
            if (light1 == "GBP" & light2 != "GBP")
                buttonGPB2.Text = buttonGPB1.Text;
            if(light2 == "GBP" & light1 == "GBP")
            {
                string l = buttonGPB1.Text;
                buttonGPB1.Text = buttonGPB2.Text;
                buttonGPB2.Text = l;
            }

            // меняем множители, во сколько раз валюта больше / меньше рубля
            double referens = multi1;
            multi1 = multi2;
            multi2 = referens;

            // подсветка
            string lig = light1;
            light1 = light2;
            light2 = lig;

            // подсказка под основными окнами 
            string Unde = UndeWin1;
            UndeWin1 = UndeWin2;
            UndeWin2 = Unde;

            // выполняем всё
            richTextBoxWin2.Text = (Math.Round((double.Parse(richTextBoxWin1.Text) * multi1 / multi2), 4)).ToString(); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой
            label7.Text = "1 " + UndeWin1 + " = " + Math.Round((multi1 / multi2), 4) + " " + UndeWin2;
            label8.Text = "1 " + UndeWin2 + " = " + Math.Round((multi2 / multi1), 4) + " " + UndeWin1;
            Light1(light1);
            Light2(light2);
        }

    }
}
