using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmail
{
    /// <summary>
    /// 发送邮件注册
    /// </summary>
    public static class EmailSetup
    {
        /// <summary>
        /// 添加邮件服务
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSimpleEmail(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<ISimpleEmail, SimpleEmail>();

        }

        /// <summary>
        /// 添加邮件服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>

        public static void AddSimpleEmail(this IServiceCollection services, IConfiguration configuration, string section = "EmailSetting")
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            EmailConfig emailConfig = configuration.GetSection(section).Get<EmailConfig>();//获取配置配置
            if (emailConfig != null && !string.IsNullOrEmpty(emailConfig.Sender))
            {
                services.AddSingleton<ISimpleEmail, SimpleEmail>(x => new SimpleEmail(emailConfig));
            }
            else
            {
                throw new ArgumentException($"邮件配置未找到，请配置邮件发送信息{section}", nameof(emailConfig));
            }

        }
    }
}
