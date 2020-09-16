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
using System.Windows.Shapes;

namespace AiSRIPInterface
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        AiSContext DB;
        int CompanyId_;
        //for all users
        public UserList()
        {
            InitializeComponent();
            CompanyId_ = -1;
            UsersGrid.CanUserAddRows = false;
            DB = new AiSContext();
            // загружаем данные (вспомогательная таблица списка статусов заказов)
            DB.Users.Load();
            DB.Companies.Load();
            UsersGrid.ItemsSource = DB.Users.Local.ToBindingList();
            comboBoxColum.ItemsSource = DB.Companies.Local;

            this.Closing += UserList_Closing;
        }

        private void UserList_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.SaveChanges();
        }



        //for user of selected company
        public UserList(int CompanyId)
        {
            
            InitializeComponent();
            UsersGrid.CanUserAddRows = false;
            CompanyId_ = CompanyId;
            DB = new AiSContext();
            // загружаем данные (вспомогательная таблица списка статусов заказов)
            DB.Users.Load();
            DB.Companies.Load();
            UsersGrid.ItemsSource = DB.Users.Local.Where(u => u.CompaniesID == CompanyId).ToList();
            comboBoxColum.ItemsSource = DB.Companies.Local;
        }

        private void addNewUserButton_Users_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            user.Login = "Введите логин";
            user.Name = "Введите имя";
            user.Password = "Введите пароль";
            user.CompaniesID = CompanyId_;
            //user.Companies = (Companies)DB.Companies.Local.Where(c => c.CompaniesID == CompanyId_);
            DB.Users.Add(user);
            DB.SaveChanges();
            DB.Users.Load();
            UsersGrid.ItemsSource = DB.Users.Local.Where(u => u.CompaniesID == CompanyId_).ToList();
        }

        private void updateButton_Users_Click(object sender, RoutedEventArgs e)
        {
            DB.SaveChanges();
            //DB.StatusTypes.Load();
            //DB.Companies.Load();
            //DB.Users.Load();
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
            if (CompanyId_ > -1)
            {
                UsersGrid.ItemsSource = DB.Users.Local.Where(u => u.CompaniesID == CompanyId_).ToList();
            }
        }
    }
}
