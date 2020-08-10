using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

namespace XMLarhiver
{
    public partial class Form1 : Form
    {
        bool fastwork;
        public Form1(bool fast)
        {
            fastwork = fast;
            InitializeComponent();
        }
        FastZip zip = new FastZip(); //Иницилизация обьекта - архиватора.
        bool megacheker = false;
        string dat="XXX";
        int stroka = 0;
        private void trial()
        {
            if (dat == "Аваыпе3423")
            {
               MessageBox.Show("Пробный периуд завершен");
               System.IO.File.WriteAllText("TrialEnd.dat", "LOokfodfkgodfjdkgnrlj4938u390tu509-c9780jv4v-v7uc-i-2kuv9kcl23l9-i-ckukqlu32u421120u0`0i304345lc0485vu4-");
               System.IO.File.WriteAllText("TrialEnd.bat", "timeout /t 3 " + Environment.NewLine + "del /q " + Application.ExecutablePath+ Environment.NewLine+ "MOVE /Y TrialEnd.dat "+ System.IO.Path.GetFileName(Application.ExecutablePath) + Environment.NewLine + "MOVE /Y ICSharpCode.SharpZipLib.dll " + System.IO.Path.GetFileName(Application.ExecutablePath) + Environment.NewLine + "MOVE /Y masks.txt " + System.IO.Path.GetFileName(Application.ExecutablePath) + Environment.NewLine + "MOVE /Y XMLarhiver.exe.config " + System.IO.Path.GetFileName(Application.ExecutablePath) + Environment.NewLine + "MOVE /Y TrialEnd.bat " + System.IO.Path.GetFileName(Application.ExecutablePath));
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "TrialEnd.bat",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process.Start(startInfo);
                Close();
            }

        }

