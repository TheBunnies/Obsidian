﻿using Obsidian.API;
using Obsidian.Chat;

namespace Obsidian
{
    public static class ServerImplementationRegistry
    {
        private static bool registered = false;

        public static void RegisterServerImplementations()
        {
            if (registered)
                return;
            registered = true;

            IChatMessage.createNew = () => new ChatMessage();
            ITextComponent.createNew = () => new TextComponent();
        }
    }
}
