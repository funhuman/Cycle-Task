using System;
using System.Drawing;
using System.Windows.Forms;

namespace CycleTask
{
    public partial class CycleTaskForm : Form
    {
        public const string LogFilePath = @"C:\cycle_task.log"; // 设置日志文件位置

        private enum loadMode { todo, yellow, all, plan, grey, is_deleted, long_time }
        private bool dayMode = false;
        private int time;

        public CycleTaskForm()
        {
            InitializeComponent();
            Control[] controls = { dateTimePicker1 };
            addDateTimePickerWheelEvent(controls);
        }

        private void cycleTaskForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Value = DateTime.Now;
            DataView.ReadOnly = true;
            loadData();
            WindowState = FormWindowState.Minimized;
            WindowState = FormWindowState.Normal;
        }

        private void loadData(int mode = 0)
        {
            int countBlue = 0, countYellow = 0;
            string sql = "select task_id, task_name as '任务名称', last_finish_day as '上次完成', yellow_line_days as '蓝线', red_line_days as '黄线' from cycle_task where ";
            if (mode == (int)loadMode.long_time)
            {
                sql += " is_deleted = 0 and red_line_days > 98";
            }
            else if(mode == (int)loadMode.is_deleted)
            {
                sql += " is_deleted = 1";
            }
            else
            {
                sql += " is_deleted = 0";
            }
            sql += " order by red_line_days, yellow_line_days;";
            DataView.DataSource = DBHelper.Reader(sql);
            DataView.Columns[0].Visible = false;
            DataView.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd";
            bool isColor, isYellow;
            var now = DateTime.Now;
            if (dayMode)
            {
                if (DateTime.Now.ToString("yyyy-MM-dd") == dateTimePicker1.Text)
                {
                    now = DateTime.Now.AddDays(1);
                }
                else
                { 
                    now = dateTimePicker1.Value;
                }
            }
            foreach (DataGridViewRow dr in DataView.Rows)
            {
                
                isColor = isYellow = false;
                // 设置蓝色
                if (Convert.ToDateTime(dr.Cells[2].Value).AddDays(Convert.ToInt32(dr.Cells[3].Value)) < now)
                {
                    dr.DefaultCellStyle.BackColor = Color.FromArgb(221, 235, 247);
                    isColor = true;
                    countBlue++;
                }
                // 设置黄色
                if (Convert.ToDateTime(dr.Cells[2].Value).AddDays(Convert.ToInt32(dr.Cells[4].Value)) < now)
                {
                    dr.DefaultCellStyle.BackColor = Color.FromArgb(252, 228, 214);
                    isColor = isYellow = true;
                    countYellow++;
                }
                // 隐藏无色
                if ((mode == (int)loadMode.yellow || mode == (int)loadMode.todo) && !isColor)
                {
                    var cm = (CurrencyManager)BindingContext[DataView.DataSource];
                    cm.SuspendBinding();
                    dr.Visible = false;
                    cm.ResumeBinding();
                }
                // 隐藏有色
                if (mode == (int)loadMode.grey && isColor)
                {
                    var cm = (CurrencyManager)BindingContext[DataView.DataSource];
                    cm.SuspendBinding();
                    dr.Visible = false;
                    cm.ResumeBinding();
                }
                // 隐藏蓝色
                if (mode == (int)loadMode.yellow && isColor && !isYellow)
                {
                    var cm = (CurrencyManager)BindingContext[DataView.DataSource];
                    cm.SuspendBinding();
                    dr.Visible = false;
                    cm.ResumeBinding();
                    continue;
                }
            }
            label1.Text = "统计：" + countYellow + " / " + (countBlue - countYellow).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DataView.SelectedRows.Count < 1)
            {
                return;
            }
            foreach (DataGridViewRow dr in DataView.SelectedRows)
            {
                if (Convert.ToDateTime(dr.Cells[2].Value).ToString("yyyy-MM-dd") != dateTimePicker1.Text)
                {
                    // 生成修改
                    DBHelper.Query($"update cycle_task set last_finish_day = '{dateTimePicker1.Text}', update_time = getdate() where task_id = {int.Parse(dr.Cells[0].Value.ToString())};");
                    // 生成日志
                    FileHelper.SaveAddFile($"{dr.Cells[1].Value.ToString()}, {dateTimePicker1.Text}", LogFilePath);
                }
            }
            loadData(comboBox1.SelectedIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CycleTaskManage.select_task_id = Convert.ToInt32(DataView.SelectedRows[0].Cells[0].Value);
            }
            catch (Exception)
            {
                CycleTaskManage.select_task_id = 0;
            }
            finally
            {
                new CycleTaskManage().ShowDialog();
                loadData(comboBox1.SelectedIndex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(comboBox1.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LogFilePath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!dayMode)
            {
                dayMode = true;
                button2.Text = "计划中";
                button2.BackColor = Color.FromArgb(255, 242, 204);
            }
            else
            {
                dayMode = false;
                button2.Text = "计划模式";
                button2.BackColor = button3.BackColor;
                button2.UseVisualStyleBackColor = true;
            }
            loadData(comboBox1.SelectedIndex);
        }

        private void DataView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                CycleTaskManage.select_task_id = Convert.ToInt32(DataView.SelectedRows[0].Cells[0].Value);
            }
            catch (Exception)
            {
                CycleTaskManage.select_task_id = 0;
            }
            finally
            {
                new CycleTaskManage().ShowDialog();
            }
        }
        private void addDateTimePickerWheelEvent(Control[] controls)
        {
            foreach (var control in controls)
            {
                control.MouseWheel += new MouseEventHandler(DateTimePicker_MouseWheel);
            }
        }
        private void DateTimePicker_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                SendKeys.Send("{UP}");
            }
            else
            {
                SendKeys.Send("{DOWN}");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            if (time >= 120)
            { 
                Hide();
            }
        }

        private void cycleTaskForm_Activated(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void cycleTaskForm_Deactivate(object sender, EventArgs e)
        {
            timer1.Start();
            time = 0;
        }

        private void CycleTaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
