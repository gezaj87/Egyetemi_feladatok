using Newtonsoft.Json;
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
using System.Diagnostics;
using System.Collections;

namespace XU3R7F_kliens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private RestApi restApi = new RestApi();
        private User user = new User();

        List<Button> activeButtons = new List<Button>();
        List<Border> activePanels = new List<Border>();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ObservableCollection<string> departments = new ObservableCollection<string>();


        public MainWindow()
        {
            InitializeComponent();

            GetDepartmentsFromServer();

            ListView1.ItemsSource = employees;
            InputSelectDepartments.ItemsSource = departments;
            EditSelectDepartments.ItemsSource = departments;
        }

        private void CloseComponents()
        {
            foreach (Button button in activeButtons)
            {
                if (button.Name == "LoginMenuButton" && user.Token != null) button.Background = new SolidColorBrush(Colors.GreenYellow);
                else if (button.Name == "LoginMenuButton" && user.Token == null) button.Background = new SolidColorBrush(Colors.Yellow);
                else button.Background = new SolidColorBrush(Colors.WhiteSmoke);

                button.FontWeight = FontWeights.Regular;
            }

            foreach (Border border in activePanels)
            {
                border.Visibility = Visibility.Collapsed;
            }

            activeButtons.Clear(); activePanels.Clear();

        }
        private void OpenComponent(Button button = null, Border border = null)
        {
            if (button != null)
            {
                button.Background = new SolidColorBrush(Colors.Beige);
                button.FontWeight = FontWeights.Bold;
                activeButtons.Add(button);
            }

            if (border != null)
            {
                border.Visibility = Visibility.Visible;
                activePanels.Add(border);
            }
        }
     
        private void GetDepartmentsFromServer()
        {
            string departmentsJSON = null;
            try
            {
                departmentsJSON = restApi.Get("departments");
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }

            List<Dictionary<string, string>> departmentsFromServer = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(departmentsJSON);

            departments.Clear();
            foreach (Dictionary<string, string> item in departmentsFromServer)
            {
                departments.Add(item["department"]);
            }
            //InputSelectDepartments.Items.Refresh();
        }
        private void GetEmployeesFromServer()
        {
            string resultJSON = null;
            try
            {
                resultJSON = restApi.Get("");
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }

            if (resultJSON.StartsWith("{\"error\":"))
            {
                Dictionary<string, string> response = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJSON);
                MessageBox.Show(response["error"]);
                return;
            }

            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(resultJSON);

            employees.Clear();
            foreach (Employee emp in list)
            {
                employees.Add(emp);
            }

            //ListView1.Items.Refresh();
        }
        

        private void LoginMenu_Click(object sender, RoutedEventArgs e)
        {
            Border border = null;
            if (!string.IsNullOrEmpty(user.Token)) border = LogoutPanel;
            else border = LoginPanel;

            if (border.Visibility == Visibility.Collapsed)
            {
                CloseComponents();
                OpenComponent(LoginMenuButton, border);

            }
            else
            {
                CloseComponents();
            }
        }
        private void LoginPost_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
              {"email", InputEmail.Text},
              {"password", InputPassword.Password}
            };

            string resultJSON = null;
            try
            {
                resultJSON = restApi.Post("login", payload);
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }


            Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJSON);
            
            
            if (result.ContainsKey("ok"))
            {
                user.Token = result["token"];
                user.Name = result["name"];

                LoginMenuButton.Background = new SolidColorBrush(Colors.GreenYellow);
                LoginMenuButton.Content = "Admin";
                AddMenuButton.Visibility = Visibility.Visible;
                EditMenuButton.Visibility= Visibility.Visible;
                DeleteMenuButton.Visibility= Visibility.Visible;
                MessageBox.Show(result["ok"]);

                InputEmail.Text = "";
                InputPassword.Password = "";

                CloseComponents();
                OpenComponent(LoginMenuButton, LogoutPanel);
                Welcome.Content = $"Welcome {user.Name}!";

            }
            
            if (result.ContainsKey("error"))
            {
                MessageBox.Show(result["error"]);
            }

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            user.Logout();
            LoginMenuButton.Content = "Login";
            AddMenuButton.Visibility = Visibility.Collapsed;
            EditMenuButton.Visibility = Visibility.Collapsed;
            DeleteMenuButton.Visibility = Visibility.Collapsed;
            MessageBox.Show("You have successfully been logged out!");
            CloseComponents();
            OpenComponent(LoginMenuButton, LoginPanel);
            
        }

        private void GetAllEmployees(object sender, RoutedEventArgs e)
        {
            GetEmployeesFromServer();
        }

        private void SearchByIdButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.SearchByIdPanel.Visibility == Visibility.Collapsed)
            {
                CloseComponents();
                OpenComponent(this.SearchByIdButton, this.SearchByIdPanel);
            }
            else CloseComponents();
        }
        private void SearchByIdSubmit_Click(object sender, RoutedEventArgs e)
        {
            string id = SearchIdInput.Text;

            string resultJSON = null;
            try
            {
                resultJSON = restApi.Get($"id/{id}");
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }


            Console.WriteLine(resultJSON);

            if (resultJSON.StartsWith("{\"error\":"))
            {
                Dictionary<string, string> response = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJSON);

                MessageBox.Show(response["error"]);
                return;
            }

            Employee emp = JsonConvert.DeserializeObject<Employee>(resultJSON);
            employees.Clear();
            employees.Add(emp);
    
            SearchIdInput.Text = "";
        }

        
        private void AddMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddPanel.Visibility == Visibility.Collapsed)
            {
                CloseComponents();
                OpenComponent(AddMenuButton, AddPanel);
            }
            else
            {
                CloseComponents();
            }
        }
        private void AddNewDepartment_Click(object sender, RoutedEventArgs e)
        {
            bool isNewDepartmentChecked = (bool)CheckBoxNewDepartment.IsChecked;
            
            if (isNewDepartmentChecked)
            {
                InputSelectDepartments.Visibility = Visibility.Collapsed;
                InputTextDepartment.Visibility = Visibility.Visible;
                PanelAddSalery.Visibility = Visibility.Visible;
            }
            else
            {
                InputSelectDepartments.Visibility = Visibility.Visible;
                InputTextDepartment.Visibility = Visibility.Collapsed;
                PanelAddSalery.Visibility = Visibility.Collapsed;
            }
        }
        private void GrandAdminPriv_Click(object sender, RoutedEventArgs e)
        {
            bool isItChecked = (bool)CheckBoxGrantAdminPriv.IsChecked;

            if (isItChecked)
            {
                PanelAddPass1.Visibility = Visibility.Visible;
                PanelAddPass2.Visibility = Visibility.Visible;
            }
            else
            {
                PanelAddPass1.Visibility = Visibility.Collapsed;
                PanelAddPass2.Visibility = Visibility.Collapsed;
            }
        }
        private void AddPost_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                {"token", user.Token},
                {"name", AddNameInput.Text},
                {"email", AddEmailInput.Text},
                {"isNewDepartment", (bool)CheckBoxNewDepartment.IsChecked? "1" : "0"},
                {"department", (bool)CheckBoxNewDepartment.IsChecked ? InputTextDepartment.Text : InputSelectDepartments.Text },
                {"salary", InputAddSalary.Text },
                {"admin", (bool)CheckBoxGrantAdminPriv.IsChecked? "1" : "0"},
                {"password1", (bool)CheckBoxGrantAdminPriv.IsChecked? AddInputPass1.Password : "0"},
                {"password2", (bool)CheckBoxGrantAdminPriv.IsChecked? AddInputPass2.Password : "0"},
            };

            string response = null;
            try
            {
                response = restApi.Post("add", payload);
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }

            Console.WriteLine(response);

            Dictionary<string, string> jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            if (jsonResponse.ContainsKey("error")) MessageBox.Show(jsonResponse["error"]);

            if (jsonResponse.ContainsKey("ok"))
            {
                MessageBox.Show(jsonResponse["ok"]);

                AddNameInput.Clear();
                AddEmailInput.Clear();
                Helper.AddInputCleaner(AddPanel);

                InputSelectDepartments.Visibility = Visibility.Visible;
                InputTextDepartment.Visibility = Visibility.Collapsed;
                PanelAddSalery.Visibility = Visibility.Collapsed;

                PanelAddPass1.Visibility = Visibility.Collapsed;
                PanelAddPass2.Visibility = Visibility.Collapsed;

                GetDepartmentsFromServer();
                GetEmployeesFromServer();
            }

        }


        // TextChangedEventHandler delegate method.
        private void EditListener(object sender, TextChangedEventArgs args)
        {
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i] == HiddenEditDepartment.Text)
                {
                    EditSelectDepartments.SelectedIndex = i;
                    break;
                }
            }

            if (HiddenEditIsAdmin.Text == "True")
            {
                EditAdminCheckbox.IsChecked = true;
                EditNewPassCheckPanel.Visibility = Visibility.Visible;
            }
            if (HiddenEditIsAdmin.Text == "False")
            {
                EditAdminCheckbox.IsChecked = false;
                EditNewPassCheckPanel.Visibility = Visibility.Hidden;
            }

            EditInputClear();

        } 
        private void EditInputClear()
        {
            EditNewPassPanel1.Visibility = Visibility.Collapsed;
            EditNewPassPanel2.Visibility = Visibility.Collapsed;
            EditNewPassCheck.IsChecked = false;
            EditNewDepartmentCheck.IsChecked = false;
            EditNewDepartmentInput.Visibility = Visibility.Collapsed;
            EditSelectDepartments.Visibility = Visibility.Visible;
            EditPasswdPanel1.Visibility = Visibility.Collapsed;
            EditPasswdPanel2.Visibility = Visibility.Collapsed;
            EditSalaryPanel.Visibility = Visibility.Collapsed;

        }
        private void EditMenuButton_Click(object sender, RoutedEventArgs e)
        {
            EditInputClear();

            if (EditPanel.Visibility == Visibility.Collapsed)
            {
                CloseComponents();
                OpenComponent(EditMenuButton, EditPanel);
            }
            else
            {
                CloseComponents();
            }
            
        }
        private void EditNewDepartment(object sender, RoutedEventArgs e)
        {
            if ((bool)(sender as CheckBox).IsChecked)
            {
                EditSelectDepartments.Visibility = Visibility.Collapsed;
                EditNewDepartmentInput.Visibility = Visibility.Visible;
                EditSalaryPanel.Visibility = Visibility.Visible;
            }
            else
            {
                EditSelectDepartments.Visibility = Visibility.Visible;
                EditNewDepartmentInput.Visibility = Visibility.Collapsed;
                EditSalaryPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void EditGiveAdminPrivileges_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(sender as CheckBox).IsChecked && !string.IsNullOrEmpty(HiddenEditIsAdmin.Text) &&!bool.Parse(HiddenEditIsAdmin.Text))
            {
                EditPasswdPanel1.Visibility = Visibility.Visible;
                EditPasswdPanel2.Visibility = Visibility.Visible;
            }
            else
            {
                EditPasswdPanel1.Visibility = Visibility.Collapsed;
                EditPasswdPanel2.Visibility = Visibility.Collapsed;
            }
        }
        private void EditNewPassCheck_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(sender as CheckBox).IsChecked)
            {
                EditNewPassPanel1.Visibility = Visibility.Visible;
                EditNewPassPanel2.Visibility = Visibility.Visible;
            }
            else
            {
                EditNewPassPanel1.Visibility = Visibility.Collapsed;
                EditNewPassPanel2.Visibility = Visibility.Collapsed;
            }
        }
        private void EditSubmitPut_Click(object sender, RoutedEventArgs e)
        {
            if (!(EditID.Content is int))
            {
                MessageBox.Show("Select an empoyee to edit!");
                return;
            }

            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                {"token", user.Token },
                {"id", EditID.Content.ToString() },
                {"name", EditNameInput.Text },
                {"email", EditEmailInput.Text },
                {"isNewDepartment", (bool)EditNewDepartmentCheck.IsChecked ? "1" : "0" },
                {"department", (bool)EditNewDepartmentCheck.IsChecked ? EditNewDepartmentInput.Text : EditSelectDepartments.Text },
                {"isAdmin", (bool)EditAdminCheckbox.IsChecked ? "1" : "0" },
                {"passwd1", EditPassInput1.Password.Length > 0 ? EditPassInput1.Password : "0" },
                {"passwd2", EditPassInput2.Password.Length > 0 ? EditPassInput2.Password : "0" },
                {"newPass", (bool)EditNewPassCheck.IsChecked ? "1" : "0" },
                {"newPasswd1", EditNewPassInput1.Password.Length > 0 ? EditNewPassInput1.Password : "0" },
                {"newPasswd2", EditNewPassInput2.Password.Length > 0 ? EditNewPassInput2.Password : "0" },
                {"salary", EditSalaryInput.Text.Length > 0 ? EditSalaryInput.Text : "0" },
            };

            string response = null;
            try
            {
                response = restApi.Put("", payload);
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }

            Console.WriteLine(response);

            Dictionary<string, string> jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            if (jsonResponse.ContainsKey("error")) MessageBox.Show(jsonResponse["error"]);

            if (jsonResponse.ContainsKey("ok"))
            {
                GetDepartmentsFromServer();
                GetEmployeesFromServer();
                MessageBox.Show(jsonResponse["ok"]);
                
            }
        }

        private void DeleteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeletePanel.Visibility == Visibility.Collapsed)
            {
                CloseComponents();
                OpenComponent(DeleteMenuButton, DeletePanel);
            }
            else
            {
                CloseComponents();
            }
        }
        private void DeleteSubmitDelete_Click(object sender, RoutedEventArgs e)
        {
            string resultJSON = null;
            try
            {
                resultJSON = restApi.Delete($"id/{user.Token}|{DeleteIdInput.Text}");
            }
            catch
            {
                MessageBox.Show("Client was not able to connect to REST API.");
                return;
            }

            Console.WriteLine(resultJSON);

            Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultJSON);

            if (result.ContainsKey("ok"))
            {
                MessageBox.Show(result["ok"]);
                //DeleteIdInput.Text = "";
                GetDepartmentsFromServer();
                GetEmployeesFromServer();
            }

            if (result.ContainsKey("error"))
            {
                MessageBox.Show(result["error"]);
            }
        }
    }
}
