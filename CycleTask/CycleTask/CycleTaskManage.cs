using System;
using System.Data;
using System.Windows.Forms;

namespace CycleTask
{
    public partial class CycleTaskManage : Form
    {
        public static int select_task_id;
        DataTable data;

        public CycleTaskManage()
        {
            InitializeComponent();
        }

        private void cycleTaskManage_Load(object sender, EventArgs e)
        {
            bind();
            comboBox1.SelectedValue = select_task_id;
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void bind()
        {
            var sql = "select task_id, task_name, last_finish_day, yellow_line_days, red_line_days, is_deleted from cycle_task";
            data = DBHelper.Reader(sql);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "task_name";
            comboBox1.ValueMember = "task_id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in data.Rows)
            {
                if (comboBox1.SelectedValue.ToString() == dataRow["task_id"].ToString())
                {
                    textBox1.Text = dataRow["task_name"].ToString();
                    textBox2.Text = dataRow["yellow_line_days"].ToString();
                    textBox3.Text = dataRow["red_line_days"].ToString();
                    button3.Text = Convert.ToBoolean(dataRow["is_deleted"]) == false ? "禁用" : "启用";
                    break;
                }
            }
        }
        private bool checkLine()
        {
            try
            {
                return Convert.ToInt32(textBox2.Text) <= 0 || Convert.ToInt32(textBox3.Text) <= 0;
            }
            catch (Exception)
            {
                return true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkLine())
            {
                return;
            }
            string sql = $"insert into cycle_task(task_name, last_finish_day, yellow_line_days, red_line_days, is_deleted) values('{textBox1.Text}', '{DateTime.Now}', {textBox2.Text}, {textBox3.Text}, 0)";
            DBHelper.Query(sql);
            FileHelper.SaveAddFile($"{textBox1.Text}, {DateTime.Now.ToString("yyyy-MM-dd")}", CycleTaskForm.LogFilePath);
            label4.Text = textBox1.Text + "添加成功！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue.ToString() == null)
                {
                    return;
                }
                if (checkLine())
                {
                    return;
                }
                string sql = $"update cycle_task set task_name = '{textBox1.Text}', yellow_line_days = '{textBox2.Text}', red_line_days = '{textBox3.Text}', update_time = getdate() where task_id = {comboBox1.SelectedValue.ToString()}";
                DBHelper.Query(sql);
                label4.Text = textBox1.Text + "修改成功！";
            }
            catch (Exception)
            {
                label4.Text = textBox1.Text + "修改失败！";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() == null)
            {
                return;
            }
            int tmp = comboBox1.SelectedIndex;
            string sql = string.Format("update cycle_task set is_deleted = '{0}' where task_id = {1}", button3.Text == "启用" ? "0" : "1", comboBox1.SelectedValue.ToString());
            label4.Text = comboBox1.Text + button3.Text + "成功！";
            DBHelper.Query(sql);
            bind();
            comboBox1.SelectedIndex = tmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            label4.Text = "清空成功！";
        }
    }
}
