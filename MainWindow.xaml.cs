using System;
using System.Collections.Generic;
using System.Data.Entity;
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
namespace AiSRIPInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        AiSContext DB;
        // сохранение и дальнейшее обновление данных 
        //(Хотя, мне кажется, здесь всё должно быть сложнее, но я пока не знаю, как реализовать очередь пакетов, чтобы избежать конфликта между пользователями)
        private void save_n_update()
        {
            DB.SaveChanges();
            DB.StatusTypes.Load();
            DB.Companies.Load();
        }
        public MainWindow()
        {
            InitializeComponent();
            //Создаём новый контекст подключения
            DB = new AiSContext();
            // загружаем данные (вспомогательная таблица списка статусов заказов)
            DB.StatusTypes.Load();
            DB.Companies.Load(); 
            DB.Users.Load();
            //В первый грид - список компаний

            CompaniesGrid.ItemsSource = DB.Companies.Local.ToBindingList();
            comboBoxColum.ItemsSource = DB.StatusTypes.Local;
            //CompaniesGrid.ItemsSource = DB.StatusTypes.Local.ToBindingList();



            //загружаем во второй грид юзеров, соответствующих по айди первой компании
            //int FirstCompanyId = DB.Companies.Local.First().ID;
            //Бинд ивентов
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            save_n_update();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompaniesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < CompaniesGrid.SelectedItems.Count; i++)
                {
                    Company company = CompaniesGrid.SelectedItems[i] as Company;
                    
                    if (company != null)
                    {
                        foreach (var user in DB.Users.Where(u => u.CompanyId == company.ID))
                        {
                            DB.Users.Remove(user);
                        }
                        DB.Companies.Remove(company);
                    }
                }
            }
            else
                MessageBox.Show("Выберите хотя бы одну компанию!");
            save_n_update();
        }

        private void viewUserListButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompaniesGrid.SelectedItems.Count == 1)
            {
                UserList userList = new UserList(((Company)CompaniesGrid.SelectedItem).ID);
                userList.Show();
            }
            else
            {
                MessageBox.Show("Выберите одну компанию!");
            }
            
        }


        private void viewAllUserListButton_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
        }
    }
}
