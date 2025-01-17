﻿using PencatatanSuhuPekerjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Services
{
    public class SendEmailService
    {
        public void SendEmail(string email, string emailRandomCode)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("xyztechnologyid@gmail.com", "System Admin");
                message.Subject = "Registration at " + DateTime.Now.ToString("dddd, MMMM dd yyyy");
                message.Body = "Please don't share this code to anyone,<br>Registration Code :<br><br><b>" + emailRandomCode + "</b><br><br> Please enter this code when you want to verfy you account !";
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("xyztechnologyid@gmail.com", "HelloWorld");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }
                 
        public void SendEmail(string email, double temperature, Employee emloyee)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("xyztechnologyid@gmail.com", "System Admin");
                message.Subject = "Warning at " + DateTime.Now.ToString("dddd, MMMM dd yyyy");
                message.Body = "Dear Mr/Ms "+ emloyee.FirstName + " " + emloyee.LastName + ",<br>Your Temperature is :<br><br><b>" + temperature + "</b><br><br> Please do the Covid19 protocol";
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("xyztechnologyid@gmail.com", "HelloWorld");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }
    }
}
