using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Commands;

namespace Botiiik
{
    class Program : ModuleBase<SocketCommandContext>
    {
        DiscordSocketClient client;
        CommandService command;

        // Program Declaration and start
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            //connection to services
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.GuildMessages | GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            client = new DiscordSocketClient(config);
            command = new CommandService();
            client.Log += Logger;

            //string token = ("MTA4NDAxMjAwODI3OTY1ODU1Ng.GbLL2s.GiSBe3MKfA1bj9Ngu7O8nlbwNnhwxVIIr8y0AU");
            string token = Console.ReadLine();
            client.MessageReceived += CommandMessageRecived;

            try
            {
                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            //Console.WriteLine(command += client.MessageReceived);

        }

        // Logging Service
        private Task Logger(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task CommandMessageRecived(SocketMessage message)
        {
            if (!message.Author.IsBot)
            {
                string originalString = message.Content.ToString();
                Console.WriteLine("message" + message.Content.ToString());
                string[] words = originalString.Split(' '); // split the string at space character
                string newString = words[0]; // take the first element of the resulting array
                Console.WriteLine("command" + newString);
                string cutOutPart = originalString.Substring(newString.Length).Trim(); // get the remaining part of the original string after the first word
                Console.WriteLine("msg" + cutOutPart);

                if (originalString.StartsWith("!"))
                {
                    if (newString == "!call" && newString != "@everyone")
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            await message.Channel.SendMessageAsync(cutOutPart);
                        }
                    }
                    else
                    {
                        await message.Channel.SendMessageAsync("stooopid idiot");
                    }
                }
            }
            else
            {
                Console.WriteLine("Bot is Bot to Bot");
            }
        }

    }

    //class Commandhandler : ModuleBase<SocketCommandContext>
    //{
    //    [Command("call")]
    //    public async Task call(string Pelmeny)
    //    {
    //        await ReplyAsync($"Finaly i made {Pelmeny}");
    //        //await Task.Delay(-1);
    //    }
    //}

}

