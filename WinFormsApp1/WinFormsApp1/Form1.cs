namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("Bot: Welcome to my chat app");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text.ToLower();
            if (string.IsNullOrEmpty(message)) {
                MessageBox.Show("please a text");
            }

            listBox1.Items.Add("You: "+textBox1.Text);
            if (message.Contains("hello"))
            {
                listBox1.Items.Add("Bot: hello to you too");
            }
            else
            {
                listBox1.Items.Add("Bot: I do not understand you");

            }
            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
