using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
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

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private string fileName = "data.db";
        private List<Product> list = new List<Product>();
        private IReaderWriter rw = new FileReadWrite();

        public MainWindow()
        {
            InitializeComponent();

            string data = rw.Reader(fileName);

            if (!string.IsNullOrEmpty(data))
            {
                string[] lines = data.Split('\n');
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        Product p = JsonConvert.DeserializeObject<Product>(line);
                        ServicesGrid.Items.Add(p);
                        list.Add(p);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)
            {
                AddProductToList();
            }
            else
            {
                if (!CheckLoginService())
                {
                    AddProductToList();
                }
                else
                {
                    MessageBox.Show("Сервис и логин уже существуют.");
                }
            }
        }

        private void AddProductToList()
        {
            string service = serviceTextBox.Text;
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            Product newProduct = new Product(service, login, password);
            list.Add(newProduct);
            ServicesGrid.Items.Add(newProduct);
        }

        private bool CheckLoginService()
        {
            foreach (Product product in list)
            {
                if (product.Service == serviceTextBox.Text && product.Login == loginTextBox.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            UpdateFileData();
        }

        private void UpdateFileData()
        {
            string gridLines = "";
            foreach (Product p in ServicesGrid.Items)
            {
                gridLines += JsonConvert.SerializeObject(p) + "\n";
            }

            if (rw.Writer(gridLines, fileName))
            {
                MessageBox.Show("Данные успешно сохранены.");
            }
            else
            {
                MessageBox.Show("Не удалось сохранить данные.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem != null)
            {
                Product selectedProduct = (Product)ServicesGrid.SelectedItem;
                list.Remove(selectedProduct);
                ServicesGrid.Items.Remove(selectedProduct);
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }
    }
}