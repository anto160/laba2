using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace laba1
{
    class Program
    {
        public static char otvet;
        public static string s;
        static void Main(string[] args)
        {
            string chislo = null;
            while (chislo != "4")
            {
                do
                {
                    Console.WriteLine("Меню");
                    Console.WriteLine(" 1 - Посмотреть все контакты\n" +
                        " 2 - Поиск\n" +
                        " 3 - Добавить контакт\n" +
                        " 4 - Выход\n");
                    chislo = Console.ReadLine();
                    switch (chislo)
                    {
                        case "1":
                            addressbook.ReadListFiles("1","0","0");
                            break;
                        case "2":
                            string n = null;
                            Console.WriteLine("Поиск по");
                            Console.WriteLine(" 1 - Имени\n" +
                                " 2 - Фамилии\n" +
                                " 3 - Имени и фамилии\n" +
                                " 4 - Телефону\n" +
                                " 5 - E-mail\n");
                            chislo = Console.ReadLine();
                            switch (chislo)
                            {
                                case "1":
                                    Console.WriteLine("Введите имя: ");
                                    n = Console.ReadLine();
                                    Console.WriteLine(String.Format("\nПоиск контактов...\n"));
                                    addressbook.ReadListFiles("2", chislo,n);
                                    break;
                                case "2":
                                    Console.WriteLine("Введите фамилию: ");
                                    n = Console.ReadLine();
                                    Console.WriteLine(String.Format("\nПоиск контактов...\n"));
                                    addressbook.ReadListFiles("2", chislo,n);
                                    break;
                                case "3":
                                    Console.WriteLine("Введите имя и фамилию: ");
                                    n = Console.ReadLine();
                                    Console.WriteLine(String.Format("\nПоиск контактов...\n"));
                                    addressbook.ReadListFiles("2", chislo,n);
                                    break;
                                case "4":
                                    Console.WriteLine("Введите телефон: ");
                                    n = Console.ReadLine();
                                    Console.WriteLine(String.Format("\nПоиск контактов...\n"));
                                    addressbook.ReadListFiles("2", chislo,n);
                                    break;
                                case "5":
                                    Console.WriteLine("Введите e-mail: ");
                                    n = Console.ReadLine();
                                    Console.WriteLine(String.Format("\nПоиск контактов...\n"));
                                    addressbook.ReadListFiles("2", chislo,n);
                                    break;
                            }
                            break;
                        case "3":
                            addressbook temp = new addressbook();
                            Console.WriteLine("Введите фамилию: ");
                            temp.Surname = Console.ReadLine();
                            Console.WriteLine("Введите имя: ");
                            temp.Name = Console.ReadLine();
                            Console.WriteLine("Введите отчество: ");
                            temp.fname = Console.ReadLine();
                            Console.WriteLine("Введите телефон: ");
                            temp.Phone = Console.ReadLine();
                            Console.WriteLine("Введите e-mail: ");
                            temp.Mail = Console.ReadLine();
                            Console.WriteLine("Введите nickname: ");
                            temp.Nickname = Console.ReadLine();
                            Console.WriteLine("Введите дату рождения в формате YYYY-MM-DD: ");
                            temp.Bday = Console.ReadLine();
                            Console.WriteLine("Введите комментарий к контакту: ");
                            temp.Note = Console.ReadLine();
                            temp.Writeaddressbook();
                            Console.WriteLine("\n Запись добавлена!");
                            break;
                        case "4": 
                            return;
                    }
                    do
                    {
                        Console.WriteLine("\nПродолжаем? y/n");
                        s = Console.ReadLine();
                        try
                        {
                            otvet = char.Parse(s);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка при вводе!!! ");
                        }
                    }
                    while (otvet != 'y' && otvet != 'n');
                    Console.Clear();
                } while (otvet == 'y');
                if (otvet == 'n')
                {
                    //Console.WriteLine("\n" + "До встречи!"); 
                    break;
                }
            }
            Console.ReadLine();
        }
    }
    class addressbook
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string fname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Nickname { get; set; }
        public string Note { get; set; }
        public string Bday { get; set; }
        public addressbook() { }
        public void Writeaddressbook()
        {
            string filename = this.Name;
            int i = 0;
            while (System.IO.File.Exists(@"Contacts\" + filename + ".vcf"))
            {
                i++;
                filename = this.Name + Convert.ToString(i);

            }
            using (StreamWriter sw = File.AppendText(@"Contacts\" + filename + ".vcf"))
            {
                sw.WriteLine("BEGIN:VCARD");
                sw.WriteLine("VERSION:3.0");
                sw.WriteLine("FN:" + this.Surname + " " + this.Name + " " +this.fname);
                sw.WriteLine("N:" + this.Surname + ";" + this.Name + ";" +this.fname + ";;;");
                sw.WriteLine("NICKNAME:"+this.Nickname);
                sw.WriteLine("BDAY:"+this.Bday);
                sw.WriteLine("TEL;TYPE=work, voice, pref:"+this.Phone);
                sw.WriteLine("EMAIL;TYPE=INTERNET:" + this.Mail);
                sw.WriteLine("MAILER:MAILER");
                sw.WriteLine("NOTE:"+this.Note);
                sw.WriteLine("END:VCARD");
            }
        }
        public static void WriteContact(int sch, string name, string surname, string phone, string mail)
        {
            Console.WriteLine(String.Format("\n#" + sch));
            Console.WriteLine(String.Format("Имя: " + name));
            Console.WriteLine(String.Format("Фамилия: " + surname));
            Console.WriteLine(String.Format("Телефон: " + phone));
            Console.WriteLine(String.Format("E-mail: " + mail));
        }

        public static void ReadListFiles(string comd_1, string comd_2, string str_find)
        {
            string name = null, surname = null, phone = null, mail = null;
            int int_cont=0, int_find = 0;
            List<string> files1 = Directory.GetFiles(@"Contacts\", "*.vcf").ToList<string>();
            foreach (var s in files1)
            {
                int_cont++;
                //Console.WriteLine(String.Format(" " + s));
                StreamReader sr = File.OpenText(s);

                string st = sr.ReadLine();
                if (st.Contains("BEGIN:VCARD")!=true)
                    continue;
                st = sr.ReadLine();
                if (st.Contains("VERSION:2.0") != true && st.Contains("VERSION:2.1") != true && st.Contains("VERSION:3.0") != true)
                    continue;
                while (true)
                {
                    st = sr.ReadLine();
                    if (st.Contains("END:VCARD"))
                        break;
                    /////////N:
                    if ((st.Remove(2) == "N;") || (st.Remove(2) == "N:"))
                    {
                        int ij = 0;
                        string N = st.Substring(st.IndexOf(":") + 1);
                        string[] words = N.Split(new char[] { ';' });
                        foreach (string ss in words)
                        {
                            ij++;
                            if (ij == 1)
                            {
                                surname = ss;
                            }
                            if (ij == 2)
                            {
                                name = ss;
                            }
                          //  Console.WriteLine(ss);
                        }
                        //Console.WriteLine(String.Format(" " + st.Remove(2)));
                    }
                    //\\\\\\\\N:
                    /////////EMAIL:
                    if ((st.Remove(6) == "EMAIL;") || (st.Remove(6) == "EMAIL:"))
                    {
                        int ij = 0;
                        string N = st.Substring(st.IndexOf(":") + 1);
                        string[] words = N.Split(new char[] { ';' });
                        foreach (string ss in words)
                        {
                            ij++;
                            if (ij == 1)
                            {
                                mail = ss;
                            }
                          //  Console.WriteLine(ss);
                        }
                        //Console.WriteLine(String.Format(" " + st.Remove(6)));
                    }
                    //\\\\\\\\EMAIL:
                    /////////TEL:
                    if ((st.Remove(4) == "TEL;") || (st.Remove(4) == "TEL:"))
                    {
                        int ij = 0;
                        string N = st.Substring(st.IndexOf(":") + 1);
                        string[] words = N.Split(new char[] { ';' });
                        foreach (string ss in words)
                        {
                            ij++;
                            if (ij == 1)
                            {
                                phone= ss;
                            }
                          //  Console.WriteLine(ss);
                        }
                        //Console.WriteLine(String.Format(" " + st.Remove(4)));
                    }
                    //\\\\\\\\EMAIL:




                }
                if (comd_1 == "1")
                {
                    addressbook.WriteContact(int_cont, name, surname, phone, mail);
                }
                if (comd_1 == "2")
                {

                    switch (comd_2)
                    {
                        case "1":
                            if (name.Contains(str_find))
                            {
                                int_find++;
                                addressbook.WriteContact(int_find, name, surname, phone, mail);
                            }
                            break;
                        case "2":
                            if (surname.Contains(str_find))
                            {
                                int_find++;
                                addressbook.WriteContact(int_find, name, surname, phone, mail);
                            }
                            break;
                        case "3":
                            if ((String.Format(name + " " + surname).Contains(str_find)) || (String.Format(surname + " " + name).Contains(str_find)))
                            {
                                int_find++;
                                addressbook.WriteContact(int_find, name, surname, phone, mail);
                            }
                            break;
                        case "4":
                            if (phone.Contains(str_find))
                            {
                                int_find++;
                                addressbook.WriteContact(int_find, name, surname, phone, mail);
                            }
                            break;
                        case "5":
                            if (mail.Contains(str_find))
                            {
                                int_find++;
                                addressbook.WriteContact(int_find, name, surname, phone, mail);
                            }
                            break;
                    }
                }
                
                sr.Close();
                continue;
            }
        }

        public override string ToString()
        {
            return String.Format("{0,-15} {1,-15} {2,-15} {3,-15}", Name, Surname, Phone, Mail);
        }
    }

}