using ExemploDAO.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExemploDAO
{
    /// <summary>
    /// Interaction logic for AddEdtPessoa.xaml
    /// </summary>
    public partial class AddEdtPessoa : Window
    {
        private Pessoa pessoa;

        public AddEdtPessoa()
        {
            InitializeComponent();
        }

        public Pessoa Execute()
        {
            this.pessoa = new Pessoa();

            this.ShowDialog();

            return this.pessoa;
        }

        public Pessoa Execute(Pessoa pessoa)
        {
            this.pessoa = pessoa;

            textBox.Text = this.pessoa.Nome;

            this.ShowDialog();

            return this.pessoa;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.pessoa = null;
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.pessoa.Nome = textBox.Text;
            this.Close();
        }
    }
}
