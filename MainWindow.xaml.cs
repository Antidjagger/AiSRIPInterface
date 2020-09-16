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
        int SelectedCompany;
        // сохранение и дальнейшее обновление данных 
        //(Хотя, мне кажется, здесь всё должно быть сложнее, но я пока не знаю, как реализовать очередь пакетов, чтобы избежать конфликта между пользователями)
        private void save_n_update()
        {

            //if (DB.Users.Where(u => u.CompanyId == ((Company)CompaniesGrid.SelectedItem).ID).Count() != UsersGrid.Items.Count)
            //{
            //    int c = UsersGrid.Items.Count - DB.Users.Where(u => u.CompanyId == ((Company)CompaniesGrid.SelectedItem).ID).Count();
            //    for (int i = c; i < UsersGrid.Items.Count-1; i++)
            //    {
            //        Users user = (Users)UsersGrid.Items[i];
            //        user.CompanyId = SelectedCompany;
            //        DB.Users.Add(user);
            //    }

            //}
            DB.SaveChanges();
            DB.StatusTypes.Load();
            DB.Companies.Load();

            DB.Users.Load();
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
            //CompaniesGrid.ItemsSource = DB.StatusTypes.Local.ToBindingList();



            //загружаем во второй грид юзеров, соответствующих по айди первой компании
            int FirstCompanyId = DB.Companies.Local.First().ID;
            SelectedCompany = FirstCompanyId;
            UsersGrid.ItemsSource = DB.Users.Local.Where(u => u.CompanyId == FirstCompanyId).ToList();
            


            //Бинд ивентов
            this.Closing += MainWindow_Closing;
            this.CompaniesGrid.SelectionChanged += CompaniesGrid_SelectionChanged;
        }



        //При изменении выбранного в первом гриде (компании) - меняется привязка данных списка пользователей
        private void CompaniesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompaniesGrid.SelectedIndex > -1)
            {
                if (CompaniesGrid.SelectedIndex < DB.Companies.Count())
                {
                    int id = DB.Companies.Local[CompaniesGrid.SelectedIndex].ID;
                    SelectedCompany = id;
                    UsersGrid.ItemsSource = DB.Users.Where(u => u.CompanyId == id).ToList();
                }
                else
                    UsersGrid.ItemsSource = null;
                DB.SaveChanges();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            save_n_update();
        }
        private void updateButton_Users_Click(object sender, RoutedEventArgs e)
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
            save_n_update();
        }
        private void deleteButton_Users_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UsersGrid.SelectedItems.Count; i++)
                {
                    Users user = UsersGrid.SelectedItems[i] as Users;
                    if (user != null)
                    {
                        DB.Users.Remove(user);
                    }
                }
            }
            DB.SaveChanges();

            DB.Users.Load();
            var id = ((Users)UsersGrid.Items[0]).CompanyId;
            UsersGrid.ItemsSource = DB.Users.Where(u => u.CompanyId == id).ToList();
        }

    }
}
