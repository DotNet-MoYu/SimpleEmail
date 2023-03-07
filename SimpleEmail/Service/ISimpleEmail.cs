using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmail
{
    /// <summary>
    /// 邮件发送
    /// </summary>
    public interface ISimpleEmail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        string SendEmail(EmailMessage emailMessage);
    }
}
