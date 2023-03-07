using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmail
{
    /// <summary>
    /// 邮件配置
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Reciever { get; set; }

        /// <summary>
        /// smtp服务器地址
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public bool Ssl { get; set; } = false;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

}
