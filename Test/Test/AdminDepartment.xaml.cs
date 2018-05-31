﻿using System;
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
using static Test.Database.DatabaseConnections;
using Test.Database;
using Test.Model;
using Npgsql;

namespace Test
{
    /// <summary>
    /// Interaction logic for AdminDepartment.xaml
    /// </summary>
    public partial class AdminDepartment : Window
    {
        public AdminDepartment()
        {
            InitializeComponent();

            DatabaseConnections db = new DatabaseConnections();
            List<Department> departments = db.GetAllDepartments();

            listBoxDepartment.ItemsSource = departments;       
        }

        Department selecteddepartment;

        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddDepartment(txtDepartment.Text);
                txtDepartment.Text = String.Empty;
                btnAddDep.IsEnabled = false;
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.RemoveDepartment(selecteddepartment);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecteddepartment = (Department)listBoxDepartment.SelectedItem;
            btnEdit.IsEnabled = true;
            btnRemove.IsEnabled = true;
            textBoxNewName.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxNewName.Text == null || textBoxNewName.Text.Length < 2)
            {
                MessageBox.Show("Du måste ange ett nytt namn på avdelningen bestående av minst två bokstäver.");
                textBoxNewName.Focus();
            }


            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.EditDepartment(selecteddepartment, textBoxNewName.Text);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAddDep.IsEnabled = true;            
        }
    }

}
