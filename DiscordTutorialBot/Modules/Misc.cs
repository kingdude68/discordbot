using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordTutorialBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            string name = Context.User.Username;
            /*var embed = new EmbedBuilder();
            embed.WithTitle(Utilities.GetFormattedAlert("ECHO", name));
            embed.WithDescription(message);
            embed.WithColor(new Color(255, 0, 255));*/

            await Context.Channel.SendMessageAsync(message/*,false, embed*/);
        }
        [Command("hello")]
        public async Task Hello()
        {
            string name = Context.User.Username;
            var embed = new EmbedBuilder();
            embed.WithTitle(Utilities.GetFormattedAlert("HELLO", name));
            embed.WithDescription("Hello " + Context.User.Mention + ", how are you doing");
            embed.WithColor(new Color(0, 0, 255));

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            // [0, 0, 0, 0] <- 4
            string name = Context.User.Username;
            var embed = new EmbedBuilder();
            embed.WithTitle(Utilities.GetFormattedAlert("PICKED", name));
            embed.WithDescription(selection);
            embed.WithColor(new Color(50, 100, 50));
            embed.WithThumbnailUrl("https://camo.githubusercontent.com/896f4bcca05b40f1ea8f2f8f7db9e98b2fa402f1/687474703a2f2f692e696d6775722e636f6d2f785a38783945532e6a7067");
            
            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("goodnight")]
        public async Task Goodnight()
        {
            string name = Context.User.Username;
            var embed = new EmbedBuilder();
            embed.WithTitle(Utilities.GetFormattedAlert("GOODNIGHT", name));
            embed.WithDescription("Goodnight " + Context.User.Mention + ", sleep well");
            embed.WithColor(new Color(50, 0, 255));
            embed.WithThumbnailUrl("https://www.telegraph.co.uk/content/dam/science/2016/12/14/JS116074101-supermoon_trans_NvBQzQNjv4BqDI1jieMJtrozPRHESlT58qtII8ZVyTloJdbKYLpyWyg.jpg?imwidth=1400");

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("for the meme")]
        public async Task Meme()
        {
            string name = Context.User.Username;
            var embed = new EmbedBuilder();
            //embed.WithTitle(Utilities.GetFormattedAlert("GOODNIGHT", name));
            embed.WithDescription(Context.User.Mention + ", I don't feel so good.");
            embed.WithColor(new Color(50, 0, 255));
            embed.WithThumbnailUrl("http://i0.kym-cdn.com/entries/icons/original/000/026/056/sponge.jpg");

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        [Command("secret")]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":x: Insufficent Permissions" + Context.User.Mention);
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsSecretOwner(SocketGuildUser user)
        {

            string targetRoleName = "SecretOwner"; //Don't do this everytime, do it at start, store it when it boots
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}
