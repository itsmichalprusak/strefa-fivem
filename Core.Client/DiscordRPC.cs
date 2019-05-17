using System;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace Core.Client
{
    public class DiscordRPC : BaseScript
    {
        private static float _lastUpdateTime;

        public DiscordRPC()
        {
            _lastUpdateTime = GetGameTimer();
            Tick += timer;
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
        }

        private async Task timer()
        {
            // Timer aktualizuje dane co 30sekund
            if (GetGameTimer() - _lastUpdateTime > 5000)
            {
                // Pobieranie ID Gracza
                var id = GetPlayerServerId(PlayerId());
                // Pobieranie Wszystkich Graczy na serwerze
                var players = Players.Count();
                // Pobieranie Nicku gracza (Później będzie zmienione na imię i nazwisko ic postaci)
                var playername = GetPlayerName(PlayerId());
                // Tytuł, w co dany gracz gra.
                Function.Call(Hash.ADD_TEXT_ENTRY, "FE_THDR_GTAO", "StrefaRP.pl");
                // AppID Discorda, aktualne ID jest podpięte pod bota StrefaRP
                SetDiscordAppId("554051836114501663");
                // Obrazek
                SetDiscordRichPresenceAsset("logo");
                // Hover - Tekst po najechaniu na obrazek
                SetDiscordRichPresenceAssetText($"StrefaRP.pl - {players}/64 online");
                // Obrazek postaci
                SetDiscordRichPresenceAssetSmall("character");
                // Hover - Tekst po najechaniu na obrazek postaci
                SetDiscordRichPresenceAssetSmallText(playername);
                // Opis który pobiera ID gracza oraz ilość graczy
                SetRichPresence($"ID: {id} - {players}/64 online");
                _lastUpdateTime = GetGameTimer();
            }

            await Delay(1000);
        }
    }
}