
using DataAccess.Repository.CRepository;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FurnitureApp.Pages
{
	public class ContactModel() : PageModel
	{
		

		public void OnGet() { }

		[BindProperty]
		public Contacts Contact { get; set; }

		public void OnPost()
		{

			MailMessage mailMessage = new MailMessage();
			mailMessage.To.Add("furnitureshop14072024@gmail.com");
			mailMessage.From = new MailAddress("furnitureshop1200@gmail.com");
			mailMessage.Subject = "Contact Furniture Shop";
			StringBuilder emailBody = new StringBuilder();

			string firstName = CapitalizeFirstLetter(Contact.FirstName);
            string lastName = CapitalizeFirstLetter(Contact.LastName);



            emailBody.AppendLine("Hi FurnitureShop,");
            emailBody.AppendLine("");
            emailBody.AppendLine($"My Name : {firstName} {lastName}, ");
			emailBody.AppendLine($"Email   : {Contact.Email} ");
			emailBody.AppendLine($"Message : {Contact.Message}");
            emailBody.AppendLine("");
            emailBody.AppendLine($"Thank you !");
            emailBody.AppendLine("");
            emailBody.AppendLine("     -----------------------------------------------------------------------");



            mailMessage.Body = emailBody.ToString();

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
			smtpClient.EnableSsl = true;
			smtpClient.Port = 587;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Credentials = new NetworkCredential("furnitureshop1200@gmail.com", "sesnojumhnqcxemp");

			try
			{
				smtpClient.Send(mailMessage);
				Console.WriteLine("=========================Successfull================");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}


		}
        public  string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }

    public class Contacts
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
