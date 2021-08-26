using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSpambotBanner.Modules
{
    public class Commands : ModuleBase
    {
        [Command("ban"), RequireBotPermission(GuildPermission.BanMembers), RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(SocketGuildUser user = null, [Remainder] string reason = "")
        {
            SocketGuild gld = Context.Guild as SocketGuild;
            SocketGuildUser Author = Context.User as SocketGuildUser;

            if (gld.Users.Contains(user))
            {

                if (user == null) { await ReplyAsync("Please specify a user to ban!"); return; }
                else
                {
                    await ReplyAsync($"{user.Mention} has been banned from **{gld.Name}**");

                    if (reason.ToLower().Contains("spammer") || reason.ToLower().Contains("scammer") || reason.ToLower().Contains("spamming") || reason.ToLower().Contains("scamming"))
                    {
                        //TODO: Add to user to the Database
                    }
                    await gld.AddBanAsync(user, 1, reason);

                    await ReplyAsync("", false, new EmbedBuilder
                    {
                        Color = Color.Red,
                        Title = "__**Banned User**__",
                        Description = $"**{Author.Mention} banned user {user.Mention} from the server for reason:** {reason}",
                        Timestamp = DateTime.Now,
                    }.Build());
                }
            }
            else { await ReplyAsync("User does not exist in this guild"); }
        }

        [Command("userinfo"), RequireBotPermission(GuildPermission.SendMessages)]
        public async Task UserInfo(SocketGuildUser user = null)
        {
            if (user == null) { await ReplyAsync("Please specifiy a user"); }
            else
            {
                string Roles = "";
                foreach (SocketRole role in user.Roles) { Roles = Roles + role.Mention.ToString(); }
                await ReplyAsync("", false, new EmbedBuilder
                {
                    Title = $"Info for **{user.Nickname}**",
                    Color = new Color(0, 170, 230),
                    ThumbnailUrl = user.GetAvatarUrl(),
                    Description = $"**Username** - {user.Username}\n**Nickname** - {user.Nickname}\nDiscriminator - {user.Discriminator}\nCreated at - {user.CreatedAt}\nCurrent Status - {user.Status}\nJoined Server At - {user.JoinedAt}\nRoles - {Roles}"
                }.Build());
            }
        }
    }

}
