﻿using Obsidian;
using Obsidian.Commands;
using Obsidian.Events.EventArgs;
using Obsidian.Plugins;

using Qmmands;

using System.Threading.Tasks;

namespace SamplePlugin
{
    public class SamplePluginClass : IPluginClass
    {
        private Server server;

        public async Task<PluginInfo> InitializeAsync(Server server)
        {
            this.server = server;

            server.Commands.AddModule<SamplePluginCommands>();

            server.Events.PlayerJoin += OnPlayerJoin;

            server.Register(new DickWorldGenerator());

            return new PluginInfo(
                "SamplePlugin",
                "Obsidian Team",
                "0.1",
                "A Sample Plugin! <3",
                "https://github.com/NaamloosDT/Obsidian"
            );
        }

        private async Task OnPlayerJoin(PlayerJoinEventArgs e)
        {
            await e.Server.SendChatAsync($"Player join event from sample plugin! {e.Joined.Username}", e.Client, 0, true);
            e.Logger.LogMessage($"Player join event to logger from sample plugin! {e.Joined.Username}");
        }
    }

    public class SamplePluginCommands : ModuleBase<CommandContext>
    {
        public CommandService Service { get; set; }

        [Command("samplecommand")]
        [Description("A sample command added by a sample plugin!")]
        public async Task SampleCommandAsync()
        {
            await Context.Server.SendChatAsync($"Sample command executed by {Context.Player.Username}" +
                $" from within a sample plugin!!!1", Context.Client, 0, false);
        }
    }
}