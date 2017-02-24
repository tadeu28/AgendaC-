using ExemploDAO.DB;
using ExemploDAO.DB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExemploDAO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = new Pessoa()
            {
                Nome = "Tadeu"
            };
            ConfigDB.Instance.PessoaRepository.Insert(pessoa);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var pessoa = (Pessoa)dataGrid.SelectedItem;

                var telefone = new Telefone()
                {
                    DDD = "32",
                    Numero = "9999",
                    Tipo = "CElular",
                    IdPessoa = pessoa.Id
                };
                ConfigDB.Instance.TelefoneRepository.Insert(telefone);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var pessoa = (Pessoa)dataGrid.SelectedItem;

                ConfigDB.Instance.TelefoneRepository.Select(pessoa);

                pessoa.Nome = "Tadeu Classe";

                ConfigDB.Instance.PessoaRepository.Update(pessoa);                
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGrid.SelectedItem != null)
            {
                var pessoa = (Pessoa)dataGrid.SelectedItem;

                dataGrid_Copy.ItemsSource = new ObservableCollection<Telefone>(pessoa.Telefones);
            }
        }

        private void button2_Copy_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pessoas = ConfigDB.Instance.PessoaRepository.Select();
            dataGrid.ItemsSource = new ObservableCollection<Pessoa>(pessoas);
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var pessoa = (Pessoa)dataGrid.SelectedItem;

                if (MessageBox.Show("Deseja Excluir " + pessoa.Nome + "?", "Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConfigDB.Instance.PessoaRepository.Delete("pessoa", pessoa.Id);
                    MessageBox.Show("Pessoa Excluída!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);

                    Window_Loaded(null, null);
                }
            }
        }

        private void button1_Copy1_Click(object sender, RoutedEventArgs e)
        {
            //var pessoa = (Pessoa)dataGrid.SelectedItem;

            if (dataGrid_Copy.SelectedItem != null)
            {
                var telefone = (Telefone)dataGrid_Copy.SelectedItem;

                if (MessageBox.Show("Deseja Excluir o Telfone " + telefone.Numero + "?", "Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConfigDB.Instance.TelefoneRepository.Delete("telefone", telefone.Id);
                    MessageBox.Show("Telefone Excluído!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    Window_Loaded(null, null);

                    var pessoas = (ObservableCollection<Pessoa>)dataGrid.ItemsSource;
                    var pessoa = pessoas.FirstOrDefault(f => f.Id == telefone.IdPessoa);

                    dataGrid_Copy.ItemsSource = new ObservableCollection<Telefone>(pessoa.Telefones);
                }
            }
        }

        private void button2_Copy2_Click(object sender, RoutedEventArgs e)
        {
            var telaPessoa = new AddEdtPessoa();

            var pessoa = telaPessoa.Execute();
            if(pessoa != null)
            {
                ConfigDB.Instance.PessoaRepository.Insert(pessoa);

                Window_Loaded(null, null);
            }
        }

        private void button2_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = (Pessoa)dataGrid.SelectedItem;
            if(pessoa != null)
            {
                var telaPessoa = new AddEdtPessoa();

                pessoa = telaPessoa.Execute(pessoa);
                if(pessoa != null)
                {
                    ConfigDB.Instance.PessoaRepository.Update(pessoa);

                    Window_Loaded(null, null);
                }
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Window_Loaded(null, null);

            if (textBox.Text != "")
            {
                var pessoas = (ObservableCollection<Pessoa>)dataGrid.ItemsSource;

                //Busca por nome exato
                //var pessoasFiltro = pessoas.Where(w => w.Nome == textBox.Text).ToList();

                //Busca por trecho
                //var pessoasFiltro = pessoas.Where(w => w.Nome.Contains(textBox.Text)).ToList();

                //Busca case insentive
                var pessoasFiltro = pessoas.Where(w => w.Nome.
                                                    ToLower().
                                                    Contains(textBox.Text.ToLower())).ToList();

                dataGrid.ItemsSource = new ObservableCollection<Pessoa>(pessoasFiltro);
            }
        }
    }
}
