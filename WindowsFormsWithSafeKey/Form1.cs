using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsWithSafeKey
{
  
  public partial class Form1 : Form
  
  {
    private string key = "PRIVATE KEY";
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      textBox1.Text = key;
    }
  }
}
