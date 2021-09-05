using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4_Valuta
{
    class Class_Create_Valuta           // класс для валют и работы с ней (текст, подсвета, подсказки, выбор, клмк)
    {
        Form1 fm1;
        
        public Class_Create_Valuta(Form1 form1)
        {
            fm1 = form1;
        }
        public void Valuta(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', string Unde = "AUD") // для тех валют у которых в ID  5 символов
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
                                                                fm1.multi2 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;    // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                fm1.richTextBoxWin2.Text = (Math.Round((double.Parse(fm1.richTextBoxWin1.Text) * fm1.multi1 / fm1.multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                fm1.UndeWin2 = Unde;
                                                                fm1.label7.Text = "1 " + fm1.UndeWin1 + " = " + Math.Round((fm1.multi1 / fm1.multi2), 4) + " " + fm1.UndeWin2;
                                                                fm1.label8.Text = "1 " + fm1.UndeWin2 + " = " + Math.Round((fm1.multi2 / fm1.multi1), 4) + " " + fm1.UndeWin1;
                                                                fm1.buttonGPB2.Text = Unde;
                                                                fm1.Light2("GBP");
                                                                fm1.light2 = "GBP";
                                                                break;
                                                            }
        }

        public void Valuta2(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', char R6 = 'A', string Unde = "AZN")  // для тех валют у которых в ID  6 символов
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
                                        if (Text[i + 6] == R6)
                                            for (int j = i + 6; j < Text.Length; j++)
                                                if (Text[j] == 'V')    // ищем внитри валюты название Valye из EUR                                     
                                                    if (Text[j + 1] == 'a')
                                                        if (Text[j + 2] == 'l')
                                                            if (Text[j + 3] == 'u')
                                                                if (Text[j + 4] == 'e')
                                                                {
                                                                    int Nominal = 0;    // ищем Nominal валюты 
                                                                    for (int nom = i + 6; nom < Text.Length; nom++)
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
                                                                    fm1.multi2 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;   // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                    fm1.richTextBoxWin2.Text = (Math.Round((double.Parse(fm1.richTextBoxWin1.Text) * fm1.multi1 / fm1.multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                    fm1.UndeWin2 = Unde;
                                                                    fm1.label7.Text = "1 " + fm1.UndeWin1 + " = " + Math.Round((fm1.multi1 / fm1.multi2), 4) + " " + fm1.UndeWin2;
                                                                    fm1.label8.Text = "1 " + fm1.UndeWin2 + " = " + Math.Round((fm1.multi2 / fm1.multi1), 4) + " " + fm1.UndeWin1;
                                                                    fm1.buttonGPB2.Text = Unde;
                                                                    fm1.Light2("GBP");
                                                                    fm1.light2 = "GBP";

                                                                    break;
                                                                }
        }

        //AUD спарва - правое окно для просмотра
        public   void AUD_Click(object sender, EventArgs e)
        {
            Valuta('R',  '0', '1',  '0',  '1',  '0', "AUD");
        }

        //AZN 
        public void AZN_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '0', '2', '0', 'A', "AZN");
        }

        //GBP
        public void GBP_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '0', '3', '5', "GBP");  
        }

        //AMD
        public void AMD_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '0', '6', '0', "AMD");
        }

        //BYN
        public void BYN_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '0', '9', '0', 'B', "BYN");
        }

        //BGN
        public void BGN_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '1', '0', '0', "BGN");

        }

        //BRL
        public void BRL_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '1', '1', '5', "BRL");

        }


        //HUF
        public void HUF_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '1', '3', '5', "HUF");
        }

        //HKD
        public void HKD_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '2', '0', '0', "HKD");

        }

        //DKK
        public void DKK_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '2', '1', '5', "DKK");
        }

        //USD
        public void USD_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '2', '3', '5', "USD");
        }

        //INR
        public void INR_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '2', '7', '0', "INR");
        }

        //KZT
        public void KZT_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '3', '3', '5', "KZT");
        }

        //CAD
        public void CAD_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '3', '5', '0', "CAD");
        }

        //KGS
        public void KGS_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '3', '7', '0', "KGS");

        }

        //CNY
        public void CNY_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '3', '7', '5', "CNY");
        }

        //MDL
        public void MDL_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '5', '0', '0', "MDL");
        }

        //NOK
        public void NOK_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '5', '3', '5', "NOK");
        }

        //PLN
        public void PLN_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '5', '6', '5', "PLN");
        }

        //RON
        public void RON_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '5', '8', '5', 'F', "RON");
        }

        //XDR
        public void XDR_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '5', '8', '9', "XDR");
        }

        //SGD
        public void SGD_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '6', '2', '5', "SGD");
        }

        //TJS
        public void TJS_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '6', '7', '0', "TJS");
        }

        //TRY
        public void TRY_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '7', '0', '0', 'J', "TRY");
        }

        //TMT
        public void TMT_Click(object sender, EventArgs e)
        {
            Valuta2('R', '0', '1', '7', '1', '0', 'A', "TMT");
        }

        //UZS
        public void UZS_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '7', '1', '7', "UZS");
        }

        //UAH
        public void UAH_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '7', '2', '0', "UAH");
        }

        //CZK
        public void CZK_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '7', '6', '0', "CZK");
        }

        //SEK
        public void SEK_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '7', '7', '0', "SEK");
        }

        //CHF
        public void CHF_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '7', '7', '5', "CHF");
        }

        //ZAR
        public void ZAR_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '8', '1', '0', "ZAR");
        }

        //KRW
        public void KRW_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '8', '1', '5', "KRW");
        }

        //JPY
        public void JPY_Click(object sender, EventArgs e)
        {
            Valuta('R', '0', '1', '8', '2', '0', "JPY");
        }


        // ------------------------------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------------------------------
        public void Valuta11(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', string Unde = "AUD") // для тех валют у которых в ID  5 символов
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
                                                                fm1.multi1 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;    // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                fm1.richTextBoxWin2.Text = (Math.Round((double.Parse(fm1.richTextBoxWin1.Text) * fm1.multi1 / fm1.multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                fm1.UndeWin1 = Unde;
                                                                fm1.label7.Text = "1 " + fm1.UndeWin1 + " = " + Math.Round((fm1.multi1 / fm1.multi2), 4) + " " + fm1.UndeWin2;
                                                                fm1.label8.Text = "1 " + fm1.UndeWin2 + " = " + Math.Round((fm1.multi2 / fm1.multi1), 4) + " " + fm1.UndeWin1;
                                                                fm1.buttonGPB1.Text = Unde;
                                                                fm1.Light1("GBP");
                                                                fm1.light1 = "GBP";
                                                                break;
                                                            }
        }

        public void Valuta22(char R0 = 'R', char R1 = '0', char R2 = '1', char R3 = '0', char R4 = '1', char R5 = '0', char R6 = 'A', string Unde = "AZN")  // для тех валют у которых в ID  6 символов
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
                                        if (Text[i + 6] == R6)
                                            for (int j = i + 6; j < Text.Length; j++)
                                                if (Text[j] == 'V')    // ищем внитри валюты название Valye из EUR                                     
                                                    if (Text[j + 1] == 'a')
                                                        if (Text[j + 2] == 'l')
                                                            if (Text[j + 3] == 'u')
                                                                if (Text[j + 4] == 'e')
                                                                {
                                                                    int Nominal = 0;    // ищем Nominal валюты 
                                                                    for (int nom = i + 6; nom < Text.Length; nom++)
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
                                                                    fm1.multi1 = Double.Parse(multi2_string.Replace('.', ',')) / Nominal;   // строки нельзя менять! а у нас там "." а нужна , - создадим новую стоку с ,    - и превратим в double
                                                                    fm1.richTextBoxWin2.Text = (Math.Round((double.Parse(fm1.richTextBoxWin1.Text) * fm1.multi1 / fm1.multi2), 4)).ToString("N3", CultureInfo.GetCultureInfo("ru-RU")); // лучiе в одну строку. Round - округляет до n (n = 4 сейчас) знаком после запятой

                                                                    fm1.UndeWin1 = Unde;
                                                                    fm1.label7.Text = "1 " + fm1.UndeWin1 + " = " + Math.Round((fm1.multi1 / fm1.multi2), 4) + " " + fm1.UndeWin2;
                                                                    fm1.label8.Text = "1 " + fm1.UndeWin2 + " = " + Math.Round((fm1.multi2 / fm1.multi1), 4) + " " + fm1.UndeWin1;
                                                                    fm1.buttonGPB1.Text = Unde;
                                                                    fm1.Light1("GBP");
                                                                    fm1.light1 = "GBP";
                                                                    break;
                                                                }
        }


        //AUD слево - левок окно для ввода
        public void AUD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '0', '1', '0', "AUD");
        }

        //AZN
        public void AZN1_Click(object sender, EventArgs e)
        {
            Valuta22('R', '0', '1', '0', '2', '0', 'A', "AZN");
        }

        //GBP
        public void GBP1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '0', '3', '5', "GBP");
        }

        //AMD
        public void AMD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '0', '6', '0', "AMD");
        }

        //BYN
        public void BYN1_Click(object sender, EventArgs e)
        {
            Valuta22('R', '0', '1', '0', '9', '0', 'B', "BYN");
        }

        //BGN
        public void BGN1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '1', '0', '0', "BGN");
        }

        //BRL
        public void BRL1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '1', '1', '5', "BRL");
        }


        //HUF
        public void HUF1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '1', '3', '5', "HUF");
        }

        //HKD
        public void HKD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '2', '0', '0', "HKD");
        }

        //DKK
        public void DKK1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '2', '1', '5', "DKK");
        }

        //USD
        public void USD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '2', '3', '5', "USD");
        }

        //INR
        public void INR1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '2', '7', '0', "INR");
        }

        //KZT
        public void KZT1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '3', '3', '5', "KZT");
        }

        //CAD
        public void CAD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '3', '5', '0', "CAD");
        }

        //KGS
        public void KGS1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '3', '7', '0', "KGS");
        }

        //CNY
        public void CNY1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '3', '7', '5', "CNY");
        }

        //MDL
        public void MDL1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '5', '0', '0', "MDL");
        }

        //NOK
        public void NOK1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '5', '3', '5', "NOK");
        }

        //PLN
        public void PLN1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '5', '6', '5', "PLN");
        }

        //RON
        public void RON1_Click(object sender, EventArgs e)
        {
            Valuta22('R', '0', '1', '5', '8', '5', 'F', "RON");
        }

        //XDR
        public void XDR1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '5', '8', '9', "XDR");
        }

        //SGD
        public void SGD1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '6', '2', '5', "SGD");
        }

        //TJS
        public void TJS1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '6', '7', '0', "TJS");
        }

        //TRY
        public void TRY1_Click(object sender, EventArgs e)
        {
            Valuta22('R', '0', '1', '7', '0', '0', 'J', "TRY");
        }

        //TMT
        public void TMT1_Click(object sender, EventArgs e)
        {
            Valuta22('R', '0', '1', '7', '1', '0', 'A', "TMT");
        }

        //UZS
        public void UZS1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '7', '1', '7', "UZS");
        }

        //UAH
        public void UAH1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '7', '2', '0', "UAH");
        }

        //CZK
        public void CZK1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '7', '6', '0', "CZK");
        }

        //SEK
        public void SEK1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '7', '7', '0', "SEK");
        }

        //CHF
        public void CHF1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '7', '7', '5', "CHF");
        }

        //ZAR
        public void ZAR1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '8', '1', '0', "ZAR");
        }

        //KRW
        public void KRW1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '8', '1', '5', "KRW");
        }

        //JPY
        public void JPY1_Click(object sender, EventArgs e)
        {
            Valuta11('R', '0', '1', '8', '2', '0', "JPY");
        }

    }
}
