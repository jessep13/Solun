using System;
using System.Collections.Generic;
using System.Text;

namespace Solun.Entities.Terminals
{
	class MailTerminal : Terminal
	{
		class Mail
		{
			string author;
			string recipient;
			string subject;
			string message;

			public string Author => author;
			public string Recipient => recipient;
			public string Subject => subject;
			public string Message => message;

			public Mail(string author, string recipient, string subject, string message)
			{
				this.author = author;
				this.recipient = recipient;
				this.subject = subject;
				this.message = message;
			}

			public override string ToString()
			{
				return $"To: {recipient}\nFrom: {author}\nSubject: {subject}\n\n{message}";
			}
		}

		List<Mail> inbox = new List<Mail>();

		public MailTerminal(int id) => SetName(id);

		public void NewMessage(string author, string recipient, string subject, string message) 
			=> inbox.Add(new Mail(author, recipient, subject, message));

		protected override void Execute()
		{
			Console.WriteLine("Type the number of the coresponding mail to view it. Enter 0 to exit.\n");

			for(int i = 0; i < inbox.Count; i++)
			{
				Mail mail = inbox.ToArray()[i];
				Console.WriteLine($"[{i + 1}] {mail.Author}: {mail.Subject}");
			}

			int input;

			do
			{
				Console.Write("\n> ");
				input = int.Parse(Console.ReadLine());

				if(input > 0 && input <= inbox.Count)
				{
					Console.WriteLine(inbox.ToArray()[input - 1].ToString());
				}
			} while(input != 0);
		}
	}
}
