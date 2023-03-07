using Microsoft.Extensions.Configuration;

namespace SimpleEmail
{



    /// <summary>
    /// <inheritdoc cref="ISimpleEmail"/>
    /// </summary>
    public class SimpleEmail : ISimpleEmail
    {
        private readonly EmailConfig _config;

        /// <summary>
        /// 配置文件注入
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public SimpleEmail(IConfiguration configuration)
        {
            EmailConfig emailConfig = configuration.GetSection("EmailSetting").Get<EmailConfig>();//获取配置配置
            if (emailConfig != null && !string.IsNullOrEmpty(emailConfig.Sender))
            {
                _config = emailConfig;
            }
            else
            {
                throw new ArgumentException("邮件配置未找到，请配置邮件发送信息EmailSetting", nameof(emailConfig));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailConfig"></param>
        public SimpleEmail(EmailConfig emailConfig)
        {
            _config = emailConfig;
        }

        /// <inheritdoc />
        public string SendEmail(EmailMessage emailMessage)
        {
            var mimeMessage = CreateMimeMessageFromEmailMessage(emailMessage);
            var result = "";
            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.Connect(_config.SmtpServer, _config.Port, _config.Ssl);
                    smtpClient.Authenticate(_config.UserName,
                    _config.Password);
                    result = smtpClient.Send(mimeMessage);
                    smtpClient.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            return result;
        }

        /// <summary>
        /// 生成MimeMessage
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            if (string.IsNullOrEmpty(message.Sender)) message.Sender = _config.Sender;
            if (string.IsNullOrEmpty(message.Reciever)) message.Reciever = _config.Reciever;
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Self", message.Sender));
            mimeMessage.To.Add(new MailboxAddress("Self", message.Reciever));
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }
    }


    /// <summary>
    ///邮件消息 
    /// </summary>
    public class EmailMessage
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
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
