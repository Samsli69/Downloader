using Projets;
using System.Net;

//Affichage ASCII
Console.ForegroundColor = ConsoleColor.DarkRed;
var ascii = new ASCII(@"
____   ____.__    .___                 .___                  .__                    .___
\   \ /   /|__| __| _/____  ____     __| _/______  _  ______ |  |   _________     __| _/
 \   Y   / |  |/ __ |/ __ \/  _ \   / __ |/  _ \ \/ \/ /    \|  |  /  _ \__  \   / __ | 
  \     /  |  / /_/ \  ___(  <_> ) / /_/ (  <_> )     /   |  \  |_(  <_> ) __ \_/ /_/ | 
   \___/   |__\____ |\___  >____/  \____ |\____/ \/\_/|___|  /____/\____(____  /\____ | 
                   \/    \/             \/                 \/                \/      \/ 
");
Console.WriteLine(ascii.ToString());
Console.WriteLine("Lien URL ? ");
var url = Console.ReadLine();
try
{
    Uri lien = new Uri(url);
    Download download = new Download();
    Console.WriteLine(download.GetFileSize(lien));
    /*while (!download.downloadFile(lien).IsCompleted)
    {
    }*/
}
catch(Exception e)
{
    Console.WriteLine("Erreur : " + e.Message);
    Environment.Exit(0);
}











