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

namespace Vasilev10
{
    public enum AccountType { текущий, сберегательный }
    public partial class Form1 : Form
    {
        Song[] songs;
        BankAccount[] accounts;
        StreamReader streamReader;
        StreamWriter streamWriter;
        string[,] Users;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            IFormattableCheck();
            SongsAss();
        }

        private void SongsAss()
        {
            songs = new Song[6];
            string[] names = new string[6] { "Oxxxymiron", "Колос", "Markul", "Винтаж", "Денис Майданов", "Три дня дождя" };
            string[] values = new string[6] { "Колесо", "Бiблiотека", "10,000 Ночей", "Знак Водолея", "Пролетая над нами", "Весна" };
            songs[0] = new Song();
            songs[0].setName(values[0]);
            songs[0].setAuthor(names[0]);
            richSongs.Text = $"{songs[0].Title()}\n";
            for (int songId =1;songId<songs.Length;songId++)
            {
                songs[songId] = new Song();
                songs[songId].setName(values[songId]);
                songs[songId].setAuthor(names[songId]);
                songs[songId].setPrev(songs[songId - 1]);
                richSongs.Text += $"{songs[songId].Title()}\n";
            }
            songs[0].setPrev(songs[songs.Length-1]);

            richTextBox2.Text = $"{songs[0].Title()} == {songs[1].Title()} ? : {songs[0].Equals(songs[1])}";
            
        }

        private void IFormattableCheck()
        {
            int example = 0;
            double example2 = 0;
            richIFormattable.Text =  $"Object: 'streamReader', IsFormattable = '{OtherEx.IsFormattableIS(streamReader)}'\n";
            richIFormattable.Text += $"Object: 'streamWriter', IsFormattable = '{OtherEx.IsFormatableAS(streamWriter)}'\n";
            richIFormattable.Text += $"Object: 'int', IsFormattable = '{OtherEx.IsFormatableAS(example)}'\n";
            richIFormattable.Text += $"Object: 'double', IsFormattable = '{OtherEx.IsFormattableIS(example2)}'\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int count = (int)numericUpDown1.Value;
            accounts = new BankAccount[count];
            for (int acc = 0; acc < count; acc++)
            {
                accounts[acc] = new BankAccount();
                accounts[acc].ChangeBalance(random.Next(100, 400000));
                accounts[acc].ChangeType((AccountType)random.Next(0, 2));
                comboNumbers.Items.Add(accounts[acc].GetNumber());
                comboFrom.Items.Add(accounts[acc].GetNumber());
                comboTo.Items.Add(accounts[acc].GetNumber());
            }
            DrawBankAccount();
        }

        private void DrawBankAccount()
        {
            richTextBox1.Clear();
            foreach (BankAccount account in accounts)
            {
                richTextBox1.Text += $"Аккаунт: {account.GetNumber()} - <<{account.GetType()}>>. Баланс: {account.GetBalance()} (руб.)\n\n";
            }
        }

        private void buttTakePut_Click(object sender, EventArgs e)
        {
            if (accounts == null)
                return;
            foreach (BankAccount account in accounts)
            {
                if (account.GetNumber().ToString() == comboNumbers.SelectedItem.ToString())
                {
                    account.PutMoney((int)PutNum.Value);
                    bool successul = account.TryTakeMoney((int)TakeNum.Value);
                    if (!successul)
                        MessageBox.Show("Недостаточно средств", "Невозможно");
                    if (TypeCheckBox.Checked)
                    {
                        if (account.GetType() == AccountType.текущий)
                            account.ChangeType(AccountType.сберегательный);
                        else
                            account.ChangeType(AccountType.текущий);
                    }
                    DrawBankAccount();
                    return;
                }
            }
        }

        private void buttTransfer_Click(object sender, EventArgs e)
        {
            int numFrom = -1;
            int numTo = -1;
            for (int accNum = 0; accNum < accounts.Length; accNum++)
            {
                if (accounts[accNum].GetNumber().ToString() == comboFrom.SelectedItem.ToString())
                    numFrom = accNum;
                else if (accounts[accNum].GetNumber().ToString() == comboTo.SelectedItem.ToString())
                    numTo = accNum;
            }
            if (numFrom != -1 & numTo != -1)
            {
                bool successul = BankAccount.TryMoneyTransfer(ref accounts[numFrom], ref accounts[numTo], (int)numericMoney.Value);
                if (!successul)
                    MessageBox.Show("Ошибка перевода", "Неудача");
                else
                    DrawBankAccount();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxReverse.Text = OtherEx.StringReverse(textBoxReverse.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = fileFind();
            if (filename == null)
                MessageBox.Show("Такого файла не существует");
            else
            {
                streamReader = new StreamReader(filename);
                streamReader.Close();
                string TextIn = streamReader.ReadToEnd();
                string TextOut = OtherEx.TextUp(TextIn);
                richSave.Text = TextOut;
                richOpen.Text = TextIn;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string saveFileName = saveFileDialog1.FileName;
                StreamWriter streamWriter  = new StreamWriter(saveFileName);
                streamWriter.WriteLine(TextOut);
                streamWriter.Close();
            }
        }

        private string fileFind()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return null;
            string filename = openFileDialog1.FileName;
            return filename;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int user = 0; user<Users.Length/2; user++)
            {
                if (Users[user, 0] == comboUsers.SelectedItem.ToString())
                    textEmail.Text = Users[user, 1];
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string filename = fileFind();
            if (filename != null)
            {
                streamReader = new StreamReader(filename);
                string[] TextIn = streamReader.ReadToEnd().Split('\n');
                streamReader.Close();
                Users = new string[TextIn.Length,2];
                for (int user = 0; user < TextIn.Length; user++)
                {
                    Users[user,0] = OtherEx.SearchUser(TextIn[user]);
                    OtherEx.SearchEmail(ref TextIn[user]);
                    Users[user, 1] = TextIn[user];
                    comboUsers.Items.Add(Users[user, 0]);
                    richUsers.Text += $"Пользователь: {Users[user, 0]}, Email: {Users[user, 1]}";
                }

            }
        }
    }
}
