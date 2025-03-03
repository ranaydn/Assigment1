using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assigment1
{
    public partial class frmToDoList: Form
    {
        private const string FilePath = "tasks.txt";
        public frmToDoList()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTask.Text))
            {
                lstTasks.Items.Add(txtTask.Text);
                txtTask.Clear();
                UpdateTaskCount();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(lstTasks.SelectedIndex != -1)
            {
                string editedTask = txtTask.Text;
                if (!string.IsNullOrWhiteSpace(editedTask))
                {
                    lstTasks.Items[lstTasks.SelectedIndex] = editedTask;
                    txtTask.Clear();
                }
        }
    }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
                UpdateTaskCount();
            }
        }

        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                string task = lstTasks.SelectedItem.ToString();
                if (!task.StartsWith("✔ "))
                {
                    lstTasks.Items[lstTasks.SelectedIndex] = "✔ " + task;
                }
            }
        }
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                txtTask.Text = lstTasks.SelectedItem.ToString().Replace("✔ ", "");
            }
        }
        private void UpdateTaskCount()
        {
            lblTotalTasks.Text = "Total Tasks: " + lstTasks.Items.Count;
        }
        private void SaveTasks()
        {
            File.WriteAllLines(FilePath, lstTasks.Items.Cast<string>());
        }

        private void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                string[] tasks = File.ReadAllLines(FilePath);
                lstTasks.Items.AddRange(tasks);
                UpdateTaskCount();
            }
        }


        private void frmToDoList_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTasks();
        }
    }



}