        private void GetNetworkTime()
        {
            try
            {
                //default Windows time server
                const string ntpServer = "time.windows.com";

                // NTP message size - 16 bytes of the digest (RFC 2030)
                var ntpData = new byte[48];

                //Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                var addresses = Dns.GetHostEntry(ntpServer).AddressList;

                //The UDP port number assigned to NTP is 123
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                //NTP uses UDP


                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {

                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();
                }

                //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                //departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;

                //Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                //Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                //Convert From big-endian to little-endian
                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                //**UTC** time
                var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
                dat = networkDateTime.Date.ToString();
                //trial();
            }
            catch { MessageBox.Show("Необходимо подключение к сети интернет!"); Close(); }
            
        }

        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetNetworkTime();
            //Загрузка настроек программы.  
            //trial();
            if (System.IO.File.Exists("config_in.inf"))
                textBox1.Text = System.IO.File.ReadAllText("config_in.inf");
            if (System.IO.File.Exists("config_out.inf"))
                textBox2.Text = System.IO.File.ReadAllText("config_out.inf");
            if (fastwork)
            {
                checkBox1.Checked = true;
                перезаписатьСуществующиеПакетыToolStripMenuItem.Checked = true;
                button3.PerformClick();
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {//Изменене пути к XML.
            //trial();
            if (textBox1.Text != "")
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            folderBrowserDialog1.SelectedPath = "";
            System.IO.File.WriteAllText("config_in.inf", textBox1.Text);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {//Изменене пути к папке выгрузки.
            //trial();
            if (textBox2.Text != "") 
            folderBrowserDialog1.SelectedPath = textBox2.Text;
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;
            folderBrowserDialog1.SelectedPath = "";
            System.IO.File.WriteAllText("config_out.inf", textBox2.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(listBox1.Items[listBox1.SelectedIndex]));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Авторские права.
            MessageBox.Show("Автор Михалко А.В." + Environment.NewLine + "Email: mihandr1@mail.ru" + Environment.NewLine
                + "Телефон: +79244306893" + Environment.NewLine + "VK: https://vk.com/erehon99");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void перезаписатьСуществующиеПакетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            перезаписатьСуществующиеПакетыToolStripMenuItem.Checked = true;
        }


        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        public string Xmlcheker(string xmlpatch)
        {
            XmlSchemaInference infer = new XmlSchemaInference();
            XmlSchemaSet schemaSet = infer.InferSchema(new XmlTextReader(xmlpatch));
            XDocument xDocument = XDocument.Load(xmlpatch);

            //XmlWriter w = XmlWriter.Create(@"C:\TESTS\shem.xsd"); Запись схемы в файл.
            //foreach (XmlSchema schema in schemaSet.Schemas())
            //{
            //  schema.Write(w); 
            //}
            //w.Close();

          //  XmlSchemaSet schemaSet = new XmlSchemaSet(); Загрузка схемы с файла
           // schemaSet.Add(null, @"C:\TESTS\shem.xsd");

            string outp;
            try
            {
                xDocument.Validate(schemaSet, null);
                outp="Проверка достоверности документа завершена успешно";
            }
            catch (XmlSchemaValidationException ex)
            {
                outp = "Произошло исключение:   {0}"+ ex.Message+Environment.NewLine+"Документ не прошел проверку достоверности.";
            }
            return outp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName="";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName!="")
            MessageBox.Show(Xmlcheker(openFileDialog1.FileName));
        }

        private void этаТестоваяВерсияДействительнаДо21102017ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Только для разраба!");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //trial();
            //Создание нового потока.
            if (System.IO.Directory.Exists(textBox1.Text) & System.IO.Directory.Exists(textBox2.Text))
            {
                //Исполняемый код нового потока

                //Основной код программы.
                //Создание локальных переменных.
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                }));
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    listBox1.Items.Add("Создание локальных переменных");
                }));
                        bool error = false;
                        string cachedir = "cache/";
                        string[] mask = System.IO.File.ReadAllLines("masks.txt");
                        string group = "cache/defult";
                        bool teg = false;
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    listBox1.Items.Add("Удаление старого Кэша");
                }));
                if (System.IO.Directory.Exists(cachedir))
                System.IO.Directory.Delete(cachedir, true); //Удаление кэша
                        //            
                        System.IO.Directory.CreateDirectory(cachedir); //Создание папки кэша
                        if (textBox1.Text != "")
                    //Формирование кэша пакетов.
                    this.Invoke(new System.Threading.ThreadStart(delegate
                    {
                        listBox1.Items.Add("Формирование кэша пакетов");
                    }));
                        int prbr = 400 / mask.Length;
                        foreach (string curmask in mask)
                        {
                            //if (error) break;
                            if (teg)
                            {
                                group = cachedir + curmask; System.IO.Directory.CreateDirectory(group); teg = false;
                        this.Invoke(new System.Threading.ThreadStart(delegate
                        {
                            listBox2.Items.Add(curmask);
                        }));
                            }
                            else
                        if (curmask == "<group>") teg = true;
                            else
                            {
                                foreach (string curfile in System.IO.Directory.GetFiles(textBox1.Text, curmask, System.IO.SearchOption.TopDirectoryOnly))
                                {
                                    //if (error) break;
                                    string tfn = group + "/" + System.IO.Path.GetFileName(curfile);
                                try
                                {
                                if (checkBox1.Checked == true) System.IO.File.Move(curfile, tfn);
                                else
                                    System.IO.File.Copy(curfile, tfn, true);
                                }
                                catch
                            {
                                System.IO.File.Copy(curfile, tfn, true);
                                this.Invoke(new System.Threading.ThreadStart(delegate
                                {
                                    listBox1.Items.Add("Ошибка перемещения файла! Вместо этого, он был скопирован.");
                                }));
                            }

                            string x; 
                                    if (curmask == "PM*61.xml") //Проверка файла PM на наличае нужных тегов.
                                    {
                                x = System.IO.File.ReadAllText(tfn);
                                if (x.Contains("<VIZOV>"))
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("Вызовы скорой помощи найдены в отчете. (ТЕГ <VIZOV>)");
                                    }));
                                }
                                else
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("ВНИМАНИЕ!!! Вызовы скорой помощи не найдены в отчете! (ТЕГ <VIZOV>)");
                                    }));
                                    error = true;
                                }

                                if (x.Contains("<RL>"))
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("Использования лекарств найдены в отчете. (ТЕГ <RL>)");
                                    }));
                                }
                                else
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("ВНИМАНИЕ!!! Использования лекарств не найдены в отчете! (ТЕГ <RL>)");
                                    }));
                                    error = true;
                                }
                                    }
                            if (curmask == "HM*61.xml") //Проверка файла HM на наличае нужных тегов.
                            {
                                x = System.IO.File.ReadAllText(tfn);
                                if (x.Contains("<N_KSG>51</N_KSG>"))
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("Внимание!!! В отчете найден 51 КСГ!!!!!!");
                                    }));
                                    error = true;
                                }
                                else
                                {
                                    this.Invoke(new System.Threading.ThreadStart(delegate
                                    {
                                        listBox1.Items.Add("Все хорошо, 51 КСГ отсутствует в отчете.");
                                    }));
                                }
                            }
                            string xmlchrez = "Проверка достоверности документа завершена успешно";// Xmlcheker(curfile);
                            if (xmlchrez == "Проверка достоверности документа завершена успешно")
                            {
                                this.Invoke(new System.Threading.ThreadStart(delegate
                                {
                                    listBox1.Items.Add("Целостность " +System.IO.Path.GetFileName(curfile)+ " не нарушена.");
                                }));
                            }
                            else
                            {
                                this.Invoke(new System.Threading.ThreadStart(delegate
                                {
                                    listBox1.Items.Add("Целостность " + System.IO.Path.GetFileName(curfile) + " нарушена!");
                                }));
                                error = true;
                            }
                                }
                            }
                        }
                        //

                        //Формирование самих пакетов.
                        int er = 0;
                        prbr = 400 / listBox2.Items.Count;
                        foreach (string curcat in listBox2.Items)
                        {
                            //if (error) break;
                    this.Invoke(new System.Threading.ThreadStart(delegate
                    {
                        listBox1.Items.Add("Проверка целостности кэша пакета " + curcat);
                    }));

                            if (System.IO.Directory.GetFiles(cachedir + curcat).Length == 3) //Проверка целостности кэша.
                            {
                        this.Invoke(new System.Threading.ThreadStart(delegate
                        {
                            listBox1.Items.Add("Формирование имени пакета" + curcat);
                        }));
                                //Формирование имени пакета
                                string zipname = ""; //Переменная имени пакета.
                                                     //Тип пакета.
                                char[] chcat;
                                chcat = curcat.ToCharArray();
                                foreach (char simvl in chcat)
                                {
                                    if (simvl != ' ')
                                        zipname = zipname + Convert.ToString(simvl);
                                    else break;
                                }
                                //
                                //Код пакета.
                                string[] dropfnm = System.IO.Directory.GetFiles(cachedir + curcat + "/", zipname + "m*.xml");
                                string dropfn = dropfnm[0];
                                char[] cchcat;
                                cchcat = dropfn.ToCharArray();
                                bool chek = false;
                                foreach (char simvl in cchcat)
                                {
                                    if (char.IsDigit(simvl))
                                    {
                                        zipname = zipname + Convert.ToString(simvl);
                                        chek = true;
                                    }
                                    else if (chek == true) break;
                                }
                                //
                                //Год, месяц пакета.
                                chek = false;
                                int colsimv = 0;
                                foreach (char simvl in cchcat)
                                {
                                    if (chek & colsimv < 4 & colsimv != 0)
                                    {
                                        zipname = zipname + Convert.ToString(simvl);
                                        colsimv++;
                                    }
                                    if (colsimv == 4) break;
                                    if (chek & colsimv == 0) { colsimv++; }
                                    if (simvl == '_') { chek = true; }
                                }
                                //
                                //Отношение пакета
                                if (curcat[curcat.Length - 1] == 'P')
                                    zipname = zipname + "01";
                                else
                                    zipname = zipname + "02";

                                zipname = zipname + ".zip";
                        //
                        //
                        if (!перезаписатьСуществующиеПакетыToolStripMenuItem.Checked & System.IO.File.Exists(textBox2.Text + "/" + zipname))
                        {
                            this.Invoke(new System.Threading.ThreadStart(delegate
                            {
                                listBox1.Items.Add("Пакет уже существует " + curcat);
                            }));
                        }
                        else
                        {
                            this.Invoke(new System.Threading.ThreadStart(delegate
                            {
                                listBox1.Items.Add("Сжатие кэша в пакет " + curcat);
                            }));
                            zip.CreateZip(textBox2.Text + "/" + zipname, cachedir + curcat, true, null); //Сжатие кэша в пакет.
                        }

                            }
                            //Сообщение при нарушении целостности пакета.
                            else
                            {
                        this.Invoke(new System.Threading.ThreadStart(delegate
                        {
                            listBox1.Items.Add("Нарушена целостность кэша пакета " + curcat);
                        }));
                                er++;
                            }
                            //
                            //

                        }
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    listBox1.Items.Add("Удаление Кэша");
                }));

                        System.IO.Directory.Delete(cachedir, true); //Удаление кэша
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    listBox1.Items.Add("Формирование пакетов завершено");
                    progressBar1.Style = ProgressBarStyle.Blocks;
                }));
                if (error) { MessageBox.Show("Ошибка! Критическое нарушение целостности XML! Формирование пакетов было прервано. Исправте ошибки и попробуйте снова. Подробнее см. в логах.", "Сбой формирования пакетов", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                else if (fastwork) Close();
                if (er > 0) MessageBox.Show("Некоторые пакеты не были сформированы. Создано " + Convert.ToString(listBox2.Items.Count - er) + " из " + Convert.ToString(listBox2.Items.Count));
                        if (checkBox2.Checked == true)
                            System.Diagnostics.Process.Start("explorer", textBox2.Text); //Открытие каталога с сформированными пакетами.
            }
            else MessageBox.Show("Для формирования пакетов необходимо указать путь к XML файлам и папку для выгрузки.");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {

        }
    }
}
