using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
            //ShowToDoList();
            Welcome();
        }

        public void ShowToDoList()
        {
            ClearMain();
            
            using (mydbEntities context = new mydbEntities())
            {
                RowDefinition row2 = new RowDefinition();
                row2.Height = new GridLength(1, GridUnitType.Auto);
                ThingsToDo.RowDefinitions.Add(row2);
                Label label = new Label();
                label.Content = "Click on the item to complete the task!";
                label.Foreground = new SolidColorBrush(Colors.Red);
                label.FontSize = 15;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                ThingsToDo.Children.Add(label);
                Grid.SetRow(label, ThingsToDo.RowDefinitions.Count - 1);

                if (context.Tasks.Count() > 0)
                {
                    foreach (var task in context.Tasks)
                    {
                        RowDefinition row = new RowDefinition();
                        row.Height = new GridLength(1, GridUnitType.Auto);
                        ThingsToDo.RowDefinitions.Add(row);
                        TextBlock textBlock = new TextBlock();

                        textBlock.MouseLeftButtonDown += TaskDone;
                        
                        textBlock.Text = string.Format($"(#{task.Id}) {task.task}");

                        textBlock.FontSize = 20;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        

                        if (task.done == 1)
                        {
                            textBlock.TextDecorations = TextDecorations.Strikethrough;
                        }

                        ThingsToDo.Children.Add(textBlock);
                        Grid.SetRow(textBlock, ThingsToDo.RowDefinitions.Count - 1);
                    }
                }
                    
            }

        }

        private void TaskDone(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            string f = textBlock.Text;
            string temp = f.Substring(f.IndexOf('#') + 1, f.IndexOf(')') - 2);

            int id = int.Parse(temp);

            using (mydbEntities context = new mydbEntities())
            {
                Tasks task = context.Tasks.FirstOrDefault(x => x.Id == id);

                if (task.done == 0)
                {
                    task.done = 1;
                }
                else
                {
                    task.done = 0;
                }

                context.SaveChanges();


                ClearMain();
                ShowToDoList();

            }

        }

        

        private void AddNewTask()
        {
            ClearMain();

            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(1, GridUnitType.Auto);
            ThingsToDo.RowDefinitions.Add(row2);
            Label label = new Label();
            label.Content = "Press enter to add new task";
            label.Foreground = new SolidColorBrush(Colors.Red);
            label.FontSize = 20;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            ThingsToDo.Children.Add(label);
            Grid.SetRow(label, ThingsToDo.RowDefinitions.Count - 1);

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Auto);
            ThingsToDo.RowDefinitions.Add(row);
            TextBox textBox = new TextBox();
            textBox.Text = "Add new task";
            textBox.Name = "xxx";
            textBox.FontSize = 20;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.KeyDown += InsertIntoDb;
            ThingsToDo.Children.Add(textBox);
            Grid.SetRow(textBox, ThingsToDo.RowDefinitions.Count - 1);

        }

        private void DeleteByID()
        {
            ClearMain();

            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(1, GridUnitType.Auto);
            ThingsToDo.RowDefinitions.Add(row2);
            Label label = new Label();
            label.Content = "Give ID number and press enter to delete!";
            label.Foreground = new SolidColorBrush(Colors.Red);
            label.FontSize = 20;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            ThingsToDo.Children.Add(label);
            Grid.SetRow(label, ThingsToDo.RowDefinitions.Count - 1);


            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Auto);
            ThingsToDo.RowDefinitions.Add(row);
            TextBox textBox = new TextBox();
            textBox.Width = 50;
            textBox.Text = "";
            textBox.Name = "xxx";
            textBox.FontSize = 20;
            textBox.VerticalAlignment = VerticalAlignment.Center;
            textBox.HorizontalAlignment = HorizontalAlignment.Center;
            textBox.KeyDown += DeleteFromDB;
            
            ThingsToDo.Children.Add(textBox);
            Grid.SetRow(textBox, ThingsToDo.RowDefinitions.Count - 1);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            using (mydbEntities context = new mydbEntities())
            {
                DeleteByID();
            }


        }

        private void InsertIntoDb(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (mydbEntities context = new mydbEntities())
                {
                    TextBox text = sender as TextBox;

                    Tasks task = new Tasks();
                    task.task = text.Text;
                    context.Tasks.Add(task);
                    context.SaveChanges();
                    MessageBox.Show("New task has been added to the list!");
                    ClearMain();
                    ShowToDoList();

                }

            }
            
        }

        private void DeleteFromDB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (mydbEntities context = new mydbEntities())
                {
                    TextBox text = sender as TextBox;

                    int id = int.Parse(text.Text);

                    Tasks task = context.Tasks.FirstOrDefault(x => x.Id == id);
                    context.Tasks.Remove(task);
                    context.SaveChanges();

                    MessageBox.Show($"Task #{id} deleted");
                    ClearMain();
                    ShowToDoList();
                }

            }

        }
        
        private void Welcome()
        {
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(1, GridUnitType.Auto);
            ThingsToDo.RowDefinitions.Add(row2);
            Label label = new Label();
            label.Content = "Welcome! Click on the menu to see your tasks";
            label.Foreground = new SolidColorBrush(Colors.Red);
            label.FontSize = 20;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            ThingsToDo.Children.Add(label);
            Grid.SetRow(label, ThingsToDo.RowDefinitions.Count - 1);
        }

        private void ClearMain()
        {
            ThingsToDo.Children.Clear();
            ThingsToDo.RowDefinitions.Clear();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowToDoList();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddNewTask();
        }
    }
}
