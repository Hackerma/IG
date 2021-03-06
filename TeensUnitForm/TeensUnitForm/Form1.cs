﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TeensUnitForm
{
    public partial class Form1 : Form
    {
        public static bool checkBirth(string s1, string s2, string s3)
        {
            if (s1 == "" || s2 == "" || s3 == "")
                return false;
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy");
            short y = short.Parse(DateTime.Now.ToString("yyyy"));
            short m = short.Parse(DateTime.Now.ToString("MM"));
            short d = short.Parse(DateTime.Now.ToString("dd"));
            short Y = short.Parse(s1);
            short M = short.Parse(s2);
            short D = short.Parse(s3);

            if (m == 2 && D > 28)
                return false;

            if (m > 12 || m < 0 || D < 0 || D > 31)
                return false;

            if ((m == 4 || m == 6 || m == 9 || m == 11) && D > 30)
                return false;

            if (y - 24 > Y && y - 13 < Y)
                return false;

            if (y - 24 < Y && y - 13 > Y)
                return true;


            if (y - 24 == Y)
            {
                if (m != M)
                    return m > M;
                return d > D;
            }

            if (m != M)
                return m < M;
            return d <= D;
        }

       

        public static bool checkName(string s)
        {
            string line;
            Dictionary<string, char> openWith = new Dictionary<string, char>();

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"name.txt");
            while ((line = file.ReadLine()) != null)
            {
                line = line.ToLower();
                openWith.Add(line, '');
            }
            file.Close();
            return openWith.ContainsKey(s);
        }
        
        static bool check_mail(string mail)
        {
            int length = mail.Length;
            int i = 0;
            while (i < length && mail[i] != '@')
            {
                i++;
            }
            while (i < length && mail[i] != '.')
            {
                i++;
            }
            string s = "";
            while (i < length)
            {
                s += mail[i];
                i += 1;
            }
            return s == ".com" || s == ".ac.uk" || s == ".uk";
        }
        static bool check_condition(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    count += 1;
            }
            return count > 2;
        }

        static bool check_postcode(string post)
        {
            string s = "";
            foreach (char c in post)
            {
                if (c >= 'A' && c <= 'Z')
                    s += 'L';
                else if (c >= '0' && c <= '9')
                    s += 'N';
            }
            if (s.Length != post.Length)
                return false;
            string[] area = new string[] { "LN", "LNN", "LLN", "LLNN", "LNL", "LLNL", "LLL" };
            int length = s.Length - 1;
            if (length < 4)
                return false;
            int outward = length - 4;
            if (s[length] != 'L' || post[length] == 'C' || post[length] == 'I' || post[length] == 'M' || post[length] == 'O' || post[length] == 'V')
                return false;
            length--;
            if (s[length] != 'L' || post[length] == 'C' || post[length] == 'I' || post[length] == 'M' || post[length] == 'O' || post[length] == 'V')
                return false;
            length--;
            if (s[length] != 'N')
                return false;
            string s2 = "";
            for (int j = 0; j < length; j++)
                s2 += s[j];
            int i = 0;
            int length2 = area.Length;
            bool check = false;
            while (i < length2 && !check)
            {
                check = area[i] == s2;
                i++;
            }
            Console.WriteLine(s2);
            return check;
        }

        static bool check_phone(string number)
        {
            if (number != null)
                return number.Length == 10;
            else return false;
        }
       
        static bool check_country(string country)
        {
            country = country.ToLower();
            return country == "england" || country == "uK" || country == "united kingdom" || country == "ireland" || country == "wales" || country == "scotland";
        }
        static bool realname(string txt)
        {
            int l = txt.Length;
            for (int i = 0; i < l; i++)
            {
                if ((txt[i] <= 'A') || ((txt[i] >= 'Z') && (txt[i] <= 'a')) || (txt[i] >= 'z'))
                {
                    if (((txt[i] == ' ') || (txt[i] == '-') || (txt[i] == 'é') || (txt[i] == 'ë') || (txt[i] == 'ê') || (txt[i] == 'è') || (txt[i] == 'ñ') || (txt[i] == 'ç') || (txt[i] == 'ù')))
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool samename(string name, string other)
        {
            int c = 0;
            int d = 0;
            int l;
            int diff;
            bool first = true;
            if (name.Length > other.Length)
            {
                l = other.Length;
                diff = name.Length - other.Length;
            }
            else
            {
                l = name.Length;
                diff = other.Length - name.Length;
                first = false;
            }
            int[] tab = new int[(diff + l) * (diff + l)];
            for (int i = 0; i <= diff; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (first)
                    {
                        if (name[i + j] == other[j])
                        {
                            c++;
                        }
                        else
                        {
                            tab[d] = c;
                            d++;
                            c = 0;
                        }
                    }
                    else
                    {
                        if (other[i + j] == name[j])
                        {
                            c++;
                        }
                        else
                        {
                            tab[d] = c;
                            d++;
                            c = 0;
                        }
                    }
                }
                tab[d] = c;
                c = 0;
                d++;
            }
            int max = 0;
            for (int i = 0; i < d; i++)
            {
                if (max < tab[i])
                {
                    max = tab[i];
                }
            }
            return ((float)max * 10 / (float)l) > 0.5f;
        }

        static bool shitname(string txt)
        {
            int c = 0;
            int l = txt.Length;
            for (int i = 0; i < l; i++)
            {
                if ((txt[i] == 'a') || (txt[i] == 'A') || (txt[i] == 'e') || (txt[i] == 'E') || (txt[i] == 'i') || (txt[i] == 'I') || (txt[i] == 'o') || (txt[i] == 'O') || (txt[i] == 'u') || (txt[i] == 'U') || (txt[i] == 'y') || (txt[i] == 'Y'))
                {
                    c = 0;
                }
                else
                {
                    c++;
                }
                if (c >= 4)
                {
                    return false;
                }
            }
            return true;
        }

        static bool notforbiddenword(string txt)
        {

            string verif = txt.ToLower();

            if (samename(verif, "azerty") || samename(verif, "qwerty") || samename(verif, "hitler") || samename(verif, "nazi"))
            {
                return false;
            }
            return true;
        }

        static bool voyelle(string txt)
        {
            int l = txt.Length;
            if (l >= 3)
            {
                for (int i = 0; i < l - 2; i++)
                {
                    if (txt[i] == txt[i + 1] && txt[i] == txt[i + 2])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool verif(string txt)
        {
            return (realname(txt) && voyelle(txt) && notforbiddenword(txt) && shitname(txt));
        }
        // .............................................................................................................

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name1 = textBox1.Text;
            String name2 = textBox21.Text;


            String[] birth = { textBox20.Text, textBox2.Text, textBox3.Text };
            String gender = comboBox1.Text;
            String street = textBox7.Text;
            String locality = textBox19.Text;
            String town = textBox18.Text;
            String code = textBox17.Text;
            String country = textBox16.Text;
            String home = textBox15.Text;
            String mobile = textBox14.Text;
            String mail = textBox13.Text;


            String name3 = textBox12.Text;
            String name4 = textBox11.Text;
            String tel = textBox10.Text;
            String mail2 = textBox9.Text;


            String swName1 = textBox5.Text;
            String swName2 = textBox4.Text;


            String med = richTextBox1.Text;
            String hospital = richTextBox2.Text;
            String spec = richTextBox3.Text;


            Boolean[] checks = { Choice1.Checked, Choice2.Checked, Choice3.Checked, Choice4.Checked,
                Choice5.Checked, Choice6.Checked, Choice7.Checked, Choice8.Checked, };


            bool b = (checkBirth(birth[2],birth[1],birth[0]) && check_mail(mail) && check_condition(med) && check_postcode(code) && check_phone(home)
                 && check_phone(mobile) && check_phone(tel) && check_country(country));
            string path;
            if (b)
            {
                path = "C:\\Users\\Use\\Desktop\\good\\" + name1 + name2 + ".txt";
            }
            else
                path = "C:\\Users\\Use\\Desktop\\bad\\" + name1 + name2 + ".txt";
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            BinaryWriter writer = new BinaryWriter(file);

            writer.Write("Teen's name: " + name1 + " " + name2 + "\r\n");
            writer.Write("Date of birth: " + birth[0] + "/" + birth[1] + "/" + birth[2] + "\r\n");
            writer.Write("Gender: " + gender + "\r\n");
            writer.Write("Address:\r\n" + street + "\r\n");
            writer.Write(locality + "\r\n");
            writer.Write(town + "\r\n");
            writer.Write(code + "\r\n");
            writer.Write(country + "\r\n");
            writer.Write("Home telephone: " + home + "\r\n");
            writer.Write("Mobile telephone:" + mobile + "\r\n");
            writer.Write("Email: " + mail + "\r\n");
            writer.Write("\r\n");

            writer.Write("Parent/Carer's name: " + name3 + " " + name4 + "\r\n");
            writer.Write("Parent/Carer's mobile telephone: " + tel + "\r\n");
            writer.Write("Parent/Carer's email: " + mail2 + "\r\n");

            writer.Write("\r\n");
            writer.Write("Social worker's name: " + swName1 + " " + swName2 + "\r\n");

            writer.Write("\r\n");
            writer.Write("Medical Conditions:\r\n" + med + "\r\n");
            writer.Write("Hospital(s) Attended:\r\n" + hospital + "\r\n");
            writer.Write("Special Requirements:\r\n" + spec + "\r\n");
            writer.Write("\r\n");

            writer.Write("How did you hear about us?\r\n");
            for (int i = 0; i < 8; ++i)
            {
                if (checks[i])
                {
                    switch (i)
                    {
                        case 1:
                            writer.Write(Choice1.Text + "\r\n");
                            break;
                        case 2:
                            writer.Write(Choice2.Text + "\r\n");
                            break;
                        case 3:
                            writer.Write(Choice3.Text + "\r\n");
                            break;
                        case 4:
                            writer.Write(Choice4.Text + "\r\n");
                            break;
                        case 5:
                            writer.Write(Choice5.Text + "\r\n");
                            break;
                        case 6:
                            writer.Write(Choice6.Text + "\r\n");
                            break;
                        case 7:
                            writer.Write(Choice7.Text + "\r\n");
                            break;
                        case 8:
                            writer.Write(Choice8.Text + ": " + textBox6.Text + "\r\n");
                            break;
                        default:
                            break;
                    }
                }
            }
            writer.Write("\r\n");


            writer.Write("Interests: " + richTextBox4.Text + "\r\n");


            writer.Close();
            file.Close();

        }
      
    }
}
