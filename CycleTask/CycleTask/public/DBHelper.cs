using System.Data;
using System.Data.SqlClient;

namespace CycleTask
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    /// 2021-06-21
    public static class DBHelper
    {
        // 获取连接字符串
        private static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConn"].ToString();

        // 定义连接状态
        private static SqlConnection conn = new SqlConnection(connString);

        // 打开连接并获取连接状态
        private static SqlConnection getConn()
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            return conn;
        }

        // 声明 SqlCommand
        private static SqlCommand cmd = null;

        // 获取 Cmd
        private static SqlCommand getCmd(string sql, SqlParameter[] ps = null)
        {
            cmd = new SqlCommand(sql, getConn());
            if (ps != null) { cmd.Parameters.AddRange(ps); }
            return cmd;
        }

        /// <summary>
        /// 查一个（第一行的第一列）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="ps">参数，默认为空</param>
        /// <returns>查到的对象，没查到为null</returns>
        public static object Scalar(string sql, SqlParameter[] ps = null)
        {
            return getCmd(sql, ps).ExecuteScalar();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="ps">参数，默认为空</param>
        /// <returns>DataTable</returns>
        public static DataTable Reader(string sql, SqlParameter[] ps = null)
        {
            DataTable dt = new DataTable();
            using (var sdr = getCmd(sql, ps).ExecuteReader())
            {
                dt.Load(sdr);
            }
            return dt;
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="ps">参数，默认为空</param>
        /// <returns>受影响的行数</returns>
        public static int Query(string sql, SqlParameter[] ps = null)
        {
            return getCmd(sql, ps).ExecuteNonQuery();
        }
    }
}
